using DIPS.Mobile.UI.Components.Pickers.DateTimePickers;
using Foundation;
using UIKit;
using DatePicker = DIPS.Mobile.UI.Components.Pickers.DateTimePickers.DatePicker;

namespace DIPS.Mobile.UI.Components.Pickers.iOS.Date
{
    internal class UIDatePickerViewController : UIDateTimePickerViewController
    {
        private readonly DatePicker m_datePicker;

        public UIDatePickerViewController(IDateTimePicker dateTimePicker) : base(dateTimePicker)
        {
            m_datePicker = (dateTimePicker as DatePicker)!;
        }

        protected override void SetDateTimePickerMode(UIDatePicker uiDatePicker)
        {
            uiDatePicker.Mode = UIDatePickerMode.Date;
        }

        protected override void SetDateTime(UIDatePicker uiDatePicker)
        {
            uiDatePicker.SetDate(m_datePicker.SelectedDate, m_datePicker);
        }

        protected override void OnFinished(UIDatePicker uiDatePicker)
        {
            m_datePicker.SelectedDate = (System.DateTime)uiDatePicker.Date;
        }
    }
}