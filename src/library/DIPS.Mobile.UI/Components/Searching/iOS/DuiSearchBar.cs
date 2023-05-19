using CoreGraphics;
using DIPS.Mobile.UI.Extensions.iOS;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Searching.iOS;

public class DuiSearchBar : MauiSearchBar
{
    public override CGSize SizeThatFits(CGSize size) 
    {
        //This might happen if the width of the parent is infinite, one case is when you put the SearchBar inside a grid column with Width=Auto
        //This is a MAUISearchBar bug, and the fix is inspired by the old Xamarin implementation : https://github.com/xamarin/Xamarin.Forms/blob/9e230ad1f31b7cee2003035ecf5c4bc1b27a4c0e/Xamarin.Forms.Platform.iOS/Renderers/SearchBarRenderer.cs#L175
        if (nfloat.IsInfinity(size.Width))
        {
            var firstParentWithWidth = this.FindParentThatMatches(uiView =>
                nfloat.IsPositive(uiView.Bounds.Width) && uiView.Bounds.Width > 0);
            if (firstParentWithWidth != null)
            {
                size.Width = firstParentWithWidth.Bounds.Width;
            }
            else //Fall back to the device size if there is no parent width to use
            {
                size.Width = (nfloat)DeviceDisplay.Current.MainDisplayInfo.Width;
            }
        }
            
        return base.SizeThatFits(size);
    }
}