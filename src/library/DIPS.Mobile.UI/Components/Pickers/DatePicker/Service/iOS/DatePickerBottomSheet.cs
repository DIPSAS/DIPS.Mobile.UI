using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Inline.iOS;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;

internal class DatePickerBottomSheet : BottomSheet
{
    private readonly InlineDatePicker m_inlineDatePicker;
    private readonly DatePicker m_datePicker;

    public DatePickerBottomSheet(DatePicker datePicker)
    {
        m_datePicker = datePicker;
        m_inlineDatePicker = new InlineDatePicker()
        {
            MaximumDate = datePicker.MaximumDate,
            MinimumDate = datePicker.MinimumDate,
            SelectedDate = datePicker.SelectedDate,
            IgnoreLocalTime = datePicker.IgnoreLocalTime
        };
        Content = m_inlineDatePicker;
    }

    protected override void OnClosed()
    {
        m_datePicker.SelectedDate = m_inlineDatePicker.SelectedDate;
        m_datePicker.SelectedDateCommand?.Execute(null);
        base.OnClosed();
    }
}