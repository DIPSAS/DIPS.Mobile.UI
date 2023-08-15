using CoreAnimation;
using CoreGraphics;
using DIPS.Mobile.UI.Components.Chips;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;

public class DUIDatePicker : UIDatePicker
{
    public override void Draw(CGRect rect)
    {
        base.Draw(rect);
        UpdateInLineLayerAttributes(); //Update attributes when its first drawn
        ValueChanged += UpdateInLineLayerAttributes;
    }

    internal void UpdateInLineLayerAttributes()
    {
        var inlineDateViewLayer = this.Subviews.FirstOrDefault()?.Subviews.FirstOrDefault()?.Subviews
            .FirstOrDefault();
        SetDefaultLayerAttributes(inlineDateViewLayer?.Layer);
        
        var inLineTimeView = this.Subviews.FirstOrDefault()?.Subviews.LastOrDefault();
        SetDefaultLayerAttributes(inLineTimeView?.Layer);
        // foreach (var inLineTimeViewSubView in inLineTimeViewSubViews)
        // {
        //     SetDefaultLayerAttributes(inLineTimeViewSubView.Superview.Layer);
        //     SetDefaultLayerAttributes(inLineTimeViewSubView.Layer);
        // }
        
        // if (OperatingSystem.IsIOSVersionAtLeast(16, 0))
        // {
        //     inLineTimeViewLayer = inlineDateViewLayer?.Superview?.Superview;
        // }
        //
        // SetDefaultLayerAttributes(inLineTimeViewLayer?.Layer);
        
    }

    private static void SetDefaultLayerAttributes(CALayer? layer)
    {
        if (layer == null)
        {
            return;
        }

        if (Chip.ColorProperty.DefaultValue is not Color defaultColor ||
            Chip.CornerRadiusProperty.DefaultValue is not int defaultCornerRadius)
        {
            return;
        }

        layer.BackgroundColor = defaultColor.ToCGColor();
        layer.CornerRadius = defaultCornerRadius;
    }

    public void DisposeLayer()
    {
        this.ValueChanged -= UpdateInLineLayerAttributes;
    }

    private void UpdateInLineLayerAttributes(object? sender, EventArgs e) => UpdateInLineLayerAttributes();
}