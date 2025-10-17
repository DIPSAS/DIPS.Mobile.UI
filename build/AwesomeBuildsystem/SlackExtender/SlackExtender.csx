#load "SlackMessageBuilder.csx"
#load "../Distribute/DistributionGroup.csx"
#load "../Logging/Logger.csx"
#load "../Distribute/ReleaseDetails.csx"

using System.Net.Http;

public static class Slack 
{
    private static readonly string BaseAddress = "https://hooks.slack.com";
    
    public static async Task PostJsonAsync(string webHookUri, string json)
    {
        using var client = new HttpClient { BaseAddress = new Uri(BaseAddress) };
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        
        // If webHookUri is a full URL, use it directly, otherwise combine with base address
        var uri = webHookUri.StartsWith("http") ? webHookUri : $"{BaseAddress}{webHookUri}";
        
        await client.PostAsync(uri, content);
    }
}

public static class SlackExtender
{
    private static string m_webHookURI;

    /// <summary>
    ///     Set default web hook-URI, this uri will be used to post json to slack
    /// </summary>
    public static void SetWebHookURI(string webhook)
    {
        m_webHookURI = webhook;
    }

     /// <summary>
    ///     Will send a slack message for App/Play Store releases.
    ///</summary>
    /// <param name="productName">Name of the product</param>
    /// <param name="versionNumber">Version number of the product</param>
    /// <param name="releaseNotes">Release notes for the new version</param>
    /// <param name="androidDownloadUri">Link to download the .apk file, will be used in download button</param>
    /// <param name="iOSInstallUrl">Link to download the .ipa file, will be used in download button</param>

    public static async Task SendAppStoreGooglePlayReleaseMessage(
        string productName,
        string version,
        string packageName,
        string releaseNotes = "",
        string androidTrack = "",
        string iOSTestFlightGroup="")
    {
    
         if(string.IsNullOrEmpty(m_webHookURI))
        {
            throw new MissingFieldException("The web hook URI has not been set. Set the web hook URI before calling this method");
        }

        var builder = new SlackMessageBuilder();
        //This was created by first using Block Kit Builder, then written in csharp after.
        //Block Kit Builder: https://app.slack.com/block-kit-builder/
        string json;
        builder
        .AddNewBlock().SetBlockSectionType(SectionType.Header).SetBlockText(() => new BlockText { Type = "plain_text", Text = $":rocket: New {productName} Release!" })
        .AddNewBlock().SetBlockSectionType(SectionType.Section)
            .SetField(() => new Field() { Text = $"*Version:* {version}" })
        .AddNewBlock().SetBlockSectionType(SectionType.Section)
            .SetField(() => new Field() { Text = $"*Package name:* {packageName}" })
        .AddDivider();
        builder.AddNewBlock().SetBlockSectionType(SectionType.Header).SetBlockText(() => new BlockText() {Type = "plain_text", Text = "Android :android: Google Play"});
        if(!string.IsNullOrEmpty(androidTrack))
        {
            builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetBlockText(() => new BlockText() { Text = $"Track: {androidTrack}" });
        }
        else
        {
            builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetBlockText(() => new BlockText() { Text = $"Track: None" });
        }
        
    
        builder.AddDivider();
        builder.AddNewBlock().SetBlockSectionType(SectionType.Header).SetBlockText(() => new BlockText() {Type = "plain_text", Text = "iOS  App Store, TestFlight"});
        if(!string.IsNullOrEmpty(iOSTestFlightGroup))
        {
            builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetField(() => new Field() { Text = $"Group: {iOSTestFlightGroup}" });
        }
        else
        {
            builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetField(() => new Field() { Text = $"Group: None" });
        }

        if(!string.IsNullOrEmpty(releaseNotes)){
            json = builder
                .AddDivider()
                .AddNewBlock().SetBlockSectionType(SectionType.Header).SetBlockText(() => new BlockText() {Type = "plain_text", Text = "Release notes :writing_hand:"})
                .AddNewBlock().SetBlockSectionType(SectionType.Section).SetBlockText(() => new BlockText(){ Text = releaseNotes})
                .AddDivider()
                .Build()
                .ToJson();
        }else{
            Logger.LogDebug($"No release notes found for {productName} {version}");
            json = builder.
                Build().
                ToJson();
        }

        await SendMessageToSlack(json);
        Console.WriteLine("✅📮 Successfully posted to Slack");
    }


