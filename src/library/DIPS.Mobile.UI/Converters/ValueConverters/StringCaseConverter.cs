using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DIPS.Mobile.UI.Extensions.Markup;

namespace DIPS.Mobile.UI.Converters.ValueConverters
{
    /// <summary>
    /// Uses <see cref="StringCaseExtension"/> to converter a string to a <see cref="StringCase"/>
    /// </summary>
    public class StringCaseConverter : IMarkupExtension, IValueConverter
    {
        /// <summary>
        /// <see cref="StringCase"/>
        /// </summary>
        public StringCase StringCase { get; set; }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        /// <inheritdoc/>
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue))
                return null;
            if (stringValue == string.Empty)
                return string.Empty;

            var stringCaseExtension = new StringCaseExtension() { Input = stringValue, StringCase = StringCase };

            return stringCaseExtension.ProvideValue(null);
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
