#load "../Command.csx"
#load "../AzureDevops.csx"
#load "../Repository.csx"



public static class dotnet
{
    public static Task Restore(string path)
    {
        return Command.ExecuteAsync("dotnet", $"restore {path}");
    }

    public static Task Build(string projectPath, string configuration = "Release")
    {
        return Command.ExecuteAsync("dotnet", $"build {projectPath} -c {configuration}");
    }

    //https://learn.microsoft.com/en-us/dotnet/maui/ios/deployment/publish-cli?view=net-maui-7.0
    public static Task PackiOS(string projectPath, string outputDir, string applicationDisplayVersion)
    {
        return Command.ExecuteAsync("dotnet", $"publish {projectPath} -f net7.0-ios -c Release -p:ArchiveOnBuild=true -p:RuntimeIdentifier=ios-arm64 -p:ApplicationDisplayVersion={applicationDisplayVersion} -p:ApplicationVersion={applicationDisplayVersion} -o {outputDir}");
    }

    //https://learn.microsoft.com/en-us/dotnet/maui/android/deployment/publish-cli?view=net-maui-7.0
    public static async Task PackAndroid(string projectPath, string outputDir, string applicationDisplayVersion, string applicationVersion)
    {
        ResolveSigningInformation(out var androidSignKeyStoreFile, out var androidSignKeyAlias, out var androidSigningKeyPass);

        await Command.ExecuteAsync("dotnet", $"publish {projectPath} -f net7.0-android -c Release -p:AndroidPackageFormats=apk -p:AndroidKeyStore=true -p:AndroidSigningKeyStore={androidSignKeyStoreFile} -p:AndroidSigningKeyAlias={androidSignKeyAlias} -p:AndroidSigningKeyPass={androidSigningKeyPass} -p:AndroidSigningStorePass={androidSigningKeyPass} -p:ApplicationDisplayVersion={applicationDisplayVersion} -p:ApplicationVersion={applicationVersion} -o {outputDir}");


        File.Delete(Path.Combine(outputDir, "com.dipsas.mobile.components.apk"));
        File.Move(Path.Combine(outputDir, "com.dipsas.mobile.components-Signed.apk"), Path.Combine(outputDir, "com.dipsas.mobile.components.apk"));
    }

    private static void ResolveSigningInformation(out string androidSignKeyStoreFile, out string androidSignKeyAlias, out string androidSigningKeyPass)
    {
        if(AzureDevops.IsBuildServer)
        {
            androidSignKeyStoreFile = AzureDevops.GetEnvironmentVariable("keystore.secureFilePath");
            androidSignKeyAlias = AzureDevops.GetEnvironmentVariable("android.keystore.alias");
            androidSigningKeyPass = AzureDevops.GetEnvironmentVariable("android.keystore.password");
        }else
        {
            var secretsDir = new DirectoryInfo(Path.Combine(Repository.RootDir(), "secrets"));
            var secretFiles = secretsDir.GetFiles();
            androidSignKeyStoreFile = secretFiles.FirstOrDefault(f => f.Extension.Contains("keystore")).FullName;
            var aliasFile = secretFiles.FirstOrDefault(f => f.Name.Contains("keystore_alias"));
            var keystorePwFile = secretFiles.FirstOrDefault(f => f.Name.Contains("keystore_password"));

            androidSignKeyAlias = File.ReadLines(aliasFile.FullName).FirstOrDefault();
            androidSigningKeyPass = File.ReadLines(keystorePwFile.FullName).FirstOrDefault();
        }
      
    }

    public static Task Pack(string projectPath, string version, string outputdir)
    {
        return Command.ExecuteAsync("dotnet", $"pack {projectPath} -p:PackageVersion={version} -o {outputdir}");
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

    public static Task NugetPush(string nupkgPath, string apiKey, string source, bool skipDuplicate = true)
    {
        string args = $"{nupkgPath} -k {apiKey} -s {source}";
        if (skipDuplicate)
        {
            args += " --skip-duplicate";
        }

        return Command.ExecuteAsync("dotnet", $"nuget push {args}");
    }

}