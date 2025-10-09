#r "nuget:Newtonsoft.Json, 13.0.3"
#load "../Helpers/FileHelper.csx"
#load "../Tools/Git.csx"
#load "../Command.csx"

using Newtonsoft.Json;
/// <summary>
/// Buildsystem helper class to support consumers of DIPS.Mobile.Essentials during build and packaging
/// </summary>
public static class MobileEssentials
{
    /// <summary>
    /// Replaces the appsettings.json ProductInfo.Version in the specific location
    /// </summary>
    /// <param name="pathToAppSettings">Path to the app settings file</param>
    /// <param name="version">The version it should be set to.</param>
    public static async Task ReplaceProductVersion(string pathToAppSettings, string version)
    {
        var appSettingsFile = FileHelper.FindSingleFileByName(pathToAppSettings, "appsettings.json");
        
        var json = await File.ReadAllTextAsync(appSettingsFile.FullName);
        dynamic jsonObj = JsonConvert.DeserializeObject(json);
        jsonObj["ProductInfo"]["Version"] = version;
        string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
        await File.WriteAllTextAsync(appSettingsFile.FullName, output);
    }
    

    /// <summary>
    /// Add git info to the app.
    /// </summary>
    /// <param name="pathToAppSettings"Path to the app settings file</param>
    /// <param name="commitSha">The commit to add to the app, this should be the commit used when packaging.</param>
    /// <param name="url">The optional url to set, this can be hardcoded in app settings file so no need to update it.</param>
    public static async Task ReplaceGitInfo(string pathToAppSettings, string commitSha, string url = "")
    {
        var appSettingsFile = FileHelper.FindSingleFileByName(pathToAppSettings, "appsettings.json");
        
        var json = await File.ReadAllTextAsync(appSettingsFile.FullName);
        dynamic jsonObj = JsonConvert.DeserializeObject(json);

        if(!string.IsNullOrEmpty(url))
        {
            jsonObj["Git"]["Url"] = url;
        }
        
        if(!string.IsNullOrEmpty(commitSha))
        {
            jsonObj["Git"]["Commit"] = commitSha;
        }

        string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
        await File.WriteAllTextAsync(appSettingsFile.FullName, output);
    }

    public static Task<string> ReadAppSettings(string pathToAppSettings)
    {
        var appSettingsFile = FileHelper.FindSingleFileByName(pathToAppSettings, "appsettings.json");
        
        return File.ReadAllTextAsync(appSettingsFile.FullName);
    }

    /// <summary>
    /// Add a creation time to the app.
    /// </summary>
    /// <param name="pathToAppSettings">Path to the app settings file</param>
    /// <param name="version">The version it should be set to.</param>
    public static async Task ReplaceAppCreationTime(string pathToAppSettings, DateTime creationTime)
    {
        var appSettingsFile = FileHelper.FindSingleFileByName(pathToAppSettings, "appsettings.json");

        string json = await File.ReadAllTextAsync(appSettingsFile.FullName);
        dynamic jsonObj = JsonConvert.DeserializeObject(json);
        jsonObj["CreationTime"] = creationTime;
        string output = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
        await File.WriteAllTextAsync(appSettingsFile.FullName, output);
    }

    public static Task ResetAppSettings(string pathToAppSettings)
    {
        var appSettingsFile = FileHelper.FindSingleFileByName(pathToAppSettings, "appsettings.json");
        return Git.ResetFile(appSettingsFile.FullName);
    }

    public static string CopyAppSettings(string pathToAppSettings, string directoryPath, string filePostFix)
    {
        var appSettingsFile = FileHelper.FindSingleFileByName(pathToAppSettings, "appsettings.json");
        if(!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        var pathToCopyTo = Path.Combine(directoryPath, $"appsettings_{filePostFix}.json");
        File.Copy(appSettingsFile.FullName, pathToCopyTo);
        return pathToCopyTo;
    }

    public static async Task<string> CopyFileFromNugetPackage(string version, string fileToCopy, string pathToCopyTo)
    {
        var tmpPath = Path.Combine(Directory.GetCurrentDirectory(), ".tmp");
        var newFilePath = string.Empty;
        if(Version.Parse(version) >= new Version(14, 0 ,1))
        {
            await Command.ExecuteAsync("nuget", $"install DIPS.Mobile.Essentials -Source https://pkgs.dev.azure.com/dips/_packaging/dips-mob-shared/nuget/v3/index.json -Version {version} -DependencyVersion Ignore -OutputDir .tmp", workingDirectory: Directory.GetCurrentDirectory());
            var essentialsEventCodesFile = Path.Combine(tmpPath, $"DIPS.Mobile.Essentials.{version}", fileToCopy);
            if(File.Exists(essentialsEventCodesFile))
            {
                if(!Directory.Exists(pathToCopyTo))
                {
                    Directory.CreateDirectory(pathToCopyTo);
                }
                
                newFilePath = Path.Combine(pathToCopyTo, fileToCopy);
                File.Move(essentialsEventCodesFile, newFilePath);
            }
        }
        Directory.Delete(tmpPath, true);

        return newFilePath;
    }
    
}