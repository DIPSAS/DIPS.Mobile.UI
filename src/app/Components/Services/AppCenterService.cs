using Newtonsoft.Json;

namespace Components.Services
{
    internal class AppCenterService : IAppCenterService
    {
        private readonly HttpClient m_httpClient;

        public AppCenterService()
        {
            m_httpClient = new HttpClient();
        }

        public async Task<Tuple<string,string>> RetrieveAndSetAppConfig()
        {
            await using var fileStream = await FileSystem.Current.OpenAppPackageFileAsync("appconfig.json");
            using var reader = new StreamReader(fileStream);

            var json = await reader.ReadToEndAsync();
            var anonymous = new
            {
                AppCenter = new
                {
                    ApiKey = "",
                    AppName = "",
                    DistributionGroup = ""
                }
            };
            var rawConfig = JsonConvert.DeserializeAnonymousType(json, anonymous);
            if (!m_httpClient.DefaultRequestHeaders.Contains("X-API-Token"))
            {
                m_httpClient.DefaultRequestHeaders.Add("X-API-Token", rawConfig.AppCenter.ApiKey);
            }

            if (m_httpClient.BaseAddress == null)
            {
                m_httpClient.BaseAddress = new Uri("https://api.appcenter.ms/v0.1/apps/dips-as/");    
            }
            
            return new Tuple<string, string>(rawConfig.AppCenter.AppName, rawConfig.AppCenter.DistributionGroup);
        }
        

        public async Task<Release?> GetLatestVersion()
        {
            var (appName, distributionGroupName) = await RetrieveAndSetAppConfig();
            var response = await m_httpClient.GetAsync(
                $"{appName}/distribution_groups/{distributionGroupName}/releases/latest");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var anon = new {Id = 0, short_version = "", uploaded_at = DateTime.Now, install_url="", download_url= ""};
            var release = JsonConvert.DeserializeAnonymousType(json, anon);
            var installUrl = release.install_url;
#if __ANDROID__
            installUrl = release.download_url;
#endif
            return release != null ? new Release(release.Id, release.short_version, release.uploaded_at, new Uri(installUrl)) : null;
        }
    }

    internal interface IAppCenterService
    {
        Task<Release?> GetLatestVersion();
    }
}