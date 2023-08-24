using Android.Text;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using DIPS.Mobile.UI.Components.Labels.Android;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class LabelHandler
{
    protected override AppCompatTextView CreatePlatformView()
    {
        return new MauiTextView(Context, (VirtualView as Label)!);
    }

    private static partial void MapOverrideMaxLinesAndLineBreakMode(LabelHandler handler, Label label)
    {
        var textView = handler.PlatformView as MauiTextView;

        textView.Ellipsize = label.LineBreakMode switch
        {
            LineBreakMode.NoWrap => null,
            LineBreakMode.WordWrap => null,
            LineBreakMode.CharacterWrap => null,
            LineBreakMode.HeadTruncation => TextUtils.TruncateAt.Start,
            LineBreakMode.TailTruncation => TextUtils.TruncateAt.End,
            LineBreakMode.MiddleTruncation => TextUtils.TruncateAt.Middle,
            _ => textView.Ellipsize
        };

        textView.SetMaxLines(label.MaxLines);
    }
}