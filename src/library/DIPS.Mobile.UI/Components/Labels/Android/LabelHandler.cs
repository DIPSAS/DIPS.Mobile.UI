using Android.Text;
using AndroidX.AppCompat.Widget;
using Microsoft.Maui.Platform;
using MauiTextView = DIPS.Mobile.UI.Components.Labels.Android.MauiTextView;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class LabelHandler
{
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