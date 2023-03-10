using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using DIPS.Mobile.UI.Extensions.Markup;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DIPS.Mobile.UI.Converters.ValueConverters
{
    /// <summary>
    /// Uses <see cref="StringCaseExtension"/> to converter a string to a <see cref="StringCase"/>
    /// </summary>
    public class StringCaseConverter : IMarkupExtension, IValueConverter
    {
        private IServiceProvider m_serviceProvider;

        /// <summary>
        /// <see cref="StringCase"/>
        /// </summary>
        public StringCase StringCase { get; set; }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            m_serviceProvider = serviceProvider;
            return this;
        }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue))
                return null;
            if (stringValue == string.Empty)
                return string.Empty;

            var stringCaseExtension = new StringCaseExtension() { Input = stringValue, StringCase = StringCase };
#nullable disable
            return stringCaseExtension.ProvideValue(null);
#nullable restore
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
