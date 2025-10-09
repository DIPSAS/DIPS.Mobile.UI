#load "../Command.csx"

#r "nuget:Newtonsoft.Json, 13.0.3"

using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

public static class AzureCLI{
    
    public static async Task<bool> Login()
    {
        try{

            await Command.ExecuteAsync($"az", "login --allow-no-subscriptions");
        
        }
        catch(Exception e)
        {
            Console.WriteLine($"Was not able to login: {e.Message}");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Configure Azure DevOps defaults when running on build server with external source control
    /// </summary>
    private static async Task ConfigureAzureDevOpsDefaults()
    {
        var organization = Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI");
        var project = Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT");
        
        if (!string.IsNullOrEmpty(organization))
        {
            Console.WriteLine($"Configuring Azure DevOps organization: {organization}");
            await Command.ExecuteAsync("az", $"devops configure --defaults organization={organization}");
        }
        
        if (!string.IsNullOrEmpty(project))
        {
            Console.WriteLine($"Configuring Azure DevOps project: {project}");
            await Command.ExecuteAsync("az", $"devops configure --defaults project={project}");
        }
    }


    /// <summary>
    /// Get a variable from a variable group
    /// </summary>
    /// <param name="variableGroupId">This has to be collected from running az pipelines variable-group list</param>
    /// <param name="variableNames">The variables to collect</param>
    /// <returns></returns>
    public static async Task<Dictionary<string, string>> GetVariable(int variableGroupId, params string[] variableNames)
    {
        var variableDict = new Dictionary<string, string>();

        // Configure Azure DevOps defaults if running on build server with GitHub source
        if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_BUILDID")))
        {
            await ConfigureAzureDevOpsDefaults();
        }

        CommandResult result = null;

        try{

            result = await Command.CaptureAsync("az", $"pipelines variable-group show --group-id {variableGroupId}");
        
        }
        catch(Exception e){
            Console.WriteLine($"Was not able to retrieve variable group: {e.Message}");

            return variableDict;
        }

        if(!result.StandardError.Equals(string.Empty))
        {
            if(string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_BUILDID")))
            {
                Console.WriteLine("You are not logged in, logging in and trying again...");

                await Login();
                return await GetVariable(variableGroupId, variableNames);
            }
            else{
                Console.Error.WriteLine(result.StandardError);

                return variableDict;
            }
        }

        try{
            var jsonObject = JObject.Parse(result.StandardOut);
            var variablesJToken = jsonObject["variables"];
            
            foreach(var variableName in variableNames)
            {
                var variableJToken = variablesJToken[variableName];

                var jTokenValue = (string)variableJToken["value"];

                variableDict.Add(variableName, jTokenValue);
            }

        }
        catch(Exception e)
        {
            Console.Error.Write($"Could not parse JSON for variable-group: {e.Message}");
        }
       
        return variableDict;
    }


    /// <summary>
    /// Downloads artifacts from a azure build id
    /// </summary>
    /// <param name="buildId">ID of the run that the artifact is associated to.</param>
    /// <param name="path">Path to download the artifact into.</param>
    /// <param name="organization">Azure DevOps organization URL. You can configure the default organization using az devops configure -d organization=ORG_URL. Required if not configured as default or picked up via git config. Example: https://dev.azure.com/MyOrganizationName/ </param>
    /// <param name="project">Name or ID of the project. You can configure the default project using az devops configure -d project=NAME_OR_ID. Required if not configured as default or picked up via git config. </param>
    /// <returns></returns>
    public static async Task DownloadArtifacts(int buildId, string path, string organization = "https://dev.azure.com/dips/", string project = "DIPS")
    {
        var artifactsToDownload = await ListArtifacts(buildId, organization, project);

        for (int i = 0; i < artifactsToDownload.Count; i++)
        {
            await Command.ExecuteAsync("az", $"pipelines runs artifact download --artifact-name {artifactsToDownload[i]} --path {path} --run-id {buildId} --org {organization} --project {project}");    
        }
    }

    /// <summary>
    /// List artifacts from an azure build id
    /// </summary>
    /// <param name="buildId">ID of the run that the artifact is associated to</param>
    /// <param name="organization">Azure DevOps organization URL. You can configure the default organization using az devops configure -d organization=ORG_URL. Required if not configured as default or picked up via git config. Example: https://dev.azure.com/MyOrganizationName/ </param>
    /// <param name="project">Name or ID of the project. You can configure the default project using az devops configure -d project=NAME_OR_ID. Required if not configured as default or picked up via git config. </param>
    /// <returns>The artifact names</returns>
    public static async Task<List<string>> ListArtifacts(int buildId, string organization = "https://dev.azure.com/dips/", string project = "DIPS")
    {
        var listArtifactsResult = await Command.CaptureAsync("az", $"pipelines runs artifact list --run-id {buildId} --org {organization} --project {project}");

        var definition = new[] { new { name = "" } };
        var artifacts = JsonConvert.DeserializeAnonymousType(listArtifactsResult.StandardOut, definition);
        return artifacts.Select(a => a.name).ToList();
    }

    public static Task QueueBuild(int definitionId, string branch, string variables="")
    {
        return Command.ExecuteAsync("az", $"pipelines build queue --definition-id {definitionId} --branch {branch} --variables {variables}");

    }

}