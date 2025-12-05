using Android.Views;
using Android.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.PanZoomContainer;

public partial class PanZoomContainerHandler : ViewHandler<PanZoomContainer, ZoomScrollView>
{
    protected override ZoomScrollView CreatePlatformView()
    {
        return new ZoomScrollView(Context);
    }

    protected override void ConnectHandler(ZoomScrollView platformView)
    {
        base.ConnectHandler(platformView);
        
        UpdateContent();
    }

    private void UpdateContent()
    {
        if (VirtualView.Source is null)
            return;

        PlatformView.RemoveAllViews();

        var image = new Image { Source = VirtualView.Source, }.ToPlatform(MauiContext!);
        PlatformView.AddView(image);
    }
    
    protected override void DisconnectHandler(ZoomScrollView platformView)
    {
        platformView.RemoveAllViews();
        
        base.DisconnectHandler(platformView);
    }
}

// Custom ScrollView with zoom/pan capabilities for Android
public class ZoomScrollView : FrameLayout
{
    private const float MinScale = 1.0f;
    private const float MaxScale = 5.0f;
    
    private ScaleGestureDetector? m_scaleDetector;
    private GestureDetector? m_gestureDetector;
    
    private float m_scaleFactor = 1.0f;

    public ZoomScrollView(Android.Content.Context context) : base(context)
    {
        m_scaleDetector = new ScaleGestureDetector(context, new ScaleListener(this));
        m_gestureDetector = new GestureDetector(context, new PanListener(this));
    }

    /// <summary>
    /// Must be implemented to ensure child view fills the container
    /// </summary>
    protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
    {
        base.OnLayout(changed, left, top, right, bottom);
        
        if (ChildCount <= 0)
            return;

        var child = GetChildAt(0);
        child?.Layout(0, 0, right - left, bottom - top);
    }

    protected override void DispatchDraw(Android.Graphics.Canvas? canvas)
    {
        if (canvas is not null)
        {
            canvas.Save();
            canvas.ClipRect(0, 0, Width, Height);
            base.DispatchDraw(canvas);
            canvas.Restore();
        }
        else
        {
            base.DispatchDraw(canvas);
        }
    }

    public override bool OnInterceptTouchEvent(MotionEvent? e)
    {
        if (e is null)
            return false;

        // Don't intercept, let events flow to OnTouchEvent
        return false;
    }

    public override bool OnTouchEvent(MotionEvent? e)
    {
        if (e is null)
            return false;

        var scaleHandled = m_scaleDetector?.OnTouchEvent(e) ?? false;
        var panHandled = m_gestureDetector?.OnTouchEvent(e) ?? false;

        // When zoomed in, prevent parent from intercepting touch events
        if (m_scaleFactor > 1.0f)
        {
            Parent?.RequestDisallowInterceptTouchEvent(true);
        }
        
        return scaleHandled || panHandled || base.OnTouchEvent(e);
    }

    private class ScaleListener : ScaleGestureDetector.SimpleOnScaleGestureListener
    {
        private readonly ZoomScrollView m_parent;

        public ScaleListener(ZoomScrollView parent)
        {
            this.m_parent = parent;
        }

        public override bool OnScale(ScaleGestureDetector? detector)
        {
            if (detector is null || m_parent.ChildCount == 0)
                return false;

            var child = m_parent.GetChildAt(0);
            if (child is null)
                return false;

            var oldScale = m_parent.m_scaleFactor;
            m_parent.m_scaleFactor *= detector.ScaleFactor;
            m_parent.m_scaleFactor = Math.Max(MinScale, Math.Min(m_parent.m_scaleFactor, MaxScale));

            // Calculate scale change
            var scaleChange = m_parent.m_scaleFactor / oldScale;

            // Adjust translation to zoom at focus point
            var focusX = detector.FocusX - m_parent.Width / 2f;
            var focusY = detector.FocusY - m_parent.Height / 2f;

            child.TranslationX = (child.TranslationX - focusX) * scaleChange + focusX;
            child.TranslationY = (child.TranslationY - focusY) * scaleChange + focusY;

            // Apply scale
            child.PivotX = child.Width / 2f;
            child.PivotY = child.Height / 2f;
            child.ScaleX = m_parent.m_scaleFactor;
            child.ScaleY = m_parent.m_scaleFactor;

            // Constrain to bounds
            var maxX = (child.Width * (m_parent.m_scaleFactor - 1)) / 2;
            var maxY = (child.Height * (m_parent.m_scaleFactor - 1)) / 2;
            child.TranslationX = Math.Max(-maxX, Math.Min(maxX, child.TranslationX));
            child.TranslationY = Math.Max(-maxY, Math.Min(maxY, child.TranslationY));

            return true;
        }
    }

    private class PanListener : GestureDetector.SimpleOnGestureListener
    {
        private readonly ZoomScrollView parent;

        public PanListener(ZoomScrollView parent)
        {
            this.parent = parent;
        }

        public override bool OnDown(MotionEvent? e)
        {
            // Return true to indicate we want to handle the gesture sequence
            // This is required for OnScroll to be called
            return parent.m_scaleFactor > 1.0f;
        }

        public override bool OnScroll(MotionEvent? e1, MotionEvent? e2, float distanceX, float distanceY)
        {
            if (parent is not { m_scaleFactor: > 1.0f, ChildCount: > 0 })
                return false;

            var child = parent.GetChildAt(0);
            if (child is null)
                return true;

            // Calculate new translation
            var newTranslationX = child.TranslationX - distanceX;
            var newTranslationY = child.TranslationY - distanceY;
                    
            // When scaling from center, content grows equally in all directions
            // At 2x scale, content extends by width/2 and height/2 beyond original size
            var maxX = (child.Width * (parent.m_scaleFactor - 1)) / 2;
            var maxY = (child.Height * (parent.m_scaleFactor - 1)) / 2;
                    
            // Constrain translation to these bounds
            child.TranslationX = Math.Max(-maxX, Math.Min(maxX, newTranslationX));
            child.TranslationY = Math.Max(-maxY, Math.Min(maxY, newTranslationY));
            return true;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            m_scaleDetector?.Dispose();
            m_scaleDetector = null;
            
            m_gestureDetector?.Dispose();
            m_gestureDetector = null;
        }
        
        base.Dispose(disposing);
    }
}
