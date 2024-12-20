﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace DIPS.Mobile.UI.Converters.ValueConverters
{
    /// <summary>
    /// A converter that accepts a boolean value as a value and inverts the boolean value as output.
    /// </summary>
    [RequireService([typeof(IXmlLineInfoProvider)])]
    public class InvertedBoolConverter : IMarkupExtension, IValueConverter
    {
        private IServiceProvider? m_serviceProvider;

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
            return this;
        }

        /// <summary>
        /// Converts a boolean value to the inverted value of the boolean value.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns></returns>
        public object Convert(object? value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !bool.TryParse(value.ToString(), out var booleanValue))
            {
                throw new XamlParseException("Value has to be of type boolean").WithXmlLineInfo(m_serviceProvider);
            }

            return !booleanValue;
        }

        /// <summary>
        /// Converts back from to the original value of <see cref="Convert"/>
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="targetType">The type to which to convert the value.</param>
        /// <param name="parameter">A parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !(bool)Convert(value, targetType, parameter, culture);
        }
    }
}
