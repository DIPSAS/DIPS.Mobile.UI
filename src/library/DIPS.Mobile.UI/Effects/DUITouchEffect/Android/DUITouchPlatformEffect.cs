using System.Windows.Input;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;
using Rectangle = System.Drawing.Rectangle;
using View = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.DUITouchEffect;

public partial class DUITouchPlatformEffect
{
    private readonly Color m_defaultNativeAnimationColor = new(128, 128, 128, 0);
    private FrameLayout? m_rippleView;

    private bool m_hasRippleDirectlyOnElement;
    
#nullable disable
    private RippleDrawable m_ripple;
    private ICommand m_command;
    private object m_commandParameter;
    private AccessibilityManager m_accessibilityManager;
    private AccessibilityListener m_accessibilityListener;
#nullable restore

    private bool m_outsideBounds;

    private ViewGroup? ViewGroup => Container as ViewGroup;

    protected override partial void OnAttached()
    {
        Control.Clickable = true;
        Control.Touch += OnTouch;
        
        m_command = DUITouchEffect.GetCommand(Element);
        m_commandParameter = DUITouchEffect.GetCommandParameter(Element);
        var contentDescription = DUITouchEffect.GetAccessibilityContentDescription(Element);
        
        CreateRipple();
        AddAccessibility();

        if (string.IsNullOrEmpty(contentDescription))
        {
            Control.ContentDescription = DUILocalizedStrings.Button;
        }
        else
        {
            Control.ContentDescription = $"{contentDescription}. {DUILocalizedStrings.Button}";
        }
    }

    private void AddAccessibility()
    {
        m_accessibilityManager = Control.Context?.GetSystemService(Context.AccessibilityService) as AccessibilityManager;
        if (m_accessibilityManager == null)
            return;

        m_accessibilityListener = new AccessibilityListener(this);
        m_accessibilityManager.AddAccessibilityStateChangeListener(m_accessibilityListener);
        m_accessibilityManager.AddTouchExplorationStateChangeListener(m_accessibilityListener);
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
    
    void OnLayoutChange(object? sender, View.LayoutChangeEventArgs e)
    {
        if (sender is not View view)
            return;

        m_rippleView!.Right = view.Width;
        m_rippleView.Bottom = view.Height;
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
            m_rippleView!.Enabled = true;
            m_rippleView.BringToFront();
            m_ripple.SetHotspot(x, y);
            m_rippleView.Pressed = true;
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
            if (m_rippleView!.Pressed)
            {
                m_rippleView!.Pressed = false;
                m_rippleView.Enabled = false;
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
        
        m_ripple.Dispose();
        Control.Click -= OnClick;
    }
    
    sealed class AccessibilityListener : Java.Lang.Object,
        AccessibilityManager.IAccessibilityStateChangeListener,
        AccessibilityManager.ITouchExplorationStateChangeListener
    {
        DUITouchPlatformEffect? m_platformTouchEffect;

        internal AccessibilityListener(DUITouchPlatformEffect platformTouchEffect)
            => this.m_platformTouchEffect = platformTouchEffect;

        public void OnAccessibilityStateChanged(bool enabled)
            => m_platformTouchEffect?.UpdateClickHandler();

        public void OnTouchExplorationStateChanged(bool enabled)
            => m_platformTouchEffect?.UpdateClickHandler();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_platformTouchEffect = null;
            }

            base.Dispose(disposing);
        }
    }
    
}