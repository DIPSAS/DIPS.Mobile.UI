namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared;

public interface IDatePicker : IView
{
    event Action<DateTime?> SelectedDateTimeChanged;
    DatePickerMode Mode { get; }
    void SetSelectedDateTime(DateTime? selectedDate);
    bool ShouldDisplayTodayButton { get; }
    bool IgnoreLocalTime { get; }
    DateTimeKind GetDateTimeKind();
}

public enum DatePickerMode
{
    Date,
    Time,
    DateAndTime
}