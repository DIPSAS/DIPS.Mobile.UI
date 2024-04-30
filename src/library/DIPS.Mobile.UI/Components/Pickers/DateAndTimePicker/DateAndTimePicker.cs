using DIPS.Mobile.UI.Components.Pickers.DatePickerShared;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePicker : View, INullableDatePicker
{
    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        // SelectedDate should not be above maximum date
        if (MaximumDate != null && SelectedDateTime > MaximumDate)
        {
            SelectedDateTime = new DateTime(MaximumDate.Value.Year,
                MaximumDate.Value.Month,
                MaximumDate.Value.Day,
                SelectedDateTime.Value.Hour, SelectedDateTime.Value.Minute, SelectedDateTime.Value.Second, SelectedDateTime.Value.Kind);
        }

        // SelectedDate should not be below minimum date
        if (MinimumDate != null && SelectedDateTime < MinimumDate)
        {
            SelectedDateTime = new DateTime(MinimumDate.Value.Year,
                MinimumDate.Value.Month,
                MinimumDate.Value.Day,
                SelectedDateTime.Value.Hour, SelectedDateTime.Value.Minute, SelectedDateTime.Value.Second, SelectedDateTime.Value.Kind);
        }
    }

    public bool IsNullable { get; set; }
    public bool IsDateOrTimeNull => SelectedDateTime is null;

    public void SetDateOrTimeNull()
    {
        SelectedDateTime = null;
        SelectedDateTimeCommand?.Execute(null);
    }
}