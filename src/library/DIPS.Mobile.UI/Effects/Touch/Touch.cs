using System.Windows.Input;

namespace DIPS.Mobile.UI.Effects.Touch;

public partial class Touch : RoutingEffect
{
    public static ICommand GetCommand(BindableObject view)
    {
        return (ICommand)view.GetValue(CommandProperty);
    }

    public static void SetCommand(BindableObject view, ICommand command)
    {
        view.SetValue(CommandProperty, command);
    }
    
    public static ICommand GetLongPressCommand(BindableObject view)
    {
        return (ICommand)view.GetValue(LongPressCommandProperty);
    }

    public static void SetLongPressCommand(BindableObject view, ICommand command)
    {
        view.SetValue(LongPressCommandProperty, command);
    }

    public static object GetLongPressCommandParameter(BindableObject view)
    {
        return view.GetValue(LongPressCommandParameterProperty);
    }
    
    public static void GetLongPressCommandParameter(BindableObject view, object obj)
    {
        view.SetValue(LongPressCommandParameterProperty, obj);
    }
    
    public static object GetCommandParameter(BindableObject view)
    {
        return view.GetValue(CommandParameterProperty);
    }

    public static void SetCommandParameter(BindableObject view, object obj)
    {
        view.SetValue(CommandParameterProperty, obj);
    }

    public static string GetAccessibilityContentDescription(BindableObject view)
    {
        return (string)view.GetValue(AccessibilityContentDescriptionProperty);
    }

    /// <summary>
    /// On Android we have to explicitly set the content description for accessibility
    /// </summary>
    /// <param name="view"></param>
    /// <param name="contentDescription">The string to be read aloud from TalkBack</param>
    public static void SetAccessibilityContentDescription(BindableObject view, string contentDescription)
    {
        view.SetValue(AccessibilityContentDescriptionProperty, contentDescription);
    }

    public static bool GetIsEnabled(BindableObject view)
    {
        return (bool)view.GetValue(IsEnabledProperty);
    }

    public static void SetIsEnabled(BindableObject view, bool isEnabled)
    {
        view.SetValue(IsEnabledProperty, isEnabled);
    }
    
    internal static TouchMode GetTouchMode(BindableObject element)
    {
        if (GetCommand(element) is not null)
        {
            if (GetLongPressCommand(element) is not null)
                return TouchMode.Both;
            
            return TouchMode.Tap;
        }
        
        return TouchMode.LongPress;

    }
    
    private static void OnTouchPropertiesChanged(BindableObject bindable, object oldValue, object? newValue)
    {
        if (bindable is not View view)
            return;
        
        if (newValue is ICommand or true)
        {
            // Refresh
            RemoveEffects(view);
            view.Effects.Add(new Touch());
        }
        else
        {
            RemoveEffects(view);
        }
    }

    private static void RemoveEffects(View view)
    {
        while (view.Effects.Any(e => e is Touch))
        {
            view.Effects.Remove(view.Effects.FirstOrDefault(e => e is Touch));
        }
    }

    internal enum TouchMode
    {
        Tap,
        LongPress,
        Both
    }

    public static Touch? PickFrom(BindableObject? bindable)
    {
        return (bindable as VisualElement)?.Effects.OfType<Touch>().FirstOrDefault();
    }
}
