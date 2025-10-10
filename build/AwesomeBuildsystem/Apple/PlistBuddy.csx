#load "../Command.csx"

/// <summary>
/// PlistBuddy is mac build-in program which help the user to edit .plist file.
///When you are working with CFBundleXXXX file, it is useful for editing. Different with other program, PlistBuddy is not set as default path. If you need to run PlistBuddy, you need to run usr/libexec/PlistBuddy.
/// </summary>
public static class PlistBuddy{
    /// <summary>
    /// Set the CFBundleIdentifier in the Info.plist
    /// </summary>
    /// <param name="projectPath"></param>
    /// <param name="bundleId"></param>
    /// <returns></returns>
    public static async Task SetBundleId(string projectPath, string bundleId)
    {
        var iOSPlist = GetInfoPlist(projectPath);
        await Command.ExecuteAsync("/usr/libexec/PlistBuddy", $"-c \"Set :CFBundleIdentifier {bundleId}\" {iOSPlist}");
        await Command.ExecuteAsync("rm", $"-f {iOSPlist}-e"); //Plistbuddy creates another file aswell, remove this
    }

       private static string GetInfoPlist(string projectPath){
        var infoPlistFile = Directory.GetFiles(projectPath).FirstOrDefault(f => f.Equals(Path.Combine(projectPath, "Info.plist")));
        if(string.IsNullOrEmpty(infoPlistFile)){
            throw new Exception($"No file named Info.plist found in path: {projectPath}");
        }
        return infoPlistFile;
    }
}