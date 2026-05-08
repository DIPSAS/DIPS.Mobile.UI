using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Platform;
using UIKit;
using ContentView = Microsoft.Maui.Controls.ContentView;

namespace DIPS.Mobile.UI.Components.BottomSheets.iOS;

public class BottomBarView : ContentView
{
    private UIView m_rootView;
    
    public BottomBarView(UIView rootView, BottomSheet bottomSheet)
    {
        m_rootView = rootView;

        Content = bottomSheet.CreateBottomBar();
        
        NativeView = this.ToPlatform(DUI.GetCurrentMauiContext!);
    }
    
    public UIView NativeView { get; }

    /// <summary>
    /// Switch the host view the bottom bar is constrained to. Used to re-pin the bar to
    /// the navigation controller's view so it stays visible across <see cref="BottomSheet.PushAsync"/>.
    /// </summary>
    public void SetRootView(UIView rootView)
    {
        if (ReferenceEquals(m_rootView, rootView))
            return;

        m_rootView = rootView;

        if (NativeView.Superview is not null)
        {
            NativeView.RemoveFromSuperview();
            Show();
        }
    }

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