using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Image = DIPS.Mobile.UI.Components.Images.Image;

namespace DIPS.Mobile.UI.Components.PanZoomContainer;

public partial class PanZoomContainerHandler : ViewHandler<PanZoomContainer, UIScrollView>
{
    private ScrollViewDelegate? m_scrollViewDelegate;
    private UIView? m_contentView;

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

        m_contentView?.RemoveFromSuperview();

        m_contentView = new Image.Image { Source = VirtualView.Source }.ToPlatform(MauiContext!);
        
        if(m_contentView is null)
            return;
        
        m_scrollViewDelegate = new ScrollViewDelegate(m_contentView);
        PlatformView.Delegate = m_scrollViewDelegate;

        PlatformView.AddSubview(m_contentView);
            
        m_contentView.TranslatesAutoresizingMaskIntoConstraints = false;
        NSLayoutConstraint.ActivateConstraints([
            m_contentView.TopAnchor.ConstraintEqualTo(PlatformView.ContentLayoutGuide.TopAnchor),
            m_contentView.BottomAnchor.ConstraintEqualTo(PlatformView.ContentLayoutGuide.BottomAnchor),
            m_contentView.LeadingAnchor.ConstraintEqualTo(PlatformView.ContentLayoutGuide.LeadingAnchor),
            m_contentView.HeightAnchor.ConstraintEqualTo(PlatformView.FrameLayoutGuide.HeightAnchor),
            m_contentView.WidthAnchor.ConstraintEqualTo(PlatformView.FrameLayoutGuide.WidthAnchor)
        ]);
            
        PlatformView.ZoomScale = 1.0f;
    }
    
    protected override void DisconnectHandler(UIScrollView platformView)
    {
        if (m_contentView is not null)
        {
            m_contentView.RemoveFromSuperview();
            m_contentView = null;
        }
        
        platformView.Delegate = null!;
        m_scrollViewDelegate?.Dispose();
        m_scrollViewDelegate = null;
        
        base.DisconnectHandler(platformView);
    }

    private class ScrollViewDelegate : UIScrollViewDelegate
    {
        private readonly WeakReference<UIView> m_view;

        public ScrollViewDelegate(UIView view)
        {
            m_view = new WeakReference<UIView>(view);
        }
        
        private UIView View => m_view.TryGetTarget(out var view) ? view : new UIView();

        public override UIView ViewForZoomingInScrollView(UIScrollView scrollView)
        {
            return View;
        }

        public override void DidZoom(UIScrollView scrollView)
        {
            CenterContent(scrollView);
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
