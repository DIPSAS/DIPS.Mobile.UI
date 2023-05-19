using System.Windows.Input;

namespace DIPS.Mobile.UI.Effects.AwesomeTouchEffect;

public class AwesomeTouchEffect : RoutingEffect
{
    public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command",
        typeof(ICommand),
        typeof(AwesomeTouchEffect),
        null,
        propertyChanged: OnCommandChanged);

    public static ICommand GetCommand(BindableObject view)
    {
        return (ICommand)view.GetValue(CommandProperty);
    }

    public static void SetCommand(BindableObject view, ICommand command)
    {
        view.SetValue(CommandProperty, command);
    }

    public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("Command",
        typeof(object),
        typeof(AwesomeTouchEffect),
        null);

    public static object GetCommandParameter(BindableObject view)
    {
        return view.GetValue(CommandParameterProperty);
    }

    public static void SetCommandParameter(BindableObject view, object obj)
    {
        view.SetValue(CommandParameterProperty, obj);
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
            view.Effects.Add(new AwesomeTouchEffect());
        }
        else
        {
            var toRemove = view.Effects.FirstOrDefault(e => e is AwesomeTouchEffect);
            if (toRemove != null)
            {
                view.Effects.Remove(toRemove);
            }
        }
    }
}