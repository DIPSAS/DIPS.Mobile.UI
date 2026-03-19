using DIPS.Mobile.UI.Components.Toolbar;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pages;

public partial class ContentPage
{
    private UIView? m_toolbarPlatformView;
    private NSLayoutConstraint[]? m_toolbarConstraints;
    private UIPanGestureRecognizer? m_scrollTrackingGesture;
    private UIView? m_scrollTrackingContainerView;

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

        // Disconnect the toolbar's handler to release platform resources
        BottomToolbar?.Handler?.DisconnectHandler();
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

    private partial void EnableScrollTrackingForView(VisualElement view)
    {
        DisableScrollTracking();

        var targetView = view.Handler?.PlatformView as UIView;
        if (targetView is null)
            return;

        m_scrollTrackingContainerView = targetView;
        m_scrollTrackingGesture = new UIPanGestureRecognizer(OnPanGesture);
        // Allow the pan to work simultaneously with scroll views, collection views, web views, etc.
        m_scrollTrackingGesture.ShouldRecognizeSimultaneously = (_, _) => true;
        // Don't steal touches — just observe
        m_scrollTrackingGesture.CancelsTouchesInView = false;
        m_scrollTrackingGesture.DelaysTouchesBegan = false;
        targetView.AddGestureRecognizer(m_scrollTrackingGesture);
    }

    private partial void DisableScrollTracking()
    {
        if (m_scrollTrackingGesture is not null && m_scrollTrackingContainerView is not null)
        {
            m_scrollTrackingContainerView.RemoveGestureRecognizer(m_scrollTrackingGesture);
        }

        m_scrollTrackingGesture?.Dispose();
        m_scrollTrackingGesture = null;
        m_scrollTrackingContainerView = null;
    }

    private void OnPanGesture(UIPanGestureRecognizer recognizer)
    {
        if (recognizer.State != UIGestureRecognizerState.Changed)
            return;

        var velocity = recognizer.VelocityInView(recognizer.View);

        // Use a velocity threshold to avoid triggering on tiny accidental touches
        const float threshold = 100f;

        if (velocity.Y < -threshold)
        {
            // Scrolling up (finger moving up = content going up = scrolling down through content)
            OnScrollDirectionDetected(isScrollingDown: true);
        }
        else if (velocity.Y > threshold)
        {
            // Scrolling down (finger moving down = content going down = scrolling up through content)
            OnScrollDirectionDetected(isScrollingDown: false);
        }
    }
}
