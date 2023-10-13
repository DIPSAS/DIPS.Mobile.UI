using CoreAnimation;
using CoreGraphics;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;
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
        
        //Tested on iOS 15,16,17
        
        //In line date picker
        var inlineDateViewLayer = this.Subviews.FirstOrDefault()?.Subviews.FirstOrDefault()?.Subviews
            .FirstOrDefault();
        SetDefaultLayerAttributes(inlineDateViewLayer);
        
        //In line time picker
        var inLineTimeView = this.Subviews.FirstOrDefault()?.Subviews.LastOrDefault();
        SetDefaultLayerAttributes(inLineTimeView);
        
    }

    private static void SetDefaultLayerAttributes(UIView? view)
    {
        if (view == null)
        {
            return;
        }

        var defaultColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_secondary_30);
        var defaultCornerRadius = DIPS.Mobile.UI.Resources.Sizes.Sizes.GetSize(SizeName.size_2);

        view.BackgroundColor = defaultColor.ToPlatform();
        view.Layer.CornerRadius = defaultCornerRadius;
    }

    public void DisposeLayer()
    {
        this.ValueChanged -= UpdateInLineLayerAttributes;
    }

    private void UpdateInLineLayerAttributes(object? sender, EventArgs e) => UpdateInLineLayerAttributes();
}