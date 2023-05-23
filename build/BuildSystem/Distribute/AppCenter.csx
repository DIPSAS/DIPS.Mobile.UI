#r "nuget:Newtonsoft.Json, 13.0.2"

#load "./ReleaseDetails.csx"
#load "./PlatformTarget.csx"
#load "../Logging/Logger.csx"
#load "../Command.csx"
#load "../RestClient.csx"


using System.Threading;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;


// Created using this guide: https://learn.microsoft.com/en-us/appcenter/distribution/uploading 
public static class AppCenter
{
    private const string AppCenterApiUrl = "https://api.appcenter.ms/v0.1/apps/";
    private const string OwnerName = "DIPS-AS";
    private const string AppCenterFileUrl = "https://file.appcenter.ms/upload/";
    private const string UploadFinished = "uploadFinished";
    private const string UploadCancelled = "uploadCanceled";
    private static string FileChunksLocation = "appcenter_temp/chunk";
    private static string TempLocation = "appcenter_temp";
    private const string AndroidMIMEType = "application/vnd.android.package-archive";
    private const string iOSMIMEType = "application/octet-stream";

    public static Task<int> PublishApplication(string appGroup, string filePath, string distributionGroup, PlatformTarget platformTarget, string apiToken, string releaseNotes)
    {
        return PublishApplication(appGroup, filePath, new[] { distributionGroup }, platformTarget, apiToken, releaseNotes);
    }

    public static async Task<int> PublishApplication(string appGroup, string filePath, string[] distributionGroups, PlatformTarget platformTarget, string apiToken, string releaseNotes)
    {
        var releaseId = await CreateRelease(appGroup, filePath, platformTarget, apiToken);

        var distributedDestinations = await DistributeRelease(releaseId, appGroup, distributionGroups, apiToken, releaseNotes);

        var uploadedAppFile = new FileInfo(filePath).Name;
        Logger.LogDebug($"🚀 Successfully distributed {uploadedAppFile} with release id:{releaseId} to {String.Join(", ", distributedDestinations)} in AppCenter");

        return releaseId;
    }

    // TODO: Combine owner name and appname into ApplicationPath
    public static async Task<int> CreateRelease(string appGroup, string filePath, PlatformTarget platformTarget, string apiToken)
    {
        // Initialize the release process and get the necessary metadata from AppCenter.
        Logger.LogDebug("Fetching metadata...");
        var metadata = await GetReleaseMetadata(appGroup, apiToken);
        Logger.LogDebug($"Metadata fetched successfully! {{ Id = {metadata.Id} }}");

        // Upload the necessary metadata and application files to AppCenter's file server
        await UploadApplicationData(metadata, appGroup, filePath, platformTarget, apiToken);

        // Commit the upload on AppCenter's api server to let it know that we have completed the upload.
        Logger.LogDebug($"Committing upload to {AppCenterApiUrl}...");
        await CommitUpload(metadata, apiToken, appGroup);
        Logger.LogDebug("Upload successfully committed!");
        
        // Poll AppCenter for the release id now that the upload has completed.
        Logger.LogDebug("Resolving release id for distribution...");
        var releaseId = await GetReleaseId(metadata, apiToken, appGroup);

        return releaseId;
    }

    public static async Task<List<string>> DistributeRelease(int releaseId, string appGroup, string[] distributionGroups, string apiToken, string releaseNotes)
    {
        Logger.LogDebug($"Distributing release to specified distribution groups... {{ ReleaseId = {releaseId} }}");

        var restClient = new RestClient(AppCenterApiUrl);
        var relativeUrl = $"{Path.Combine(OwnerName, appGroup)}/releases/{releaseId}";
        var additionalHeaders = new Dictionary<string, string>
        {
            { "X-API-TOKEN", apiToken }
        };

        //var data = new { destinations = new[] { new { name = DistributionGroup }, new { name = "Collaborators" } } };
        var data = new { destinations = distributionGroups.Select(d => new { name = d } ), release_notes = releaseNotes };
        var request = RestClient.CreateHttpRequestMessage(HttpMethod.Patch, relativeUrl, null, null, default, default, additionalHeaders);
        request.Content = new StringContent(JsonConvert.SerializeObject(data));
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        var response = await restClient.HttpClient.SendAsync(request);

        var responseBody = await response.Content.ReadAsStringAsync();
        
        response.EnsureSuccessStatusCode();
        var definition = new { destinations = new[] { new { name = "" } } };
        var distribution = JsonConvert.DeserializeAnonymousType(responseBody, definition);
        var distributedDestionations = new List<string>();
        for (int i = 0; i < distribution.destinations.Length; i++)
        {
            distributedDestionations.Add(distribution.destinations[i].name);
        }
        return distributedDestionations;
    }

