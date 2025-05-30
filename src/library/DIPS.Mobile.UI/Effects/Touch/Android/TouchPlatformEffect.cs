using Android.Content.Res;
using Android.Graphics.Drawables;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using Color = Microsoft.Maui.Graphics.Color;
using Colors = Microsoft.Maui.Graphics.Colors;
using View = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.Touch;

public partial class TouchPlatformEffect
{
    internal static readonly Color s_defaultNativeAnimationColor = new(128, 128, 128, 75);

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
        
        var contentDescription = Touch.GetAccessibilityContentDescription(Element);
        
        var colorStateList = new ColorStateList(
            [[]],
            [s_defaultNativeAnimationColor.ToPlatform()]);
        
        var ripple = new RippleDrawable(colorStateList, null, new ColorDrawable(Colors.White.ToPlatform()));
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
        
        if (!string.IsNullOrEmpty(contentDescription))
        {
            Control.ContentDescription = $"{contentDescription}. {DUILocalizedStrings.Button}";
        }
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