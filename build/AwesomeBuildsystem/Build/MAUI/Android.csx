#load "../../Command.csx"
#load "../../Android/SDK.csx"
#load "../../Android/AndroidManifest.csx"
#load "../../dotnet/dotnet.csx"
#load "../../Helpers/FileHelper.csx"

using System.Xml;

public static class Android
{
    /// <summary>
    /// Build Android project from a path
    /// </summary>
    /// <param name="androidProjectPath">The project path to the Android project</param>
    /// <param name="configuration">The msbuild configuration to run under</param>
    /// <returns></returns>
    public static Task Build(string androidProjectPath, string configuration ="Release"){
        return dotnet.Build($"{androidProjectPath}.csproj", configuration, targetFramework:"net9.0-android");
    }
    
    /// <summary>
    /// Package an Android app from a path
    /// </summary>
    /// <param name="androidProjectPath">The project path to the Android project</param>
    /// <param name="keyStorePath">The path to the keystore to use during signing</param>
    /// <param name="keyStorePassword">The password to use during keystore signing</param>
    /// <param name="keyStoreAlias">The keystore alias to use during signing</param>
    /// <param name="keyPassword">The key password to use during signing</param>
    /// <param name="outputDirectory">The output directory to use when signing</param>
    /// <param name="configuration">The msbuild configuration to run under</param>
    /// <param name="shouldZipAlign">Determines if the android .apk should be zip aligned before its signed</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <remarks>When creating an Android package it must be signed with a keystore. Please contact Team Mobil to get the keystore details.</remarks>
     public static async Task Package(
        string androidProjectPath, 
        string keyStorePath,
        string keyStorePassword,
        string keyStoreAlias,
        string keyPassword,
        string versionCode,
        string versionName,
        string outputDirectory = null,
        string applicationId = "",
        string packageFormat = "aab"){

        if(!File.Exists(keyStorePath)){
                    throw new Exception($"The Android keystore file does not exist: {keyStorePath}");
        }
        outputDirectory = outputDirectory
                          == null ? (Environment.GetEnvironmentVariable("BUILD_ARTIFACTSTAGINGDIRECTORY") ?? Path.Combine(Directory.GetCurrentDirectory(), "output")) : outputDirectory;
        var androidProjectFilePath = FileHelper.FindSingleFileByExtension(androidProjectPath, ".csproj");
        await dotnet.PackAndroid(androidProjectFilePath.FullName, outputDirectory, versionName, versionCode, applicationId:applicationId, androidPackageFormat:packageFormat);

        var artifactFiles = FileHelper.FindAllFilesByExtension(outputDirectory, $".{packageFormat}");
        var signedFiles = artifactFiles.Where(f => f.Name.Contains("-signed", StringComparison.InvariantCultureIgnoreCase));
        foreach (var signedFile in signedFiles)
        {
            File.Delete(signedFile.FullName);
        }

        var additionalFiles = Directory.GetFiles(outputDirectory, "*");
        foreach (var file in additionalFiles)
        {
            if(!file.EndsWith($".{packageFormat}"))
            {
                File.Delete(file);
            }
        }

        var appFilePath = FileHelper.FindSingleFileByExtension(outputDirectory, $".{packageFormat}");
        if(packageFormat == "aab")
        {
            //Taken from: https://developer.android.com/build/building-cmdline#sign_cmdline
            await Command.ExecuteAsync("jarsigner", $"-verbose -sigalg SHA256withRSA -digestalg SHA-256 -storepass {keyStorePassword} -keystore {keyStorePath} -keypass {keyStorePassword} {appFilePath} {keyStoreAlias}");
        }

        if(packageFormat == "apk")
        {
            var apkFilePath = appFilePath.FullName;
            //Rename before zip aligning
            var unalignedApkFilePath = apkFilePath.Replace(".apk", ".unaligned.apk");
            File.Move(apkFilePath, unalignedApkFilePath, true);
            var zipAlign = SDK.FindAndroidBuildTool("zipalign");
            //Zip align it
            await Command.ExecuteAsync(zipAlign, $"4 {unalignedApkFilePath} {apkFilePath}");
            //Verify zip align worked
            await Command.ExecuteAsync(zipAlign, $"-c 4 {apkFilePath}");
            //Delete the unaligned apk
            File.Delete(unalignedApkFilePath);

            //Caution: If you sign the APK before making any further change to the apk, the apk signature is invalidated.
            //What is apksigner? https://developer.android.com/studio/command-line/apksigner
            var apksigner = SDK.FindAndroidBuildTool("apksigner");
            //Sign it
            await Command.ExecuteAsync(apksigner, $"sign --ks {keyStorePath} --ks-pass pass:{keyStorePassword} --ks-key-alias {keyStoreAlias} --key-pass pass:{keyPassword} --verbose {apkFilePath}");
            //Check whether the APK's signatures are expected to be confirmed as valid on all Android platforms that the APK supports
            await Command.ExecuteAsync(apksigner, $"verify -v {apkFilePath}");
        }
        
    }

    /// <summary>
    /// Cleans the Android project
    /// </summary>
    /// <param name="androidProjectPath">The project path to the Android project</param>
    /// <returns></returns>
    public static async Task Clean(string androidProjectPath, string configuration = "release"){
        var androidProjectFilePath = FileHelper.FindSingleFileByExtension(androidProjectPath, ".csproj").FullName;
        await dotnet.Clean(androidProjectFilePath, configuration);
    }

    private static string GetProjectFile(string projectPath){
            var androidProjectFile = Directory.GetFiles(projectPath).FirstOrDefault(f => f.EndsWith(".csproj"));
            if(androidProjectFile == null){
                throw new Exception($"No Android .csproj project found in path: {projectPath}");

            }
            return androidProjectFile;
        }
}