    /// <summary>
    /// Get details about a release
    /// </summary>
    /// <param name="appGroup">The app group to get the release from</param>
    /// <param name="distributionGroup">The distribution group to get the release from</param>
    /// <param name="releaseId">The release id of the release</param>
    /// <param name="apiToken">The app center api token</param>
    /// <remarks>api: https://openapi.appcenter.ms/#/distribute/releases_getLatestByDistributionGroup</remarks>
    /// <returns></returns>
    public static async Task<ReleaseDetails> GetReleaseDetails(string appGroup, string distributionGroup, int releaseId, string apiToken)
    {
        var restClient = new RestClient(AppCenterApiUrl);
        var relativeUrl = $"{OwnerName}/{appGroup}/distribution_groups/{distributionGroup}/releases/{releaseId}";

        var response = await restClient.GetAsync(relativeUrl, default, default, GetAdditionalHeaders(apiToken));
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<ReleaseDetails>(responseBody);
    }

    public static async Task<ReleaseDetails> GetLatestVersionFromDistributionGroup(string appGroup, string distributionGroup, string apiToken)
    {
        var restClient = new RestClient(AppCenterApiUrl);
        var relativeUrl = $"{OwnerName}/{appGroup}/distribution_groups/{distributionGroup}/releases/latest";
        var response = await restClient.GetAsync(relativeUrl, default, default, GetAdditionalHeaders(apiToken));
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<ReleaseDetails>(responseBody);
    }

    private static async Task UploadApplicationData(ReleaseMetadata metadata, string appGroup, string filePath, PlatformTarget platformTarget, string apiToken)
    {
        try
        {
            // Create additional metadata for the upload process
            var appType = platformTarget is PlatformTarget.Android ? AndroidMIMEType : iOSMIMEType;
            var theFile = new FileInfo(filePath);
            var uploadMetadata = new UploadMetadata(theFile.Name, appType);

            Logger.LogDebug($"Uploading file '{uploadMetadata.FileName}' to {AppCenterFileUrl}. {{ UploadId = {metadata.Id} }}");

            Logger.LogDebug("Setting metadata...");
            var result = await SetMetadata(metadata, uploadMetadata, apiToken, filePath);

            Logger.LogDebug($"Metadata set successfully! {{ ChunkSize = {result.ChunkSize}}}");

            Logger.LogDebug($"Splitting up file...");

            if(!Directory.Exists(TempLocation)){
                Directory.CreateDirectory(TempLocation);
            }
            
            // We have received a preferred chunk size from AppCenter, so split the file into smaller files.
            await Command.ExecuteAsync("split", $"-b {result.ChunkSize} {theFile.FullName} \"{FileChunksLocation}\"");
            
            // Upload the splitted files in sequence to AppCenter
            Logger.LogDebug("Uploading file chunks...");
            await UploadFileChunks(metadata, uploadMetadata, TempLocation);
            Logger.LogDebug("Chunk upload completed!");
            Directory.Delete(TempLocation, true);

            Logger.LogDebug("Finalizing upload...");
            await FinishUpload(metadata, apiToken);     
        }
        catch (Exception e)
        {
            // Cancel the upload process on AppCenter's api server if the upload failed.
            Logger.LogDebug("Cancelling upload due to exception: {e.Message}");
            await CommitUpload(metadata, apiToken, appGroup, false);
            throw e;
        }
    }

    private static async Task<int> GetReleaseId(ReleaseMetadata metadata, string apiToken, string appGroup)
    {
        var restClient = new RestClient(AppCenterApiUrl);
        var relativeUrl = $"{Path.Combine(OwnerName, appGroup)}/uploads/releases/{metadata.Id}";

        var definition = new { release_distinct_id = 0 ,upload_status=""};
        var delayBetweenPolls = 3000;
        Logger.LogDebug("Waiting for upload to be ready to be published...");
        for (int i = 0; i < 15; i++)
        {
            var response = await restClient.GetAsync(relativeUrl, default, default, GetAdditionalHeaders(apiToken));
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            var upload = JsonConvert.DeserializeAnonymousType(responseBody, definition);
            if (upload.upload_status == "readyToBePublished")
            {
                Logger.LogDebug("Upload is ready to be published");
                return upload.release_distinct_id;
            }
            Logger.LogDebug($"Upload status is {upload.upload_status}, try again in {delayBetweenPolls}ms");
            await Task.Delay(delayBetweenPolls);
        }

        throw new Exception("Unable to resolve release id from checking upload!");
    }

    private static async Task FinishUpload(ReleaseMetadata metadata, string apiToken)
    {
        var fileRestClient = new RestClient(AppCenterFileUrl);
        var relativeUrl = $"finished/{metadata.PackageAssetId}";
        var parameters = new Dictionary<string, string>
        {
            { "token", metadata.UrlEncodedToken }
        };

        var response = await fileRestClient.PostAsync(relativeUrl, null, default, default, parameters, GetAdditionalHeaders(apiToken));
        response.EnsureSuccessStatusCode();
    }

