using DIPS.Mobile.UI.API.Library;
using Google.Android.Material.Button;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControlHandler : ViewHandler<SegmentedControl, MaterialButtonToggleGroup>
{
    protected override MaterialButtonToggleGroup CreatePlatformView()
    {
        return new MaterialButtonToggleGroup(Context);
    }

    public static partial void MapSegments(SegmentedControlHandler handler, SegmentedControl segmentedControl)
    {
        handler.PlatformView.RemoveAllViews();

        foreach (var segmentControl in segmentedControl.Segments)
        {
            if (!DUI.TryGetResourceId("materialButtonOutlinedStyle", out var styleId, "attr"))
            {
                continue;
            }

            var materialButton = new MaterialButton(handler.Context, null, styleId);
            if (!string.IsNullOrEmpty(segmentControl.Title))
            {
                materialButton.Text = segmentControl.Title;
            }

            if (segmentControl.Icon != null)
            {
                if (segmentControl.Icon is FileImageSource fileImageSource)
                {
                    if (DUI.TryGetDrawableFromFileImageSource(fileImageSource, out var drawable))
                    {
                        materialButton.Icon = drawable;
                    }
                }
            }

            handler.PlatformView.AddView(materialButton);
        }
    }
}