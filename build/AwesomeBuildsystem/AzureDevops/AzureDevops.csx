#r "nuget:Newtonsoft.Json, 13.0.3"
#load "../Logging/Logger.csx"

using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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
    /// The event that caused the build to run.
    /// </summary>
    /// <remarks>Returns null if this is not a build server</remarks>
    /// <remarks>Returns Unknown when not running on a build server or if the reason from the build environment is unknown</remarks>
    public static BuildReason BuildReason {
        get {
            if(IsBuildServer && Enum.TryParse<BuildReason>(GetEnvironmentVariable("Build.Reason"), true, out var result)){
                return result;
            }
            return BuildReason.Unknown;
        }
    }

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
    public static string GetEnvironmentVariable(string environmentvariable){
        var modifiedVariable = environmentvariable.ToUpper().Replace(".", "_");
        return Environment.GetEnvironmentVariable(modifiedVariable);
    }

    /// <summary>
    /// Create a thread in a pull request.
    /// </summary>
    /// <param name="comments">A list of the comments.</param>
    /// <param name="repositoryId">Repository ID of the pull request's target branch.</param>
    /// <param name="pullRequestId">ID of the pull request.</param>
    /// <param name="organization">The name of the Azure DevOps organization.</param>
    /// <param name="project">Project ID or project name</param>
    /// <param name="apiVersion">Version of the API to use. This should be set to '7.0' to use this version of the api.</param>
    /// <param name="personalaccesstoken">Personal Access Token (PAT) to use as authentication, default is the build serveres PAT.</param>
    /// <remarks>Based on: https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-threads/create?view=azure-devops-rest-7.0</remarks>
    public static async Task<HttpResponseMessage> PostPullRequestThread(PullRequestComment[] comments,  string repositoryId = null ,int pullRequestId = 0, string organization = "dips", string project = "DIPS", string apiVersion="7.0", string personalaccesstoken = null, bool postSameThreadMultipleTimes = false){
        if(repositoryId == null){
            repositoryId = GetEnvironmentVariable("Build.Repository.ID");
        }
        if(pullRequestId == 0){
            int.TryParse(GetEnvironmentVariable("System.PullRequest.PullRequestId"), out var id);
            pullRequestId = id;
        }
        if(personalaccesstoken == null){
            personalaccesstoken = GetEnvironmentVariable("System.AccessToken");
        }
        if(pullRequestId == 0 || repositoryId == null || personalaccesstoken == null){
            Logger.LogError($"Pullrequest id,repository id or personalaccesstoken is not set. PullRequestId: {pullRequestId}, Repository.ID: {repositoryId}, PersonalAccesstoken:{personalaccesstoken}.", true);
        }
        if(AzureDevops.BuildReason is not BuildReason.PullRequest){
            Logger.LogDebug($"Unable to post to pull request thread as the build is not started from a pull request");
            return new HttpResponseMessage();
        }
        //First check if the thread was already posted, no need to post again if we dont want to
        if(!postSameThreadMultipleTimes){
            var pullRequestThreads = await ListPullRequestThreads(repositoryId, pullRequestId, organization, project, apiVersion, personalaccesstoken);
            var threadsWithComments = new List<GitPullRequestCommentThread>();
            foreach (var comment in comments)
            {

                var threadWithSameComment = pullRequestThreads.FirstOrDefault(thread => thread.Comments.Any(c =>
                {
                    if(c.Content == null) return false;
                    return c.Content.Equals(comment.Content, StringComparison.InvariantCultureIgnoreCase);
                }));
                if(threadWithSameComment != null){
                    threadsWithComments.Add(threadWithSameComment);
                }
                
            }
            if(threadsWithComments.Any()){
                var numberOfThreadsWithCommentsThatAreTheSame = threadsWithComments.DistinctBy(t => t.Id).Count();
                if(numberOfThreadsWithCommentsThatAreTheSame == 1){
                    return new HttpResponseMessage(){};
                }
            }
        }

        var httpclient = new HttpClient();
        httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", personalaccesstoken))));
        var request = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/threads?api-version={apiVersion}";
        var json = JsonConvert.SerializeObject(new PullRequestCommentRequest(){Comments = comments});
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpclient.PostAsync(request, content);
        return response;
    }

    /// <summary>
    /// List all threads from a pull request
    /// </summary>
    /// <param name="repositoryId">Repository ID of the pull request's target branch.</param>
    /// <param name="pullRequestId">ID of the pull request.</param>
    /// <param name="organization">The name of the Azure DevOps organization.</param>
    /// <param name="project">Project ID or project name</param>
    /// <param name="apiVersion">Version of the API to use. This should be set to '7.0' to use this version of the api.</param>
    /// <param name="personalaccesstoken">Personal Access Token (PAT) to use as authentication, default is the build serveres PAT.</param>
    /// <returns></returns>
    /// <remarks>Based on: https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-threads/list?view=azure-devops-rest-7.0</remarks>
    public static async Task<List<GitPullRequestCommentThread>> ListPullRequestThreads(string repositoryId = null, int pullRequestId = 0, string organization = "dips", string project = "DIPS", string apiVersion="7.0", string personalaccesstoken = null){
        
        if(repositoryId == null){
            repositoryId = GetEnvironmentVariable("Build.Repository.ID");
        }
        if(pullRequestId == 0){
            int.TryParse(GetEnvironmentVariable("System.PullRequest.PullRequestId"), out var id);
            pullRequestId = id;
        }
        if(personalaccesstoken == null){
            personalaccesstoken = GetEnvironmentVariable("System.AccessToken");
        }

        if(pullRequestId == 0 || repositoryId == null || personalaccesstoken == null){
            Logger.LogError($"Pullrequest id,repository id or personalaccesstoken is not set. PullRequestId: {pullRequestId}, Repository.ID: {repositoryId}, PersonalAccesstoken:{personalaccesstoken}.", true);
        }

        if(pullRequestId == 0 && AzureDevops.BuildReason is not BuildReason.PullRequest){
            Logger.LogDebug($"Unable to list threads from pull request as the build is not started from a pull request");
            return new List<GitPullRequestCommentThread>();
        } 


        var httpclient = new HttpClient();
        httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", personalaccesstoken))));
        var request = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/threads?api-version={apiVersion}";
        var response = await httpclient.GetAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        var valueResponse = JsonConvert.DeserializeAnonymousType(json, new {value = new List<GitPullRequestCommentThread>()});
        return valueResponse.value;
    }

    /// <summary>
    /// Get all work items id's related to a pull request
    /// </summary>
    /// <param name="repositoryId">Repository ID of the pull request's target branch.</param>
    /// <param name="pullRequestId">ID of the pull request.</param>
    /// <param name="organization">The name of the Azure DevOps organization.</param>
    /// <param name="project">Project ID or project name</param>
    /// <param name="apiVersion">Version of the API to use. This should be set to '7.0' to use this version of the api.</param>
    /// <param name="personalaccesstoken">Personal Access Token (PAT) to use as authentication, default is the build serveres PAT.</param>
    /// <returns>List of workitem id's</returns>
    /// <remarks>Based on: https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-threads/list?view=azure-devops-rest-7.0</remarks>
    public static async Task<List<string>> GetWorkItemIdsRelatedToPullRequest(string repositoryId = null, int pullRequestId = 0, string organization = "dips", string project = "DIPS", string apiVersion="7.0", string personalAccessToken = null)
    {

        if(repositoryId == null){
            repositoryId = GetEnvironmentVariable("Build.Repository.ID");
        }
        if(pullRequestId == 0){
            int.TryParse(GetEnvironmentVariable("System.PullRequest.PullRequestId"), out var id);
            pullRequestId = id;
        }
        if(personalAccessToken == null){
            personalAccessToken = GetEnvironmentVariable("System.AccessToken");
        }

        if(pullRequestId == 0 || repositoryId == null || personalAccessToken == null){
            Logger.LogError($"Pullrequest id,repository id or personalaccesstoken is not set. PullRequestId: {pullRequestId}, Repository.ID: {repositoryId}, PersonalAccesstoken:{personalAccessToken}.", true);
        }

        if(pullRequestId == 0 && AzureDevops.BuildReason is not BuildReason.PullRequest){
            Logger.LogDebug($"Unable to work item id's as the build is not started from a pull request");
            return new List<string>();
        }

        var httpclient = new HttpClient();
        httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", personalAccessToken))));
        var request = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullRequests/{pullRequestId}/workitems?api-version={apiVersion}";
        var response = await httpclient.GetAsync(request);
        var json = await response.Content.ReadAsStringAsync();
        var dynamicObject = JsonConvert.DeserializeObject<dynamic>(json);

        List<string> workItemIds = new List<string>();
        foreach(var value in dynamicObject.value)
        {
            workItemIds.Add(value.id.Value);
        }
        return workItemIds;
    }   

    /// <summary>
    /// Get PR related to a commit
    /// </summary>
    /// <param name="commit">Commit ID.</param>
    /// <param name="repositoryId">Repository ID of the pull request's target branch.</param>
    /// <param name="organization">The name of the Azure DevOps organization.</param>
    /// <param name="project">Project ID or project name</param>
    /// <param name="apiVersion">Version of the API to use. This should be set to '7.0' to use this version of the api.</param>
    /// <param name="personalaccesstoken">Personal Access Token (PAT) to use as authentication, default is the build serveres PAT.</param>
    /// <returns>PR ID</returns>
    /// <remarks>Based on: https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-query/get?view=azure-devops-rest-7.0</remarks>
    public static async Task<int> GetRelatedPullRequestToCommit(string commit = null, string repositoryId = null, string organization = "dips", string project = "DIPS", string apiVersion="7.0", string personalAccessToken = null)
    {
        if(commit == null)
        {
            commit = GetEnvironmentVariable("Build.SourceVersion");
        }

        if(repositoryId == null){
            repositoryId = GetEnvironmentVariable("Build.Repository.ID");
        }
        if(personalAccessToken == null){
            personalAccessToken = GetEnvironmentVariable("System.AccessToken");
        }

        if(repositoryId == null || personalAccessToken == null){
            Logger.LogError($"Repository id or personalaccesstoken is not set. Repository.ID: {repositoryId}, PersonalAccesstoken:{personalAccessToken}.", true);
        }

        var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(
                    System.Text.ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", personalAccessToken))));

        var request = $"https://dev.azure.com/{organization}/{project}/_apis/git/repositories/{repositoryId}/pullrequestquery?api-version={apiVersion}";
        var query = new GitPullRequestQuery
        {
            QueryInput = new GitPullRequestQueryInput[]{
                new GitPullRequestQueryInput
                {
                    Commits = new string[]
                    {
                        commit
                    }
                }
            }
        };

        Logger.LogDebug($"Finding related PR to commit: {commit}");

        var serializedObject = JsonConvert.SerializeObject(query);

        var result = await httpClient.PostAsync(request, new StringContent(serializedObject, Encoding.UTF8, "application/json"));
        var json = await result.Content.ReadAsStringAsync();

        var dynamicObject = JsonConvert.DeserializeObject<dynamic>(json);
        
        var dynamicObjectFirstElementFromResults = dynamicObject["results"][0];

        var commitMetadataList = dynamicObjectFirstElementFromResults[commit];

        if(commitMetadataList == null)
        {
            Logger.LogDebug($"Could not find related PR to commit: {commit}");
            return -1;
        }

        var commitMetadata = commitMetadataList[0];
        var pullRequestId = (int)commitMetadata["pullRequestId"].Value;

        Logger.LogDebug($"Found related PR: {pullRequestId} to commit: {commit}");

        return pullRequestId;
    }

    public static async Task<GitPullRequestCommentThread> GetThreadWithCommentContent(string commentContent, string repositoryId = null, int pullRequestId = 0, string organization = "dips", string project = "DIPS", string apiVersion = "7.0", string personalAccessToken = null)
    {
        Logger.LogDebug($"Getting thread in PR with comment-content: {commentContent}.");

        var pullRequestThreads = await ListPullRequestThreads(repositoryId, pullRequestId, organization, project, apiVersion, personalAccessToken);
        
        foreach(var pullRequestThread in pullRequestThreads)
        {
            if(pullRequestThread.Comments == null)
                continue;

            var targetComment = pullRequestThread.Comments.FirstOrDefault(comment => !string.IsNullOrEmpty(comment.Content) && comment.Content.Contains(commentContent));
            if(targetComment != null)
            {
                return pullRequestThread;
            }
        }

        return null;
    }

    //https://learn.microsoft.com/en-us/azure/devops/pipelines/scripts/logging-commands?view=azure-devops&tabs=bash#uploadsummary-add-some-markdown-content-to-the-build-summary
    public static void UploadSummary(string markdownFilePath)
    {
        WriteLine($"##vso[task.uploadsummary]{markdownFilePath}");
    }

    //https://learn.microsoft.com/en-gb/azure/devops/pipelines/scripts/logging-commands?view=azure-devops&tabs=bash#upload-upload-an-artifact
    public static void UploadArtifact(string artifactName, string containerFolder,string filePath)
    {
        WriteLine($"##vso[artifact.upload containerfolder={containerFolder};artifactname={artifactName}]{filePath}");
    }
}

