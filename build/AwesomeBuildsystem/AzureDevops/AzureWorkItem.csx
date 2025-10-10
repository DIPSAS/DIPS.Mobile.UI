#r "nuget:Newtonsoft.Json, 13.0.3"
#load "../Logging/Logger.csx"
#load "./AzureDevops.csx"
#load "../Tools/VersionUtils.csx"

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Linq;
using System.Text.RegularExpressions;

public static class AzureWorkItem
{
    private const string ReleaseNotesFileName = "ReleaseNotes.json";
    private const string ChildIdentifier = "System.LinkTypes.Hierarchy-Forward";
    private const string FileAttachmentIdentifier = "AttachedFile";
    private readonly static byte[] s_pngSignature = {0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A};
    private readonly static byte[] s_jpgSignatureStart = {0xFF, 0xD8};
    private readonly static byte[] s_jpgSignatureEnd = {0xFF, 0xD9};

    /// <summary>
    /// Fetches a defined work item from Azure Boards and uses it to generate a JSON file containing release notes of previous versions.
    /// </summary>
    /// <param name="outputDirectory">The file path where the resulting JSON file should be stored.</param>
    /// <param name="imageDirectory">The file path where the downloaded image files should be stored.</param>
    /// <param name="releaseNoteItemId">The work item from Azure Boards that contains all release note PBI's as children.</param>
    /// <param name="maxTotalImageSizeInBytes">The max size that release note images can be in total, in bytes.</param>
    /// <param name="organization">The name of the Azure DevOps organization.</param>
    /// <param name="project">Project ID or project name</param>
    /// <param name="personalAccessToken">Personal Access Token (PAT) to use as authentication, default is the build serveres PAT.</param>
    public static async Task CreateReleaseNotes(string outputDirectory, string imageDirectory, string productVersionFilePath, int releaseNoteItemId = 0, long? maxTotalImageSizeInBytes = null, string organization = "dips", string project = "DIPS", string personalAccessToken = null)
    {
        if(releaseNoteItemId == 0)
        {
            releaseNoteItemId = int.Parse(AzureDevops.GetEnvironmentVariable("ReleaseNoteItem"));
        }

        if(personalAccessToken == null)
        {
            personalAccessToken = AzureDevops.GetEnvironmentVariable("System.AccessToken");
        }

        if (maxTotalImageSizeInBytes == null)
        {
            maxTotalImageSizeInBytes = long.Parse(AzureDevops.GetEnvironmentVariable("MaxTotalImageSizeInBytes"));
        }

        if (!Directory.Exists(outputDirectory))
        {
            Directory.CreateDirectory(outputDirectory);
        }

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", personalAccessToken))));
        
        Logger.LogDebug($"Retrieving children from work item {releaseNoteItemId}...");

        var workItemResponse = await GetWorkItemWithRelations(releaseNoteItemId, httpClient, project, organization);

        var featureChildren = await GetChildren(workItemResponse, httpClient);
        featureChildren = featureChildren.OrderByDescending(c => c.Fields.TargetVersion).ToList();
        featureChildren = FilterCurrentVersion(featureChildren, productVersionFilePath);

        var releaseNotes = new List<ReleaseNote>();

        int totalImageSizeUsed = 0;
        bool shouldGetImages = maxTotalImageSizeInBytes > 0;

        foreach (var workItem in featureChildren)
        {
            var tasks = await GetChildren(workItem, httpClient);

            if (tasks is null)
            {
                Logger.LogWarning($"Work item with id={workItem.Id} has no tasks! No release note associated with this work item will be created.");
                continue;
            }

            tasks = tasks.OrderBy(w => w.Fields.Priority);

            var releaseNoteElements = new List<ReleaseNoteElement>();
            int index = 0;
            foreach (var task in tasks)
            {
                var releaseNoteElement = CreateReleaseNoteElement(task);
                releaseNoteElements.Add(releaseNoteElement);

                if (!shouldGetImages)
                {
                    continue;
                }

                var imageAttachment = GetImageAttachmentFromWorkItem(task);
                if (imageAttachment is null)
                {
                    continue;
                }

                var imageData = await DownloadImageData(imageAttachment.Url, httpClient);
                if (totalImageSizeUsed + imageData.Length >= maxTotalImageSizeInBytes)
                {
                    Logger.LogWarning($"The max amount of total image size has been exceeded! Max is {maxTotalImageSizeInBytes} bytes.");
                    continue;
                }

                var extension = GetImageExtensionFromByteArray(imageData);
                if (extension is null)
                {
                    continue;
                }

                var fileName = $"releasenote_img_{index++}{extension}";
                var filePath = Path.Combine(imageDirectory, fileName);
                
                var success = SaveByteData(filePath, imageData);
                if (!success)
                {
                    continue;
                }

                releaseNoteElement.ImageFilePath = fileName;
                totalImageSizeUsed += imageData.Length;
            }

            var version = ExtractVersionNumber(workItem.Fields.TargetVersion).ToString();
            var releaseNote = new ReleaseNote { Title = workItem.Fields.Title, Version = version, Elements = releaseNoteElements };

            releaseNotes.Add(releaseNote);

            // Should only get images for first release note
            shouldGetImages = false;
        }

        Logger.LogDebug($"All work items retrieved successfully! (Found {releaseNotes.Count})");

        SaveReleaseNotesToFile(releaseNotes, Path.Combine(outputDirectory, ReleaseNotesFileName));
    }

