using CoreGraphics;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

internal class BottomSheetViewContainer : UIView
{
    readonly BottomSheet m_bottomSheet;
    readonly UIView m_nativeView;

    internal BottomSheetViewContainer(BottomSheet bottomSheet, IMauiContext mauiContext)
    {
        m_bottomSheet = bottomSheet;
        m_nativeView = bottomSheet.ToPlatform(mauiContext);
        AddSubview(m_nativeView);
    }

    public override void LayoutSubviews()
    {
        base.LayoutSubviews();
        var r = m_bottomSheet.Measure(Application.Current.MainPage.Window.Width, Application.Current.MainPage.Window.Height);
        m_nativeView.Frame = new CGRect(0, 0, Bounds.Width, r.Request.Height);
    }
}