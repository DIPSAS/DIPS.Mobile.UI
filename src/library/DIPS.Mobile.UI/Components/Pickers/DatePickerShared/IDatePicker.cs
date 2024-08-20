namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared;

public interface IDatePicker : IView
{
    event Action<DateTime?> SelectedDateTimeChanged;
    DatePickerMode Mode { get; }
    void SetSelectedDateTime(DateTime? selectedDate);
    bool DisplayTodayButton { get; }
    bool IsNullable();
    bool IgnoreLocalTime { get; }
    DateTimeKind GetKind();
}

public enum DatePickerMode
{
    Date,
    Time,
    DateAndTime
}