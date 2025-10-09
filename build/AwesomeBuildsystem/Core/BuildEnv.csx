/// <summary>
/// Provides build environment paths and configuration
/// </summary>
public static class BuildEnv 
{
    /// <summary>
    /// Gets the root directory of the project (repository root)
    /// - Azure DevOps: Uses BUILD_SOURCESDIRECTORY
    /// - Local: Navigates up from build directory to project root
    /// </summary>
    public static string RootDir => Environment.GetEnvironmentVariable("BUILD_SOURCESDIRECTORY") ?? 
                                  Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), ".."));
    
    /// <summary>
    /// Gets the source directory path (where .csproj files are located)
    /// </summary>
    public static string SrcDir => Path.Combine(RootDir, "src");
    
    /// <summary>
    /// Gets the output directory for build artifacts
    /// </summary>
    public static string OutputDir => Path.Combine(RootDir, "output");
}