     public static async Task SendMultipleReleaseMessage(
        string productName,
        List<ReleaseDetails> androidReleases,
        List<ReleaseDetails> iOSReleases)
    {
        if(string.IsNullOrEmpty(m_webHookURI))
        {
            throw new MissingFieldException("The web hook URI has not been set. Set the web hook URI before calling this method");
        }

        iOSReleases.ForEach(ios => ios.InstallUrl = FixiOSInstallUrl(ios.InstallUrl));
        var version = iOSReleases.FirstOrDefault().ShortVersion;

        var builder = new SlackMessageBuilder();
        //This was created by first using Block Kit Builder, then written in csharp after.
        //Block Kit Builder: https://app.slack.com/block-kit-builder/
        string json;
        builder
        .AddNewBlock().SetBlockSectionType(SectionType.Header).SetBlockText(() => new BlockText { Type = "plain_text", Text = $":rocket: New {productName} Release!" })
        .AddNewBlock().SetBlockSectionType(SectionType.Section)
            .SetField(() => new Field() { Text = $"*Version:* {version}" })
        .AddDivider();
        builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetBlockText(() => new BlockText() {Type = "plain_text", Text = "Android :android:"});
        builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetBlockText(() => new BlockText() { Text = $"*Destination*: {androidReleases.First().DistributionGroups.First().Name}" });
        foreach(var androidRelease in androidReleases){
            builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetField(() => new Field() {Text = $"<{androidRelease.DownloadUrl}|*{androidRelease.BundleIdentifier}*>"});
        }
        builder.AddDivider();
        builder.AddNewBlock().SetBlockSectionType(SectionType.Header).SetBlockText(() => new BlockText() {Type = "plain_text", Text = "iOS "});
        builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetField(() => new Field() { Text = $"*Destination*: {iOSReleases.First().DistributionGroups.First().Name}" });
        foreach(var iOSRelease in iOSReleases){
            builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetField(() => new Field() { Text = $"<{iOSRelease.InstallUrl}|*{iOSRelease.BundleIdentifier}*>" });
        }

        json = builder.Build().ToJson();
        await SendMessageToSlack(json);
        Console.WriteLine("✅📮 Successfully posted to Slack");
    }


    /// <summary>
    /// Sends a message to slack that its ready to be published to the Store with a download url to use to download and upload to the store.
    /// </summary>
    /// <param name="productName"></param>
    /// <param name="version"></param>
    /// <param name="artifactsUrl"></param>
    /// <returns></returns>
    /// <exception cref="MissingFieldException"></exception>
    public static async Task SendPublicReleaseReady(string productName, string version, string artifactsUrl)
    {
        if(string.IsNullOrEmpty(m_webHookURI))
        {
            throw new MissingFieldException("The web hook URI has not been set. Set the web hook URI before calling this method");
        }

        var builder = new SlackMessageBuilder();
        //This was created by first using Block Kit Builder, then written in csharp after.
        //Block Kit Builder: https://app.slack.com/block-kit-builder/
        string json;
        builder
        .AddNewBlock().SetBlockSectionType(SectionType.Header).SetBlockText(() => new BlockText { Type = "plain_text", Text = $":rocket: New {productName} Public Release Ready!" })
        .AddNewBlock().SetBlockSectionType(SectionType.Section)
            .SetField(() => new Field() { Text = $"*Version:* {version}" })
        .AddDivider();
        builder.AddNewBlock().SetBlockSectionType(SectionType.Section).SetField(() => new Field() {Text = $"<{artifactsUrl}|*Download and upload the artifacts to the stores.*>"});

        json = builder.Build().ToJson();
        await SendMessageToSlack(json);
        Console.WriteLine("✅📮 Successfully posted to Slack");
    }

    private static string FixiOSInstallUrl(string installUrl){
        if(installUrl.StartsWith("itms-services://")){
            return installUrl.Replace("itms-services://", "itms-services://0.0.0.0"); //Workaround to magically make the itms-service url valid for slack: https://stackoverflow.com/questions/57399789/how-to-send-ad-hoc-app-download-link-itms-service-on-slack-using-message-buttons
        }
        return installUrl;
    }

    private static async Task SendMessageToSlack(string json)
    {
        try{
            await Slack.PostJsonAsync(m_webHookURI, json);
        }
        catch(Exception e)
        {

            Logger.LogDebug("Json tried to get posted to slack: "+json);
            Logger.LogDebug("Webhook tried to use to post to slack: " + m_webHookURI);
            throw new Exception($"Was not able to post json to slack: {e.Message}");
        }
    }

}