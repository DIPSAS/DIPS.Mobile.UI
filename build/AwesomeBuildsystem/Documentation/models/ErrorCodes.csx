#load "EventCodes.csx"

public partial class Root
{
    public ErrorCode[] ErrorCodes { get; set; }
}

public class ErrorCode : EventCode{
    public string Solution { get; set; }
}