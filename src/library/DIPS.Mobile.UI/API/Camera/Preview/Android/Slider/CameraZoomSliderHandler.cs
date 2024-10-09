using Android.Views;
using AndroidX.Camera.Core;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.API.Camera.Preview.Android.Slider;

internal class CameraZoomSliderHandler() : ViewHandler<CameraZoomSlider, Google.Android.Material.Slider.Slider>(PropertyMapper)
{
#nullable disable
    private OnZoomSliderTouchListener m_onZoomSliderListener;
#nullable enable

    public static IPropertyMapper<CameraZoomSlider, CameraZoomSliderHandler> PropertyMapper = new PropertyMapper<CameraZoomSlider, CameraZoomSliderHandler>(ViewMapper)
    {
        [nameof(CameraZoomSlider.ZoomLevel)] = SetZoomLevel
    };

    protected override Google.Android.Material.Slider.Slider CreatePlatformView()
    {
        return new Google.Android.Material.Slider.Slider(Context);
    }

    protected override void ConnectHandler(Google.Android.Material.Slider.Slider platformView)
    {
        base.ConnectHandler(platformView);

        m_onZoomSliderListener = new OnZoomSliderTouchListener(VirtualView.CameraControl);
        platformView.SetOnTouchListener(m_onZoomSliderListener);
        platformView.LabelBehavior = 2; //Disables the label when dragging
        platformView.ContentDescription = DUILocalizedStrings.ZoomLevel;
    }
    
    private static void SetZoomLevel(CameraZoomSliderHandler cameraZoomSliderHandler, CameraZoomSlider cameraZoomSlider)
    {
        cameraZoomSliderHandler.PlatformView.Value = (float)cameraZoomSlider.ZoomLevel;
    }
    
    protected override void DisconnectHandler(Google.Android.Material.Slider.Slider platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.SetOnTouchListener(null);
        platformView.ClearOnSliderTouchListeners();
    }
}

 internal class OnZoomSliderTouchListener : Java.Lang.Object, global::Android.Views.View.IOnTouchListener
 {
     private readonly ICameraControl m_cameraControl;
     private MotionEventActions m_previousAction;

     public OnZoomSliderTouchListener(ICameraControl cameraControl)
     {
         m_cameraControl = cameraControl;
     }

     public bool OnTouch(global::Android.Views.View? v, MotionEvent? e)
     {
         if (e == null) return false;
         if (v is not Google.Android.Material.Slider.Slider slider) return false;

         switch (e.Action)
         {
             case MotionEventActions.ButtonPress:
                 break;
             case MotionEventActions.ButtonRelease:
                 break;
             case MotionEventActions.Cancel:
                 break;
             case MotionEventActions.Down:
                 break;
             case MotionEventActions.HoverEnter:
                 break;
             case MotionEventActions.HoverExit:
                 break;
             case MotionEventActions.HoverMove:
                 break;
             case MotionEventActions.Mask:
                 break;
             case MotionEventActions.Move:
                 if (m_previousAction is MotionEventActions.Down or MotionEventActions.Move)
                 {
                     m_cameraControl.SetLinearZoom(slider.Value);
                 }

                 break;
             case MotionEventActions.Outside:
                 break;
             case MotionEventActions.Pointer1Down:
                 break;
             case MotionEventActions.Pointer1Up:
                 break;
             case MotionEventActions.Pointer2Down:
                 break;
             case MotionEventActions.Pointer2Up:
                 break;
             case MotionEventActions.Pointer3Down:
                 break;
             case MotionEventActions.Pointer3Up:
                 break;
             case MotionEventActions.PointerIdMask:
                 break;
             case MotionEventActions.PointerIdShift:
                 break;
             case MotionEventActions.Up:
                 if (m_previousAction == MotionEventActions.Down)
                 {
                     m_cameraControl.SetLinearZoom(slider.Value);
                 }

                 break;
             default:
                 throw new ArgumentOutOfRangeException();
         }

         m_previousAction = e.Action;
         return false;
     }

     public bool IsZoomAction =>
         m_previousAction is MotionEventActions.Down or MotionEventActions.Move;
 }