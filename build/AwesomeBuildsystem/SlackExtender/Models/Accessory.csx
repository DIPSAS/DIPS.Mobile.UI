#load "BlockText.csx"

#r "nuget:Newtonsoft.Json, 13.0.3"

using Newtonsoft.Json;

public class Accessory {
    public string Type { get; set; }
    public BlockText Text { get; set; }
    public string Url { get; set;}
}