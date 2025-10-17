using AndroidX.AppCompat.Widget;
using DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel.Android;

namespace DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel;

public partial class CheckTruncatedLabelHandler
{
    protected override AppCompatTextView CreatePlatformView()
    {
        return new MauiTextView(Context, (VirtualView as CheckTruncatedLabel)!);
    }

    protected override void DisconnectHandler(AppCompatTextView platformView)
    {
        base.DisconnectHandler(platformView);

        if (platformView is MauiTextView textView)
        {
            textView.Cleanup();
        }
    }
}