using DIPS.Mobile.UI.Components.Slidable.Util;
using DIPS.Mobile.UI.Exceptions;

namespace DIPS.Mobile.UI.API.Vibration;

public static partial class VibrationService
{
    public static partial void Vibrate(int duration) => throw new Only_Here_For_UnitTests();

    public static partial void Click() => throw new Only_Here_For_UnitTests();

    public static partial void HeavyClick() => throw new Only_Here_For_UnitTests();

    public static partial void DoubleClick() => throw new Only_Here_For_UnitTests();

    public static partial void Error() => throw new Only_Here_For_UnitTests();

    public static partial void Success() => throw new Only_Here_For_UnitTests();

    public static partial IPlatformFeedbackGenerator Generate() => throw new Only_Here_For_UnitTests();
}