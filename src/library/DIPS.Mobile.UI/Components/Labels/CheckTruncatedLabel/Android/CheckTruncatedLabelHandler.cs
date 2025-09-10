using Android.Text;
using AndroidX.AppCompat.Widget;
using MauiTextView = DIPS.Mobile.UI.Components.Labels.Android.MauiTextView;

namespace DIPS.Mobile.UI.Components.Labels.CheckTruncatedLabel;

public partial class CheckTruncatedLabelHandler
{
    protected override AppCompatTextView CreatePlatformView()
    {
        return new MauiTextView(Context, (VirtualView as CheckTruncatedLabel)!);
    }
    
    private static void MapOverrideMaxLinesAndLineBreakMode(CheckTruncatedLabelHandler handler, CheckTruncatedLabel label)
    {
        handler.PlatformView.Ellipsize = label.LineBreakMode switch
        {
            LineBreakMode.NoWrap => null,
            LineBreakMode.WordWrap => null,
            LineBreakMode.CharacterWrap => null,
            LineBreakMode.HeadTruncation => TextUtils.TruncateAt.Start,
            LineBreakMode.TailTruncation => TextUtils.TruncateAt.End,
            LineBreakMode.MiddleTruncation => TextUtils.TruncateAt.Middle,
            _ => handler.PlatformView.Ellipsize
        };

        handler.PlatformView.SetMaxLines(label.MaxLines);
    }
}