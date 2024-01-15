using CoreAnimation;
using CoreGraphics;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class ListItem
{
    private partial void SetCornerRadiusPlatform()
    {
        //Border.StrokeShape = new RoundRectangle has memory leakage on iOS
        if (this.Border.Handler is BorderHandler {PlatformView: { } platformView})
        {
            //this works, but it won't give you different corner radius for each corner
            // var layer = platformView.Layer.Sublayers.FirstOrDefault();
            // layer.CornerRadius = (nfloat)CornerRadius.BottomLeft;
            // layer.BackgroundColor = BackgroundColor.ToCGColor();
            // Border.BackgroundColor = Colors.Transparent;

            var layerName = "backgroundLayer";

            // Remove previous background layer if any
            var prevBackgroundLayer = platformView.Layer.Sublayers?.FirstOrDefault(x => x.Name == layerName);
            prevBackgroundLayer?.RemoveFromSuperLayer();

            UIBezierPath cornerPath = null;
            var bounds = Border.Bounds;
            cornerPath = CreateRoundedRectPath(bounds,CornerRadius);
            // Create a shape layer that draws our background.
            var shapeLayer = new CAShapeLayer
            {
                Frame = bounds,
                Path = cornerPath.CGPath,
                MasksToBounds = true,
                FillColor = BackgroundColor.ToCGColor(),
                Name = layerName
            };

            AddLayer(shapeLayer, 0, platformView);
        
        

            // if (platformView is ContentView contentView)
            // {
            //     SetPrivatePropertyValue(contentView, "Clip", border);
            // }

            // static void SetPrivatePropertyValue<T>(T obj, string propertyName, object newValue)
            // {
            //     // add a check here that the object obj and propertyName string are not null
            //     foreach (var fi in obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            //     {
            //         if (fi.Name.ToLower().Contains(propertyName.ToLower()))
            //         {
            //             fi.SetValue(obj, newValue);
            //             break;
            //         }
            //     }
            // }

            Border.BackgroundColor = Colors.Transparent;
        }
    }

public void AddLayer(CALayer layer, int position, UIView viewToAddTo)
{
    // If there is already a layer with the given name, remove it before inserting.
    if (layer != null)
    {
        // There's no background layer yet, insert it.
        if (position > -1)
            viewToAddTo.Layer.InsertSublayer(layer, position);
        else
            viewToAddTo.Layer.AddSublayer(layer);
    }
}
    

    public static UIBezierPath CreateRoundedRectPath(CGRect rect, CornerRadius cornerRadius)
    {
        var path = new UIBezierPath();

        path.MoveTo(new CGPoint(rect.Width - cornerRadius.TopRight, rect.Y));

        path.AddArc(new CGPoint((float)rect.X + rect.Width - cornerRadius.TopRight, (float)rect.Y + cornerRadius.TopRight), (nfloat)cornerRadius.TopRight, (float)(Math.PI * 1.5), (float)Math.PI * 2, true);
        path.AddLineTo(new CGPoint(rect.Width, rect.Height - cornerRadius.BottomRight));

        path.AddArc(new CGPoint((float)rect.X + rect.Width - cornerRadius.BottomRight, (float)rect.Y + rect.Height - cornerRadius.BottomRight), (nfloat)cornerRadius.BottomRight, 0, (float)(Math.PI * .5), true);
        path.AddLineTo(new CGPoint(cornerRadius.BottomLeft, rect.Height));

        path.AddArc(new CGPoint((float)rect.X + cornerRadius.BottomLeft, (float)rect.Y + rect.Height - cornerRadius.BottomLeft), (nfloat)cornerRadius.BottomLeft, (float)(Math.PI * .5), (float)Math.PI, true);
        path.AddLineTo(new CGPoint(rect.X, cornerRadius.TopLeft));

        path.AddArc(new CGPoint((float)rect.X + cornerRadius.TopLeft, (float)rect.Y + cornerRadius.TopLeft), (nfloat)cornerRadius.TopLeft, (float)Math.PI, (float)(Math.PI * 1.5), true);

        path.ClosePath();

        return path;
    }

}