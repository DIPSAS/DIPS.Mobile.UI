namespace DIPS.Mobile.UI.Components.SystemMessage;

public class SystemMessage : ContentView
{
    public SystemMessage()
    {
        var label = new Label() { Text = "Test!" };

        Content = label;
    }
}