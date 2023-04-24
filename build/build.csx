#load "BuildSystem/Steps.csx"
#load "BuildSystem/Build/Xamarin/Android.csx"
#load "BuildSystem/Build/Xamarin/iOS.csx"
#load "BuildSystem/Build/MSBuild.csx"
#load "BuildSystem/Repository.csx"
#load "BuildSystem/Command.csx"
#load "BuildSystem/Nuget.csx"
#load "BuildSystem/dotnet/dotnet.csx"
#load "BuildSystem/AzureDevops.csx"
#load "BuildSystem/Versioning/VersionUtil.csx"
#load "BuildSystem/Git.csx"

private static string RootDir = Repository.RootDir();
private static string ChangeLogPath = Path.Combine(RootDir, "CHANGELOG.MD");
private static string SrcDir = Path.Combine(RootDir, "src");
//Full solution with library and samples app
private static string SolutionPath = SrcDir;
//Libary
private static string LibraryDir = Path.Combine(SolutionPath, "library", "DIPS.Mobile.UI");
private static string LibraryProjectPath = Path.Combine(LibraryDir, "DIPS.Mobile.UI.csproj");
//App
private static string AppDir = Path.Combine(SolutionPath, "app");
private static string AppProjectPath = Path.Combine(AppDir, "Components.csproj");
//Solution with nuget tests to test the nuget package
private static string NugetTestSolutionPath = Path.Combine(RootDir, "src", "tests", "nugettest");
private static string OutputDir = Path.Combine(RootDir, "output");
private static string LibraryPackageVersion = "1.1.0";

AsyncStep build = async () =>
{
    await dotnet.Restore(LibraryDir);
    await dotnet.Build(LibraryProjectPath);
    //TODO: ADD UNIT TESTS!!
};

AsyncStep test = async () =>
{
    await dotnet.Restore(LibraryDir);
    await dotnet.Build(LibraryProjectPath);
    //TODO: ADD UNIT TESTS!!
};

AsyncStep pack = async () =>
{
    if (Directory.Exists(OutputDir))
    {
        Directory.CreateDirectory(OutputDir);
    }

    await PackLibrary();
};

AsyncStep publish = async () =>
{
    await Command.ExecuteAsync("ls", $"-a {OutputDir}");
    var nupkgFile = FileHelper.FindSingleFileByExtension(OutputDir, "nupkg");

    //Code sign
    var codeSignPath = AzureDevops.GetEnvironmentVariable("codesign.securefilepath");
    var codesignPw = AzureDevops.GetEnvironmentVariable("nuget.dipsas.certpw");

    await Command.ExecuteAsync("nuget", $"sign {nupkgFile.FullName} -CertificatePath {codeSignPath} -CertificatePassword {codesignPw}  -Timestamper http://timestamp.digicert.com/");

    //Push
    var apiKey = AzureDevops.GetEnvironmentVariable("dipsmobileuiNugetApiKey");
    if (string.IsNullOrEmpty(apiKey))
    {
        throw new Exception("dipsmobileuiNugetApiKey: is not set for this build. Unable to push nuget package");
    }

    throw new Exception($"Would have pushed:{nupkgFile.FullName}");
    // await dotnet.NugetPush(nupkgFile.FullName, apiKey, "https://api.nuget.org/v3/index.json");
};

AsyncStep nugetTest = async () =>
{
    //Clear from nuget cache
    var homePath = Environment.GetEnvironmentVariable("HOME");
    var oldNugetPackageFilePath = Path.Combine(homePath, ".nuget", "packages", "dips.mobile.ui");
    if (File.Exists(oldNugetPackageFilePath))
    {
        Directory.Delete(oldNugetPackageFilePath, true);
    }

    //Clear from local packages folder
    var packagesDir = Path.Combine(NugetTestSolutionPath, "packages");
    if (Directory.Exists(packagesDir))
    {
        var files = Directory.EnumerateFiles(Path.Combine(NugetTestSolutionPath, "packages"));
        foreach (var file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }
    }
    else
    {
        Directory.CreateDirectory(packagesDir);
    }


    await dotnet.Restore(LibraryDir);
    await dotnet.Build(LibraryProjectPath);
    await PackLibrary(packagesDir);
};

var args = Args;
if(args.Count() == 0){
    await ExecuteSteps(new string[]{"help"});
    WriteLine("Please select steps to run:");
    var input = ReadLine();
    args = input.Split(' ');
}

await ExecuteSteps(args);


async Task<FileInfo> PackLibrary(string outputdir = null)
{
    outputdir ??= OutputDir;
    var version = VersionUtil.GetLatestVersionFromChangelog(ChangeLogPath);
    if (!await Git.CurrentBranchIsMaster())
    {
        var buildNumber = AzureDevops.GetEnvironmentVariable("Build.BuildNumber");
        version += $"-pre{buildNumber}";
    }
    await dotnet.Pack(LibraryProjectPath, version, outputdir);
    return FileHelper.FindSingleFileByExtension(outputdir, ".nupkg");;
}
