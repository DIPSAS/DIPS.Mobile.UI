using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = Microsoft.Maui.Graphics.Colors;
using ContentView = Microsoft.Maui.Controls.ContentView;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

public class BottomBarView : ContentView
{
    private readonly UIView m_rootView;
    
    public BottomBarView(UIView rootView, BottomSheet bottomSheet)
    {
        m_rootView = rootView;

        Content = bottomSheet.CreateBottomBar();
        
        NativeView = this.ToPlatform(DUI.GetCurrentMauiContext!);
    }
    
    public UIView NativeView { get; }

    public void SetConstraints()
    {
        NativeView.TranslatesAutoresizingMaskIntoConstraints = false;
        
        NSLayoutConstraint.ActivateConstraints([
            NativeView.LeadingAnchor.ConstraintEqualTo(m_rootView.LeadingAnchor),
            NativeView.BottomAnchor.ConstraintEqualTo(m_rootView.BottomAnchor),
            NativeView.TrailingAnchor.ConstraintEqualTo(m_rootView.TrailingAnchor),
            NativeView.HeightAnchor.ConstraintEqualTo((nfloat)BottomSheet.BottomBarHeight)
        ]);
    }

    public void Show()
    {
        m_rootView.AddSubview(NativeView);
        SetConstraints();
    }

    public void Remove()
    {
        NativeView.RemoveFromSuperview();
    }
}