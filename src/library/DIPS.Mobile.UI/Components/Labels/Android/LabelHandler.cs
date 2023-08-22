using Android.Text;
using AndroidX.AppCompat.Widget;
using DIPS.Mobile.UI.Components.Labels.Android;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class LabelHandler
{
    protected override AppCompatTextView CreatePlatformView()
    {
        return new AppCompatTextViewWithCustomEllipse(Context);
    }

    protected override void ConnectHandler(AppCompatTextView platformView)
    {
        base.ConnectHandler(platformView);
        
        
        
    }
    
    private static partial void MapOverrideMaxLinesAndLineBreakMode(LabelHandler handler, Label label)
    {
        var textView = handler.PlatformView;

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