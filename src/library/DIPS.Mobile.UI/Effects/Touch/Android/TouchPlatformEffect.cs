using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using Action = System.Action;
using Color = Microsoft.Maui.Graphics.Color;
using Rectangle = System.Drawing.Rectangle;
using View = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.Touch;

public partial class TouchPlatformEffect
{
    private readonly Color m_defaultNativeAnimationColor = new(128, 128, 128, 0);
    private FrameLayout? m_rippleView;

    private bool m_hasRippleDirectlyOnElement;
    
#nullable disable
    private RippleDrawable m_ripple;
#nullable restore

    private bool m_outsideBounds;
    private Touch.TouchMode m_touchMode;

    private ViewGroup? ViewGroup => Container as ViewGroup;

    protected override partial void OnAttached()
    {
        m_touchMode = Touch.GetTouchMode(Element);
            
        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both)
        {
            Control.Clickable = true;
            Control.SetOnClickListener(new ClickListener(OnClick));
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both)
        {
            Control.LongClickable = true;
            Control.SetOnLongClickListener(new LongClickListener(OnLongClick));
        }
        
        Control.SetOnTouchListener(new TouchListener(OnTouch));
        
        var contentDescription = Touch.GetAccessibilityContentDescription(Element);
        
        CreateRipple();

        if (string.IsNullOrEmpty(contentDescription))
        {
            Control.ContentDescription = DUILocalizedStrings.Button;
        }
        else
        {
            Control.ContentDescription = $"{contentDescription}. {DUILocalizedStrings.Button}";
        }
    }

    private void OnLongClick()
    {
        Touch.GetLongPressCommand(Element).Execute(Touch.GetLongPressCommandParameter(Element));
    }
    
    private void OnClick()
    {
        Touch.GetCommand(Element).Execute(Touch.GetCommandParameter(Element));
    }

    private void CreateRipple()
    {
        var colorStateList = new ColorStateList(
            new[] { new int[] { } },
            new[] { (int)m_defaultNativeAnimationColor.ToPlatform() });
        
        m_ripple = new RippleDrawable(colorStateList, ViewGroup == null ? Control.Background : null, null);

        if (ViewGroup != null)
        {
            m_rippleView = new FrameLayout(Container.Context ?? throw new NullReferenceException())
            {
                LayoutParameters = new ViewGroup.LayoutParams(-1, -1),
                Clickable = false,
                Focusable = false,
                Enabled = false,
            };
        
            m_rippleView.Foreground = m_ripple;
            
            Control.LayoutChange += OnLayoutChange;
        }
        
        ApplyRipple();
    }

    private void ApplyRipple()
    {
        if (ViewGroup == null)
        {
            Control.Background = m_ripple;
            m_hasRippleDirectlyOnElement = true;
        }
        else
        {
            ViewGroup.AddView(m_rippleView);
            ViewGroup.SetClipChildren(false);
        }
    }
    
    private void OnLayoutChange(object? sender, View.LayoutChangeEventArgs e)
    {
        if (sender is not View view)
            return;

        m_rippleView!.Right = view.Width;
        m_rippleView.Bottom = view.Height;
    }

    private void OnTouch(MotionEvent e)
    {
        if (e.ActionMasked == MotionEventActions.Down)
        {
            OnTouchDown(e.GetX(), e.GetY());
            m_outsideBounds = false;
        }
        else if(e.ActionMasked == MotionEventActions.Up)
        {
            if(!m_outsideBounds)
                EndRipple();
        }
        else if(e.ActionMasked == MotionEventActions.Move)
        {
            var screenPointerCoords = new System.Drawing.Point((int)(Control.Left + e.GetX()), (int)(Control.Top + e.GetY()));
            var viewRect = new Rectangle(Control.Left, Control.Top, Control.Right - Control.Left, Control.Bottom - Control.Top);
            m_outsideBounds = !viewRect.Contains(screenPointerCoords);
            if(m_outsideBounds)
            {
                EndRipple();
            }
        }
    }

    private void OnTouchDown(float x, float y)
    {
        if (m_hasRippleDirectlyOnElement)
        {
            Control.Pressed = true;
        }
        else
        {
            m_rippleView!.Enabled = true;
            m_rippleView.BringToFront();
            m_ripple.SetHotspot(x, y);
            m_rippleView.Pressed = true;
        }
    }

    private void EndRipple()
    {
        if (m_hasRippleDirectlyOnElement)
        {
            if(Control.Pressed)
                Control.Pressed = false;
        }
        else
        {
            if (m_rippleView!.Pressed)
            {
                m_rippleView!.Pressed = false;
                m_rippleView.Enabled = false;
            }
        }
    }

    protected override partial void OnDetached()
    {
        if(!m_hasRippleDirectlyOnElement)
            Control.LayoutChange -= OnLayoutChange;
        
        m_ripple.Dispose();
    }

    internal class TouchListener : Java.Lang.Object, View.IOnTouchListener
    {
        private readonly Action<MotionEvent> m_action;

        public TouchListener(Action<MotionEvent> action)
        {
            m_action = action;
        }
        
        public bool OnTouch(View? v, MotionEvent? e)
        {
            m_action.Invoke(e!);
            return false;
        }
    }

    internal class ClickListener : Java.Lang.Object, View.IOnClickListener
    {
        private readonly Action m_action;

        public ClickListener(Action action)
        {
            m_action = action;
        }
        
        public void OnClick(View? v)
        {
            m_action.Invoke();
        }
    }

    internal class LongClickListener : Java.Lang.Object, View.IOnLongClickListener
    {
        private readonly Action m_action;

        public LongClickListener(Action action)
        {
            m_action = action;
        }
        
        public bool OnLongClick(View? v)
        {
            m_action.Invoke();
            return true;
        }
    }
    
}