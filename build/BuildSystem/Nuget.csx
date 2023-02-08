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
}