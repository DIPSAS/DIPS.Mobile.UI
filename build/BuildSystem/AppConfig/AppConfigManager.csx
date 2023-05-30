#load "AppConfig.csx"
#r "nuget:Newtonsoft.Json, 13.0.2"
using Newtonsoft.Json;

public static class AppConfigManager
{
    public static async void InsertAppConfig(string appConfigFilePath, string appName, string distributionGroup, string apiKey)
    {
    
        var currentJson = await File.ReadAllTextAsync(appConfigFilePath);

        var anonymous = new
        {
            AppCenter = new
            {
                ApiKey = "",
                AppName = "",
                DistributionGroup = ""
            }
        };
        var appConfig = JsonConvert.DeserializeObject<AppConfig>(currentJson);
        appConfig.AppCenter.AppName = appName;
        appConfig.AppCenter.DistributionGroup = distributionGroup;
        appConfig.AppCenter.ApiKey = apiKey;

        await File.WriteAllLinesAsync(appConfigFilePath, new string[]{JsonConvert.SerializeObject(appConfig)});

    }
}