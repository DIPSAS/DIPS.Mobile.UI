#load "../Command.csx"
public static class Xcode
{
    public static async Task<XCodeVersion> GetVersion()
    {
        var result = await Command.CaptureAsync("/usr/bin/xcodebuild", "-version");
        var versions = result.StandardOut.Split("\n");
        var version = versions.FirstOrDefault(s => s.StartsWith("Xcode", StringComparison.InvariantCultureIgnoreCase)).Replace("Xcode ", "");
        var buildVersion = versions.FirstOrDefault(s => s.StartsWith("build version", StringComparison.InvariantCultureIgnoreCase)).Replace("Build version ","");
        return new XCodeVersion(version, buildVersion);
    }
}

public record XCodeVersion(string Version, string BuildVersion);