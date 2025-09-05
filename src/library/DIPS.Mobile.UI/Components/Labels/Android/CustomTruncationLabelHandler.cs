using AndroidX.AppCompat.Widget;
using MauiTextView = DIPS.Mobile.UI.Components.Labels.Android.MauiTextView;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class CustomTruncationLabelHandler
{
    protected override AppCompatTextView CreatePlatformView()
    {
        return new MauiTextView(Context, (VirtualView as CustomTruncationLabel)!);
    }
}