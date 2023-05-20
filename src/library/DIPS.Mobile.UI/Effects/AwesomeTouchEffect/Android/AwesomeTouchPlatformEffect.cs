using System.Drawing;
using System.Windows.Input;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;
using AndroidX.ConstraintLayout.Core.Widgets;
using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;
using Point = Microsoft.Maui.Graphics.Point;
using Rectangle = System.Drawing.Rectangle;
using View = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.AwesomeTouchEffect;

public partial class AwesomeTouchPlatformEffect
{
    private readonly Color m_defaultNativeAnimationColor = new(128, 128, 128, 0);
    private FrameLayout? rippleView;

    private bool m_hasRippleDirectlyOnElement;
    
#nullable disable
    private RippleDrawable ripple;
    private ICommand m_command;
    private object m_commandParameter;
    private AccessibilityManager m_accessibilityManager;
    private AccessibilityListener m_accessibilityListener;
#nullable restore

    private bool m_outsideBounds = false;

    private ViewGroup? ViewGroup => Container as ViewGroup;

    protected override partial void OnAttached()
    {
        Control.Clickable = true;
        Control.LongClickable = true;
        Control.Touch += OnTouch;
        
        m_command = AwesomeTouchEffect.GetCommand(Element);
        m_commandParameter = AwesomeTouchEffect.GetCommandParameter(Element);
        
        CreateRipple();
        AddAccessibility();
    }

    private void AddAccessibility()
    {
        m_accessibilityManager = Control.Context?.GetSystemService(Context.AccessibilityService) as AccessibilityManager;
        if (m_accessibilityManager != null)
        {
            m_accessibilityListener = new AccessibilityListener(this);
            m_accessibilityManager.AddAccessibilityStateChangeListener(m_accessibilityListener);
            m_accessibilityManager.AddTouchExplorationStateChangeListener(m_accessibilityListener);
        }
    }

    private void CreateRipple()
    {
        var colorStateList = new ColorStateList(
            new[] { new int[] { } },
            new[] { (int)m_defaultNativeAnimationColor.ToPlatform() });
        
        ripple = new RippleDrawable(colorStateList, null, null);

        if (ViewGroup != null)
        {
            rippleView = new FrameLayout(Container.Context ?? throw new NullReferenceException())
            {
                LayoutParameters = new ViewGroup.LayoutParams(-1, -1),
                Clickable = false,
                Focusable = false,
                Enabled = false,
            };
        
            rippleView.Foreground = ripple;
            
            Control.LayoutChange += OnLayoutChange;
        }
        
        ApplyRipple();
    }

    private void ApplyRipple()
    {
        if (ViewGroup == null)
        {
            Control.Foreground = ripple;
            m_hasRippleDirectlyOnElement = true;
        }
        else
        {
            ViewGroup.AddView(rippleView);
            ViewGroup.SetClipChildren(false);
        }
    }
    
    void OnLayoutChange(object? sender, View.LayoutChangeEventArgs e)
    {
        if (sender is not View view)
            return;

        rippleView!.Right = view.Width;
        rippleView.Bottom = view.Height;
    }

    private void OnTouch(object? sender, View.TouchEventArgs e)
    {
        if (e.Event?.ActionMasked == MotionEventActions.Down)
        {
            OnTouchDown(e.Event.GetX(), e.Event.GetY());
            m_outsideBounds = false;
        }
        else if(e.Event?.ActionMasked == MotionEventActions.Up)
        {
            if(!m_outsideBounds)
                OnTouchUp();
        }
        else if(e.Event?.ActionMasked == MotionEventActions.Move)
        {
            var screenPointerCoords = new System.Drawing.Point((int)(Control.Left + e.Event.GetX()), (int)(Control.Top + e.Event.GetY()));
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
            rippleView!.Enabled = true;
            rippleView.BringToFront();
            ripple.SetHotspot(x, y);
            rippleView.Pressed = true;
        }
    }

    private void OnTouchUp()
    {
        m_command?.Execute(m_commandParameter);
        EndRipple();
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
            if (rippleView!.Pressed)
            {
                rippleView!.Pressed = false;
                rippleView.Enabled = false;
            }
        }
    }
    
    void OnClick(object? sender, EventArgs args)
    {
        m_command?.Execute(m_commandParameter);
    }
    
    private void UpdateClickHandler()
    {
        Control.Click -= OnClick;
        Control.Click += OnClick;
    }

    protected override partial void OnDetached()
    {
        if(!m_hasRippleDirectlyOnElement)
            Control.LayoutChange -= OnLayoutChange;
        
        ripple.Dispose();
    }
    
    sealed class AccessibilityListener : Java.Lang.Object,
        AccessibilityManager.IAccessibilityStateChangeListener,
        AccessibilityManager.ITouchExplorationStateChangeListener
    {
        AwesomeTouchPlatformEffect? platformTouchEffect;

        internal AccessibilityListener(AwesomeTouchPlatformEffect platformTouchEffect)
            => this.platformTouchEffect = platformTouchEffect;

        public AccessibilityListener(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }

        public void OnAccessibilityStateChanged(bool enabled)
            => platformTouchEffect?.UpdateClickHandler();

        public void OnTouchExplorationStateChanged(bool enabled)
            => platformTouchEffect?.UpdateClickHandler();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                platformTouchEffect = null;

            base.Dispose(disposing);
        }
    }
    
}