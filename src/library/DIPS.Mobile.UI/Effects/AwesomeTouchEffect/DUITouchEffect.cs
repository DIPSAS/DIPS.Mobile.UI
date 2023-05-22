using System.Windows.Input;

namespace DIPS.Mobile.UI.Effects.AwesomeTouchEffect;

public partial class DUITouchEffect : RoutingEffect
{
    public static ICommand GetCommand(BindableObject view)
    {
        return (ICommand)view.GetValue(CommandProperty);
    }

    public static void SetCommand(BindableObject view, ICommand command)
    {
        view.SetValue(CommandProperty, command);
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
    
    private static void OnCommandChanged(BindableObject bindable, object oldValue, object? newValue)
    {
        if (bindable is not View view)
        {
            return;
        }

        var shouldHaveContextMenu = newValue != null;
        if (shouldHaveContextMenu)
        {
            view.Effects.Add(new DUITouchEffect());
        }
        else
        {
            var toRemove = view.Effects.FirstOrDefault(e => e is DUITouchEffect);
            if (toRemove != null)
            {
                view.Effects.Remove(toRemove);
            }
        }
    }
}