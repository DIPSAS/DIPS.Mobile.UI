#load "../Command.csx"


public static class dotnet
{
    public static async Task<CommandResult> Restore(string path)
    {
        var result = await Command.CaptureAsync("dotnet", $"restore {path}");
        if(result.StandardError != string.Empty){
            throw new Exception(result.StandardError);
        }
        return result;
    }

    public static async Task<CommandResult> Build(string projectPath)
    {
        var result = await Command.CaptureAsync("dotnet", $"build {projectPath}");
        if(result.StandardError != string.Empty){
            throw new Exception(result.StandardError);
        }
        return result;
    }
}