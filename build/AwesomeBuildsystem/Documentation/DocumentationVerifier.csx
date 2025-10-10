#r "nuget:YamlDotNet, 15.1.2"
#load "models/EventCodes.csx"
#load "models/ErrorCodes.csx"
#load "models/ReleaseNotes.csx"
#load "../Logging/Logger.csx"

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public static class DocumentationVerifier
{
    public static async Task<bool> VerifyEventCodes(string errorCodeYamlFilePath, string errorCodeCsharpFilePath, string[] errorCodesToSkip = null)
    {
        if(!File.Exists(errorCodeYamlFilePath))
        {
            Logger.LogError($"{errorCodeYamlFilePath} does not exist", true);
        }

        if(!File.Exists(errorCodeCsharpFilePath))
        {
            Logger.LogError($"{errorCodeCsharpFilePath} does not exist", true);
        }

        var yml = await File.ReadAllTextAsync(errorCodeYamlFilePath);
        var deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();
        var root = deserializer.Deserialize<Root>(yml);
        if (root == null) return false;
        Logger.LogDebug($"Comparing event codes: \n YAML: {errorCodeYamlFilePath} \n C#: {errorCodeCsharpFilePath}");
        EventCode[] yamlEventCodes;
        if(root.ErrorCodes != null)
        {
            yamlEventCodes = root.ErrorCodes;
        }
        else
        {
            yamlEventCodes = root.EventCodes;
        }
        return await Compare(errorCodeCsharpFilePath, yamlEventCodes, errorCodesToSkip);
    }

    private static async Task<bool> Compare(string csharpFilePath, EventCode[] yamlEventCodes, string[] errorCodesToSkip = null)
    {
        var csharpContent = await File.ReadAllLinesAsync(csharpFilePath);

         var csharpErrorCodes = new Dictionary<string, string>();
         var csharpErrorCodesToSkip = new List<string>();

        var regex = new Regex(@"\s*public\s+static\s+string\s+(?<ShortName>\w+)\s*=>\s*\""(?<Code>[^""]+)\"";\s*(?://(?<Comment>.*))?", RegexOptions.Compiled);

        foreach (var line in csharpContent)
        {
            var match = regex.Match(line);
            if (match.Success)
            {
                var shortName = match.Groups["ShortName"].Value.Trim();
                var code = match.Groups["Code"].Value.Trim();
                var comment = match.Groups["Comment"].Success ? match.Groups["Comment"].Value.Trim() : null;
                csharpErrorCodes[shortName] = code;
                if(comment != null && comment.Equals("NotProduction", StringComparison.InvariantCultureIgnoreCase))
                {
                    csharpErrorCodesToSkip.Add(code);
                }
            }
        }

        var failed = false;
        foreach (var yamlEventCode in yamlEventCodes)
        {
            if(yamlEventCode.Deprecated != null)
            {
                Logger.LogDebug($"🧟 Skipping due to deprecation: {yamlEventCode.ShortName} - {yamlEventCode.Code}");
                continue;
            }

            if(errorCodesToSkip != null)
            {
                if(errorCodesToSkip.Any(codeToSkip => yamlEventCode.Code.StartsWith(codeToSkip))){
                    Logger.LogDebug($"🟡 Skipping: {yamlEventCode.ShortName} - {yamlEventCode.Code}");
                    continue;
                }
            }

            if(csharpErrorCodesToSkip.Any(codeToSkip => yamlEventCode.Code == codeToSkip))
            {
                Logger.LogDebug($"🟡 Skipping: {yamlEventCode.ShortName} - {yamlEventCode.Code} as its marked with //NotProduction");
                continue;
            }

            if (csharpErrorCodes.ContainsKey(yamlEventCode.ShortName))
            {
                if (csharpErrorCodes[yamlEventCode.ShortName] == yamlEventCode.Code)
                {
                    Logger.LogDebug($"✅ OK: {yamlEventCode.ShortName} = {yamlEventCode.Code}");
                }
                else
                {
                    Logger.LogDebug($"❌ Wrong: {yamlEventCode.ShortName} has wrong code. YAML: {yamlEventCode.Code}, C#: {csharpErrorCodes[yamlEventCode.ShortName]}");
                    failed = true;
                }
            }
            else
            {
                Logger.LogDebug($"❌ Missing: {yamlEventCode.ShortName} does not exist in C#. Code from YAML: {yamlEventCode.Code}");
                failed = true;
            }
        }

        foreach (var csharpErrorcode in csharpErrorCodes)
        {
            var code = csharpErrorcode.Value;
            var shortName = csharpErrorcode.Key;
            if(!yamlEventCodes.Any(eventCode => eventCode.Code == csharpErrorcode.Value))
            {
                if(csharpErrorCodesToSkip.Any(codeToSkip => codeToSkip == code))
                {
                    Logger.LogDebug($"🟡 Skipping: {shortName} - {code} as its marked with //NotProduction");
                    continue;
                }
                    Logger.LogDebug($"❌ Missing: {shortName} = {code} was found in C# but not in YAML. If it is not meant for production yet, add a //NotProduction after the const in C# code.");
                    continue;
            }
        }
        return failed;
    }
}