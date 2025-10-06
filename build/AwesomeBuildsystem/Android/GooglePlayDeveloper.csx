#r "../../DIPS.Buildsystem.Core.dll"
#load "../Logging/Logger.csx"
#r "nuget:Google.Apis.AndroidPublisher.v3, 1.67.0.3354"
#r "nuget:Google.Apis.Oauth2.v2 , 1.67.0.1869"
#r "nuget:Google.Apis, 1.67.0"
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using System.IO;
#nullable enable
//https://developers.google.com/android-publisher#publishing
//https://developers.google.com/android-publisher/api-ref/rest/v3/edits.apks/upload
//https://github.com/googleapis/google-api-dotnet-client/blob/main/Src/Generated/Google.Apis.AndroidPublisher.v3/Google.Apis.AndroidPublisher.v3.cs

public static class GooglePlayDeveloper
{
    //Taken from Java Samples: https://github.com/googlesamples/android-play-publisher-api/blob/f4e9a7609f1528ac82fced693e0bff2939ce2931/v3/java/src/com/google/play/developerapi/samples/AndroidPublisherHelper.java#L60
    public static string MIME_TYPE_APK = "application/vnd.android.package-archive";
    public static string MIME_TYPE_AAB = "application/octet-stream";

    /// <summary>
    /// Uploads an apk to the Google Play store. Will add the package to the correct App project and if provided with a track it will assign the version to the track.
    /// </summary>
    /// <param name="appFilePath">The path to the apk file</param>
    /// <param name="packageName">The package name of the apk</param>
    /// <param name="serviceAccountJsonFilePath">The service account json file used for authentication</param>
    /// <param name="track"></param>
    /// <returns></returns>
    /// <remarks>
    ///Based on : https://developers.google.com/android-publisher/edits
    ///Specifically the workflow of uploading: https://developers.google.com/android-publisher/edits#workflow
    /// </remarks>
    public static async Task Upload(string appFilePath, string packageName, string serviceAccountJsonFilePath,string packageVersion, string? track = null)
    {
        TimeSpan? timeout = null;
        var isAab = !appFilePath.EndsWith(".apk");
        if (isAab)
        {
            timeout = TimeSpan.FromMinutes(3);
        }
        
        (var edits, var appEdit) = await InitializeEditsApi(serviceAccountJsonFilePath, packageName, timeout);

        //https://developers.google.com/android-publisher/api-ref/rest/v3/edits.apks/upload

        long? versionCode = null;
        if(isAab)
        {
            var aab = await TryUploadAab(appFilePath, packageName, appEdit, edits, true);
            if(aab != null)
            {
                versionCode = aab.VersionCode;
            }
        }
        else
        {
            var apk = await TryUploadApk(appFilePath, packageName, appEdit, edits, true);
            if(apk != null)
            {
                versionCode = apk.VersionCode;
            }
        }

        if(versionCode == null)
        {
            Logger.LogError("Google Play Developer: The app was not verified to be uploaded, versionCode was null", true);
            return;
        }

         Logger.LogSuccess($"Google Play Developer: {packageName} was verified for uploading.");
        var didAssignToTrack = false;
        if(!string.IsNullOrEmpty(track)) //Move the app from bundle library to a track. Once this is done there is no way back in Google Play.
        {
            Logger.LogDebug($"Google Play Developer: {packageName} was verified for uploading.");
            var theTrack = new Track
            {
            Releases = new List<TrackRelease>(){
            new TrackRelease()
                {
                    Name = $"{packageVersion}",
                    Status = "completed",
                    VersionCodes = new List<long?>(){(long)versionCode.Value}
                    //Can set release notes if we want
                }
            }};

            var updateTrackRequest = edits.Tracks.Update(
                theTrack , packageName, appEdit.Id, track);
            await updateTrackRequest.ExecuteAsync();
            didAssignToTrack = true;
        }

        var commitRequest = edits.Commit(packageName, appEdit.Id);
        var response = await commitRequest.ExecuteAsync(); //Commit when everything is finalized
        if(didAssignToTrack)
        {
            Logger.LogSuccess($"Google Play Developer: {packageName} was successfully uploaded and assigned to Track: {track}");
        }else
        {
            Logger.LogSuccess($"Google Play Developer: {packageName} was successfully uploaded but not assigned to a track.");
        }

    }
    
