using DIPS.Mobile.UI.Components.Pickers.Platforms;
using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePicker : View, IDateTimePicker
{
    public TimePicker()
    {
        BackgroundColor = Colors.GetColor(ColorName.color_secondary_30);
        
#if __IOS__
        // DatePickers on iOS takes up invisible space
        WidthRequest = 100;
#endif
    }
}