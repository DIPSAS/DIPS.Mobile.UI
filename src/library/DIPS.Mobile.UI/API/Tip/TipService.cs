namespace DIPS.Mobile.UI.API.Tip;

public static partial class TipService
{
    /// <summary>
    /// Show a tip with a <see cref="message"/> anchored to the <see cref="anchoredView"/> for <see cref="durationInMilliseconds"/> ms.
    /// </summary>
    /// <param name="message">The message to display in the tip.</param>
    /// <param name="anchoredView">The view to anchor the tip to.</param>
    /// <param name="durationInMilliseconds">The duration of the tip.</param>
    public static partial void Show(string message, View anchoredView, int durationInMilliseconds = 4000);
}