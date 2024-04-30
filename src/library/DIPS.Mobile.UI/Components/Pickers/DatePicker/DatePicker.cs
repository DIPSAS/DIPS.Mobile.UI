using DIPS.Mobile.UI.Components.Pickers.DatePickerShared;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker
{
    public partial class DatePicker : View, INullableDatePicker
    {
        protected override void OnHandlerChanged()
        {
            base.OnHandlerChanged();
            
            // SelectedDate should not be above maximum date
            if (MaximumDate != null && SelectedDate > MaximumDate)
            {
                var kind = SelectedDate.Value.Kind;
                SelectedDate = DateTime.SpecifyKind(MaximumDate.Value, kind);
            }

            // SelectedDate should not be below minimum date
            if (MinimumDate != null && SelectedDate < MinimumDate)
            {
                var kind = SelectedDate.Value.Kind;
                SelectedDate = DateTime.SpecifyKind(MinimumDate.Value, kind);
            }
        }

        public bool IsNullable { get; set; }
        public bool IsDateOrTimeNull => SelectedDate is null;

        public void SetDateOrTimeNull()
        {
            SelectedDate = null;
            SelectedDateCommand?.Execute(null);
        }
    }
}