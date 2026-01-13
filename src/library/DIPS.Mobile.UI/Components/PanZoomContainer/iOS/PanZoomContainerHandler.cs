using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Image = DIPS.Mobile.UI.Components.Images.Image;

namespace DIPS.Mobile.UI.Components.PanZoomContainer;

public partial class PanZoomContainerHandler : ViewHandler<PanZoomContainer, UIScrollView>
{
    private ScrollViewDelegate? m_scrollViewDelegate;
    private UIView? m_nativeImageView;
    private UITapGestureRecognizer? m_doubleTapGesture;

    protected override UIScrollView CreatePlatformView()
    {
        var scrollView = new UIScrollView
        {
            MinimumZoomScale = 1.0f,
            MaximumZoomScale = 5.0f,
            BouncesZoom = true,
            ShowsVerticalScrollIndicator = false,
            ShowsHorizontalScrollIndicator = false,
            ClipsToBounds = true
        };
        
        return scrollView;
    }

    protected override void ConnectHandler(UIScrollView platformView)
    {
        base.ConnectHandler(platformView);
        
        UpdateContent();
    }

    private void UpdateContent()
    {
        if (VirtualView.Source is null)
            return;

        m_nativeImageView?.RemoveFromSuperview();
        
        m_nativeImageView = new Image.Image { Source = VirtualView.Source }.ToPlatform(MauiContext!);
        
        if(m_nativeImageView is null)
            return;
        
        m_scrollViewDelegate = new ScrollViewDelegate(m_nativeImageView, VirtualView);
        PlatformView.Delegate = m_scrollViewDelegate;

        PlatformView.AddSubview(m_nativeImageView);
            
        m_nativeImageView.TranslatesAutoresizingMaskIntoConstraints = false;
        NSLayoutConstraint.ActivateConstraints([
            m_nativeImageView.TopAnchor.ConstraintEqualTo(PlatformView.ContentLayoutGuide.TopAnchor),
            m_nativeImageView.BottomAnchor.ConstraintEqualTo(PlatformView.ContentLayoutGuide.BottomAnchor),
            m_nativeImageView.LeadingAnchor.ConstraintEqualTo(PlatformView.ContentLayoutGuide.LeadingAnchor),
            m_nativeImageView.HeightAnchor.ConstraintEqualTo(PlatformView.FrameLayoutGuide.HeightAnchor),
            m_nativeImageView.WidthAnchor.ConstraintEqualTo(PlatformView.FrameLayoutGuide.WidthAnchor)
        ]);
            
        PlatformView.ZoomScale = 1.0f;
        
        // Add double-tap gesture
        m_doubleTapGesture = new UITapGestureRecognizer(HandleDoubleTap)
        {
            NumberOfTapsRequired = 2
        };
        PlatformView.AddGestureRecognizer(m_doubleTapGesture);
    }
    
    private void HandleDoubleTap(UITapGestureRecognizer gesture)
    {
        if (PlatformView.ZoomScale > 1.0f)
        {
            // Zoom out to minimum scale
            PlatformView.SetZoomScale(1.0f, true);
        }
        else
        {
            // Zoom in to 2x at tap location
            var tapPoint = gesture.LocationInView(m_nativeImageView);
            var zoomRect = new CoreGraphics.CGRect(
                tapPoint.X - (PlatformView.Bounds.Width / 4),
                tapPoint.Y - (PlatformView.Bounds.Height / 4),
                PlatformView.Bounds.Width / 2,
                PlatformView.Bounds.Height / 2
            );
            PlatformView.ZoomToRect(zoomRect, true);
        }
    }
    
    protected override void DisconnectHandler(UIScrollView platformView)
    {
        if (m_doubleTapGesture is not null)
        {
            platformView.RemoveGestureRecognizer(m_doubleTapGesture);
            m_doubleTapGesture.Dispose();
            m_doubleTapGesture = null;
        }
        
        if (m_nativeImageView is not null)
        {
            m_nativeImageView.RemoveFromSuperview();
            m_nativeImageView = null;
        }
        
        platformView.Delegate = null!;
        m_scrollViewDelegate?.Dispose();
        m_scrollViewDelegate = null;
        
        base.DisconnectHandler(platformView);
    }

    private class ScrollViewDelegate : UIScrollViewDelegate
    {
        private readonly WeakReference<UIView> m_view;
        private readonly WeakReference<PanZoomContainer> m_virtualView;

        public ScrollViewDelegate(UIView view, PanZoomContainer virtualView)
        {
            m_view = new WeakReference<UIView>(view);
            m_virtualView = new WeakReference<PanZoomContainer>(virtualView);
        }
        
        private UIView View => m_view.TryGetTarget(out var view) ? view : new UIView();
        private PanZoomContainer VirtualView => m_virtualView.TryGetTarget(out var virtualView) ? virtualView : new PanZoomContainer();

        public override UIView ViewForZoomingInScrollView(UIScrollView scrollView)
        {
            return View;
        }

        public override void DidZoom(UIScrollView scrollView)
        {
            CenterContent(scrollView);

            VirtualView.IsZoomed = scrollView.ZoomScale > 1;
        }

        private void CenterContent(UIScrollView scrollView)
        {
            // Center content when it's smaller than the scroll view
            var offsetX = Math.Max((scrollView.Bounds.Width - scrollView.ContentSize.Width) * 0.5f, 0.0f);
            var offsetY = Math.Max((scrollView.Bounds.Height - scrollView.ContentSize.Height) * 0.5f, 0.0f);
            
            scrollView.ContentInset = new UIEdgeInsets((nfloat)offsetY, (nfloat)offsetX, (nfloat)offsetY, (nfloat)offsetX);
        }
    }
}
