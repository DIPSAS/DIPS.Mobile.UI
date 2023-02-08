#load "BuildSystem/Steps.csx"
#load "BuildSystem/Build/Xamarin/Android.csx"
#load "BuildSystem/Build/Xamarin/iOS.csx"
#load "BuildSystem/Build/MSBuild.csx"
#load "BuildSystem/Repository.csx"
#load "BuildSystem/Command.csx"
#load "BuildSystem/Nuget.csx"
#load "BuildSystem/AzureDevops.csx"

private static string RootDir = Repository.RootDir();
private static string SrcDir = Path.Combine(RootDir, "src");
//Full solution with library and samples app
private static string SolutionPath = SrcDir;
private static string LibraryAndroidPath = Path.Combine(RootDir, "src","library","DIPS.Mobile.UI.Droid");
private static string LibraryiOSPath = Path.Combine(RootDir, "src", "library", "DIPS.Mobile.UI.iOS");
//Source generator paths
private static string SourceGeneratorPath = Path.Combine(SrcDir, "sourcegenerator", "DIPS.Mobile.UI.SourceGenerator");
//Solution with nuget tests to test the nuget package
private static string NugetTestSolutionPath = Path.Combine(RootDir, "src", "tests", "nugettest");
private static string NugetTestAndroidPath = Path.Combine(RootDir, "src", "tests", "nugettest", "NugetTest.Droid");
private static string NugetTestiOSPath = Path.Combine(RootDir, "src", "tests", "nugettest", "NugetTest.iOS");

private static string NugetVersion = "1.0.0";

AsyncStep ci = async () =>
{
    await MSBuild.Build(SourceGeneratorPath);
    await Android.Build(LibraryAndroidPath);
    //TODO: ADD UNIT TESTS!!
    await iOS.Build(LibraryiOSPath);
};

AsyncStep cd = async () =>
{
    Console.WriteLine(AzureDevops.GetEnvironmentVariable("Build.BuildId"));
    Console.WriteLine(AzureDevops.GetEnvironmentVariable("Build.BuildNumber"));
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

    await MSBuild.Build(SourceGeneratorPath);
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
