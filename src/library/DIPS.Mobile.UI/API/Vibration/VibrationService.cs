using DIPS.Mobile.UI.Components.Slidable.Util;

namespace DIPS.Mobile.UI.API.Vibration;

public static partial class VibrationService
{
    public static partial void Vibrate(int duration);

    public static partial void Click();

    public static partial void HeavyClick();

    public static partial void DoubleClick();

    public static partial void Error();

    public static partial void Success();

    public static partial void SelectionChanged();
}