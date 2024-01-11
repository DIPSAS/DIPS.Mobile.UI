using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class LabelHandler
{
    protected override MauiLabel CreatePlatformView()
    {
        return new MauiLabel(VirtualView as Label);
    }

    private static partial void MapOverrideMaxLinesAndLineBreakMode(LabelHandler handler, Label label)
    {
        switch (label.LineBreakMode)
        {
            case LineBreakMode.NoWrap:
                handler.PlatformView.LineBreakMode = UILineBreakMode.Clip;
                break;
            case LineBreakMode.WordWrap:
                handler.PlatformView.LineBreakMode = UILineBreakMode.WordWrap;
                break;
            case LineBreakMode.CharacterWrap:
                handler.PlatformView.LineBreakMode = UILineBreakMode.CharacterWrap;
                break;
            case LineBreakMode.HeadTruncation:
                handler.PlatformView.LineBreakMode = UILineBreakMode.HeadTruncation;
                break;
            case LineBreakMode.MiddleTruncation:
                handler.PlatformView.LineBreakMode = UILineBreakMode.MiddleTruncation;
                break;
            case LineBreakMode.TailTruncation:
                handler.PlatformView.LineBreakMode = UILineBreakMode.TailTruncation;
                break;
        }

        handler.PlatformView.Lines = label.MaxLines;
    }

}