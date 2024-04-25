namespace DIPS.Mobile.UI.Components.Pickers.Platforms;

public interface IDateTimePicker : IView
{
    bool IsNullable { get; set; }
    bool IsDateTimeOrTimeSpanDefault { get; }
}