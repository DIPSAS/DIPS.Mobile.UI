using Android.Animation;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Buttons.Android;

public class ButtonHandler : Microsoft.Maui.Handlers.ButtonHandler
{
    public ButtonHandler() : base(PropertyMapper, CommandMapper)
    {
    }

    public static IPropertyMapper<Button, ButtonHandler> PropertyMapper = new PropertyMapper<Button, ButtonHandler>(Mapper)
    {
        [nameof(IButton.Background)] = MapBackgroundColor
    };

   
    private static void MapBackgroundColor(ButtonHandler handler, Button button)
    {
        handler.PlatformView.UpdateBackground(button);
        
        // Bug in Android where setting the button to transparent does not remove its shadows, setting StateListAnimator to null removes shadows 
        if(Equals(button.Background, Brush.Transparent) || Equals(button.BackgroundColor, Colors.Transparent))
            handler.PlatformView.StateListAnimator = null;
    }
}