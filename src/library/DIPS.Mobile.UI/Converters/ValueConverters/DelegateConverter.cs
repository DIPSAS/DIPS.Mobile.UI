using System.Globalization;

namespace DIPS.Mobile.UI.Converters.ValueConverters;

internal class DelegateConverter : IValueConverter
{
    private readonly Func<object?> m_convertAction;
    private readonly Func<object?>? m_convertBackAction;

    public DelegateConverter(Func<object?> convertAction, Func<object?>? convertBackAction = null)
    {
        m_convertAction = convertAction;
        m_convertBackAction = convertBackAction;
    }
    
    public object? Convert(object value, Type targetType, object parameter, CultureInfo culture) => m_convertAction.Invoke();

    public object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => m_convertBackAction?.Invoke();
}