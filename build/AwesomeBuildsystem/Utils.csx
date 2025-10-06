#load "Command.csx"
#load "Logging/Logger.csx"

#r "DIPS.Buildsystem.Core.dll"
using System.Text.RegularExpressions;
using DIPS.Buildsystem.Core.Integrations;
using DIPS.Buildsystem.Core.Versioning;

public static class Utils{
    /// <summary>
    /// Get version number from changelog
    /// </summary>
    /// <param name="rootDirectory">Root directory of the project. (<see cref="BuildEnv.RootDir"/>)</param>
    /// <param name="changelogHeaderPrefix">See <see cref="DIPS.Buildsystem.Core.Versioning.VersionUtil.GetLatestVersionFromChangelog(string, string, string)"/></param>
    /// <param name="versionPattern">See <see cref="DIPS.Buildsystem.Core.Versioning.VersionUtil.GetLatestVersionFromChangelog(string, string, string)"/></param>
    public static string GetChangelogVersion(string rootDirectory, string changelogHeaderPrefix = null, string versionPattern = null){
        var changelog = GetChangelog(rootDirectory);

        var versionName = string.Empty;
        if(!string.IsNullOrEmpty(changelogHeaderPrefix) && !string.IsNullOrEmpty(versionPattern)) {
            versionName = VersionUtil.GetLatestVersionFromChangelog(changelog, changelogHeaderPrefix, versionPattern);

        }else if(!string.IsNullOrEmpty(changelogHeaderPrefix) && string.IsNullOrEmpty(versionPattern)){
            versionName = VersionUtil.GetLatestVersionFromChangelog(changelog, headerPrefix: changelogHeaderPrefix);

        }else if(string.IsNullOrEmpty(changelogHeaderPrefix) && !string.IsNullOrEmpty(versionPattern)){
            versionName = VersionUtil.GetLatestVersionFromChangelog(changelog, versionPattern: versionPattern);
        }

        if(string.IsNullOrEmpty(versionName) || versionName.Equals("0.0.0")){
            throw new Exception($"Unable to retrieve version number from changelog.{Environment.NewLine} Changelog path: {changelog}{Environment.NewLine} Changelog prefix: {changelogHeaderPrefix ?? "## Version"}{Environment.NewLine} Version pattern: {versionPattern ?? "[0-9]+\\.[0-9]+\\.[0-9]+"}");
        }
        return versionName;
    }

    /// <summary>
    /// Get changelog notes from latest version
    /// </summary>
    /// <param name="rootDirectory">Root directory of the project. (<see cref="BuildEnv.RootDir"/>)</param>
    /// <param name="changelogHeader">Full header of changelog</param>
    /// <returns>All text between latest header and the header before in changelog</returns>
    public static string GetChangelogNotes(string rootdirectory, string changelogHeader = null){
        return VersionUtil.GetReleaseNotesFromChangelog(GetChangelog(rootdirectory), changelogHeader, text => Regex.Replace(text, changelogHeader, string.Empty).Trim());
    }

    /// <summary>
    /// Get env-variable BUILD_BUILDID if running on buildserver
    /// </summary>
    /// <returns>buildId</returns>
    /// <exception cref="Exception"></exception>
    /// <remarks>Unable to find environment variable BUILD_BUILDID from Azure Pipelines</remarks>
    public static int GetBuildId(){
        if(!BuildServer.IsBuildServer){
            return 1;
        }

        return int.Parse(Environment.GetEnvironmentVariable("BUILD_BUILDID"));
    }

    /// <summary>
    /// Remove first instance of a substring in string
    /// </summary>
    /// <param name="inputText">Input text that contains a unwanted substring (e.g. a \n at the start)</param>
    /// <param name="subStringToRemove">The substring to be removed</param>
    /// <returns><see cref="inputText"/> with the first occurence of <see cref="subStringToRemove"/> removed</returns>
    public static string RemoveFirstInstanceOfSubstring(string inputText, string subStringToRemove){
        int index = inputText.IndexOf(subStringToRemove);
        if(index < -1){
            throw new Exception($"'{subStringToRemove}' is not found in the input text");
        }
        return (index < 0) ? inputText : inputText.Remove(index, subStringToRemove.Length);
    }

    private static string GetChangelog(string path){
        var changelogFile = Directory.GetFiles(path).FirstOrDefault(f => f.Contains("CHANGELOG.md"));
        if(string.IsNullOrEmpty(changelogFile)){
            throw new Exception($"No file named CHANGELOG.md found in path: {path}");
        }
        return changelogFile;
    }
}