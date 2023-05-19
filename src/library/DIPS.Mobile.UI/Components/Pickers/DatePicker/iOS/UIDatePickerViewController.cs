using DIPS.Mobile.UI.Components.Pickers.Platforms.iOS;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS
{
    /// <summary>
    /// Will finish implementing later
    /// </summary>
    internal class UIDatePickerViewController : UIDateTimePickerViewController
    {
        private readonly DatePicker m_datePicker;

        public UIDatePickerViewController(DatePicker datePicker) : base(datePicker)
        {
            m_datePicker = datePicker!;
        }

        protected override void SetDateTimePickerMode(UIDatePicker uiDatePicker)
        {
            uiDatePicker.Mode = UIDatePickerMode.Date;
        }

        protected override void SetDateTime(UIDatePicker uiDatePicker)
        {
           //uiDatePicker.SetDate(m_datePicker.SelectedDate, m_datePicker);
        }

        protected override void OnFinished(UIDatePicker uiDatePicker)
        {
            m_datePicker.SelectedDate = (DateTime)uiDatePicker.Date;
        }
    }
}