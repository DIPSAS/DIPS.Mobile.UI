using DIPS.Mobile.UI.Components.Pickers.Platforms;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePicker : View, IDateTimePicker
{
    public TimePicker()
    {
        BackgroundColor = Colors.GetColor(ColorName.color_secondary_30);
    }

    public bool IsNullable { get; set; }
    public bool IsDateTimeOrTimeSpanDefault => IsNullable && SelectedTime == default;
}