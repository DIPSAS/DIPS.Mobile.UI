using System.Windows.Input;

namespace DIPS.Mobile.UI.Effects.Touch;

public partial class Touch
{
    public static readonly BindableProperty CommandProperty = BindableProperty.CreateAttached("Command",
        typeof(ICommand),
        typeof(Touch),
        null,
        propertyChanged: OnCommandChanged);
    
    public static readonly BindableProperty CommandParameterProperty = BindableProperty.CreateAttached("Command",
        typeof(object),
        typeof(Touch),
        null);
    
    public static readonly BindableProperty AccessibilityContentDescriptionProperty = BindableProperty.CreateAttached("AccessibilityContentDescription",
        typeof(string),
        typeof(Touch),
        null);
}