    private static WorkItemLink GetImageAttachmentFromWorkItem(WorkItemResponse workItem)
    {
        var pictureAttachments = workItem.Relations.Where(r => r.Rel == FileAttachmentIdentifier);

        if (pictureAttachments.Count() > 1)
        {
            Logger.LogWarning($"Work item '{workItem.Id}' has more than one image attached! Only the first image will be used.");
        }

        return pictureAttachments.FirstOrDefault();
    }

    private static async Task<WorkItemResponse> GetWorkItemWithRelations(int id, HttpClient httpClient, string project, string organization)
    {
        var featureRequest = $"https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/{id}?api-version=7.0&$expand=relations";
        var response = await httpClient.GetAsync(featureRequest);
        var featureJson = await response.Content.ReadAsStringAsync();
        var workItemResponse = JsonConvert.DeserializeObject<WorkItemResponse>(featureJson);

        return workItemResponse;
    }

    private static void SaveReleaseNotesToFile(List<ReleaseNote> releaseNotes, string filePath)
    {
        if(!releaseNotes.Any())
        {
            Logger.LogDebug($"No release notes to add to the application.");
            return;
        }
        try
        {
            Logger.LogDebug($"Saving release notes to '{filePath}'...");

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            var releaseNoteJson = JsonConvert.SerializeObject(releaseNotes);

            File.WriteAllText(filePath, releaseNoteJson);

            Logger.LogDebug($"Successfully saved release notes to '{filePath}'!");
        }
        catch (IOException e)
        {
            Logger.LogWarning($"Failed to write release notes to file. Error: {e}");
        }
    }

    private static async Task<byte[]> DownloadImageData(string url, HttpClient httpClient)
    {
        Logger.LogDebug($"Downloading image from {url}...");

        var response = await httpClient.GetAsync(url);
        var content = await response.Content.ReadAsByteArrayAsync();

        return content;
    }

    private static IEnumerable<WorkItemResponse> FilterCurrentVersion(IEnumerable<WorkItemResponse> workItems, string productVersionFilePath)
    {
        var currentVersion = VersionUtils.GetProductVersion(productVersionFilePath);
        return workItems.Where(w => ExtractVersionNumber(w.Fields.TargetVersion) <= currentVersion);
    }

