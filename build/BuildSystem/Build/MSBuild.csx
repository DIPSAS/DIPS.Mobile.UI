#load "../Command.csx"
#load "../Helpers/FileHelper.csx"

public static class MSBuild
{
    public static Task Build(string projectFile, string configuration = "Release", string workingDirectory = null)
    {
        return Command.ExecuteAsync("msbuild", $"{projectFile} /p:Configuration=Release", workingDirectory);
    }
}