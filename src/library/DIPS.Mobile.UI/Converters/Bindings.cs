using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.Converters;

public static class Bindings
{
    public static IValueConverter
        ConvertDelegate(Func<object?> convertFunc, Func<object?>? convertBackFunc = null) =>
        new DelegateConverter(convertFunc, convertBackFunc);
}