namespace DIPS.Mobile.UI.Internal.Logging;

/// <summary>
/// Based on: https://learn.microsoft.com/en-us/previous-versions/xamarin/ios/deploy-test/debugging-in-xamarin-ios?tabs=macos#accessing-the-console
/// </summary>
internal static partial class DUILogService
{
    private static partial void InternalLogDebug(string tag, string message)
    {
        Console.WriteLine($@"DEBUG: {tag} - {message}");
    }

    private static partial void InternalLogError(string tag, string message)
    {
        Console.WriteLine($@"ERROR: {tag} - {message}");
    }
}