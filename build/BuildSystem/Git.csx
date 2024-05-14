#load "Command.csx"
#load "AzureDevops.csx"

public static class Git{

    public static async Task<string> GetCurrentBranch(string workingDirectory = null, bool isPrBranch = false)
    {
        if (AzureDevops.IsBuildServer) //When building on azure it checks out a commit based on a branch, so the branch name has to be collected from a environment variable
        {
            var branch = (isPrBranch) ? Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_SOURCEBRANCH") : Environment.GetEnvironmentVariable("BUILD_SOURCEBRANCH");
            return branch.Replace("refs/heads/", "");
        }

        var commandResult = await ExecuteGitCommand("rev-parse --abbrev-ref HEAD", workingDirectory: workingDirectory);
        var currentBranch = commandResult.StandardOut.Replace("\n", "");
        return currentBranch.Trim();
    }


    public static async Task FetchAll(string workingDirectory = null)
    {
        await ExecuteGitCommand("fetch --all", workingDirectory);
    }

    public static async Task<bool> CurrentBranchIsMaster(string workingDirectory = null)
    {
        return await GetCurrentBranch(workingDirectory) == "master";
    }

    public static async Task<bool> CurrentBranchIsMain(string workingDirectory = null)
    {
        return await GetCurrentBranch(workingDirectory) == "main";
    }

    public static async Task<bool> CurrentBranchIsPatch(string workingDirectory = null)
    {
        return await GetCurrentBranch(workingDirectory).StartsWith("patch");
    }


    private static Task<CommandResult> ExecuteGitCommand(string arguments, string workingDirectory){
        return Command.CaptureAsync("git", arguments, workingDirectory);
    }

}