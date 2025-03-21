using System.Windows.Input;

namespace DIPS.Mobile.UI.Effects.Touch;

public partial class Touch
{
    public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command",
        typeof(ICommand),
        typeof(Touch),
        null,
        propertyChanged: OnTouchPropertiesChanged);
    
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("CommandParameter",
        typeof(object),
        typeof(Touch),
        null);
    
    public static readonly BindableProperty LongPressCommandProperty = BindableProperty.CreateAttached("LongPressCommand",
        typeof(ICommand),
        typeof(Touch),
        null,
        propertyChanged: OnTouchPropertiesChanged);
    
    public static readonly BindableProperty LongPressCommandParameterProperty = BindableProperty.CreateAttached("LongPressCommandParameter",
        typeof(object),
        typeof(Touch),
        null);
    
    public static readonly BindableProperty AccessibilityContentDescriptionProperty = BindableProperty.CreateAttached("AccessibilityContentDescription",
        typeof(string),
        typeof(Touch),
        null);

    public static readonly BindableProperty IsEnabledProperty = BindableProperty.CreateAttached("IsEnabled",
        typeof(bool),
        typeof(Touch),
        true);
}