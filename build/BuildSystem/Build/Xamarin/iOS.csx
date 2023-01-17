#load "../../Command.csx"
#load "../../Helpers/FileHelper.csx"

public static class iOS
{
    /// <summary>
    /// Build the iOS project from a path
    /// </summary>
    /// <param name="iOSProjectPath">The project path to the iOS project</param>
    /// <param name="configuration">The msbuild configuration to run under</param>
    /// <returns></returns>
    /// <remarks>This runs on iPhoneSimulator platform to save time</remarks>
    public static Task Build(string iOSProjectPath, string configuration ="Release", string platform = "AnyCPU"){
        var iOSProject = FileHelper.FindSingleFileByExtension(iOSProjectPath, ".csproj").FullName;
        return Command.ExecuteAsync("msbuild", $"{iOSProject} /p:Platform={platform} /p:Configuration={configuration}");
    }
    
    /// <summary>
    /// Clean the iOS project from a path
    /// </summary>
    /// <param name="iOSProjectPath">The project path to the iOS project</param>
    /// <param name="configuration">The configuration to run ms build under</param>
    /// <returns></returns>
    public static async Task Clean(string iOSProjectPath, string configuration="Release", string platform = "AnyCPU"){
        var iOSProject = FileHelper.FindSingleFileByExtension(iOSProjectPath,".csproj").FullName;
        await Command.ExecuteAsync("msbuild", $"{iOSProject} /t:clean /p:Configuration={configuration} /p:Platform={platform}");
    }
}