    private static async Task CommitUpload(ReleaseMetadata metadata, string apiToken, string appGroup, bool didSucceed = true)
    {
        var apiRestClient = new RestClient(AppCenterApiUrl);
        var relativeUrl = $"{Path.Combine(OwnerName, appGroup)}/uploads/releases/{metadata.Id}";

        var data = new { upload_status = didSucceed ? UploadFinished : UploadCancelled };

        var request = RestClient.CreateHttpRequestMessage(HttpMethod.Patch, relativeUrl, null, null, default, default, GetAdditionalHeaders(apiToken));
        request.Content = new StringContent(JsonConvert.SerializeObject(data));
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        
        var response = await apiRestClient.HttpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
    }

    private static async Task UploadFileChunks(ReleaseMetadata metadata, UploadMetadata appMetadata, string chunkPath)
    {
        var restClient = new RestClient(AppCenterFileUrl);
        var relativeUrl = $"upload_chunk/{metadata.PackageAssetId}";
        var directoryInfo = new DirectoryInfo(chunkPath);
        var files = directoryInfo.GetFiles();
        var totalFiles = files.Length;
        var blockNumber = 0;
        foreach (var file in files.OrderBy(f => f.Name))
        {
            Logger.LogDebug($"Uploading chunk {blockNumber} of {totalFiles}...");

            blockNumber++;
            var contentLength = file.Length;

            var parameters = new Dictionary<string, string>
            {
                { "token", metadata.UrlEncodedToken },
                { "block_number", blockNumber.ToString()}
            };

            var request = RestClient.CreateHttpRequestMessage(HttpMethod.Post, relativeUrl, null, null, "application/json", parameters);
            request.Content = new StreamContent(File.OpenRead(file.FullName));
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(appMetadata.AppType);
            request.Content.Headers.Add("Content-Length", contentLength.ToString());

            var response = await restClient.HttpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }

    }

    private static async Task<ReleaseMetadata> GetReleaseMetadata(string appGroup, string apiToken)
    {
        var restClient = new RestClient(AppCenterApiUrl);
        var relativeUrl = $"{Path.Combine(OwnerName,appGroup)}/uploads/releases";

        var response = await restClient.PostAsync(relativeUrl, null, "application/json", "application/json", null, GetAdditionalHeaders(apiToken));
        response.EnsureSuccessStatusCode(); 

        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<ReleaseMetadata>(responseBody);
    }

    private static async Task<SetMetadataResult> SetMetadata(ReleaseMetadata metadata, UploadMetadata appMetadata, string apiToken, string filePath)
    {
        var fileInfo = new FileInfo(filePath);
        var fileSize = fileInfo.Length;

        var restClient = new RestClient(AppCenterFileUrl);
        var relativeUrl = $"set_metadata/{metadata.PackageAssetId}";
        var parameters = new Dictionary<string, string>
        {
            { "file_name", appMetadata.FileName },
            { "file_size", fileSize.ToString() },
            { "token", metadata.UrlEncodedToken },
            { "content_type", appMetadata.AppType }
        };

        var response = await restClient.PostAsync(relativeUrl, null, "application/json", "application/json", parameters, GetAdditionalHeaders(apiToken));

        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<SetMetadataResult>(responseBody);
    }

    private static Dictionary<string, string> GetAdditionalHeaders(string apiToken) => new Dictionary<string, string> { { "X-API-TOKEN", apiToken } };

    private class ReleaseMetadata
    {
        public string Id { get; set; }
        [JsonProperty("package_asset_id")]
        public string PackageAssetId { get; set; }
        [JsonProperty("upload_domain")]
        public string UploadDomain { get; set; }
        public string Token { get; set; }
        [JsonProperty("url_encoded_token")]
        public string UrlEncodedToken { get; set; }
    }

    private class SetMetadataResult
    {
        public bool Error { get; set; }
        public string Id { get; set; }
        [JsonProperty("chunk_size")]
        public int ChunkSize { get; set; }
        [JsonProperty("resume_restart")]
        public bool ResumeRestart { get; set; }
        [JsonProperty("chunk_list")]
        public int[] ChunkList { get; set; }
        [JsonProperty("blob_partitions")]
        public int BlobPartitions { get; set; }
        [JsonProperty("status_code")]
        public string StatusCode { get; set; }
    }

    private class UploadMetadata
    {
        public UploadMetadata(string fileName, string appType)
        {
            FileName = fileName;
            AppType = appType;
        }

        public string FileName { get; }
        public string AppType { get; }
    }
}