using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.Internal.Logging;

internal static partial class DUILogService
{
    private const string LogTag = "DIPS.Mobile.UI DUI";

    /// <summary>
    /// Will log errors that people can see in the application log.
    /// </summary>
    /// <param name="message">The message to log as an error.</param>
    internal static void LogError<T>(string message) where T : class
    {
        LogError(nameof(T), message);
    }

    /// <summary>
    /// Will log errors that people can see in the application log.
    /// </summary>
    /// <param name="context">The context that you are logging from. Do use nameof(class) that is logging. </param>
    /// <param name="message">The message to log as an error.</param>
    internal static void LogError(string context, string message)
    {
        Console.WriteLine($@"ERROR: {context} - {message}");
    }

    /// <summary>
    /// Will log traces that people can see in the application log.
    /// </summary>
    /// <param name="message">The message to log to traces.</param>
    /// <remarks>Will only be visible if the application is run under debug and the consumer has enabled <see cref="DUI.ShouldLogDebug"/></remarks>
    internal static void LogDebug<T>(string message) where T : class
    {
        LogDebug(typeof(T).ToString(), message);
    }

    /// <summary>
    /// Will log traces that people can see in the application log.
    /// </summary>
    /// /// <param name="context">The context that you are logging from. Do use nameof(class) that is logging. </param>
    /// <param name="message">The message to log to traces.</param>
    /// <remarks>Will only be visible if the application is run under debug and the consumer has enabled <see cref="DUI.ShouldLogDebug"/></remarks>
    internal static void LogDebug(string context, string message)
    {
        if (DUI.IsDebug && DUI.ShouldLogDebug)
        {
            Console.WriteLine($@"DEBUG: {context} - {message}");
        }   
    }
}