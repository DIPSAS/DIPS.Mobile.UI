#load "SectionType.csx"
#load "Accessory.csx"
#load "Field.csx"

#r "nuget:Newtonsoft.Json, 13.0.3"

using Newtonsoft.Json;

public class Block
{
    public string Type { get; set; }

    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
#nullable enable
    public BlockText? Text { get; set; }
#nullable disable


    [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
#nullable enable
    public Accessory? Accessory { get; set; }
#nullable disable

[JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
#nullable enable
    public List<Field>? Fields { get; set; }
#nullable disable
}