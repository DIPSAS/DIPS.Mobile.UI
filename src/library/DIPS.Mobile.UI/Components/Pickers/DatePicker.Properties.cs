using System;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Pickers
{
    public partial class DatePicker
    {
        public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
            nameof(Description),
            typeof(string),
            typeof(DatePicker));

        /// <summary>
        /// The description for people to read when using the date picker.
        /// </summary>
        /// <remarks>This should provide a well suited description on what people are selecting a date for.</remarks>
        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
            nameof(SelectedDate),
            typeof(DateTime),
            typeof(DatePicker), propertyChanged: OnSelectedDateChanged);

        /// <summary>
        /// The date people selected from the date picker.
        /// </summary>
        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public static readonly BindableProperty SelectedDateDisplayFormat = BindableProperty.Create(
            nameof(DateFormat),
            typeof(DateConverter.DateConverterFormat),
            typeof(DatePicker));

        /// <summary>
        /// The format of the <see cref="SelectedDate"/> when displaying it to people.
        /// </summary>
        public DateConverter.DateConverterFormat DateFormat
        {
            get => (DateConverter.DateConverterFormat)GetValue(SelectedDateDisplayFormat);
            set => SetValue(SelectedDateDisplayFormat, value);
        }
    }
}