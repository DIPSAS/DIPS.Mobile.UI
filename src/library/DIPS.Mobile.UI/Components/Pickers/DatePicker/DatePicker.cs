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

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            
            OnSelectedDateChanged();
            
            // SelectedDate should not be above maximum date
            if (MaximumDate != null && SelectedDate > MaximumDate)
            {
                var kind = SelectedDate.Kind;
                SelectedDate = DateTime.SpecifyKind(MaximumDate.Value, kind);
            }

            // SelectedDate should not be below minimum date
            if (MinimumDate != null && SelectedDate < MinimumDate)
            {
                var kind = SelectedDate.Kind;
                SelectedDate = DateTime.SpecifyKind(MinimumDate.Value, kind);
            }
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
                SelectedDate = selectedDate.Value;
            }
            
            SelectedDateCommand?.Execute(null);
            SelectedDateTimeChanged?.Invoke(selectedDate);
        }

        public virtual bool IsNullable()
        {
            return false;
        }

        public virtual DateTimeKind GetKind()
        {
            return SelectedDate.Kind;
        }

#if __IOS__
        internal virtual DateTime SetSelectedDateOnPopoverOpen()
        {
            return SelectedDate;
        }
#endif
    }
}