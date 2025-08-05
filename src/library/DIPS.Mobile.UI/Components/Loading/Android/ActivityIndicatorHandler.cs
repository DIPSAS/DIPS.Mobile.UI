using Google.Android.Material.ProgressIndicator;
using ProgressBar = Android.Widget.ProgressBar;

namespace DIPS.Mobile.UI.Components.Loading.Android;

public class ActivityIndicatorHandler : Microsoft.Maui.Handlers.ActivityIndicatorHandler
{
    protected override CircularProgressIndicator CreatePlatformView()
    {
        return new CircularProgressIndicator(Context) { Indeterminate = true };
    }
}