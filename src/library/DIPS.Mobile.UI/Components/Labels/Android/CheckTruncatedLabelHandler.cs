using AndroidX.AppCompat.Widget;
using MauiTextView = DIPS.Mobile.UI.Components.Labels.Android.MauiTextView;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class CheckTruncatedLabelHandler
{
    protected override AppCompatTextView CreatePlatformView()
    {
        return new MauiTextView(Context, VirtualView as CheckTruncatedLabel);
    }
}