// Based on: https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-query/get?view=azure-devops-rest-7.0
public class GitPullRequestQuery
{
    [JsonProperty("queries")]
    public GitPullRequestQueryInput[] QueryInput { get; set; }
}

// Based on: https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-query/get?view=azure-devops-rest-7.0
public class GitPullRequestQueryInput
{
    [JsonProperty("items")]
    public string[] Commits { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = "lastMergeCommit";
}

public enum BuildReason{
    /// <summary>
    /// A user manually queued the build.
    /// </summary>
    Manual,
    /// <summary>
    /// Continuous integration (CI) triggered by a Git push or a TFVC check-in.
    /// </summary>
    IndividualCI,
    /// <summary>
    /// Continuous integration (CI) triggered by a Git push or a TFVC check-in, and the Batch changes was selected.
    /// </summary>
    BatchedCI,
    /// <summary>
    /// Scheduled trigger.
    /// </summary>
    Schedule,
    /// <summary>
    /// A user manually queued the build of a specific TFVC shelveset.
    /// </summary>
    ValidateShelveset,
    /// <summary>
    /// Gated check-in trigger.
    /// </summary>
    CheckInShelveset,
    /// <summary>
    /// The build was triggered by a Git branch policy that requires a build.
    /// </summary>
    PullRequest,
    /// <summary>
    /// The build was triggered by another build
    /// </summary>
    BuildCompletion,
    /// <summary>
    /// The build was triggered by a resource trigger or it was triggered by another build.
    /// </summary>
    ResourceTrigger,
    Unknown,
}

/// <summary>
/// Represents a comment in a thread in a pull request
/// </summary>
/// <remarks>Based on : https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-threads/create?view=azure-devops-rest-7.0&tabs=HTTP#comment</remarks>
public class PullRequestComment{
    public string Content { get; set; }
    /// <summary>
    /// https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-threads/create?view=azure-devops-rest-7.0&tabs=HTTP#commenttype
    /// </summary>
    public string CommentType { get; set; } = "text";
    /// <summary>
    /// https://learn.microsoft.com/en-us/rest/api/azure/devops/git/pull-request-threads/create?view=azure-devops-rest-7.0&tabs=HTTP#commentthreadstatus
    /// </summary>
    public int ParentCommentId { get; set; } = 0;

    public IdentityRef[] UsersLiked { get; set; }
    
}

public class PullRequestCommentRequest{
    public PullRequestComment[] Comments { get; set; }
    public string Status { get; set; } = "active";
}

public class GitPullRequestCommentThread{
    public int Id { get; set; }
    public List<PullRequestComment> Comments { get; set; }
}

public class IdentityRef{
    string DisplayName { get; set; }
}
