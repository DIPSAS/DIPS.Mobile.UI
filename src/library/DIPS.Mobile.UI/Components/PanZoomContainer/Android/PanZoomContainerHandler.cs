using Android.Content;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.PanZoomContainer;

public partial class PanZoomContainerHandler : ViewHandler<PanZoomContainer, ZoomScrollView>
{
    protected override ZoomScrollView CreatePlatformView()
    {
        return new ZoomScrollView(Context, VirtualView);
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
    private readonly WeakReference<PanZoomContainer> m_virtualView;
    private const float MinScale = 1.0f;
    private const float MaxScale = 5.0f;
    
    private ScaleGestureDetector? m_scaleDetector;
    private GestureDetector? m_gestureDetector;
    
    private float m_scaleFactor = 1.0f;

    public ZoomScrollView(Context context, PanZoomContainer virtualView) : base(context)
    {
        m_virtualView = new WeakReference<PanZoomContainer>(virtualView);
        
        m_scaleDetector = new ScaleGestureDetector(context, new ScaleListener(this));
        m_gestureDetector = new GestureDetector(context, new PanListener(this));
    }
    
    private PanZoomContainer VirtualView => m_virtualView.TryGetTarget(out var view) ? view : new PanZoomContainer();

    private void SetIsZoomed()
    {
        VirtualView.IsZoomed = m_scaleFactor > MinScale;
    }
    
    /// <summary>
    /// Calculate the actual image content bounds within the ImageView (excluding letterbox areas)
    /// </summary>
    private Android.Graphics.RectF? GetImageContentBounds(View child)
    {
        if (child is not ImageView imageView)
            return null;
            
        var drawable = imageView.Drawable;
        if (drawable == null)
            return null;
            
        var drawableWidth = (float)drawable.IntrinsicWidth;
        var drawableHeight = (float)drawable.IntrinsicHeight;
        
        if (drawableWidth <= 0 || drawableHeight <= 0)
            return null;
            
        var viewWidth = (float)child.Width;
        var viewHeight = (float)child.Height;
        
        // Calculate scaling to fit (AspectFit behavior)
        var scale = Math.Min(viewWidth / drawableWidth, viewHeight / drawableHeight);
        
        // Calculate actual rendered image size
        var imageWidth = drawableWidth * scale;
        var imageHeight = drawableHeight * scale;
        
        // Calculate letterbox offsets (centering)
        var left = (viewWidth - imageWidth) / 2f;
        var top = (viewHeight - imageHeight) / 2f;
        
        return new Android.Graphics.RectF(left, top, left + imageWidth, top + imageHeight);
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
        private readonly WeakReference<ZoomScrollView> m_parent;

        public ScaleListener(ZoomScrollView parent)
        {
            this.m_parent = new WeakReference<ZoomScrollView>(parent);
        }

        private ZoomScrollView Parent => m_parent.TryGetTarget(out var parent) ? parent : new ZoomScrollView(Android.App.Application.Context, new PanZoomContainer());
        
        public override bool OnScale(ScaleGestureDetector? detector)
        {
            if (detector is null || Parent.ChildCount == 0)
                return false;

            var child = Parent.GetChildAt(0);
            if (child is null)
                return false;

            var oldScale = Parent.m_scaleFactor;
            Parent.m_scaleFactor *= detector.ScaleFactor;
            Parent.m_scaleFactor = Math.Max(MinScale, Math.Min(Parent.m_scaleFactor, MaxScale));

            // Calculate scale change
            var scaleChange = Parent.m_scaleFactor / oldScale;

            // Adjust translation to zoom at focus point
            var focusX = detector.FocusX - Parent.Width / 2f;
            var focusY = detector.FocusY - Parent.Height / 2f;

            child.TranslationX = (child.TranslationX - focusX) * scaleChange + focusX;
            child.TranslationY = (child.TranslationY - focusY) * scaleChange + focusY;

            // Apply scale
            child.PivotX = child.Width / 2f;
            child.PivotY = child.Height / 2f;
            child.ScaleX = Parent.m_scaleFactor;
            child.ScaleY = Parent.m_scaleFactor;

            // Constrain to bounds - account for actual image content bounds (excluding letterbox)
            var imageBounds = Parent.GetImageContentBounds(child);
            if (imageBounds != null)
            {
                // Calculate scaled image dimensions
                var scaledImageWidth = imageBounds.Width() * Parent.m_scaleFactor;
                var scaledImageHeight = imageBounds.Height() * Parent.m_scaleFactor;
                
                // Calculate how much the scaled image exceeds the viewport
                var maxX = Math.Max(0, (scaledImageWidth - Parent.Width) / 2);
                var maxY = Math.Max(0, (scaledImageHeight - Parent.Height) / 2);
                
                child.TranslationX = Math.Max(-maxX, Math.Min(maxX, child.TranslationX));
                child.TranslationY = Math.Max(-maxY, Math.Min(maxY, child.TranslationY));
            }
            else
            {
                // Fallback to old calculation if we can't get image bounds
                var maxX = (child.Width * (Parent.m_scaleFactor - 1)) / 2;
                var maxY = (child.Height * (Parent.m_scaleFactor - 1)) / 2;
                child.TranslationX = Math.Max(-maxX, Math.Min(maxX, child.TranslationX));
                child.TranslationY = Math.Max(-maxY, Math.Min(maxY, child.TranslationY));
            }

            Parent.SetIsZoomed();

            return true;
        }
    }

    private class PanListener : GestureDetector.SimpleOnGestureListener
    {
        private readonly WeakReference<ZoomScrollView> m_parent;

        public PanListener(ZoomScrollView parent)
        {
            this.m_parent = new WeakReference<ZoomScrollView>(parent);
        }

        private ZoomScrollView Parent => m_parent.TryGetTarget(out var parent) ? parent : new ZoomScrollView(Android.App.Application.Context, new PanZoomContainer());
        
        public override bool OnDown(MotionEvent? e)
        {
            // Return true to indicate we want to handle the gesture sequence
            // This is required for OnScroll to be called
            return true;
        }
        
        public override bool OnDoubleTap(MotionEvent? e)
        {
            if (e is null || Parent.ChildCount == 0)
                return false;

            var child = Parent.GetChildAt(0);
            if (child is null)
                return false;

            if (Parent.m_scaleFactor > 1.0f)
            {
                // Zoom out to minimum scale
                Parent.m_scaleFactor = MinScale;
                child.ScaleX = MinScale;
                child.ScaleY = MinScale;
                child.TranslationX = 0;
                child.TranslationY = 0;
            }
            else
            {
                // Zoom in to 2x at tap location
                Parent.m_scaleFactor = 2.0f;
                
                // Calculate focus point relative to center
                var focusX = e.GetX() - Parent.Width / 2f;
                var focusY = e.GetY() - Parent.Height / 2f;
                
                // Apply scale
                child.PivotX = child.Width / 2f;
                child.PivotY = child.Height / 2f;
                child.ScaleX = Parent.m_scaleFactor;
                child.ScaleY = Parent.m_scaleFactor;
                
                // Position the zoom to center on tap point
                child.TranslationX = -focusX;
                child.TranslationY = -focusY;
                
                // Constrain to bounds - account for actual image content bounds (excluding letterbox)
                var imageBounds = Parent.GetImageContentBounds(child);
                if (imageBounds != null)
                {
                    // Calculate scaled image dimensions
                    var scaledImageWidth = imageBounds.Width() * Parent.m_scaleFactor;
                    var scaledImageHeight = imageBounds.Height() * Parent.m_scaleFactor;
                    
                    // Calculate how much the scaled image exceeds the viewport
                    var maxX = Math.Max(0, (scaledImageWidth - Parent.Width) / 2);
                    var maxY = Math.Max(0, (scaledImageHeight - Parent.Height) / 2);
                    
                    child.TranslationX = Math.Max(-maxX, Math.Min(maxX, child.TranslationX));
                    child.TranslationY = Math.Max(-maxY, Math.Min(maxY, child.TranslationY));
                }
                else
                {
                    // Fallback to old calculation
                    var maxX = (child.Width * (Parent.m_scaleFactor - 1)) / 2;
                    var maxY = (child.Height * (Parent.m_scaleFactor - 1)) / 2;
                    child.TranslationX = Math.Max(-maxX, Math.Min(maxX, child.TranslationX));
                    child.TranslationY = Math.Max(-maxY, Math.Min(maxY, child.TranslationY));
                }
            }

            Parent.SetIsZoomed();
            return true;
        }

        public override bool OnScroll(MotionEvent? e1, MotionEvent? e2, float distanceX, float distanceY)
        {
            if (Parent is not { m_scaleFactor: > 1.0f, ChildCount: > 0 })
                return false;

            var child = Parent.GetChildAt(0);
            if (child is null)
                return true;

            // Calculate new translation
            var newTranslationX = child.TranslationX - distanceX;
            var newTranslationY = child.TranslationY - distanceY;
                    
            // Constrain to bounds - account for actual image content bounds (excluding letterbox)
            var imageBounds = Parent.GetImageContentBounds(child);
            if (imageBounds != null)
            {
                // Calculate scaled image dimensions
                var scaledImageWidth = imageBounds.Width() * Parent.m_scaleFactor;
                var scaledImageHeight = imageBounds.Height() * Parent.m_scaleFactor;
                
                // Calculate how much the scaled image exceeds the viewport
                var maxX = Math.Max(0, (scaledImageWidth - Parent.Width) / 2);
                var maxY = Math.Max(0, (scaledImageHeight - Parent.Height) / 2);
                
                child.TranslationX = Math.Max(-maxX, Math.Min(maxX, newTranslationX));
                child.TranslationY = Math.Max(-maxY, Math.Min(maxY, newTranslationY));
            }
            else
            {
                // Fallback to old calculation
                var maxX = (child.Width * (Parent.m_scaleFactor - 1)) / 2;
                var maxY = (child.Height * (Parent.m_scaleFactor - 1)) / 2;
                child.TranslationX = Math.Max(-maxX, Math.Min(maxX, newTranslationX));
                child.TranslationY = Math.Max(-maxY, Math.Min(maxY, newTranslationY));
            }
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
