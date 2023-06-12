using DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;
using DIPS.Mobile.UI.Components.Pickers.Platforms;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker
{
    public partial class DatePicker : View, IDateTimePicker
    {
        public DatePicker()
        {
            BackgroundColor = Colors.GetColor(ColorName.color_secondary_30);
        }

        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            
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
    }
}