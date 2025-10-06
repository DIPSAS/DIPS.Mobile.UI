#load "../Command.csx"
#load "../Logging/Logger.csx"
//https://keith.github.io/xcode-man-pages/altool.1.html
//xcrun altool -- Validate and Upload apps for the App Store, or Notarize apps for distribution outside of the Mac App Store.
public static class altool
{
    private static string APIKeyDirEnvironmentVariableKey => "API_PRIVATE_KEYS_DIR";
    /// <summary>
    // Uploads the given app archive for App Store submission. The app archive is submitted to the App Store. If successful, the app begins the process for being accepted into the App Store. If the upload is rejected, a list of errors is returned that need to be addressed before uploading again. It may be useful to use --validate-app first to catch common problems without uploading the entire package.
    /// </summary>
    /// <param name="ipaFilePath"></param>
    /// <param name="appleId"></param>
    /// <param name="apiKey"></param>
    /// <param name="apiIssuer"></param>
    /// <returns></returns>
    public static async Task<bool> ValidateApp(string ipaFilePath, string apiKeyId, string apiKeyLocation, string apiIssuer)
    {
        var oldApirDir = Environment.GetEnvironmentVariable(APIKeyDirEnvironmentVariableKey); //Get the old dir
        Environment.SetEnvironmentVariable(APIKeyDirEnvironmentVariableKey, apiKeyLocation); //Set a new one
        var result = await Command.CaptureAsync("xcrun", $"altool --validate-app -f {ipaFilePath} -t ios --apiKey {apiKeyId} --apiIssuer {apiIssuer}");
        var didArchive = false;
        if(!string.IsNullOrEmpty(result.StandardError) || !string.IsNullOrEmpty(result.StandardOut))
        {
            if(result.StandardOut.Contains("no errors validating archive", StringComparison.InvariantCultureIgnoreCase)){
                didArchive = true;
                Logger.LogSuccess("The app is valid to be uploaded to App Store.");
            }
            if(!didArchive)
            {    
                if(result.StandardError.Contains("Failed to load AuthKey file", StringComparison.InvariantCultureIgnoreCase))
                {
                    Logger.LogError($"Unable to locate the api key file in {apiKeyLocation}: \n {result.StandardError}", true);
                }
                Logger.LogError($"The app was not valid: \n {result.StandardError}", true);
            }
        }

        Environment.SetEnvironmentVariable(APIKeyDirEnvironmentVariableKey, oldApirDir); //Set a new one
        return didArchive;
    }

    public static async Task<bool> UploadPackage(string ipaFilePath, string appleId, string bundleId, string bundleVersion, string apiKeyId, string apiKeyLocation, string apiIssuer, bool shouldExit)
    {
        var oldApirDir = Environment.GetEnvironmentVariable(APIKeyDirEnvironmentVariableKey); //Get the old dir
        Environment.SetEnvironmentVariable(APIKeyDirEnvironmentVariableKey, apiKeyLocation); //Set a new one
        Logger.LogDebug($"Uploading {bundleId} to App: {appleId}");
        var result = await Command.CaptureAsync("xcrun", $"altool --upload-package {ipaFilePath} -t ios --apple-id {appleId} --bundle-id {bundleId} --bundle-version {bundleVersion} --bundle-short-version-string {bundleVersion} --apiKey {apiKeyId} --apiIssuer {apiIssuer}");
        var didUpload = false;
        if(!string.IsNullOrEmpty(result.StandardError) || !string.IsNullOrEmpty(result.StandardOut))
        {
            if(result.StandardOut.Contains("no errors uploading", StringComparison.InvariantCultureIgnoreCase)){
                didUpload = true;
                Logger.LogSuccess($"The app build was uploaded to App Store Connect. See here: https://appstoreconnect.apple.com/apps/{appleId}/testflight/ios");
            }
            if(!didUpload)
            {    
                if(result.StandardError.Contains("Failed to load AuthKey file", StringComparison.InvariantCultureIgnoreCase))
                {
                    Logger.LogError($"Unable to locate the api key file in {apiKeyLocation}: \n {result.StandardError}", shouldExit);
                }
                Logger.LogError($"The app was not uploaded: \n {result.StandardError}", shouldExit);
            }
        }
        Environment.SetEnvironmentVariable(APIKeyDirEnvironmentVariableKey, oldApirDir); //Set a new one
        return didUpload;
    }
}