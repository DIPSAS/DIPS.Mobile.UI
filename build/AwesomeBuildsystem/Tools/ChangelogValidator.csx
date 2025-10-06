#r "../DIPS.Buildsystem.Core.dll"
#r "nuget:NuGet.Versioning, 6.8.0"

#load "../Utils.csx"
#load "../Logging/Logger.csx"
#load "VersionUtils.csx"
#load "../Command.csx"
#load "Git.csx"


using System.Text.RegularExpressions;
using DIPS.Buildsystem.Core.Tools;
using NuGet.Versioning;

public static class ChangelogValidator
{
    /// <summary>
    ///     Validates the changelog, comparing it to the target branch's changelog to check if it was bumped. It will also check against the rules of the product version.
    ///     Currently has a dependency with Azure devops pipeline, as retrieving the target branch is done by using
    ///     an environment variable (SYSTEM_PULLREQUEST_TARGETBRANCH) set by the pipeline. 
    /// </summary>
    /// <remarks>The variable 'SYSTEM_PULLREQUEST_TARGETBRANCH' is initialized only if the build ran because of a Git PR affected by a branch policy in Azure Devops. </remarks>
    /// <param name="changelogPath">Where the CHANGELOG.md is located</param>
    /// <param name="headerPrefix">See <see cref="DIPS.Buildsystem.Core.Versioning.VersionUtil.GetLatestVersionFromChangelog(string, string, string)"/></param>
    /// <param name="versionPattern">See <see cref="DIPS.Buildsystem.Core.Versioning.VersionUtil.GetLatestVersionFromChangelog(string, string, string)"/></param>
    /// <returns>True if the changelog is valid and has been bumped</returns>
    public static async Task<bool> ValidateChangelog(string changelogPath, string productVersionFilePath, string headerPrefix, string versionPattern)
    {
        var currentLatestChangelogVersionNumber = Utils.GetChangelogVersion(changelogPath, headerPrefix, versionPattern);

        //Check if changelog version matches rules of major and minor from product version
        var expectedMajorAndMinorAppVersion = VersionUtils.GetMajorAndMinorAppVersionFromProductVersion(productVersionFilePath);
        if(Version.TryParse(currentLatestChangelogVersionNumber, out var currentChangelogVersion)){
            if(currentChangelogVersion.Major != expectedMajorAndMinorAppVersion.Major){
                Logger.LogError($"Changelog major version number is different than product version from: {productVersionFilePath}", false);
                return false;
            }
            if(currentChangelogVersion.Minor != expectedMajorAndMinorAppVersion.Minor){
                Logger.LogError($"Changelog minor version number is different than product version from: {productVersionFilePath}", false);
                return false;
            }
        }

        //Compare current version against latest version
        await Git.FetchAll();

        var targetBranch = Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_TARGETBRANCH");
        var branchName = GetBranchName(targetBranch); 

        var targetChangelogContent = await Command.CaptureAsync("git", $"show {branchName}:CHANGELOG.md");
        var targetLatestChangelogVersionNumber = GetLatestVersionFromChangelogContent(targetChangelogContent.StandardOut, headerPrefix, versionPattern);
        
        var targetLatestChangelogVersion = Version.Parse(targetLatestChangelogVersionNumber);
        var currentLatestChangelogVersion = Version.Parse(currentLatestChangelogVersionNumber);
        Logger.LogDebug($"Current changelog version: {currentLatestChangelogVersion}, {branchName} version: {targetLatestChangelogVersion}");
        return currentLatestChangelogVersion > targetLatestChangelogVersion;
    }

    private static string GetBranchName(string targetBranch)
    {
        var branchName = targetBranch.Replace("refs/heads/", "");

        if(!branchName.Contains("origin"))
        {
            branchName = $"origin/{branchName}";
        }

        return branchName;
    }

    private static async Task RenameChangelog(string path, string newPath)
    {
        await Command.ExecuteAsync("mv", $"{path} {newPath}");
    }

    private static async Task RemoveFile(string path)
    {
        await Command.ExecuteAsync("rm", $"-rf {path}");
    }

    private static async Task GetChangelogFromBranch(string changeLogPath, string targetBranch)
    {
        await Command.ExecuteAsync("git", $"restore --source origin/{targetBranch} -- {changeLogPath}/CHANGELOG.md");
    }

    /// Taken from: https://dev.azure.com/dips/DIPS/_git/BuildSystem-Compiled?path=/src/DIPS.Buildsystem.Core/Versioning/VersionUtil.cs
    /// Changed from passing a path to a file to the content of a file
    private static string GetLatestVersionFromChangelogContent(
            string content,
            string headerPrefix,
            string versionPattern)
    {
        var changeLogContent = content;

        var headerRegex = new Regex(headerPrefix + versionPattern);
        var versionRegex = new Regex(versionPattern);
        var matches = headerRegex.Matches(changeLogContent);

        string version = "0.0.0";
        foreach (Match match in matches)
        {
            var matchVersion = versionRegex.Match(match.Value).Value;
            version = GetHighestVersion(version, matchVersion);
        }

        return version;
    }

    /// Taken from: https://dev.azure.com/dips/DIPS/_git/BuildSystem-Compiled?path=/src/DIPS.Buildsystem.Core/Versioning/VersionUtil.cs
    private static string GetHighestVersion(string version1String, string version2String)
    {
        if (string.IsNullOrWhiteSpace(version1String))
        {
            return version2String;
        }

        if (string.IsNullOrWhiteSpace(version2String))
        {
            return version1String;
        }

        var version1 = new NuGetVersion(version1String);
        var version2 = new NuGetVersion(version2String);

        var result = version1.CompareTo(version2);
        return result >= 0 ? version1String : version2String;
    }
}