public partial class Root
{
    
    public EventCode[] EventCodes { get; set; }
}

public class EventCode
{
    public string Code { get; set; }
    public string ShortName { get; set; }

    public string Description { get; set; }


    public string FromVersion { get; set; }


    public string[] Parameters { get; set; }
   
    public Deprecated Deprecated { get; set; }
}

public class Deprecated
{
    public string FromVersion { get; set; }

    public string[] ReplacedWith { get; set; }
}