    private static bool SaveByteData(string filePath, byte[] data)
    {
        try
        {
    
            Logger.LogDebug($"Saving image to {filePath}...");

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Dispose();
            }

            File.WriteAllBytes(filePath, data);

            return true;
        }
        catch (Exception e)
        {
            Logger.LogWarning($"Failed to save byte array. Error: {e}");
            return false;
        }
    }

    private static string GetImageExtensionFromByteArray(byte[] data)
    {
        bool isPng = true;
        for (int i = 0; i < s_pngSignature.Length; i++)
        {
            if (data[i] != s_pngSignature[i])
            {
                isPng = false;
                break;
            }
        }

        if (isPng) 
        {
            Logger.LogDebug("Image is in PNG format.");
            return ".png";
        }

        bool isJpg = true;

        for (int i = 0; i < s_jpgSignatureStart.Length; i++)
        {
            if (data[i] != s_jpgSignatureStart[i])
            {
                isJpg = false;
                break;
            }
        }

        for (int i = 0; i < s_jpgSignatureEnd.Length; i++)
        {
            int index = data.Length - s_jpgSignatureEnd.Length + i;
            if (data[index] != s_jpgSignatureEnd[i])
            {
                isJpg = false;
                break;
            }
        }

        if (isJpg) 
        {
            Logger.LogDebug("Image is in JPG format.");
            return ".jpg";
        }

        Logger.LogWarning("Image was neither in PNG or JPG format! Skipping...");

        return null;
    }

    private static ReleaseNoteElement CreateReleaseNoteElement(WorkItemResponse workItem)
    {
        if (workItem.Fields.Description is null)
        {
            Logger.LogWarning($"Task with id={workItem.Id} has no description!");
        }

        var description = Regex.Replace(workItem.Fields.Description ?? "", "<.*?>", string.Empty);
        return new ReleaseNoteElement { Title = workItem.Fields.Title, Description = description };
    }

    private static async Task<IEnumerable<WorkItemResponse>> GetChildren(WorkItemResponse workItem, HttpClient httpClient, string organization = "dips", string project = "DIPS")
    {
        var workItemRelationLinks = GetWorkItemRelationLinks(workItem);
        if (workItemRelationLinks.Count == 0)
        {
            return null;
        }
        var workItemsRequest = $"https://dev.azure.com/{organization}/{project}/_apis/wit/workitemsbatch?api-version=7.0";
        var requestJson = JsonConvert.SerializeObject(new WorkItemBatchRequest { Ids = workItemRelationLinks });
        var content = new StringContent(requestJson, Encoding.UTF8, "application/json");
        var batchResponse = await httpClient.PostAsync(workItemsRequest, content);
        var batchJson = await batchResponse.Content.ReadAsStringAsync();
        var workItemBatchResponse = JsonConvert.DeserializeObject<WorkItemBatchResponse>(batchJson);

        var workItems = workItemBatchResponse.Value;

        return workItems.Where(w => !string.Equals(w.Fields.State, "Removed"));
    }

    private static List<int> GetWorkItemRelationLinks(WorkItemResponse workItem)
    {
        return workItem.Relations.Where(r => string.Equals(r.Rel, ChildIdentifier)).Select(r => int.Parse(r.Url.Substring(r.Url.LastIndexOf("/") + 1, r.Url.Length - r.Url.LastIndexOf("/") - 1))).ToList();
    }

    private static Version ExtractVersionNumber(string str)
    {
        var regex = new Regex(@"\d+(?:\.\d+)+");
        var match = regex.Match(str);
        if (match.Success)
        {
            return Version.Parse(match.Groups[0].Value);
        }

        throw new ArgumentException($"String '{str}' did not contain a version!");
    }

    public static async Task<bool> CheckIfCommentIsLiked(string commentContent, string repositoryId = null, int pullRequestId = 0, string organization = "dips", string project = "DIPS", string apiVersion="7.0", string personalaccesstoken = null) 
    {
        Logger.LogDebug($"Checking if comment in PR with content: {commentContent} has been liked.");

        var pullRequestThreads = await AzureDevops.ListPullRequestThreads(repositoryId, pullRequestId, organization, project, apiVersion, personalaccesstoken);

        PullRequestComment targetPullRequestComment = null;
        foreach(var pullRequestThread in pullRequestThreads)
        {
            if(pullRequestThread.Comments == null)
                continue;

            var targetComment = pullRequestThread.Comments.FirstOrDefault(comment => !string.IsNullOrEmpty(comment.Content) && comment.Content.Contains(commentContent));
            if(targetComment != null)
            {
                targetPullRequestComment = targetComment;
                break;
            }
        }

        if(targetPullRequestComment == null)
            return false;

        return targetPullRequestComment.UsersLiked.Length > 0;
    }

    public static async Task SetWorkItemsToReadyForTest(List<string> workItemIds, string workItemTester = null, string organization = "dips", string project = "DIPS", string apiVersion = "7.0", string personalAccessToken = null)
    {
        Logger.LogDebug($"Setting work items to 'Ready For Test'.");

        if(personalAccessToken == null)
        {
            personalAccessToken = AzureDevops.GetEnvironmentVariable("System.AccessToken");
        }
        
        List<Task> tasks = new List<Task>();

        foreach(var id in workItemIds)
        {
            var document = new JsonPatchDocument
            {
                new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.State",
                    Value = "Ready For Test"
                }
            };

            if(workItemTester != null)
            {
                document.Add(new JsonPatchOperation()
                {
                    Operation = Operation.Add,
                    Path = "/fields/System.AssignedTo",
                    Value = workItemTester
                });
            }

           tasks.Add(PatchWorkItem(int.Parse(id), document, organization, project, personalAccessToken, apiVersion));
        }

        try{
            await Task.WhenAll(tasks);
        }
        catch
        {
            Logger.LogDebug($"Could not set work item(s) to 'Ready For Test'.");
        }

    }

    private static async Task PatchWorkItem(int id, JsonPatchDocument document, string organization, string project, string personalAccessToken, string apiVersion)
    {
        var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", personalAccessToken))));

        var patchRequestUri = $"https://dev.azure.com/{organization}/{project}/_apis/wit/workitems/{id}?api-version={apiVersion}";

        Logger.LogDebug($"Patching work item {id}");

        try{
            var result = await httpClient.PatchAsync(patchRequestUri, new StringContent(JsonConvert.SerializeObject(document), Encoding.UTF8, "application/json-patch+json"));

            if (!result.IsSuccessStatusCode)
            {
                throw new Exception($"Could not patch work item {id}.");
            }
        }
        catch
        {
            throw new Exception($"Could not patch work item {id}.");
        }

    }
}

