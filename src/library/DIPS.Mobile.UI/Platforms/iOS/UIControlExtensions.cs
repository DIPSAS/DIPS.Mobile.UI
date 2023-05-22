using UIKit;

namespace DIPS.Mobile.UI.Platforms.iOS;

public static class UIControlExtensions
{
    public static void SetHorizontalAlignment(this UIControl uiControl, View view)
    {
        switch (view.HorizontalOptions.Alignment)
        {
            case LayoutAlignment.Start:
                uiControl.HorizontalAlignment = UIControlContentHorizontalAlignment.Left;
                break;
            
            case LayoutAlignment.Center:
                uiControl.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
                break;
            case LayoutAlignment.End:
                uiControl.HorizontalAlignment = UIControlContentHorizontalAlignment.Right;
                break;
        }
    }
}