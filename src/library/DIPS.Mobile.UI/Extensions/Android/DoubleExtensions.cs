using Android.Util;
using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.Extensions.Android;

public static class DoubleExtensions
{
    public static int ToMauiPixel(this double value) => (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)value, DUI.GetCurrentMauiContext!.Context!.Resources!.DisplayMetrics);
}