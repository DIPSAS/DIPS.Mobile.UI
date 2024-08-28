using Android.Util;
using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.Extensions.Android;

public static class IntExtensions
{
    public static int ToMauiPixel(this int value) => (int)TypedValue.ApplyDimension(ComplexUnitType.Dip, value, DUI.GetCurrentMauiContext!.Context!.Resources!.DisplayMetrics);

}