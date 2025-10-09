using System.Text.RegularExpressions;

/// <summary>
/// Utilities for working with version numbers and changelogs
/// </summary>
public static class VersionUtil 
{
    /// <summary>
    /// Gets the latest version number from a changelog file
    /// </summary>
    /// <param name="changelogPath">Path to the changelog file</param>
    /// <param name="headerPrefix">Header prefix pattern (default: "## Version")</param>
    /// <param name="versionPattern">Version regex pattern (default: semantic version)</param>
    /// <returns>The latest version string found, or "0.0.0" if none found</returns>
    public static string GetLatestVersionFromChangelog(string changelogPath, string headerPrefix = "## Version", string versionPattern = @"[0-9]+\.[0-9]+\.[0-9]+")
    {
        if (!File.Exists(changelogPath))
        {
            throw new FileNotFoundException($"Changelog file not found: {changelogPath}");
        }
        
        var content = File.ReadAllText(changelogPath);
        return GetLatestVersionFromChangelogContent(content, headerPrefix, versionPattern);
    }
    
    /// <summary>
    /// Gets the latest version number from changelog content
    /// </summary>
    /// <param name="content">The changelog content as string</param>
    /// <param name="headerPrefix">Header prefix pattern</param>
    /// <param name="versionPattern">Version regex pattern</param>
    /// <returns>The latest version string found, or "0.0.0" if none found</returns>
    public static string GetLatestVersionFromChangelogContent(string content, string headerPrefix = "## Version", string versionPattern = @"[0-9]+\.[0-9]+\.[0-9]+")
    {
        var headerRegex = new Regex(headerPrefix + versionPattern);
        var versionRegex = new Regex(versionPattern);
        var matches = headerRegex.Matches(content);

        string version = "0.0.0";
        foreach (Match match in matches)
        {
            var matchVersion = versionRegex.Match(match.Value).Value;
            version = GetHighestVersion(version, matchVersion);
        }

        return version;
    }
    
    /// <summary>
    /// Gets release notes from a changelog file for the latest version
    /// </summary>
    /// <param name="changelogPath">Path to the changelog file</param>
    /// <param name="changelogHeader">Full header text to match (optional)</param>
    /// <param name="transform">Optional transformation function for the notes</param>
    /// <returns>Release notes text</returns>
    public static string GetReleaseNotesFromChangelog(string changelogPath, string changelogHeader = null, Func<string, string> transform = null)
    {
        if (!File.Exists(changelogPath))
        {
            throw new FileNotFoundException($"Changelog file not found: {changelogPath}");
        }
        
        var content = File.ReadAllText(changelogPath);
        var lines = content.Split('\n');
        
        bool foundHeader = false;
        var notes = new List<string>();
        
        foreach (var line in lines)
        {
            if (!foundHeader && (changelogHeader == null || line.Contains(changelogHeader ?? "## Version")))
            {
                foundHeader = true;
                continue;
            }
            
            if (foundHeader)
            {
                if (line.StartsWith("## "))
                {
                    break; // Next version header found
                }
                notes.Add(line);
            }
        }
        
        var result = string.Join("\n", notes).Trim();
        return transform?.Invoke(result) ?? result;
    }
    
    /// <summary>
    /// Compares two version strings and returns the higher one
    /// </summary>
    /// <param name="version1String">First version string</param>
    /// <param name="version2String">Second version string</param>
    /// <returns>The higher version string</returns>
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

        try
        {
            var version1 = new Version(version1String);
            var version2 = new Version(version2String);
            
            var result = version1.CompareTo(version2);
            return result >= 0 ? version1String : version2String;
        }
        catch
        {
            // Fallback to string comparison if version parsing fails
            return string.Compare(version1String, version2String, StringComparison.OrdinalIgnoreCase) >= 0 ? version1String : version2String;
        }
    }
}