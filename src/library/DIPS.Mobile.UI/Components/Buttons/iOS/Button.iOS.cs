using CoreGraphics;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Buttons;

public partial class Button
{
    /// <summary>
    /// To make sure the icon is smaller when the button is smaller
    /// </summary>
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        var largeButtonHeight = Sizes.GetSize(SizeName.size_12);
        
        var percentOfHeight = (float)height / largeButtonHeight;
        ((Handler!.PlatformView as UIButton)!).ImageView.Transform = CGAffineTransform.MakeScale(percentOfHeight, percentOfHeight);
    }
}