using System.ComponentModel;
using System.Globalization;

namespace DIPS.Mobile.UI.Converters.ValueConverters;

public class UnderlyingContentTypeConverter : TypeConverter
{
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string text)
            return base.ConvertFrom(context, culture, value);

        return new Components.Labels.Label { Text = text };
    }

  
}