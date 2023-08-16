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

    private void UpdateInLineLayerAttributes()
    {
        if (PreferredDatePickerStyle == UIDatePickerStyle.Inline) return; //Changing these colors when the style is inline will change the entire in line date picker. Which messes up the colors.
        
        var inlineDateViewLayer = this.Subviews.FirstOrDefault()?.Subviews.FirstOrDefault()?.Subviews
            .FirstOrDefault();
        SetDefaultLayerAttributes(inlineDateViewLayer);
        
        var inLineTimeView = this.Subviews.FirstOrDefault()?.Subviews.LastOrDefault(); //Tested with physical device iOS 15 and 16, does not work with simulator iOS 14
        
        if (inLineTimeView is {BackgroundColor: null}) //TODO: iOS 14 (remove when iOS 17 is out). 
            //This happens for at least iOS 14, we then grab the first subview to set the layer color
        {
            inLineTimeView = inLineTimeView.Subviews.FirstOrDefault();
        }
        SetDefaultLayerAttributes(inLineTimeView);
        
    }

    private static void SetDefaultLayerAttributes(UIView? view)
    {
        if (view == null)
        {
            return;
        }

        if (Chip.ColorProperty.DefaultValue is not Color defaultColor ||
            Chip.CornerRadiusProperty.DefaultValue is not int defaultCornerRadius)
        {
            return;
        }

        view.BackgroundColor = defaultColor.ToPlatform();
        view.Layer.CornerRadius = defaultCornerRadius;
    }

    public void DisposeLayer()
    {
        this.ValueChanged -= UpdateInLineLayerAttributes;
    }

    private void UpdateInLineLayerAttributes(object? sender, EventArgs e) => UpdateInLineLayerAttributes();
}