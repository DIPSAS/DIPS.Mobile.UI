#load "../Command.csx"
#r "../DIPS.Buildsystem.Core.dll"

using DIPS.Buildsystem.Core.Integrations;

public static class Git{

    public static async Task<string> GetCurrentBranch(string workingDirectory = null, bool isPrBranch = false)
    {
        if (BuildServer.IsBuildServer) //When building on azure it checks out a commit based on a branch, so the branch name has to be collected from a environment variable
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
        var currentBranch = await GetCurrentBranch(workingDirectory);
        return currentBranch.Equals("main", StringComparison.OrdinalIgnoreCase);
    }

    public static async Task<bool> IsPatchBranch(string workingDirectory = null)
    {
        var currentBranch = await GetCurrentBranch(workingDirectory);
        return currentBranch.StartsWith("patch", StringComparison.OrdinalIgnoreCase);
    }

    public static async Task<bool> ShouldAddPreSuffix(string workingDirectory = null)
    {
        return !await CurrentBranchIsMain(workingDirectory) && !await IsPatchBranch(workingDirectory);
    }

    public static async Task<string> GetCurrentGitCommit()
    {
        var result = await Command.CaptureAsync("git", "rev-parse --verify HEAD");
        return result.StandardOut.Replace("\n", "");
    }

    public static async Task TagAndPush(string tag, string message,string workingDirectory = null)
    {
        await ExecuteGitCommand($"tag -af {tag} -m \"{message}\"", workingDirectory);
        await ExecuteGitCommand($"push -f origin {tag}", workingDirectory);
    }

    public static Task ResetFile(string filePath)
    {
        return Command.ExecuteAsync("git", $"checkout -- {filePath}");
    }

    private static Task<CommandResult> ExecuteGitCommand(string arguments, string workingDirectory){
        return Command.CaptureAsync("git", arguments, workingDirectory);
    }

}