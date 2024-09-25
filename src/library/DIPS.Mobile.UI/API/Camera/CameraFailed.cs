namespace DIPS.Mobile.UI.API.Camera;

public delegate void CameraFailed(CameraException e);

public class CameraException(string context, Exception e, string title = "", string description = "")
    : Exception($"Camera failed, context:{context}, message:{e.Message}", e)
{
    public string Context { get; } = context;

    public string Title { get; } = title;
    
    public string Description { get; } = description;
    
}