public enum Operation
{
    Add,
    Remove,
    Replace,
    Move,
    Copy,
    Test,
}

public class JsonPatchOperation
{
    [JsonProperty("op")]
    public Operation Operation { get; set; }
    public string Path { get; set; }
    public object Value { get; set; }
}

public class JsonPatchDocument : List<JsonPatchOperation>
{

}

public class WorkItemResponse
{
    public int Id { get; set; }
    public WorkItemFields Fields { get; set; }
    public List<WorkItemLink> Relations { get; set; }
}

public class WorkItemFields
{
    [JsonProperty("System.Title")]
    public string Title { get; set; }
    [JsonProperty("System.Description")]
    public string Description { get; set; }
    [JsonProperty("DIPS.TargetVersion")]
    public string TargetVersion { get; set; }
    [JsonProperty("Microsoft.VSTS.Common.Priority")]
    public int Priority { get; set; }
    [JsonProperty("System.State")]
    public string State { get; set; }
}

public class WorkItemLink
{
    public string Url { get; set; }

    public string Rel { get; set; }
}

public class WorkItemBatchRequest
{
    [JsonProperty("$expand")]
    public string Expand { get; } = "relations";
    public List<int> Ids { get; set; }
}

public class WorkItemBatchResponse
{
    public List<WorkItemResponse> Value { get; set; }
}

public class ReleaseNote
{
    public string Version { get; set; }
    public string Title { get; set; }
    public List<ReleaseNoteElement> Elements { get; set; }
}

public class ReleaseNoteElement
{
    public string Title { get; set; }
    public string Description {get; set; }
    public string ImageFilePath { get; set; }
}