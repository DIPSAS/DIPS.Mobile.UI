#load "../Command.csx"
public static class macOS
{
    public static async Task<macOSVersion> GetVersion()
    {
        var result = await Command.CaptureAsync("sw_vers", "");
        var information = result.StandardOut.Split("\n");
        var version = information.FirstOrDefault(s => s.StartsWith("ProductVersion:")).Replace("ProductVersion:", "").Replace("\t", "");
        var buildVersion = information.FirstOrDefault(s => s.StartsWith("BuildVersion:")).Replace("BuildVersion:", "").Replace("\t", "");
        return new macOSVersion(version, buildVersion);
    }
}

public record macOSVersion(string Version, string BuildVersion);