#load "../Command.csx"
#load "../AzureDevops.csx"


public static class dotnet
{
    public static Task Restore(string path) => Command.ExecuteAsync("dotnet", $"restore {path}");
    
    public static Task Build(string projectPath) => Command.ExecuteAsync("dotnet", $"build {projectPath}");

    //https://learn.microsoft.com/en-us/dotnet/maui/ios/deployment/publish-cli?view=net-maui-7.0
    public static Task PackiOS(string projectPath, string outputDir, string applicationDisplayVersion, string applicationVersion){
        return Command.ExecuteAsync("dotnet", $"publish {projectPath} -f net7.0-ios -c Release -p:ArchiveOnBuild=true -p:RuntimeIdentifier=ios-arm64 -p:ApplicationDisplayVersion={applicationDisplayVersion} -p:ApplicationVersion={applicationVersion} -o {outputDir}");
    }

    //https://learn.microsoft.com/en-us/dotnet/maui/android/deployment/publish-cli?view=net-maui-7.0
    public static async Task PackAndroid(string projectPath, string outputDir, string applicationDisplayVersion, string applicationVersion){
        var androidSignKeyStoreFile = AzureDevops.GetEnvironmentVariable("keystore.secureFilePath");
        var androidSignKeyAlias = AzureDevops.GetEnvironmentVariable("android.keystore.alias");
        var androidSigningKeyPass = AzureDevops.GetEnvironmentVariable("android.keystore.password");

        await Command.ExecuteAsync("dotnet", $"publish {projectPath} -f net7.0-android -c Release -p:AndroidSigningKeyStore={androidSignKeyStoreFile} -p:AndroidSigningKeyAlias={androidSignKeyAlias} -p:AndroidSigningKeyPass={androidSigningKeyPass} -p:AndroidSigningStorePass={androidSigningKeyPass} -p:ApplicationDisplayVersion={applicationDisplayVersion} -p:ApplicationVersion={applicationVersion} -o {outputDir}");

        File.Move(Path.Combine(outputDir, "com.dipsas.mobile.components-Signed.apk"), Path.Combine(outputDir, "com.dipsas.mobile.components.apk"));
    }

    public static Task Pack(string projectPath, string version, string outputdir) => Command.ExecuteAsync("dotnet", $"pack {projectPath} -p:PackageVersion={version} -o {outputdir}");

    public static Task Test(string projectPath, string testResultsDir = "")
    {
        var args = "";
        if(!string.IsNullOrEmpty(testResultsDir))
        {
            args += $"-l:trx;LogFileName={testResultsDir}/testresults.xml";
        }
        return Command.ExecuteAsync("dotnet", $"test {projectPath} {args}");
    } 

    public static Task NugetPush(string nupkgPath, string apiKey, string source, bool skipDuplicate=true) 
    {
        var args = $"{nupkgPath} -k {apiKey} -s {source}";
        if(skipDuplicate)
        {
            args += " --skip-duplicate";
        }
        
        return Command.ExecuteAsync("dotnet", $"nuget push {args}");
    }

}