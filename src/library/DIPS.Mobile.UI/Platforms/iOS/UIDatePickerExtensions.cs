using CoreAnimation;
using CoreGraphics;
using CoreImage;
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

        //TryAddBackgroundToInLineLabelWhenTimePicker(datePicker); <-- Does not work time picker as of yet, TODO: Fix this.
        
    }

    private static void TryAddBackgroundToInLineLabelWhenTimePicker(UIDatePicker datePicker)
    {
        datePicker.Subviews[0].InvokeActionForAllChildren(uiview =>
        {
            if (uiview.Layer.CornerRadius != 0)
            {
                uiview.Layer.CornerRadius = 0;
                uiview.Layer.BackgroundColor = UIColor.Blue.CGColor;
                uiview.Layer.BackgroundFilters = null;
                if (uiview.Layer.PresentationLayer != null)
                {
                    uiview.Layer.PresentationLayer.BackgroundColor = UIColor.Blue.CGColor;
                }
            }
        });

        //Another test
        foreach (var subview in datePicker.Subviews[0].Subviews[1].Subviews)
        {
            subview.Alpha = 0;
        }

        foreach (var subview in datePicker.Subviews.First().Subviews[1].Subviews)
        {
            subview.Alpha = 0;
        }
    }

    public static void PaintAllSubLayers(CALayer layer, CGColor cgColor)
    {
        while (true)
        {
            layer.BackgroundColor = UIColor.Red.CGColor;
            layer.LayoutIfNeeded();
            layer.LayoutSublayers();
            if (!layer.ModelLayer.Equals(layer))
            {
                layer = layer.ModelLayer;
                continue;
            }

            break;
        }
    }

    public class MyUIDatePicker : UIDatePicker
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            // DrawLayer();
        }

        public override void DrawLayer(CALayer layer, CGContext context)
        {
            base.DrawLayer(layer, context);
            // DrawLayer();
        }

        public override void DrawRect(CGRect area, UIViewPrintFormatter formatter)
        {
            base.DrawRect(area, formatter);
            // DrawLayer();
        }

        public override void WillDrawLayer(CALayer layer)
        {
            base.WillDrawLayer(layer);
            DrawLayer();
        }

        public void DrawLayer()
        {
            var viewToCreateLayerTo = this.Subviews.FirstOrDefault()?.Subviews.FirstOrDefault()?.Subviews
                .First();
            if (viewToCreateLayerTo != null)
            {
                viewToCreateLayerTo.Layer.BackgroundColor = UIColor.Orange.CGColor;
                viewToCreateLayerTo.Layer.ModelLayer.BackgroundColor = UIColor.Orange.CGColor;
                viewToCreateLayerTo.Layer.ModelLayer.BorderColor = UIColor.Orange.CGColor;
                viewToCreateLayerTo.Layer.ModelLayer.BorderWidth = 2;
                viewToCreateLayerTo.Layer.ModelLayer.ShadowColor = UIColor.Orange.CGColor;
            }
        }
    }
}