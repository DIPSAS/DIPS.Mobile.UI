namespace DIPS.Mobile.UI.API.Camera.Extensions.Android;

public class StatusAndNavigationBarColors(int statusBarColorInt, int navigationBarColorInt)
{
    public int StatusBarColorInt { get; } = statusBarColorInt;
    public int NavigationBarColorInt { get; } = navigationBarColorInt;
}