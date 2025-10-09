/// <summary>
/// Provides build environment paths and configuration
/// </summary>
public static class BuildEnv 
{
    /// <summary>
    /// Gets the root directory of the project
    /// </summary>
    public static string RootDir => Directory.GetCurrentDirectory();
    
    /// <summary>
    /// Gets the source directory path
    /// </summary>
    public static string SrcDir => Path.Combine(RootDir, "src");
    
    /// <summary>
    /// Gets the output directory for build artifacts
    /// </summary>
    public static string OutputDir => Environment.GetEnvironmentVariable("BUILD_ARTIFACTSTAGINGDIRECTORY") ?? Path.Combine(Directory.GetCurrentDirectory(), "output");
    
    /// <summary>
    /// Gets the sources directory from build environment
    /// </summary>
    public static string SourcesDirectory => Environment.GetEnvironmentVariable("BUILD_SOURCESDIRECTORY") ?? Directory.GetCurrentDirectory();
}