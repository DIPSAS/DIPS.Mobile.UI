#r "AwesomeBuildsystem/DIPS.Buildsystem.Core.dll"

//iOS
#load "AwesomeBuildsystem/Build/MAUI/iOS.csx"
#load "AwesomeBuildsystem/Apple/AppStoreConnect.csx"
#load "AwesomeBuildsystem/Apple/Xcode.csx"
#load "AwesomeBuildsystem/Apple/macOS.csx"
//Android
#load "AwesomeBuildsystem/Build/MAUI/Android.csx"
#load "AwesomeBuildsystem/Android/AndroidManifest.csx"
#load "AwesomeBuildsystem/Android/GoogleServices.csx"

#load "AwesomeBuildsystem/Utils.csx"
#load "AwesomeBuildsystem/Command.csx"
#load "AwesomeBuildsystem/Tools/ChangelogValidator.csx"
#load "AwesomeBuildsystem/Tools/VersionUtils.csx"
#load "AwesomeBuildsystem/Delivery/DeliveryService.csx"
#load "AwesomeBuildsystem/AzureDevops/AzureCLI.csx"
#load "AwesomeBuildsystem/AzureDevops/AzureDevops.csx"
#load "AwesomeBuildsystem/AzureDevops/AzureWorkItem.csx"
#load "AwesomeBuildsystem/SlackExtender/SlackExtender.csx"
#load "AwesomeBuildsystem/Mobile.Essentials/MobileEssentials.csx"
#load "AwesomeBuildsystem/Logging/Logger.csx"
#load "AwesomeBuildsystem/Android/GooglePlayDeveloper.csx"
#load "AwesomeBuildsystem/Documentation/InternalDocumentationFactory.csx"
#load "AwesomeBuildsystem/Documentation/DocumentationVerifier.csx"
#load "AwesomeBuildsystem/Helpers/StringHelper.csx"
#load "AwesomeBuildsystem/Helpers/NugetHelper.csx"
#load "AwesomeBuildsystem/Build/MAUI/PackageIdentifier.csx"

using DIPS.Buildsystem.Core.Slack;
using DIPS.Buildsystem.Core.Build;
using DIPS.Buildsystem.Core.DotnetBuild;
using DIPS.Buildsystem.Core.Files;
using DIPS.Buildsystem.Core.Versioning;
using DIPS.Buildsystem.Core.Logging;
using DIPS.Buildsystem.Core.PackageManagement;
using DIPS.Buildsystem.Core.Tasks;
using DIPS.Buildsystem.Core.Test;
using DIPS.Buildsystem.Core.Tools;
using DIPS.Buildsystem.Core.Utils;
using System.Text.RegularExpressions;
using System.Globalization;

const string ProjectName = "Components";
const string Configuration = "Release";
private static string SolutionName = "DIPS.Mobile.UI.sln";
private static string SolutionPath = BuildEnv.SrcDir;
private static string RootDirectory = BuildEnv.RootDir;
private static string ChangelogHeaderPrefix = "## [";
private static string VersionPattern = "[0-9]+.[0-9]+.[0-9]+";
private static string ComponentsProject = "Components.csproj";
private static string LibraryProject = "DIPS.Mobile.UI.csproj";
private static string ProductVersionFileName = "productversion.txt";
private static string ProductVersionPath = Path.Combine(SolutionPath, "..", ProductVersionFileName);
private static string ComponentsProjectPath = Path.Combine(SolutionPath, "app", "Components", ComponentsProject);
private static string ComponentsPath = Path.Combine(SolutionPath, "app", "Components");
private static string LibraryProjectPath = Path.Combine(SolutionPath, "library", "DIPS.Mobile.UI", LibraryProject);
private static string LibraryPath = Path.Combine(SolutionPath, "library", "DIPS.Mobile.UI");
private static string ChangeLogPath = Path.Combine(RootDirectory, "CHANGELOG.md");
private static string OutputDir = Path.Combine(RootDirectory, "output");
private static string RawAssetsPath = Path.Combine(ComponentsPath, "Resources", "Raw");
private static string ImageAssetsPath = Path.Combine(ComponentsPath, "Resources", "Images");
private static string AndroidPlatformPlath = Path.Combine(ComponentsPath, "Platforms", "Android");
private static string iOSPlatformPlath = Path.Combine(ComponentsPath, "Platforms", "iOS");
private static string TestPath = Path.Combine(SolutionPath, "tests", "unittests");
private static string TestProject = "DIPS.Mobile.UI.UnitTests";
private static int SharedVariableGroupId = 14;
private static string dotNetVersion = "9.0";
readonly string ChocoOutputDirectory = Path.Combine(BuildEnv.OutputDir);
public string CustomerKeyStoreFile = "dips-mob-android.keystore";
public string AndroidAppUploadKeystoreFile = "dips-mob-android-aab.keystore";
public static string PackageIdentifierName = "com.dipsas.components";
public bool IsDryRun => !string.IsNullOrEmpty(AzureDevops.GetEnvironmentVariable("IsDryRun")) || !string.IsNullOrEmpty(AzureDevops.GetEnvironmentVariable("DryRun"));
public string AppStoreConnectId = "6753674372";
public string AndroidTrack = "internal";

