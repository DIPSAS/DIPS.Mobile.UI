/// <summary>
/// Abstraction on top of both azure devops environment and rest api
/// </summary>
public static class AzureDevops
{
    /// <summary>
    /// Set to True if the script is being run by a build task.
    /// </summary>
    /// <remarks>This variable is agent-scoped, and can be used as an environment variable in a script and as a parameter in a build task, but not as part of the build number or as a version control tag.</remarks>
    public static bool IsBuildServer => GetEnvironmentVariable("TF_BUILD") != null;
    /// <summary>
    /// Use the OAuth token to access the REST API. 
    /// Use System.AccessToken from YAML scripts. 
    /// </summary>
    /// <remarks>This variable is agent-scoped, and can be used as an environment variable in a script and as a parameter in a build task, but not as part of the build number or as a version control tag.</remarks>
    public static string AccessToken => GetEnvironmentVariable("System.AccessToken");

    /// <summary>
    /// Get environment variable when running in azure devops
    /// </summary>
    /// <param name="environmentvariable"></param>
    /// <returns></returns>
    /// <remarks>List of predefined variables: https://learn.microsoft.com/en-us/azure/devops/pipelines/build/variables</remarks>
    public static string GetEnvironmentVariable(string environmentvariable)
    {
        var modifiedVariable = environmentvariable.ToUpper().Replace(".", "_");
        return Environment.GetEnvironmentVariable(modifiedVariable);
    }
}