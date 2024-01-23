using System.ComponentModel;
using System.Globalization;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace DIPS.Mobile.UI.Converters.ValueConverters;

public class InLineContentTypeConverter : TypeConverter
{
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is not string text)
            return base.ConvertFrom(context, culture, value);

        return new Components.Labels.Label { Text = text, Style = Styles.GetLabelStyle(LabelStyle.ValueInlineWithKey) };
    }

  
}