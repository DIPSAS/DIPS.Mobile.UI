using DIPS.Mobile.UI.Components.Toolbar;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    private UIView? m_toolbarPlatformView;
    private NSLayoutConstraint[]? m_toolbarConstraints;

    private partial void AttachBottomToolbarOnPlatform()
    {
        if (BottomToolbar is null || Handler?.PlatformView is not UIView pageView)
            return;

        // Remove existing if re-attaching
        DetachBottomToolbarOnPlatform();

        // Get the toolbar's native view via handler
        var mauiContext = Handler.MauiContext;
        if (mauiContext is null)
            return;

        m_toolbarPlatformView = BottomToolbar.ToPlatform(mauiContext);
        m_toolbarPlatformView.TranslatesAutoresizingMaskIntoConstraints = false;

        // Find the VC's view as the container (so it overlays above page content)
        var containerView = FindViewController()?.View ?? pageView;
        containerView.AddSubview(m_toolbarPlatformView);

        var safeArea = containerView.SafeAreaLayoutGuide;

        var constraints = new NSLayoutConstraint[]
        {
            m_toolbarPlatformView.BottomAnchor.ConstraintEqualTo(safeArea.BottomAnchor),
            m_toolbarPlatformView.LeadingAnchor.ConstraintEqualTo(safeArea.LeadingAnchor),
            m_toolbarPlatformView.TrailingAnchor.ConstraintEqualTo(safeArea.TrailingAnchor),
        };

        m_toolbarConstraints = constraints;
        NSLayoutConstraint.ActivateConstraints(m_toolbarConstraints);
    }

    private partial void DetachBottomToolbarOnPlatform()
    {
        if (m_toolbarPlatformView is null)
            return;

        if (m_toolbarConstraints is not null)
        {
            NSLayoutConstraint.DeactivateConstraints(m_toolbarConstraints);
            m_toolbarConstraints = null;
        }

        m_toolbarPlatformView.RemoveFromSuperview();
        m_toolbarPlatformView = null;
    }

    private UIViewController? FindViewController()
    {
        if (Handler?.PlatformView is not UIView platformView)
            return null;

        var responder = platformView.NextResponder;
        while (responder is not null)
        {
            if (responder is UIViewController vc)
                return vc;
            responder = responder.NextResponder;
        }

        return null;
    }
}
