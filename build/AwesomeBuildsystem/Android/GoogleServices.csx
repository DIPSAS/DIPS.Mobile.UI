#r "nuget:Newtonsoft.Json, 13.0.3"
using Newtonsoft.Json;

public static class GoogleServices{
    public static async Task<GoogleServicesRoot> ReadGoogleServicesJson(string path){
        var json = await File.ReadAllTextAsync(Path.Combine(path, "google-services.json"));
        return JsonConvert.DeserializeObject<GoogleServicesRoot>(json);
    }

    public static async Task<bool> PackageNameExists(string path, string packageName){
        var googleServices = await ReadGoogleServicesJson(path);
        return googleServices.Client.Any(c => c.ClientInfo.AndroidClientInfo.PackageName == packageName);
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class AndroidClientInfo
    {
        [JsonProperty("package_name")]
        public string PackageName { get; set; }
    }

    public class ApiKey
    {
        [JsonProperty("current_key")]
        public string CurrentKey { get; set; }
    }

    public class AppinviteService
    {
        [JsonProperty("other_platform_oauth_client")]
        public List<OtherPlatformOauthClient> OtherPlatformOauthClient { get; set; }
    }

    public class Client
    {
        [JsonProperty("client_info")]
        public ClientInfo ClientInfo { get; set; }

        [JsonProperty("oauth_client")]
        public List<OauthClient> OauthClient { get; set; }

        [JsonProperty("api_key")]
        public List<ApiKey> ApiKey { get; set; }

        [JsonProperty("services")]
        public Services Services { get; set; }
    }

    public class ClientInfo
    {
        [JsonProperty("mobilesdk_app_id")]
        public string MobilesdkAppId { get; set; }

        [JsonProperty("android_client_info")]
        public AndroidClientInfo AndroidClientInfo { get; set; }
    }

    public class IosInfo
    {
        [JsonProperty("bundle_id")]
        public string BundleId { get; set; }
    }

    public class OauthClient
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_type")]
        public int ClientType { get; set; }
    }

    public class OtherPlatformOauthClient
    {
        [JsonProperty("client_id")]
        public string ClientId { get; set; }

        [JsonProperty("client_type")]
        public int ClientType { get; set; }

        [JsonProperty("ios_info")]
        public IosInfo IosInfo { get; set; }
    }

    public class ProjectInfo
    {
        [JsonProperty("project_number")]
        public string ProjectNumber { get; set; }

        [JsonProperty("firebase_url")]
        public string FirebaseUrl { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("storage_bucket")]
        public string StorageBucket { get; set; }
    }

    public class GoogleServicesRoot
    {
        [JsonProperty("project_info")]
        public ProjectInfo ProjectInfo { get; set; }

        [JsonProperty("client")]
        public List<Client> Client { get; set; }

        [JsonProperty("configuration_version")]
        public string ConfigurationVersion { get; set; }
    }

    public class Services
    {
        [JsonProperty("appinvite_service")]
        public AppinviteService AppinviteService { get; set; }
    }

