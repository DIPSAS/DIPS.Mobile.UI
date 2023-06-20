using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Views;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using Action = System.Action;
using Color = Microsoft.Maui.Graphics.Color;
using View = Android.Views.View;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Effects.Touch;

public partial class TouchPlatformEffect
{
    private readonly Color m_defaultNativeAnimationColor = new(128, 128, 128, 75);

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
        
        var contentDescription = Touch.GetAccessibilityContentDescription(Element);
        
        var colorStateList = new ColorStateList(
            new[] { Array.Empty<int>() },
            new[] { (int)m_defaultNativeAnimationColor.ToPlatform() });
        
        var ripple = new RippleDrawable(colorStateList, ViewGroup == null ? Control.Background : null, null);
        
        if (Control.Background is null)
            Control.Background = ripple;
        else
            Control.Foreground = ripple;
        
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

    protected override partial void OnDetached()
    {
    }
    
}