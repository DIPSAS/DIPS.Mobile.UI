#r "../DIPS.BuildSystem.Core.dll"
#r "nuget:Newtonsoft.Json, 13.0.3"
#r "nuget:NuGet.Versioning, 6.8.0"

#load "../Utils.csx"
#load "../Command.csx"
#load "../Logging/Logger.csx"
#load "../Mobile.Essentials/MobileEssentials.csx"
#load "Git.csx"
#load "AppSettings.csx"

using System.Text.RegularExpressions;
using DIPS.Buildsystem.Core.Tools;
using NuGet.Versioning;
using Newtonsoft.Json;

public static class VersionUtils
{
    /// <summary>
    ///     Compares the version from <see cref="versionFilePath"/> with a potential release branch. If the target branch is not a release branch, it will not validate.
    /// </summary>
    /// <param name="versionFilePath">The file path of the product version text file. This should ONLY contain a version number with no additional formatting.</param>
    /// <remarks>
    ///     Only branches with the format "release/[version]" will be validated. If the branch begins with "release/", but does not contain a version number, the method will fail.
    /// </remarks>
    public static bool ValidateProductVersion(string versionFilePath, string targetBranch)
    {
        var version = GetProductVersion(versionFilePath);

        Logger.LogDebug($"{versionFilePath} -> {version}");

        var branchName = GetBranchName(targetBranch);

        var releaseBranchVersion = GetReleaseBranchVersion(branchName);
        if (releaseBranchVersion is null)
        {
            // Is not a release branch, won't need to compare version
            return true;
        }

        Logger.LogDebug($"{branchName} -> {releaseBranchVersion}");

        return version.Equals(releaseBranchVersion);
    }

    public static Version GetMajorAndMinorAppVersionFromProductVersion(string versionFilePath)
    {
        var productVersion = GetProductVersion(versionFilePath);
        var major = 0;
        if (productVersion.Major > -1)
        {
            major = productVersion.Major;
        }
        var minorString = "";
        if (productVersion.Minor > -1)
        {
            minorString += $"{productVersion.Minor}";

            if (productVersion.Build > -1)
            {
                minorString += $"{productVersion.Build}";
            }
            else
            {
                minorString += $"0";
            }

            if (productVersion.Revision > -1)
            {
                minorString += $"{productVersion.Revision}";
            }
            else
            {
                minorString += $"0";
            }
        }
        int.TryParse(minorString, out var minor);
        return new Version(major, minor);
    }

/// <summary>
///     Extracts the version from <see cref="productVersionFilePath"/> and injects it into the appsettings.json file contained within <see cref="androidPath"/> and <see cref="iOSPath"/>.
///     appsettings.json MUST exist, with the top-level property of "ProductVersion" defined.
/// </summary>
/// <param name="productVersionFilePath">The file path of the product version text file. This should ONLY contain a version number with no additional formatting.</param>
/// <param name="appSettingsPath">The file path where appsettings.json resides.</param>
public static async Task CopyProductVersionToAppSettings(string productVersionFilePath, string appSettingsPath)
    {
        var version = GetProductVersion(productVersionFilePath);

        Logger.LogDebug($"Writing product version ({version}) to appsettings.json...");
        await MobileEssentials.ReplaceProductVersion(appSettingsPath, version.ToString());
        Logger.LogDebug($"Product version copied to appsettings.json!");
    }

    /// <summary>
    ///     Retrieves the current product version from the file located at <see cref="productVersionFilePath"/>.
    /// </summary>
    /// <param name="versionFilePath">The file path of the product version text file. This should ONLY contain a version number with no additional formatting.</param>
    public static Version GetProductVersion(string productVersionFilePath)
    {
        var fileContent = File.ReadAllLines(productVersionFilePath).First();
        if (!Version.TryParse(fileContent, out var version))
        {
            throw new Exception($"File '{productVersionFilePath}' was not in a correct format!");
        }

        var build = version.Build > -1 ? version.Build : 0;

        return new Version(version.Major, version.Minor, build);
    }    

    public static async Task<List<(DateTime, List<string>)>> GetChangesFromChangelogWithDates(string changeLogPath, string headerPrefix)
    {
        var lines = await File.ReadAllLinesAsync(changeLogPath);
        List<(DateTime, List<string>)> results = new List<(DateTime, List<string>)>();
        DateTime? currentDate = null;
        List<string> currentChanges = new List<string>();
        Regex dateRegex = new Regex(@"\d{4}-\d{2}-\d{2}");

        foreach (var line in lines)
        {
            if (line.StartsWith(headerPrefix))
            {
                // If not the first date, add the collected date and changes to the list
                if (currentDate is DateTime time)
                {
                    results.Add((time, new List<string>(currentChanges)));
                    currentChanges.Clear();
                }

                Match match = dateRegex.Match(line);
                if (match.Success)
                {
                    if(DateTime.TryParse(match.Value, out var parsedDate))
                    {
                        currentDate = parsedDate;
                    }
                    
                }
            }
            else if (line.StartsWith("- "))
            {
                currentChanges.Add(line);
            }
        }

        // Add the last collected items
        if (currentDate is DateTime dateTime)
        {
            results.Add((dateTime, currentChanges));
        }

        return results;
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

    private static Version GetReleaseBranchVersion(string branchName)
    {
        if (!branchName.StartsWith("origin/release/"))
        {
            return null;
        }
        var versionString = branchName.Replace("origin/release/", "");

        if (!Version.TryParse(versionString, out var version))
        {
            throw new Exception($"Branch '{branchName}' was a release branch, but could not parse version.");
        }

        var build = version.Build > -1 ? version.Build : 0;

        return new Version(version.Major, version.Minor, build);;
    }
}