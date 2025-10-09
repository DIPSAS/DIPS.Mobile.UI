/// <summary>
/// Provides build server detection and environment utilities
/// </summary>
public static class BuildServer 
{
    /// <summary>
    /// Gets a value indicating whether the current environment is a build server
    /// </summary>
    public static bool IsBuildServer => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_BUILDID"));
    
    /// <summary>
    /// Gets the build ID from the build server environment
    /// </summary>
    public static string BuildId => Environment.GetEnvironmentVariable("BUILD_BUILDID") ?? "1";
    
    /// <summary>
    /// Gets the source branch name from the build server environment
    /// </summary>
    public static string SourceBranch => Environment.GetEnvironmentVariable("BUILD_SOURCEBRANCH");
    
    /// <summary>
    /// Gets the pull request source branch name from the build server environment
    /// </summary>
    public static string PullRequestSourceBranch => Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_SOURCEBRANCH");
}