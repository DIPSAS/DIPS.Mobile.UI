using DIPS.Mobile.UI.Components.Pickers.Platforms;

namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePicker : View, IDateTimePicker
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
                SelectedDateTime.Hour, SelectedDateTime.Minute, SelectedDateTime.Second, SelectedDateTime.Kind);
        }

        // SelectedDate should not be below minimum date
        if (MinimumDate != null && SelectedDateTime < MinimumDate)
        {
            SelectedDateTime = new DateTime(MinimumDate.Value.Year,
                MinimumDate.Value.Month,
                MinimumDate.Value.Day,
                SelectedDateTime.Hour, SelectedDateTime.Minute, SelectedDateTime.Second, SelectedDateTime.Kind);
        }
    }
}