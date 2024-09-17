using Android.Views;
using Microsoft.Maui.Handlers;
using Application = Android.App.Application;
using Object = Java.Lang.Object;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera.Preview;

internal partial class PreviewViewHandler() : ViewHandler<PreviewView, AndroidX.Camera.View.PreviewView>(ViewMapper)
{
    protected override AndroidX.Camera.View.PreviewView CreatePlatformView()
    {
        return new AndroidX.Camera.View.PreviewView(Context);
    }

    protected override void ConnectHandler(AndroidX.Camera.View.PreviewView platformView)
    {
        base.ConnectHandler(platformView);
        
        platformView.SetOnTouchListener(new TouchScaleListener(
            scaleFactor =>
        {
            OnScaled?.Invoke(scaleFactor);
        }, (x, y) =>
        {
            OnTapped?.Invoke(x, y);
        }));
    }

    public event Action<float>? OnScaled;
    public event Action<float, float>? OnTapped;
}

internal class TouchScaleListener : Object, View.IOnTouchListener, ScaleGestureDetector.IOnScaleGestureListener
{
    private readonly Action<float>? m_onScale;
    private readonly Action<float, float>? m_onTouch;
    private readonly ScaleGestureDetector m_scaleGestureDetector;

    public TouchScaleListener(Action<float>? onScale, Action<float, float>? onTouch)
    {
        m_onScale = onScale;
        m_onTouch = onTouch;
        
        m_scaleGestureDetector = new ScaleGestureDetector(Application.Context, this);
    }

    public bool OnTouch(View? v, MotionEvent? e)
    {
        if (e is null)
            return true;
        
        m_scaleGestureDetector.OnTouchEvent(e);
        
        if(e.Action == MotionEventActions.Down)
            m_onTouch?.Invoke(e.RawX, e.RawY);
        
        return true;
    }

    public bool OnScale(ScaleGestureDetector detector)
    {
        m_onScale?.Invoke(detector.ScaleFactor);
        return true;
    }

    public bool OnScaleBegin(ScaleGestureDetector detector)
    {
        return true;
    }

    public void OnScaleEnd(ScaleGestureDetector detector) {}
}