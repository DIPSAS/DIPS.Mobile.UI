using Android.Views;
using Android.Widget;
using AndroidX.Camera.View;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Handlers;
using Color = Android.Graphics.Color;
using Slider = Google.Android.Material.Slider.Slider;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera.Preview;

//Preview: https://developer.android.com/media/camera/camera2/camera-preview
public partial class CameraPreviewHandler : ViewHandler<CameraPreview, RelativeLayout>
{
    private Slider? m_slider;
    private ScaleGestureDetector? m_scaleGestureDetector;
    private OnZoomSliderTouchListener? m_onZoomSliderListener;

    public CameraPreviewHandler() : base(ViewMapper, ViewCommandMapper)
    {
    }

    protected override RelativeLayout CreatePlatformView()
    {
        var relativeLayout = new RelativeLayout(Context);
        relativeLayout.LayoutParameters = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent,
            ViewGroup.LayoutParams.MatchParent);

        var previewView = new PreviewView(Context);
        previewView.LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent,
            ViewGroup.LayoutParams.MatchParent);
        previewView.SetBackgroundColor(Color.Transparent);
        PreviewView = previewView;

        relativeLayout.AddView(PreviewView);
        return relativeLayout;
    }

    public PreviewView PreviewView { get; internal set; }

    //Inspiration = https://proandroiddev.com/android-camerax-tap-to-focus-pinch-to-zoom-zoom-slider-eb88f3aa6fc6
    internal void AddZoomSlider(CameraController cameraController)
    {
        var slider = new Google.Android.Material.Slider.Slider(Context);
        var layoutParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
            ViewGroup.LayoutParams.WrapContent);
        layoutParams.AddRule(LayoutRules.AlignParentBottom);
        layoutParams.LeftMargin = 50.0.ToMauiPixel();
        layoutParams.RightMargin = 50.0.ToMauiPixel();
        layoutParams.BottomMargin = 250.0.ToMauiPixel();
        slider.LayoutParameters = layoutParams;
        slider.LabelBehavior = 2; //Disables the label when dragging
        m_onZoomSliderListener = new OnZoomSliderTouchListener(cameraController);
        slider.SetOnTouchListener(m_onZoomSliderListener);
        slider.ContentDescription = DUILocalizedStrings.ZoomLevel;
        PlatformView.AddView(slider);
        m_slider = slider;
    }


    internal void UpdateZoomSlider(double linearZoom, LifecycleCameraController cameraController)
    {
        if (m_onZoomSliderListener is {IsZoomAction: true}) return; //To prevent awkward slider / zooming sync

        if (m_slider != null)
        {
            m_slider.Value = (float)linearZoom;
        }
    }

    internal void RemoveZoomSlider()
    {
        m_onZoomSliderListener = null;
        
        if (m_slider == null)
        {
            return;
        }

        m_slider.SetOnTouchListener(null);
        m_slider.ClearOnSliderTouchListeners();
        PlatformView.RemoveView(m_slider);
        m_slider = null;
    }

    internal class OnZoomSliderTouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        private readonly CameraController m_cameraController;
        private MotionEventActions m_previousAction;

        public OnZoomSliderTouchListener(CameraController cameraController)
        {
            m_cameraController = cameraController;
        }


        public bool OnTouch(View? v, MotionEvent? e)
        {
            if (e == null) return false;
            if (v is not Slider slider) return false;

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
                        m_cameraController.SetLinearZoom(slider.Value);    
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
                        m_cameraController.SetLinearZoom(slider.Value);
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
}