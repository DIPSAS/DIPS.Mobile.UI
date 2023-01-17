#load "Block.csx"

#r "nuget:Newtonsoft.Json, 13.0.2"

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

public class SlackMessage
{
    public List<Block> Blocks { get; set; } = new List<Block>();

    public string ToJson()
    {
        var serializerSettings = new JsonSerializerSettings();
        serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        return JsonConvert.SerializeObject(this, serializerSettings);
    }
}

