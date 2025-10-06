using System;
using System.Linq;
using System.Xml.Linq;

public static class NugetHelper
{

    /// <summary>
    /// Get the current version of a package from Packages.props file.
    /// </summary>
    /// <param name="package">The package to look for</param>
    /// <returns>The current version pinned in Packages.props file.</returns>
    public static string GetCurrentVersionFromPackagesProps(string package, string packagesPropsFilePath)
    {
        // Check if the file exists
        if (!File.Exists(packagesPropsFilePath))
        {
            Console.WriteLine("File not found.");
            return string.Empty;
        }

        // Load the XML content from the file
        XDocument xdoc = XDocument.Load(packagesPropsFilePath);

        // Find the PackageVersion element with Include="DIPS.Mobile.Essentials"
        var versionElement = xdoc.Descendants("PackageVersion")
                                .FirstOrDefault(e => (string)e.Attribute("Include") == package);

        // Get the Version attribute value
        string version = versionElement?.Attribute("Version")?.Value;
        return version;
    }

    /// <summary>
    /// Get the current version of a package from Packages.props file.
    /// </summary>
    /// <param name="package">The package to look for</param>
    /// <returns>The current version pinned in Packages.props file.</returns>
    public static string GetCurrentVersionFromCsproj(string package, string csProjPath)
    {
        // Check if the file exists
        if (!File.Exists(csProjPath))
        {
            Console.WriteLine("File not found.");
            return string.Empty;
        }

        // Load the XML content from the file
        XDocument xdoc = XDocument.Load(csProjPath);

        // Find the PackageVersion element with Include="DIPS.Mobile.Essentials"
        var versionElement = xdoc.Descendants("PackageReference")
                                .FirstOrDefault(e => (string)e.Attribute("Include") == package);

        // Get the Version attribute value
        string version = versionElement?.Attribute("Version")?.Value;
        return version;
    }
}