    /// <summary>
    /// Verifies an apk to the Google Play store. Will try to the package to the correct App project.
    /// </summary>
    /// <param name="appFilePath">The path to the app file</param>
    /// <param name="packageName">The package name of the app</param>
    /// <param name="serviceAccountJsonFilePath">The service account json file used for authentication</param>
    /// <param name="track"></param>
    /// <returns></returns>
    public static async Task VerifyUpload(string appFilePath, string packageName, string serviceAccountJsonFilePath)
    {
        try{
            (var edits, var appEdit) = await InitializeEditsApi(serviceAccountJsonFilePath, packageName);
            if(appFilePath.EndsWith(".apk"))
            {
                var apk = await TryUploadApk(appFilePath, packageName, appEdit, edits, false);
                if(apk != null)
                {
                    Logger.LogSuccess("Google Play Developer: app was verified and can be uploaded.");
                }
                else
                {
                    Logger.LogError("Google Play Developer: app was not verified and can not be uploaded.", false);
                }
            }else //Bundle
            {
                var apk = await TryUploadAab(appFilePath, packageName, appEdit, edits, false);
                if(apk != null)
                {
                    Logger.LogSuccess("Google Play Developer: app was verified and can be uploaded.");
                }
                else
                {
                    Logger.LogError("Google Play Developer: app was not verified and can not be uploaded.", false);
                }
            }
            
        }catch(Exception e)
        {
            Logger.LogError($"Google Play Developer: app was not verified and can not be uploaded: \n {e.Message}", false);
            Logger.LogDebug($"\n StackTrace:\n {e.StackTrace}");
        }        
    }

    private static async Task<Tuple<EditsResource, AppEdit>> InitializeEditsApi(string serviceAccountJsonFilePath, string packageName, TimeSpan? timeSpan = null)
    {
        var initializer = new BaseClientService.Initializer()
        {
            HttpClientInitializer = GetCredatials(serviceAccountJsonFilePath),
            ApplicationName = "DIPS Continious Delivery",
            
        };
        var publisherService = new AndroidPublisherService(initializer);
        if (timeSpan != null)
        {
            publisherService.HttpClient.Timeout = timeSpan.Value;    
        }
        
        var edits = publisherService.Edits;
        
        //https://developers.google.com/android-publisher/api-ref/rest/v3/edits#AppEdit
        var appEdit = new AppEdit();
        var insertRequest = edits.Insert(appEdit, packageName);
        var insertResponse = await insertRequest.ExecuteAsync();
        appEdit = insertResponse;
        return new Tuple<EditsResource, AppEdit>(edits, appEdit);
    }

    private static async Task<Bundle?> TryUploadAab(string aabFilePath, string packageName, AppEdit appEdit,
        EditsResource edits, bool shouldThrow)
    {
        Bundle bundle = new Bundle();
        
        using(var fileStream = File.Open(aabFilePath, FileMode.Open))
        {
            Logger.LogDebug($"Google Play Developer: Starting upload verification of {packageName}");
            var uploadRequest = edits.Bundles.Upload(packageName, appEdit.Id, fileStream, MIME_TYPE_AAB);

            uploadRequest.ResponseReceived += response =>
            {
                bundle = response;
            };
            
            var uploadProgress = await uploadRequest.UploadAsync();
            

            if(uploadProgress.Status != Google.Apis.Upload.UploadStatus.Completed)
            {
                if(uploadProgress.Exception is not null)
                {
                    if(shouldThrow){
                        throw uploadProgress.Exception;
                    }
                    Logger.LogDebug(uploadProgress.Exception.Message);
                    Logger.LogDebug(uploadProgress.Exception.StackTrace);
                    return null;
                }
            }
            return bundle;
        }
    }
    //https://developers.google.com/android-publisher/api-ref/rest/v3/edits.apks/upload
    private static async Task<Apk?> TryUploadApk(string apkFilePath, string packageName, AppEdit appEdit, EditsResource edits, bool shouldThrow)
    {
        Apk apk = new Apk();
        
        using(var fileStream = File.Open(apkFilePath, FileMode.Open))
        {
            Logger.LogDebug($"Google Play Developer: Starting upload verification of {packageName}");
            var uploadRequest = edits.Apks.Upload(packageName, appEdit.Id, fileStream, MIME_TYPE_APK);
            
            uploadRequest.ResponseReceived += response =>
            {
                apk = response;
            };
            
            
            var uploadProgress = await uploadRequest.UploadAsync();
            

            if(uploadProgress.Status != Google.Apis.Upload.UploadStatus.Completed)
            {
                if(uploadProgress.Exception is not null)
                {
                    if(shouldThrow){
                        throw uploadProgress.Exception;
                    }
                    Logger.LogDebug(uploadProgress.Exception.Message);
                    Logger.LogDebug(uploadProgress.Exception.StackTrace);
                    return null;
                }
            }
            return apk;
        }
    }


    private static ServiceAccountCredential GetCredatials(string serviceAccountJsonFilePath)
    {
        ServiceAccountCredential credential;
        using (var stream = new FileStream(serviceAccountJsonFilePath, FileMode.Open, FileAccess.Read))
        {
            credential = ServiceAccountCredential.FromServiceAccountData(stream);
            credential.Scopes = new[] { AndroidPublisherService.Scope.Androidpublisher };
        }
        return credential;
    }
}