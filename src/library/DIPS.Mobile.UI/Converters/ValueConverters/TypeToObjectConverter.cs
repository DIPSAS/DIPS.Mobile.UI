﻿using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace DIPS.Mobile.UI.Converters.ValueConverters
{
    /// <summary>
    /// Converter that can be used to check the type of the binding against a <see cref="Type"/>
    /// </summary>
    [RequireService([typeof(IXmlLineInfoProvider)])]
    public class TypeToObjectConverter : IMarkupExtension, IValueConverter
    {
        private IServiceProvider? m_serviceProvider;

        /// <summary>
        /// The object to return when the binding and <see cref="Type"/> is the same type
        /// </summary>
        public object? TrueObject { get; set; }
        /// <summary>
        /// THe object to return when the binding and <see cref="Type"/> is not the same
        /// </summary>
        public object? FalseObject { get; set; }

        /// <summary>
        /// The type to check against, use {x:Type namespace:MyType}
        /// </summary>
        public Type? Type { get; set; }
        
        /// <summary>
        /// Determines if the expression should be regarded as 'true' as long as the value is an instance of a sub-type of <see cref="Type"/>
        /// </summary>
        public bool IncludeInheritance { get; set; }

        /// <inheritdoc/>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new XamlParseException($"value can not be null").WithXmlLineInfo(m_serviceProvider);
            if (Type == null)
                throw new XamlParseException($"Type to check against can not be null").WithXmlLineInfo(m_serviceProvider);
            if (TrueObject == null)
                throw new XamlParseException($"{nameof(TrueObject)} can not be null").WithXmlLineInfo(m_serviceProvider);
            if (FalseObject == null)
                throw new XamlParseException($"{nameof(FalseObject)} can not be null").WithXmlLineInfo(m_serviceProvider);

            if (IncludeInheritance)
            {
                return Type.IsInstanceOfType(value) ? TrueObject : FalseObject;
            }
            
            return (value.GetType() == Type) ? TrueObject : FalseObject;
        }
        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public object ProvideValue(IServiceProvider serviceProvider) 
        {
            m_serviceProvider = serviceProvider;
            return this;
        }
    }
}
