using System.ComponentModel;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views.Accessibility;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using Button = Android.Widget.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using View = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.Touch;

public partial class TouchPlatformEffect
{
    internal static readonly Color s_defaultNativeAnimationColor = Colors.GetColor(ColorName.color_fill_button_cta_hover, 0.1f);

    private Touch.TouchMode m_touchMode;

    private bool m_changedBackground;
    private Drawable? m_defaultBackground;

    private partial void Init()
    {
        m_touchMode = Touch.GetTouchMode(Element);
        
        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both)
        {
            Control.Clickable = true;
            Control.Click -= OnClick;
            Control.Click += OnClick;
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both)
        {
            Control.LongClickable = true;
            Control.LongClick -= OnLongClick;
            Control.LongClick += OnLongClick;
        }
        
        var colorStateList = new ColorStateList(
            [[]],
            [s_defaultNativeAnimationColor.ToPlatform()]);
        
        var existingForeground = Control.Foreground;

        // Only replace if there’s something to ripple over
        if (existingForeground != null)
        {
            var ripple = new RippleDrawable(
                colorStateList,
                existingForeground,
                new ColorDrawable(Android.Graphics.Color.White));

            m_defaultBackground = existingForeground;
            Control.Foreground = ripple;
        }
        else
        {
            // fallback if no foreground drawable
            var ripple = new RippleDrawable(
                colorStateList,
                null,
                new ColorDrawable(Android.Graphics.Color.White));

            if (Control.Background is null)
            {
                m_changedBackground = true; 
                m_defaultBackground = Control.Background; 
                Control.Background = ripple;
            }
            else
            {
                m_defaultBackground = Control.Foreground;
                Control.Foreground = ripple;
            }
        }
    }

    private partial void OnAccessibilityDescriptionSet()
    {
        var existingDelegate = Control.GetAccessibilityDelegate();
        
        // We don't want TalkBack to only say "Button"
        if(existingDelegate is null)
            return;
        
        Control.SetAccessibilityDelegate(new TouchAccessibilityDelegate(existingDelegate));
    }
    
    private void OnLongClick(object? sender, View.LongClickEventArgs longClickEventArgs)
    {
        Touch.GetLongPressCommand(Element)?.Execute(Touch.GetLongPressCommandParameter(Element));
    }
    
    private void OnClick(object? sender, EventArgs eventArgs)
    {
        Touch.GetCommand(Element)?.Execute(Touch.GetCommandParameter(Element));
    }

    private partial void Dispose(bool isDetaching)
    {
        if (m_touchMode is Touch.TouchMode.Tap or Touch.TouchMode.Both)
        {
            Control.Click -= OnClick;
            if(!Control.HasOnClickListeners)
                Control.Clickable = false;
        }

        if (m_touchMode is Touch.TouchMode.LongPress or Touch.TouchMode.Both)
        {
            Control.LongClick -= OnLongClick;
            if(!Control.HasOnLongClickListeners)
                Control.LongClickable = false;
        }
        
        Control.ContentDescription = null;

        if (m_changedBackground)
        {
            Control.Background = m_defaultBackground;
        }
        else
        {
            Control.Foreground = m_defaultBackground;
        }
    }
    
}

/// <summary>
/// Extends the accessibility delegate to set the class name to Button
/// <remarks>TalkBack will then say `Button`</remarks>
/// </summary>
internal class TouchAccessibilityDelegate : View.AccessibilityDelegate
{
    private readonly View.AccessibilityDelegate? m_existingDelegate;

    public TouchAccessibilityDelegate(View.AccessibilityDelegate? existingDelegate)
    {
        m_existingDelegate = existingDelegate;
    }

    public override void OnInitializeAccessibilityNodeInfo(View host, AccessibilityNodeInfo info)
    {
        base.OnInitializeAccessibilityNodeInfo(host, info);
        
        m_existingDelegate?.OnInitializeAccessibilityNodeInfo(host, info);
        
        info.ClassName = "android.widget.Button";
    }
}