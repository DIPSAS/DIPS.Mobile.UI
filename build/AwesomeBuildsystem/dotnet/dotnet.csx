#load "../Command.csx"



public static class dotnet
{
    public static Task Restore(string path)
    {
        return Command.ExecuteAsync("dotnet", $"restore {path}");
    }

    public static Task Clean(string path, string configuration="Release", string targetFramework="")
    {
        var args = $"-c {configuration}";
        if(!string.IsNullOrEmpty(targetFramework))
        {
            args += $" -p:TargetFramework={targetFramework}";
        }

        return Command.ExecuteAsync("dotnet", $"clean {path} {args}");
    }

    public static Task Build(string projectPath, string configuration = "Release", string targetFramework="")
    {
        var args = $"-c {configuration}";
        if(!string.IsNullOrEmpty(targetFramework))
        {
            args += $" -p:TargetFramework={targetFramework}";
        }
        return Command.ExecuteAsync("dotnet", $"build {projectPath} {args}");
    }

    //https://learn.microsoft.com/en-us/dotnet/maui/ios/deployment/publish-cli?view=net-maui-8.0
    public static Task PackiOS(string projectPath, string outputDir, string applicationDisplayVersion, string applicationId = "")
    {
        var args = $"-f net10.0-ios -c Release -p:ArchiveOnBuild=true -p:RuntimeIdentifier=ios-arm64 -p:ApplicationDisplayVersion={applicationDisplayVersion} -p:ApplicationVersion={applicationDisplayVersion} -o {outputDir}";
        if(!string.IsNullOrEmpty(applicationId))
        {
            args += $" -p:ApplicationId={applicationId}";
        }

        return Command.ExecuteAsync("dotnet", $"publish {projectPath} {args}");
    }

    //https://learn.microsoft.com/en-us/dotnet/maui/android/deployment/publish-cli?view=net-maui-8.0
    public static async Task PackAndroid(string projectPath, string outputDir, string applicationDisplayVersion, string applicationVersion, string applicationId = "", string androidPackageFormat = "aab")
    {
         var args = $"-f net10.0-android -c Release -p:ApplicationDisplayVersion={applicationDisplayVersion} -p:ApplicationVersion={applicationVersion} -o {outputDir} -r android-arm64 -p:AndroidPackageFormat={androidPackageFormat}";
        if(!string.IsNullOrEmpty(applicationId))
        {
            args += $" -p:ApplicationId={applicationId}";
        }

        await Command.ExecuteAsync("dotnet", $"publish {projectPath} {args}");
    }
    

    public static Task Test(string projectPath, string testResultsDir = "")
    {
        string args = "";
        if (!string.IsNullOrEmpty(testResultsDir))
        {
            args += $"-l:trx;LogFileName={testResultsDir}/testresults.xml";
        }
        return Command.ExecuteAsync("dotnet", $"test {projectPath} {args}");
    }
}
