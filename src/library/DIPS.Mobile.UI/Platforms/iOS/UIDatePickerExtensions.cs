using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Resources.Colors;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Platforms.iOS;

public static class UIDatePickerExtensions
{
    public static void SetInLineLabelColors(this UIDatePicker datePicker)
    {
        datePicker.TintColor = Colors.GetColor(ColorName.color_primary_90).ToPlatform();
        // datePicker.Subviews[0].InvokeActionForAllChildren(uiview =>
        // {
        //     if(uiview.Layer.CornerRadius != 0)
        //     {
        //         uiview.Layer.CornerRadius = 0;
        //         uiview.Layer.BackgroundColor = UIColor.Blue.CGColor;
        //         uiview.Layer.BackgroundFilters = null;
        //         if(uiview.Layer.PresentationLayer != null) 
        //         {
        //             uiview.Layer.PresentationLayer.BackgroundColor = UIColor.Blue.CGColor;
        //         }
        //     }
        // });
        // //foreach (var subview in datePicker.Subviews[0].Subviews[1].Subviews)
        // //{
        // //    subview.Alpha = 0;
        // //}
        // //foreach (var subview in datePicker.Subviews.First().Subviews[1].Subviews)
        // //{
        // //    subview.Alpha = 0;
        // //}
        //
        AddBackgroundToInlineLabel(datePicker.Subviews.First().Subviews.First().Subviews.First());
    }

    private static void AddBackgroundToInlineLabel(UIView uiView)
    {
        uiView.Alpha = 0; //Remove the gray area

        if (Chip.ColorProperty.DefaultValue is Color defaultColor &&
            Chip.CornerRadiusProperty.DefaultValue is int defaultCornerRadius)
        {
            var viewToCreateLayerTo = uiView.Superview;
            viewToCreateLayerTo.Layer.CornerRadius = new nfloat(defaultCornerRadius);
            viewToCreateLayerTo.Layer.BackgroundColor = defaultColor.ToCGColor();
        }
    }
}