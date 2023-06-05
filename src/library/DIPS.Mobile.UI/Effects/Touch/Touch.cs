using System.Windows.Input;

namespace DIPS.Mobile.UI.Effects.Touch;

public partial class Touch : RoutingEffect
{
    public static ICommand GetCommand(BindableObject view)
    {
        return (ICommand)view.GetValue(Touch.CommandProperty);
    }

    public static void SetCommand(BindableObject view, ICommand command)
    {
        view.SetValue(Touch.CommandProperty, command);
    }

    public static object GetCommandParameter(BindableObject view)
    {
        return view.GetValue(Touch.CommandParameterProperty);
    }

    public static void SetCommandParameter(BindableObject view, object obj)
    {
        view.SetValue(Touch.CommandParameterProperty, obj);
    }

    public static string GetAccessibilityContentDescription(BindableObject view)
    {
        return (string)view.GetValue(Touch.AccessibilityContentDescriptionProperty);
    }

    /// <summary>
    /// On Android we have to explicitly set the content description for accessibility
    /// </summary>
    /// <param name="view"></param>
    /// <param name="contentDescription">The string to be read aloud from TalkBack</param>
    public static void SetAccessibilityContentDescription(BindableObject view, string contentDescription)
    {
        view.SetValue(Touch.AccessibilityContentDescriptionProperty, contentDescription);
    }
    
    private static void OnCommandChanged(BindableObject bindable, object oldValue, object? newValue)
    {
        if (bindable is not View view)
        {
            return;
        }
        
        if (newValue is ICommand)
        {
            view.Effects.Add(new Touch());
        }
        else
        {
            var toRemove = view.Effects.FirstOrDefault(e => e is Touch);
            if (toRemove != null)
            {
                view.Effects.Remove(toRemove);
            }
        }
    }
}
