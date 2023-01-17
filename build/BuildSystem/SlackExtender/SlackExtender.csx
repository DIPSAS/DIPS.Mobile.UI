#load "SlackMessageBuilder.csx"
#load "../Distribute/DistributionGroup.csx"
#load "../Logging/Logger.csx"
#load "../Distribute/ReleaseDetails.csx"
#r "../DIPS.Buildsystem.Core.dll"

using DIPS.Buildsystem.Core.Slack;

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
    ///     Will send a slack message of a single android + ios release
    ///</summary>
    /// <param name="productName">Name of the product</param>
    /// <param name="versionNumber">Version number of the product</param>
    /// <param name="releaseNotes">Release notes for the new version</param>
    /// <param name="androidDownloadUri">Link to download the .apk file, will be used in download button</param>
    /// <param name="iOSInstallUrl">Link to download the .ipa file, will be used in download button</param>

    public static async Task SendSingleReleaseMessage(
        string productName,
        List<DistributionGroup> distributionGroups,
        string version,
        string androidDownloadUri, 
        string iOSInstallUrl,
        string releaseNotes)
    {
        if(string.IsNullOrEmpty(m_webHookURI))
        {
            throw new MissingFieldException("The web hook URI has not been set. Set the web hook URI before calling this method");
        }

        iOSInstallUrl = FixiOSInstallUrl(iOSInstallUrl);

        var builder = new SlackMessageBuilder();
        //This was created by first using Block Kit Builder, then written in csharp after.
        //Block Kit Builder: https://app.slack.com/block-kit-builder/
        string json;
        builder
        .AddNewBlock().SetBlockSectionType(SectionType.Header).SetBlockText(() => new BlockText { Type="plain_text", Text = $":rocket: New {productName} Release!" })
        .AddNewBlock().SetBlockSectionType(SectionType.Section)
            .SetField(() => new Field(){ Text=$"*Destination:* {String.Join(", ", distributionGroups.Select(d => d.Name))}"})
            .SetField(() => new Field(){ Text=$"*Version:* {version}"})
        .AddDivider()
        .AddNewBlock().SetBlockSectionType(SectionType.Section)
            .SetField(() => new Field(){ Text=$"<{androidDownloadUri}|*Download Android :android:*>"})
            .SetField(() => new Field(){ Text=$"<{iOSInstallUrl}|*Download iOS *>"});

        if(!string.IsNullOrEmpty(releaseNotes)){
            json = builder
                .AddDivider()
                .AddNewBlock().SetBlockSectionType(SectionType.Section).SetBlockText(() => new BlockText(){ Text = "*Release notes* :writing_hand:"})
                .AddNewBlock().SetBlockSectionType(SectionType.Section).SetBlockText(() => new BlockText(){ Text = releaseNotes})
                .AddDivider()
                .Build()
                .ToJson();
        }else{
            Logger.LogDebug($"No release notes found for {productName} {version} with destination: {distributionGroups.Select(d => d.Name)}");
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