using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public static class VersionUtil
    {
        private const string DefaultChangelogFilename = "CHANGELOG.md";
        public const string HeaderPrefix = "## [";
        public const string VersionPattern = "[0-9]+.[0-9]+.[0-9]+";
        public const string HeaderPattern = @"##\s+[0-9]+\.[0-9]+\.[0-9]+";

        /// <summary>
        /// Gets the release notes for the latest version based on the changelog.
        /// Extracts all text between the header for the last version and the header for the previous version.
        /// </summary>
        /// <param name="changelogPath">Path to the changelog. Default is "BuildEnv.RootDir\CHANGELOG.md".</param>
        /// <param name="headerPattern">The regex pattern for the headers. Default "### Version [0-9]+\.[0-9]+\.[0-9]+".</param>
        /// <param name="modifyLogLine">Function to modify every logline before it is added</param>
        public static string GetReleaseNotesFromChangelog(string changelogPath, string headerPattern = null, Func<string, string> modifyLogLine = null)
        {
            headerPattern = headerPattern ?? HeaderPattern;
            var modifier = modifyLogLine ?? (s => s);
            var changeLogLines = File.ReadAllLines(changelogPath);
            var headerRegex = new Regex(headerPattern);

            var latestVersion = GetFromChangelog(changelogPath, headerPattern);

            var releaseNotes = new List<string>();
            var blockFound = false;
            foreach (var changeLogLine in changeLogLines)
            {
                if (blockFound && headerRegex.Match(changeLogLine).Success)
                {
                    break;
                }

                if (changeLogLine.Contains(latestVersion))
                {
                    blockFound = true;
                }

                if (blockFound)
                {
                    var line = modifier(changeLogLine);
                    if (line != null)
                    {
                        releaseNotes.Add(line);
                    }
                }
            }

            return string.Join(Environment.NewLine, releaseNotes);
        }

        /// <summary>
        /// Gets the latest version from the changelog based on a header pattern.
        /// </summary>
        /// <param name="changelogPath">Path to the changelog. Default is "BuildEnv.RootDir\CHANGELOG.md".</param>
        /// <param name="headerPrefix">Prefix for the version header. Default "### Version"</param>
        /// <param name="versionPattern">The regex pattern for the header containing the version. Default "[0-9]+\.[0-9]+\.[0-9]+".</param>
        public static string GetLatestVersionFromChangelog(
            string changelogPath,
            string headerPrefix = HeaderPrefix,
            string versionPattern = VersionPattern)
        {
            var changeLogContent = File.ReadAllText(changelogPath);

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

        /// <summary>
        /// Gets the latest version from the changelog based on a header pattern.
        /// </summary>
        /// <param name="changelogPath">Path to the changelog. Default is "BuildEnv.RootDir\CHANGELOG.md".</param>
        /// <param name="versionPattern">The regex pattern for the header containing the version. Default "### Version [0-9]+\.[0-9]+\.[0-9]+".</param>
        public static string GetFromChangelog(string changelogPath = null, string versionPattern = null)
        {
            versionPattern = versionPattern ?? HeaderPattern;
            var changeLogContent = File.ReadAllText(changelogPath);
            var regex = new Regex(versionPattern);
            var matches = regex.Matches(changeLogContent);

            var versionRegex = new Regex(VersionPattern);

            string version = "0.0.0";
            foreach (Match match in matches)
            {
                var matchVersion = versionRegex.Match(match.Value).Value;
                version = GetHighestVersion(version, matchVersion);
            }

            return version;
        }

        /// <summary>
        /// Gets the first match from the changelog based on a pattern.
        /// </summary>
        /// <param name="changelogPath">Path to the changelog. Default is "BuildEnv.RootDir\CHANGELOG.md".</param>
        /// <param name="pattern">The regex pattern for the header containing the version. Default "### Version [0-9]+\.[0-9]+\.[0-9]+".</param>
        public static string GetFirstMatchFromPattern(string changelogPath = null, string pattern = null)
        {
            pattern = pattern ?? HeaderPattern;
            var changeLogContent = File.ReadAllText(changelogPath);
            var regex = new Regex(pattern);
            var match = regex.Match(changeLogContent);

            var versionRegex = new Regex(VersionPattern);
            var version = versionRegex.Match(match.Value).Value;

            return version;
        }

        /// <summary>
        /// Returns the highest version of the two version specified.
        /// </summary>
        /// <param name="version1String">The first version string.</param>
        /// <param name="version2String">The second version string.</param>
        /// <returns>The highest version.</returns>
        public static string GetHighestVersion(string version1String, string version2String)
        {
            if (string.IsNullOrWhiteSpace(version1String))
            {
                return version2String;
            }

            if (string.IsNullOrWhiteSpace(version2String))
            {
                return version1String;
            }

            var version1 = new Version(version1String);
            var version2 = new Version(version2String);

            var result = version1.CompareTo(version2);
            return result >= 0 ? version1String : version2String;
        }

        public static Version GetFromTopOfChangelog(string changelogPath)
        {
            var flexiblePattern = @"[#]{2,3}.*?(\d+\.\d+\.\d+).*?(\d+[-/]\d+[-/]\d+)";
            //                                 <-- group 1 -->   <---- group 2 ---->
            //                              .*? : lazy / non-greedy match
            var changeLogContent = File.ReadAllText(changelogPath);
            var regex = new Regex(flexiblePattern);
            var matches = regex.Matches(changeLogContent);
            var version = new Version("0.0.0");
            if (matches.Count > 0)
            {
                version = new Version(matches[0].Groups[1].Value);
            }
            return version;
        }
    }