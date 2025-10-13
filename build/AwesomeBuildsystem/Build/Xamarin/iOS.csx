#load "../../Command.csx"
#load "../../Helpers/FileHelper.csx"
#load "../../Core/BuildEnv.csx"

public static class iOS
{
    /// <summary>
    /// Build the iOS project from a path
    /// </summary>
    /// <param name="iOSProjectPath">The project path to the iOS project</param>
    /// <param name="configuration">The msbuild configuration to run under</param>
    /// <returns></returns>
    /// <remarks>This runs on iPhoneSimulator platform to save time</remarks>
    public static Task Build(string iOSProjectPath, string configuration ="Release"){
        var iOSProject = FileHelper.FindSingleFileByExtension(iOSProjectPath, ".csproj").FullName;
        return Command.ExecuteAsync("msbuild", $"{iOSProject} /p:Platform=iPhoneSimulator /p:Configuration={configuration}");
    }

    /// <summary>
    /// Package a iOS app from a project path
    /// </summary>
    /// <param name="iOSProjectPath">The project path to the iOS project</param>
    /// <param name="versionName">VersionName fetched from changelog</param>
    /// <param name="outputDirectory">The output directory for the signed .ipa application</param>
    /// <param name="configuration">The msbuild configuration to run under</param>
    /// <returns>awaitable task</returns>
    /// <remarks>Packaging a iOS app requires you to generate an mobile provisioning from http://developer.apple.com and a code sign identity. The running Mac need access to the profile, signcode and the p12 certificate needs to exist in the keychain. The signing properties of the release configuration needs to be set to automatic for this to work.</remarks>
    public static async Task Package(
        string iOSProjectPath,
        string versionName,
        string outputDirectory = null,
        string configuration ="Release"){

        outputDirectory = outputDirectory
                        == null ? BuildEnv.OutputDir : outputDirectory;
        var iOSProject = FileHelper.FindSingleFileByExtension(iOSProjectPath, ".csproj");
        //TODO: Update version and other plist features here with Plistbuddy
        await RemoveUnwantedPlistBrackets(GetInfoPlist(iOSProjectPath));
        await iOSBundleVersionNumbers(iOSProjectPath, versionName);
        
        await Command.ExecuteAsync("msbuild", $"{iOSProject} /p:Configuration={configuration} /p:Platform=iPhone /p:BuildIpa=true");

            //Make sure there is one, and only one .ipa in the bin configuration iPhone folder
        var ipaFile = FileHelper.FindSingleFileByExtension(Path.Combine(iOSProjectPath, "bin", "iPhone", configuration), ".ipa");
    
        Console.WriteLine($"Copy {ipaFile.Name} to {outputDirectory}");
        var outPutDir = outputDirectory.EndsWith('/') ? outputDirectory : outputDirectory+"/";
        var newIpaFilePath = outPutDir + ipaFile.Name;
        File.Copy(ipaFile.FullName, newIpaFilePath, true);
    }

    // Update Info.plist file in the iOS project
    private static async Task iOSBundleVersionNumbers(string iOSProjectPath, string versionName)
    {
        var iOSPlist = GetInfoPlist(iOSProjectPath);
        await Command.ExecuteAsync("/usr/libexec/PlistBuddy", $"-c \"Set :CFBundleVersion {versionName}\" {iOSPlist}");
        await Command.ExecuteAsync("rm", $"-f {iOSPlist}-e"); //Plistbuddy creates another file aswell, remove this
    }

    /// <summary>
    /// Updates the CFBundleIdentifier in the Info.plist
    /// </summary>
    /// <param name="iOSProjectPath"></param>
    /// <param name="bundleId"></param>
    /// <returns></returns>
    public static async Task SetBundleId(string iOSProjectPath, string bundleId)
    {
        var iOSPlist = GetInfoPlist(iOSProjectPath);
        await Command.ExecuteAsync("/usr/libexec/PlistBuddy", $"-c \"Set :CFBundleIdentifier {bundleId}\" {iOSPlist}");
        await Command.ExecuteAsync("rm", $"-f {iOSPlist}-e"); //Plistbuddy creates another file aswell, remove this
    }

    /// Remove unwanted brackets in Info.plist
    private static async Task RemoveUnwantedPlistBrackets(string iOSPlistPath){
        await Command.ExecuteAsync("sed", $"-i -e \"s/[][]//g\" {iOSPlistPath}");
    }

    private static string GetInfoPlist(string projectPath){
        var infoPlistFile = Directory.GetFiles(projectPath).FirstOrDefault(f => f.Equals(Path.Combine(projectPath, "Info.plist")));
        if(string.IsNullOrEmpty(infoPlistFile)){
            throw new Exception($"No file named Info.plist found in path: {projectPath}");
        }
        return infoPlistFile;
    }

    private static string GetProjectFile(string projectPath){
        var iOSProjectFile = Directory.GetFiles(projectPath).FirstOrDefault(f => f.EndsWith(".csproj"));
        if(iOSProjectFile == null){
            throw new Exception($"No iOS .csproj project found in path: {projectPath}");

        }
        return iOSProjectFile;
    }
    
    /// <summary>
    /// Clean the iOS project from a path
    /// </summary>
    /// <param name="iOSProjectPath">The project path to the iOS project</param>
    /// <param name="configuration">The configuration to run ms build under</param>
    /// <returns></returns>
    public static async Task Clean(string iOSProjectPath, string configuration="Release"){
        var iOSProject = FileHelper.FindSingleFileByExtension(iOSProjectPath,".csproj").FullName;
        await Command.ExecuteAsync("msbuild", $"{iOSProject} /t:clean /p:Configuration={configuration} /p:Platform=iPhoneSimulator");
        await Command.ExecuteAsync("msbuild", $"{iOSProject} /t:clean /p:Configuration={configuration} /p:Platform=iPhone");
    }
}