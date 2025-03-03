using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

public class BottomSheetNavigationViewController : UINavigationController
{
    private readonly BottomSheet m_bottomSheet;

    public BottomSheetNavigationViewController(BottomSheet bottomSheet, UIViewController viewController) : base(viewController)
    {
        m_bottomSheet = bottomSheet;

        var appearance = new UINavigationBarAppearance();
        appearance.ConfigureWithOpaqueBackground();
        appearance.BackgroundColor = m_bottomSheet.BackgroundColor.ToPlatform();
        // Removes the separator line
        appearance.ShadowColor = UIColor.Clear;
        
        NavigationBar.StandardAppearance = appearance;
        NavigationBar.ScrollEdgeAppearance = NavigationBar.StandardAppearance;
        NavigationBar.TintColor = Colors.GetColor(ColorName.color_icon_action).ToPlatform();
    }
    
    public void SetPositioning()
    {
        this.SetPositioning(m_bottomSheet);   
    }
}