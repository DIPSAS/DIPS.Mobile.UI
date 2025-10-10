
#load "DistributionGroup.csx"

using Newtonsoft.Json;

public class ReleaseDetails
{
    public int Id { get; set; }
    public string Version { get; set; }

    [JsonProperty("short_version")]
    public string ShortVersion{ get; set; }
    [JsonProperty("download_url")]
    public string DownloadUrl { get; set; }
    [JsonProperty("install_url")]
    public string InstallUrl { get; set; }

    [JsonProperty("release_notes")]
    public string ReleaseNotes{ get; set; }

    [JsonProperty("distribution_groups")]
    public List<DistributionGroup> DistributionGroups{ get; set; }

    [JsonProperty("bundle_identifier")]
    public string BundleIdentifier { get; set; }
}