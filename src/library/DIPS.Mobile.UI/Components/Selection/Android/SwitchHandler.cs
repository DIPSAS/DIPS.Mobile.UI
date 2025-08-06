using AndroidX.AppCompat.Widget;
using Google.Android.Material.MaterialSwitch;

namespace DIPS.Mobile.UI.Components.Selection.Android;

public class SwitchHandler : Microsoft.Maui.Handlers.SwitchHandler
{
    protected override SwitchCompat CreatePlatformView()
    {
        return new MaterialSwitch(Context);
    }
}