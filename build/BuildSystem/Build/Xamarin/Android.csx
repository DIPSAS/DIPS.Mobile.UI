#load "../../Command.csx"
#load "../../Android/SDK.csx"
#load "../../Android/AndroidManifest.csx"
#load "../../Helpers/FileHelper.csx"

public static class Android
{
    /// <summary>
    /// Build Android project from a path
    /// </summary>
    /// <param name="androidProjectPath">The project path to the Android project</param>
    /// <param name="configuration">The msbuild configuration to run under</param>
    /// <returns></returns>
    public static Task Build(string androidProjectPath, string configuration ="Release"){
        var androidProjectFilePath = FileHelper.FindSingleFileByExtension(androidProjectPath,".csproj").FullName;
        return Command.ExecuteAsync("msbuild", $"{androidProjectFilePath} /p:Configuration={configuration}");
    }


    /// <summary>
    /// Cleans the Android project
    /// </summary>
    /// <param name="androidProjectPath">The project path to the Android project</param>
    /// <returns></returns>
    public static async Task Clean(string androidProjectPath, string configuration = "release"){
        var androidProjectFilePath = FileHelper.FindSingleFileByExtension(androidProjectPath, ".csproj").FullName;
        await Command.ExecuteAsync("msbuild", $"{androidProjectFilePath} /t:clean /p:Configuration={configuration}");
    }
}