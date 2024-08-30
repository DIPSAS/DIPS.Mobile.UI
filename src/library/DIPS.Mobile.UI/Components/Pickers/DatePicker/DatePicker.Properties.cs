using System.Windows.Input;
using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker
{
    public partial class DatePicker
    {
        /// <summary>
        /// The date people selected from the date picker.
        /// </summary>
        public DateTime SelectedDate
        {
            get => (DateTime)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        /// <summary>
        /// The command to be executed when people selected a date from the date picker.
        /// </summary>
        public ICommand? SelectedDateCommand
        {
            get => (ICommand)GetValue(SelectedDateCommandProperty);
            set => SetValue(SelectedDateCommandProperty, value);
        }
        
        /// <summary>
        /// If this is false, the <see cref="DatePicker"/> will use local time zone.
        /// If this is true, the <see cref="DatePicker"/> will use UTC time zone.
        /// </summary>
        public bool IgnoreLocalTime
        {
            get => (bool)GetValue(IgnoreLocalTimeProperty);
            set => SetValue(IgnoreLocalTimeProperty, value);
        }
        
        /// <summary>
        /// Sets the minimum date that people can choose
        /// </summary>
        public DateTime? MinimumDate
        {
            get => (DateTime?)GetValue(MinimumDateProperty);
            set => SetValue(MinimumDateProperty, value);
        }
        
        /// <summary>
        /// Sets the maximum date that people can choose
        /// </summary>
        public DateTime? MaximumDate
        {
            get => (DateTime?)GetValue(MaximumDateProperty);
            set => SetValue(MaximumDateProperty, value);
        }

        public DateConverter.DateConverterFormat DateConverterFormat { get; set; }
        public bool ShouldDisplayTodayButton { get; set; }
        public bool ShouldCloseOnDateSelected { get; set; } = true;
        
        public static readonly BindableProperty SelectedDateProperty = BindableProperty.Create(
            nameof(SelectedDate),
            typeof(DateTime),
            typeof(DatePicker),
            defaultBindingMode:BindingMode.TwoWay,
            defaultValue:null,
            propertyChanged: (bindable, _, _) => ((DatePicker)bindable).OnSelectedDateChanged());
        
        public static readonly BindableProperty SelectedDateCommandProperty = BindableProperty.Create(
            nameof(SelectedDateCommand),
            typeof(ICommand),
            typeof(DatePicker));
        
        public static readonly BindableProperty IgnoreLocalTimeProperty = BindableProperty.Create(
            nameof(IgnoreLocalTime),
            typeof(bool),
            typeof(DatePicker));
        
        public static readonly BindableProperty MinimumDateProperty = BindableProperty.Create(
            nameof(MinimumDate),
            typeof(DateTime?),
            typeof(DatePicker));
        
        public static readonly BindableProperty MaximumDateProperty = BindableProperty.Create(
            nameof(MaximumDate),
            typeof(DateTime?),
            typeof(DatePicker));
    }
}