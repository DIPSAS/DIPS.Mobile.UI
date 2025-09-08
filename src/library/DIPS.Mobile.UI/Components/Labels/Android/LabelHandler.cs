using Android.Text;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class LabelHandler
{
    private static partial void MapOverrideMaxLinesAndLineBreakMode(LabelHandler handler, Label label)
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