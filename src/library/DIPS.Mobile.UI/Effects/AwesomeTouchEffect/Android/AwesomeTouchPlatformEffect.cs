using System.Windows.Input;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Microsoft.Maui.Platform;
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
#nullable restore

    private ViewGroup? ViewGroup => Container as ViewGroup;

    protected override partial void OnAttached()
    {
        Control.Clickable = true;
        Control.LongClickable = true;
        Control.Touch += OnTouch;
        
        m_command = AwesomeTouchEffect.GetCommand(Element);
        m_commandParameter = AwesomeTouchEffect.GetCommandParameter(Element);
        
        CreateRipple();
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
        }
        else if(e.Event?.ActionMasked == MotionEventActions.Up)
        {
            OnTouchUp();
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

    protected override partial void OnDetached()
    {
        if(!m_hasRippleDirectlyOnElement)
            Control.LayoutChange -= OnLayoutChange;
        
        ripple.Dispose();
    }
}