#load "BuildSystem/Steps.csx"
#load "BuildSystem/Build/Xamarin/Android.csx"
#load "BuildSystem/Build/Xamarin/iOS.csx"
#load "BuildSystem/Build/MSBuild.csx"
#load "BuildSystem/Repository.csx"
#load "BuildSystem/Command.csx"
#load "BuildSystem/Nuget.csx"
#load "BuildSystem/dotnet/dotnet.csx"
#load "BuildSystem/AzureDevops.csx"

private static string RootDir = Repository.RootDir();
private static string SrcDir = Path.Combine(RootDir, "src");
//Full solution with library and samples app
private static string SolutionPath = SrcDir;
//Libary
private static string LibraryDir = Path.Combine(SolutionPath, "library");
private static string LibraryProjectPath = Path.Combine(LibraryDir, "DIPS.Mobile.UI.csproj");
//App
private static string AppDir = Path.Combine(SolutionPath, "app");
private static string AppProjectPath = Path.Combine(AppDir, "Components.csproj");
//Solution with nuget tests to test the nuget package
private static string NugetTestSolutionPath = Path.Combine(RootDir, "src", "tests", "nugettest");
private static string OutputDir = Path.Combine(RootDir, "output");
private static string LibraryPackageVersion = "1.1.0";

AsyncStep ci = async () =>
{
    await dotnet.Restore(LibraryDir);
    await dotnet.Build(LibraryProjectPath);
    //TODO: ADD UNIT TESTS!!
};

AsyncStep cd = async () =>
{
    if(Directory.Exists(OutputDir)){
        Directory.CreateDirectory(OutputDir);
    }

    var buildNumber = AzureDevops.GetEnvironmentVariable("Build.BuildNumber");
    var nugetSourceFeed = AzureDevops.GetEnvironmentVariable("nugetSourceFeed");
    if(string.IsNullOrEmpty(nugetSourceFeed)){
        throw new Exception("nugetSourceFeed: is not set for this build. Unable to push nuget package");
    }

    var version = string.Format("{0}-pre{1}", NugetVersion, buildNumber);
    await Nuget.Pack(RootDir, version, OutputDir);
    var nupkgFile = FileHelper.FindSingleFileByExtension(OutputDir, ".nupkg");
    await Nuget.Push(nupkgFile.FullName, nugetSourceFeed);
};


AsyncStep nugetTest = async () =>
{
    //Clear from nuget cache
    var homePath = Environment.GetEnvironmentVariable("HOME");
    var oldNugetPackageFilePath = Path.Combine(homePath, ".nuget", "packages", "dips.mobile.ui");
    if(File.Exists(oldNugetPackageFilePath)){
        Directory.Delete(oldNugetPackageFilePath, true);
    }
    
    //Clear from local packages folder
    var files = Directory.EnumerateFiles(Path.Combine(NugetTestSolutionPath, "packages"));
    foreach (var file in files)
    {
        File.SetAttributes(file, FileAttributes.Normal);
        File.Delete(file);
    }

    await Android.Build(LibraryAndroidPath);
    await iOS.Build(LibraryiOSPath);
    await Nuget.Pack(RootDir, NugetVersion, Path.Combine(NugetTestSolutionPath, "packages"));
};

var args = Args;
if(args.Count() == 0){
    await ExecuteSteps(new string[]{"help"});
    WriteLine("Please select steps to run:");
    var input = ReadLine();
    args = input.Split(' ');
}

await ExecuteSteps(args);
