namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared;

public interface INullableDatePicker : IView
{
    bool IsNullable { get; }
    bool IsDateOrTimeNull { get; }
    void SetDateOrTimeNull();
}