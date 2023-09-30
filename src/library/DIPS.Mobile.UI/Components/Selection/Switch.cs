namespace DIPS.Mobile.UI.Components.Selection;

public class Switch : Microsoft.Maui.Controls.Switch
{
#if __ANDROID__
    public Switch()
    {
        HeightRequest = 0; //Bug: On Android, the component takes more space than it actually is.
    }
#endif    
}