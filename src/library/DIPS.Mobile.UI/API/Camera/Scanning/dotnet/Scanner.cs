namespace DIPS.Mobile.UI.API.Camera.Scanning;

public partial class Scanner
{
    public partial Task<string> Start(Preview preview) { return Task.FromResult(string.Empty);}
    public partial void Stop(){}
}