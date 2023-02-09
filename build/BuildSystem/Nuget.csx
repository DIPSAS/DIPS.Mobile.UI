#load "Command.csx"

public static class Nuget
{
    public static async Task<CommandResult> Pack(string nuspecpath, string version, string outputDirectory, bool verbose = true)
    {
        var result = await Command.CaptureAsync("nuget", $"pack -version {version} -OutputDirectory {outputDirectory}", nuspecpath);
        if(result.StandardError != string.Empty){
            throw new Exception(result.StandardError);
        }
        return result;
    }

    public static async Task<CommandResult> Restore(string path){
        var result = await Command.CaptureAsync("nuget", $"restore {path}");
        if(result.StandardError != string.Empty){
            throw new Exception(result.StandardError);
        }
        return result;
    }


public static async Task<CommandResult> Push(string nupkgPath, string source){
        var result = await Command.CaptureAsync("nuget", $"push {nupkgPath} -NonInteractive -source {source} -ApiKey VSTS -Verbosity Detailed");
        if(result.StandardError != string.Empty){
            throw new Exception(result.StandardError);
        }
        return result;
    }
}