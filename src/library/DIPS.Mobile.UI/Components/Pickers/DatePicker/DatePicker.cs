using System.Globalization;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared;
using DIPS.Mobile.UI.Converters.ValueConverters;
using IDatePicker = DIPS.Mobile.UI.Components.Pickers.DatePickerShared.IDatePicker;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker
{
    public partial class DatePicker : Chip, IDatePicker
    {
        public DatePicker()
        {
            Tapped += OnTapped;
        }

        private void OnTapped(object? sender, EventArgs e)
        {
            DatePickerService.Open(this, this);
        }

        protected override void OnHandlerChanging(HandlerChangingEventArgs args)
        {
            base.OnHandlerChanging(args);
            
            if(args.NewHandler is null)
                return;
            
            SelectedDate = ValidateDateTime(SelectedDate);            
            OnSelectedDateChanged();
        }

        protected DateTime ValidateDateTime(DateTime dateTime)
        {
            // SelectedDate should not be above maximum date
            if (dateTime > MaximumDate)
            {
                return new DateTime(MaximumDate.Value.Year, MaximumDate.Value.Month, MaximumDate.Value.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, SelectedDate.Kind);
            }

            // SelectedDate should not be below minimum date
            if (dateTime < MinimumDate)
            {
                return new DateTime(MinimumDate.Value.Year, MinimumDate.Value.Month, MinimumDate.Value.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond, SelectedDate.Kind);
            }

            return DateTime.SpecifyKind(dateTime, SelectedDate.Kind);
        }

        private void OnSelectedDateChanged()
        {
            SetTitle(SelectedDate);
        }

        internal void SetTitle(DateTime selectedDate)
        {
            var convertedDisplayValue =
                new DateConverter { Format = DateConverterFormat, IgnoreLocalTime = IgnoreLocalTime }.Convert(selectedDate,
                    null, null,
                    CultureInfo.CurrentCulture);
            if (convertedDisplayValue is string displayItemAsString)
            {
                Title = displayItemAsString;
            }
        }

        public event Action<DateTime?>? SelectedDateTimeChanged;
        public DatePickerMode Mode => DatePickerMode.Date;

        public virtual void SetSelectedDateTime(DateTime? selectedDate)
        {
            if (selectedDate.HasValue)
            {
                if(selectedDate.Value.Ticks == SelectedDate.Ticks)
                    return;
                
                SelectedDate = ValidateDateTime(selectedDate.Value);
            }

            SelectedDateCommand?.Execute(selectedDate);
            SelectedDateTimeChanged?.Invoke(selectedDate);
        }

        public virtual DateTimeKind GetDateTimeKind()
        {
            return SelectedDate.Kind;
        }

        internal virtual DateTime GetDateOnOpen()
        {
            return ValidateDateTime(SelectedDate);
        }
    }
}