TaskRunner
    .AsyncTask("validateChangelog")
    .Description("check if changelog has been bumped")
    .Alias("vc")
    .DoesBefore(() => { Console.WriteLine("##[group]validateChangelog 📝"); return Task.CompletedTask; })
    .Does(async () => {

            var result = await ChangelogValidator.ValidateChangelog(BuildEnv.RootDir, ProductVersionPath, ChangelogHeaderPrefix, VersionPattern);
            if(result)
            {
                Logger.LogSuccess("Changelog has been bumped!");
            }
            else
            {
                Logger.LogError("Changelog has not been bumped!", true);
            }
        })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("init")
    .Description("initialize the solution. Downloading required nuget packages and making sure you are set to build this solution")
    .Alias("i")
    .DoesBefore(() => { Console.WriteLine("##[group]init"); return Task.CompletedTask; })
    .Does(() => dotnet.Restore(SolutionPath))
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("clean")
    .Description("initialize the solution. Downloading required nuget packages and making sure you are set to build this solution")
    .Alias("c")
    .DoesBefore(() => { Console.WriteLine("##[group]clean 🧹"); return Task.CompletedTask; })
    .Does(async () => 
    {
        //Clean soulution and android + ios project
        await Command.ExecuteAsync("msbuild", $"{BuildEnv.SrcDir}/{SolutionName} /t:clean /p:Configuration={Configuration}");
        await dotnet.Clean(ComponentsPath);

        //Remove all bin and obj folders
        Console.WriteLine($"Deleting all obj and bin subfolders under: {SolutionPath}");
        await Command.ExecuteAsync("find", ". -name \"obj\" -exec rm -rf \"{}\" +", SolutionPath);
        await Command.ExecuteAsync("find", ". -name \"bin\" -exec rm -rf \"{}\" +", SolutionPath);
        await Command.ExecuteAsync("rm", $"-rf {BuildEnv.OutputDir}");

    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("buildAndroid")
    .Description("build android project")
    .Alias("ba")
    .IsDependentOn("init")
    .DoesBefore(() => { Console.WriteLine("##[group]buildAndroid 🛠"); return Task.CompletedTask; })
    .Does(() => Android.Build(ComponentsPath))
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("packageAndroid")
    .Description("sign and package the android app")
    .Alias("pa")
    .IsDependentOn("init")
    .DoesBefore(() => {
        Console.WriteLine("##[group]packageAndroid 📦");
        Directory.CreateDirectory(BuildEnv.OutputDir);
        return Task.CompletedTask;
    })
    .Does(async() =>
    {
        //Get keystore secrets
        var variables =  await AzureCLI.GetVariable(SharedVariableGroupId, "android.keystore.alias","android.keystore.password","android.privatekey.password");
        //Get version code and version name
        var versionCode = Utils.GetBuildId().ToString();
        var versionName = Utils.GetChangelogVersion(RootDirectory, ChangelogHeaderPrefix, VersionPattern);

        await Android.Package(ComponentsPath, 
                        $"{GetKeyStoreFile()}", 
                        variables["android.keystore.password"], 
                        variables["android.keystore.alias"], 
                        variables["android.privatekey.password"],
                        versionCode,
                        versionName);
        
    })
    .DoesAfter(() => { 
        Console.WriteLine("##[endgroup]"); 
        return Task.CompletedTask; 
    });


TaskRunner
    .AsyncTask("buildiOS")
    .Description("build iOS project")
    .Alias("bi")
    .IsDependentOn("init")
    .DoesBefore(() => { Console.WriteLine("##[group]buildiOS 🛠"); return Task.CompletedTask; })
    .Does(() => iOS.Build(LibraryPath))
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("packageiOS")
    .Description("sign and package the ios app")
    .Alias("pi")
    .IsDependentOn("init")
    .DoesBefore(() =>
    {
        Console.WriteLine("##[group]packageiOS 📦");
        Directory.CreateDirectory(BuildEnv.OutputDir);
        return Task.CompletedTask;
    })
    .Does(async () =>
    {
        var versionName = Utils.GetChangelogVersion(RootDirectory, ChangelogHeaderPrefix, VersionPattern);

        await iOS.Package(ComponentsPath, versionName);
    })
    .DoesAfter(() => { 
        Console.WriteLine("##[endgroup]"); return Task.CompletedTask; 
        });

TaskRunner
    .AsyncTask("buildLibrary")
    .Description("build the DIPS.Mobile.UI library")
    .Alias("bl")
    .IsDependentOn("init")
    .DoesBefore(() => { Console.WriteLine("##[group]buildLibrary 📚"); return Task.CompletedTask; })
    .Does(async () => {
        await dotnet.Build(LibraryPath, Configuration);
    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("packageLibrary")
    .Description("package the DIPS.Mobile.UI library as NuGet package")
    .Alias("pl")
    .IsDependentOn("buildLibrary")
    .DoesBefore(() => { 
        Console.WriteLine("##[group]packageLibrary 📦");
        Directory.CreateDirectory(BuildEnv.OutputDir);
        return Task.CompletedTask;
    })
    .Does(async () => {
        var version = await GetVersionWithBranchSuffix();
        await Command.ExecuteAsync("dotnet", $"pack {LibraryProjectPath} -c {Configuration} -o {BuildEnv.OutputDir} /p:PackageVersion={version}");
    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("publishLibrary")
    .Description("publish the DIPS.Mobile.UI library to NuGet feed")
    .Alias("publ")
    .IsDependentOn("packageLibrary")
    .DoesBefore(() => { Console.WriteLine("##[group]publishLibrary 🚀"); return Task.CompletedTask; })
    .Does(async () => {
        if (!AzureDevops.IsBuildServer)
        {
            throw new Exception("You are not allowed to publish from your local machine.");
        }

        var nugetPackages = Directory.GetFiles(BuildEnv.OutputDir, "*.nupkg");
        if (!nugetPackages.Any())
        {
            throw new Exception("No NuGet packages found to publish.");
        }

        foreach (var package in nugetPackages)
        {
            Logger.LogDebug($"Publishing NuGet package: {package}");
            
            if (IsDryRun)
            {
                Logger.LogSuccess($"DryRun is enabled: Would have published {Path.GetFileName(package)} to NuGet feed.");
            }
            else
            {
                var apiKey = AzureDevops.GetEnvironmentVariable("dipsmobileuiNugetApiKey");
                await Command.ExecuteAsync("dotnet", $"nuget push {package} --source https://api.nuget.org/v3/index.json --api-key {apiKey}");
                Logger.LogSuccess($"Published {Path.GetFileName(package)} to NuGet feed.");
            }
        }
    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("createResourcesPR")
    .Description("Create a PR with updated design token resources")
    .Alias("crpr")
    .DoesBefore(() => { Console.WriteLine("##[group]createResourcesPR 🎨"); return Task.CompletedTask; })
    .Does(async () =>
    {
        var prBranchName = "designToken-resources-update";
        
        //checkout new branch
        Logger.LogDebug($"Trying to create {prBranchName}");
        await Command.CaptureAsync("git", $"branch -D {prBranchName}"); //Clean it up if its there
        await Command.CaptureAsync("git", $"checkout -b {prBranchName}");

        //Where is everything located
        //Generated resources
        var generatedAndroidDir = new DirectoryInfo(Path.Combine(OutputDir, "android"));
        var generatedTokensDir = new DirectoryInfo(Path.Combine(OutputDir, "tokens"));

        var generatedAndroidColorFile = generatedAndroidDir.GetFiles().FirstOrDefault(f => f.Name.Equals("colors.xml"));
        var generatedDotnetMauiColorsDir = generatedTokensDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("colors"));
        var generatedDotnetMauiIconsDir = generatedTokensDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("icons"));
        var generatedDotnetMauiSizesDir = generatedTokensDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("sizes"));
        var generatedDotnetMauiAnimationsDir = generatedTokensDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("animations"));

        //The source repository paths
        var libraryResourcesDir = new DirectoryInfo(Path.Combine(LibraryPath, "Resources"));
        var libraryAndroidDir = new DirectoryInfo(Path.Combine(LibraryPath, "Platforms", "Android"));

        var libraryDotnetMauiColorsDir = libraryResourcesDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Colors"));
        var libraryDotnetMauiIconsDir = libraryResourcesDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Icons"));
        var libraryDotnetMauiSizesDir = libraryResourcesDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Sizes"));
        var libraryDotnetMauiAnimationsDir = libraryResourcesDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Animations"));

        //Icons
        // TODO: Implement DesignTokenApplier.TryAddIcons or replace with appropriate logic
        Logger.LogDebug("Processing icons...");
        
        //Animations
        // TODO: Implement DesignTokenApplier.TryAddAnimations or replace with appropriate logic  
        Logger.LogDebug("Processing animations...");

        //Sizes
        // TODO: Implement DesignTokenApplier.TryAddSizes or replace with appropriate logic
        Logger.LogDebug("Processing sizes...");

        //Colors
        // TODO: Implement DesignTokenApplier.TryAddColors or replace with appropriate logic
        Logger.LogDebug("Processing colors...");
        if(generatedAndroidColorFile != null && libraryAndroidDir.Exists)
        {
            var valuesDir = Path.Combine(libraryAndroidDir.FullName, "Resources", "values");
            Directory.CreateDirectory(valuesDir);
            generatedAndroidColorFile.CopyTo(Path.Combine(valuesDir, generatedAndroidColorFile.Name), true);
        }
        

        //Bump changelog
        var changesetMessage = "Resources was updated from DIPS.Mobile.DesignTokens";
        var currentVersion = Utils.GetChangelogVersion(RootDirectory, ChangelogHeaderPrefix, VersionPattern);
        var versionParts = currentVersion.Split('.');
        var nextMinorVersion = int.Parse(versionParts[1]) + 1;
        var nextVersion = $"{versionParts[0]}.{nextMinorVersion}.0";
        
        var changelogContent = File.ReadAllText(ChangeLogPath);
        var updatedContent = $"## [{nextVersion}] \n- {changesetMessage}\n\n{changelogContent}";
        File.WriteAllText(ChangeLogPath, updatedContent);

        //Commit changes
        Logger.LogDebug($"Resources moved to folders, commiting changes");
        await Command.CaptureAsync("git", "add .", RootDirectory);
        await Command.CaptureAsync("git", $"commit -m 'Generated'");

        Logger.LogDebug($"Pushing {prBranchName} to repository");
        await Command.CaptureAsync("git", $"push -f origin {prBranchName}");

        //Create PR
        await Command.CaptureAsync("gh", $"auth login");
        await Command.ExecuteAsync("gh", $"pr create --title \"{changesetMessage}\" --body \"Here is the latest resources that was generated by DIPS.Mobile.DesignTokens repository\" --head {prBranchName}");
    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("test")
    .Description("run the shared test project")
    .Alias("t")
    .IsDependentOn("init")
    .DoesBefore(() => { Console.WriteLine("##[group]test 🧪"); return Task.CompletedTask; })
    .Does(async () => {
        //Verify before running unit tests
        await TaskRunner.RunAsync(new [] {"validate"}, false);
        await Command.ExecuteAsync("dotnet", $"test {TestPath}/{TestProject} -c Test");
    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("releasenote")
    .Description("get list of release notes from azure boards")
    .Alias("rn")
    .DoesBefore(() => { Console.WriteLine("##[group]releasenote 🗒"); return Task.CompletedTask; })
    .Does(async () => await AzureWorkItem.CreateReleaseNotes(RawAssetsPath, ImageAssetsPath, ProductVersionPath))
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("buildAll")
    .Description("build both Components app and DIPS.Mobile.UI library")
    .Alias("ball")
    .DoesBefore(() => { Console.WriteLine("##[group]buildAll 🛠"); return Task.CompletedTask; })
    .Does(async () => {
        await TaskRunner.RunAsync(new[] { "buildAndroid", "buildiOS", "buildLibrary" }, false);
    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("packageAll")
    .Description("package both Components app and DIPS.Mobile.UI library")
    .Alias("pall")
    .DoesBefore(() => { Console.WriteLine("##[group]packageAll 📦"); return Task.CompletedTask; })
    .Does(async () => {
        await TaskRunner.RunAsync(new[] { "packageAndroid", "packageiOS", "packageLibrary" }, false);
    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("ci")
    .Description("Complete CI build - builds, tests, and packages both Components app and DIPS.Mobile.UI library")
    .Alias("ci")
    .DoesBefore(() => { Console.WriteLine("##[group]CI Build 🚀"); return Task.CompletedTask; })
    .Does(async () => {
        // Run validation first
        await TaskRunner.RunAsync(new[] { "validateChangelog" }, false);
        
        // Build everything
        await TaskRunner.RunAsync(new[] { "buildAll" }, false);
        
        // Run tests
        await TaskRunner.RunAsync(new[] { "test" }, false);
        
        // Package everything
        await TaskRunner.RunAsync(new[] { "packageAll" }, false);
        
        Logger.LogSuccess("CI build completed successfully!");
    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });

TaskRunner
    .AsyncTask("publish")
    .Description("publishes the android and ios apps to stores")
    .Alias("pub")
    .DoesBefore(() => { Console.WriteLine("##[group]publish 🚀"); return Task.CompletedTask; })
    .Does( async () =>
    {
        if (!AzureDevops.IsBuildServer)
        {
            throw new Exception("You are not allowed to publish from your local machine.");
        }

        if (!await Git.CurrentBranchIsMain())
        {
            var currentBranch = await Git.GetCurrentBranch();
            // TODO: Enable when works 
            // throw new Exception($"You are not allowed to publish to stores from branch: {currentBranch}. Publishing to stores is only allowed from the main branch.");
        }

        //Create output folder
        if (!Directory.Exists(BuildEnv.OutputDir))
        {
            Directory.CreateDirectory(BuildEnv.OutputDir);
        }

        var outputFiles = Directory.GetFiles(BuildEnv.OutputDir);
        if(!outputFiles.Any(f => f.EndsWith(".ipa")) && !outputFiles.Any(f => f.EndsWith(".aab")))
        {
            await AzureCLI.DownloadArtifacts(Utils.GetBuildId(), BuildEnv.OutputDir);
        }

        outputFiles = Directory.GetFiles(BuildEnv.OutputDir); //Update outputFiles
        var packageName = $"{PackageIdentifierName}";
        var ipaFilePath = outputFiles.FirstOrDefault(f => f.EndsWith(".ipa"));
        var aabFilePath = outputFiles.FirstOrDefault(f => f.EndsWith(".aab"));
        var version = Utils.GetChangelogVersion(RootDirectory, ChangelogHeaderPrefix, VersionPattern);
        var releaseNote = Utils.GetChangelogNotes(RootDirectory, $"{ChangelogHeaderPrefix}{VersionPattern}] - [0-9]+-[0-9]+-[0-9]+");
        var isIOSDelivery = ipaFilePath != null;
        var isAndroidDelivery = aabFilePath != null;

        Logger.LogDebug($"Will publish apps with version: {version}");
        
        if(isIOSDelivery)
        {
            Logger.LogDebug("Will start upload process by using App Store Connect");
            var iosVariables = await AzureCLI.GetVariable(SharedVariableGroupId, "ios.appstoreconnect.api.keyid","ios.appstoreconnect.api.issuer");
            var appStoreConnectapiKey = iosVariables["ios.appstoreconnect.api.keyid"];
            var appstoreConnectApiIssuer = iosVariables["ios.appstoreconnect.api.issuer"];
            var apiKeyLocation = AzureDevops.GetEnvironmentVariable("Agent.TempDirectory");

            if(IsDryRun)
            {
                Logger.LogSuccess("DryRun is enabled: Would have started the upload process, but will verify the App against App Store instead.");
                try
                {
                    await AppStoreConnect.VerifyPackage(ipaFilePath, appStoreConnectapiKey, apiKeyLocation, appstoreConnectApiIssuer);
                }
                catch(Exception e)
                {
                    Logger.LogDebug(e.Message);
                }
                
            }
            else
            {
                //Uploads to builds, which means that internal test flight groups that has checked "automatic distribution" will get it.
                await AppStoreConnect.UploadPackage(ipaFilePath, AppStoreConnectId, packageName, version, appStoreConnectapiKey, apiKeyLocation, appstoreConnectApiIssuer, false);
                //TODO: Comment this in to enable whats new from changelog. But solve the nullpointer first:https://dev.azure.com/dips/DIPS/_build/results?buildId=382785&view=results
                // if(!IsCustomerDelivery)
                // {
                //     var files = Directory.GetFiles(apiKeyLocation);
                //     var appStoreConnectPrivateKeyFilePath = "";
                //     foreach (var file in files)
                //     {
                //         if(file.Contains(appStoreConnectapiKey)){
                //             appStoreConnectPrivateKeyFilePath = file;
                //         }
                //     }

                //     AppStoreConnect.AuthenticateFromPrivateKeyFile(appStoreConnectPrivateKeyFilePath, appstoreConnectApiIssuer, appStoreConnectapiKey, 15);
                //     await AppStoreConnect.ModifyWhatsNew(appleId, version, releaseNote);
                // }
            }
        }

        if (isAndroidDelivery) //Is Android delivery, delivering to Google Play Store
        {
            //Google Play Store
            Logger.LogDebug("Will start upload process by using Google Play Console.");
            var clientSecretsFilePath = AzureDevops.GetEnvironmentVariable("googleserviceaccount.secureFilePath");
            if (IsDryRun)
            {
                Logger.LogSuccess("DryRun is enabled: Would have started the upload process, but will verify the App against Google Play instead.");
                try
                {
                    await GooglePlayDeveloper.VerifyUpload(aabFilePath, packageName, clientSecretsFilePath);
                }catch(Exception e)
                {
                    Logger.LogDebug(e.Message);
                }
            }
            else
            {
                await GooglePlayDeveloper.Upload(aabFilePath, packageName, clientSecretsFilePath, version, AndroidTrack);
            }
        }

        //Delivery to slack when both deliveries was OK.
        if(isIOSDelivery && isAndroidDelivery && !IsDryRun)
        {
            SlackExtender.SetWebHookURI("/services/T0ACXDN4C/B09KD4G9691/5pmbp2slfOJoIIOOerZyg8R0"); //This is configured here : https://api.slack.com/apps/A04D1D1S4E7/incoming-webhooks
            await SlackExtender.SendAppStoreGooglePlayReleaseMessage(ProjectName, version, packageName, releaseNote, AndroidTrack, "Internal Testing");
        }
    })
    .DoesAfter((Func<Task>)(async() => { 
        await CreateDependencyVersionsToPipeline();
        Console.WriteLine("##[endgroup]");
    }));

TaskRunner
    .AsyncTask("printChanges")
    .Description("Will print all changes from and to a date")
    .Alias("prtc")
    .DoesBefore(() => { return Task.CompletedTask; })
    .Does( (Func<Task>)(async () =>
    {
        var allChanges = await VersionUtils.GetChangesFromChangelogWithDates(Path.Combine(RootDirectory, "changelog.md"), ChangelogHeaderPrefix);
        DateTime fromDate = DateTime.MinValue;
        DateTime toDate = DateTime.MinValue;
        var format = "dd.MM.yyyy";
        if(AzureDevops.IsBuildServer)
        {
            DateTime.TryParseExact(AzureDevops.GetEnvironmentVariable("fromDate"),format, CultureInfo.InvariantCulture, DateTimeStyles.None,  out fromDate);
            DateTime.TryParseExact(AzureDevops.GetEnvironmentVariable("toDate"), format, CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate);
        }
        else
        {
            WriteLine("From date:");
            DateTime.TryParseExact(ReadLine(),format, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
            WriteLine("To date (leave empty to use todays date):");
            DateTime.TryParseExact(ReadLine(),format, CultureInfo.InvariantCulture, DateTimeStyles.None, out toDate);
        }

        if(fromDate == DateTime.MinValue)
        {
            Logger.LogError($"From date is not set. Format has to be: {format}", true);
        }

        if(toDate == DateTime.MinValue)
        {
            toDate = DateTime.Now;
        }
        
        var relevantChanges = new List<string>();
        foreach (var (date, changes) in allChanges)
        {
            if(date >= fromDate && date <= toDate){
                relevantChanges.AddRange(changes);
            }
        }

        if(!Directory.Exists(BuildEnv.OutputDir))
        {
            Directory.CreateDirectory(BuildEnv.OutputDir);
        }

        var changesFilePath = Path.Combine(BuildEnv.OutputDir, "changes.md");
        var file = File.CreateText(Path.Combine(changesFilePath));
        if(relevantChanges.Count > 0)
        {
            var header = $"Changes between {fromDate.ToString(format)} - {toDate.ToString(format)}";
            await file.WriteAsync($"# {header} \n");
            Logger.LogSuccess(header);
        }
        else
        {
            file.Close();
            return;
        }

        foreach (var change in relevantChanges)
        {
            await file.WriteAsync($"{change}\n");
            WriteLine(change);
        }

        file.Close();
        AzureDevops.UploadArtifact("changes", "changes", changesFilePath);
        AzureDevops.UploadSummary(changesFilePath);
    }))
    .DoesAfter(() => { return Task.CompletedTask; });

TaskRunner
    .AsyncTask("dryRun")
    .Description("Trigger a dry run of a branch")
    .Alias("dry")
    .DoesBefore(() => { Console.WriteLine("##[group]dryRun 📦"); return Task.CompletedTask; })
    .Does(async () => {
        var branch = await Git.GetCurrentBranch();
        var componentsCDDefinitionId = 1356;
        Logger.LogDebug($"Will start Components CD (definition id: {componentsCDDefinitionId}) Dry Run for branch: {branch}");
        if(IsDryRun)
        {
            Logger.LogDebug("IsDryRun is set, will exit. It would have started azure build");
        }
        else
        {
            await AzureCLI.QueueBuild(componentsCDDefinitionId, branch, "isDryRun=true");
        }
        
    })
    .DoesAfter(() => { Console.WriteLine("##[endgroup]"); return Task.CompletedTask; });


private async Task UpdateGitCommitAndAppCreationDate()
{
    // Get the time zone for Norway
    TimeZoneInfo norwayTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");    
    // Convert the current UTC time to Norway time, respecting daylight saving
    DateTime norwayTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, norwayTimeZone);

    await MobileEssentials.ReplaceAppCreationTime(RawAssetsPath, norwayTime);
    var gitCommit = await Git.GetCurrentGitCommit();
    await MobileEssentials.ReplaceGitInfo(RawAssetsPath, gitCommit);
}

private string GetKeyStoreFile(){
    var keyStoreFileName = AndroidAppUploadKeystoreFile;

    if(AzureDevops.IsBuildServer){
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "work/_temp",keyStoreFileName);
    }
    else{
        return Path.Combine(BuildEnv.RootDir, "secrets", keyStoreFileName);
    }
}

private async Task<string> GetVersionWithBranchSuffix()
{
    var version = Utils.GetChangelogVersion(RootDirectory, ChangelogHeaderPrefix, VersionPattern);
    if (await Git.ShouldAddPreSuffix())
    {
        return $"{version}-pre";
    }
    return version;
}

Args.Remove("skipMAUIBootstrap"); //People can skip maui bootstrap
Args.Remove("skipBootstrap"); //People can skip bootstrap

await BuildWindow.RunAsync(Args, ProjectName);

// Nuget.Install("DIPS.Mobile.Essentials", sourceFeed: "https://pkgs.dev.azure.com/dips/_packaging/dips-mob-shared/nuget/v3/index.json", outputDir: ".tmp");
//⭐ Want to use Unicode icons, get them here: https://www.compart.com/en/unicode/block/U+1F300

public static async Task CreateDependencyVersionsToPipeline()
{
    //Add maui versions metadata to pipeline debug
    var pipelineDebugPath = Path.Combine(BuildEnv.OutputDir, "pipeline_debug");
    if(!Directory.Exists(pipelineDebugPath))
    {
        Directory.CreateDirectory(pipelineDebugPath);
    }
    var dotnetVersions = await GetDOTNETversions();
    var xcodeVersion = await Xcode.GetVersion();
    var macOSVersion = await macOS.GetVersion();
    var versionjson = $@"
{{
    "".net-maui"" : ""{dotnetVersions.Item1}"",
    "".net-ios"" : ""{dotnetVersions.Item2}"",
    "".net-android"" : ""{dotnetVersions.Item3}"",
    ""xcode-version"" : ""{xcodeVersion.Version}"",
    ""xcode-buildVersion"" : ""{xcodeVersion.BuildVersion}"",
    ""macos-version"" : ""{macOSVersion.Version}"",
    ""macos-buildVersion"" : ""{macOSVersion.BuildVersion}""
}}";
    var jsonFilePath = Path.Combine(pipelineDebugPath, "dependency-versions.json");
    var file = File.CreateText(jsonFilePath);
    await file.WriteAsync(versionjson);
    file.Close();
    
    if(AzureDevops.IsBuildServer)
    {
        AzureDevops.UploadArtifact("dependency-versions.json", "pipeline-debug", jsonFilePath);
    }
}

public static async Task<Tuple<string,string,string>> GetDOTNETversions()
{
    var dotnetWorkloadList = (await Command.CaptureAsync("dotnet", "workload list")).StandardOut.Split("\n");


    string iOSVersion = "";
    string androidVersion = "";
    string mauiVersion = "";
    foreach (var workload in dotnetWorkloadList)
    {
        if(workload.StartsWith("ios"))
        {
            iOSVersion = StringHelper.Between(StringHelper.RemoveWhitespace(workload), "ios", "SDK");
        }

        if(workload.StartsWith("android"))
        {
            androidVersion = StringHelper.Between(StringHelper.RemoveWhitespace(workload), "android", "SDK");
        }

        if(workload.StartsWith("maui"))
        {
            mauiVersion = StringHelper.Between(StringHelper.RemoveWhitespace(workload), "maui", "SDK");
        }
    }

    return new Tuple<string, string, string>(mauiVersion, iOSVersion, androidVersion);
}
