#load "Command.csx"

public static class Nuget
{
    public static Task Pack(string nuspecpath, string outputDirectory)
    {
        return Command.CaptureAsync("nuget", $"pack -OutputDirectory {outputDirectory}", nuspecpath);
    }
}