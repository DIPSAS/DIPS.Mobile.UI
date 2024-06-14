using Android.Util;

namespace DIPS.Mobile.UI.Internal.Logging;

internal static partial class DUILogService
{
    private static partial void InternalLogDebug(string tag, string message)
    {
        Log.Debug(tag, message);
    }

    private static partial void InternalLogError(string tag, string message)
    {
        Log.Error(tag, message);
    }
}