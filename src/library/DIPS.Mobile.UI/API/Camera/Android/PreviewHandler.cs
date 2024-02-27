using Android.Views;
using Android.Widget;
using AndroidX.Camera.View;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;
using Slider = Google.Android.Material.Slider.Slider;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.API.Camera;

//Preview: https://developer.android.com/media/camera/camera2/camera-preview
public partial class PreviewHandler : ViewHandler<Preview, RelativeLayout>
{
    private Slider? m_slider;
    private ScaleGestureDetector? m_scaleGestureDetector;
    private OnZoomSliderTouchListener? m_onZoomSliderListener;

    public PreviewHandler() : base(ViewMapper, ViewCommandMapper)
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
        var slider = new Slider(Context);
        var layoutParams = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.WrapContent,
            ViewGroup.LayoutParams.WrapContent);
        layoutParams.AddRule(LayoutRules.AlignParentBottom);
        layoutParams.LeftMargin = 50;
        layoutParams.RightMargin = 50;
        layoutParams.BottomMargin = 150;
        slider.LayoutParameters = layoutParams;
        m_onZoomSliderListener = new OnZoomSliderTouchListener(cameraController);
        slider.SetOnTouchListener(m_onZoomSliderListener);
        PlatformView.AddView(slider);
        m_slider = slider;
    }
    

    internal void UpdateZoomSlider(double linearZoom, LifecycleCameraController cameraController)
    {
        if (m_onZoomSliderListener is {IsDragging: true}) return; //To prevent awkward slider / zooming sync

        if (m_slider != null)
        {
            m_slider.Value = (float)linearZoom;
        }
    }

    internal void RemoveZoomSlider()
    {
        if (m_slider != null)
        {
            m_slider.ClearOnSliderTouchListeners();
            PlatformView.RemoveView(m_slider);
            m_slider = null;
        }
    }

    internal class OnZoomSliderTouchListener : Java.Lang.Object, View.IOnTouchListener, View.IOnDragListener
    {
        private readonly CameraController m_cameraController;

        public OnZoomSliderTouchListener(CameraController cameraController)
        {
            m_cameraController = cameraController;
        }


        public bool OnTouch(View? v, MotionEvent? e)
        {
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
                    IsDragging = true;
                    if (v is Slider slider)
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
                    if (IsDragging)
                    {
                        IsDragging = false;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }

        public bool IsDragging { get; set; }

        public bool OnDrag(View? v, DragEvent? e)
        {
            return true;
        }
    }
}