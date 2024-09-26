using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using Foundation;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.StreamingBottomToolbar;

internal partial class ShutterButton
{
    private partial void AddPlatformGestureRecognizer()
    {
        var nativeView = this.ToPlatform(DUI.GetCurrentMauiContext!);
        nativeView.AddGestureRecognizer(new TouchGestureRecognizer(this, m_shutterContentWhiteOverlay, m_onTappedShutterButton));
    }

    public partial void Dispose()
    {
        var nativeView = this.ToPlatform(DUI.GetCurrentMauiContext!);
        foreach (var gestureRecognizer in nativeView.GestureRecognizers ?? [])
        {
            nativeView.RemoveGestureRecognizer(gestureRecognizer);
        }        
    }
}

internal class TouchGestureRecognizer(
    Grid shutterButton,
    Border shutterContentWhiteOverlay,
    Action onTappedShutterButton)
    : UIGestureRecognizer
{
    private readonly UIView m_nativeShutterButton = shutterButton.ToPlatform(DUI.GetCurrentMauiContext!);

    private bool m_isCancelled;

    public override void TouchesBegan(NSSet touches, UIEvent evt)
    {
        base.TouchesBegan(touches, evt);
        
        m_isCancelled = false;
        
        shutterContentWhiteOverlay.ScaleTo(.85f, 150);
    }

    public override void TouchesEnded(NSSet touches, UIEvent evt)
    {
        base.TouchesEnded(touches, evt);
        
        if(m_isCancelled)
            return;
        
        onTappedShutterButton.Invoke();
        shutterContentWhiteOverlay.ScaleTo(1);
    }
    
    public override void TouchesCancelled(NSSet touches, UIEvent evt)
    {
        base.TouchesCancelled(touches, evt);

        m_isCancelled = true;
        
        shutterContentWhiteOverlay.ScaleTo(1);
    }
    
    public override void TouchesMoved(NSSet touches, UIEvent evt)
    {
        if (m_isCancelled)
        {
            return;
        }
        
        var touchPoint = GetTouchPoint(touches);

        if (touchPoint == null || !m_nativeShutterButton.Bounds.Contains(touchPoint.Value))
        {
            m_isCancelled = true;
            shutterContentWhiteOverlay.ScaleTo(1);
        }
        
        base.TouchesMoved(touches, evt);
    }
    
    private CGPoint? GetTouchPoint(NSSet touches) =>
        (touches.AnyObject as UITouch)?.LocationInView(m_nativeShutterButton);
}