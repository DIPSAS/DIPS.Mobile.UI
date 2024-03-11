namespace DIPS.Mobile.UI.API.Tip;

public static partial class TipService
{
    public static async partial void Show(string message, View anchoredView, int durationInMilliseconds){}
    public static partial void Show(string message, ToolbarItem anchoredToolbarItem, int durationInMilliseconds = 4000){}
}