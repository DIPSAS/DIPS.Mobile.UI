using DIPS.Mobile.UI.Converters.ValueConverters;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

public class DateTimeView : DateViewBase
{
    protected override void OnViewCreated()
    {
        //Time of day label
        var timeOfDayLabel = CreateLabel(new Label(), SizeName.size_3);
        timeOfDayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_system_white),
                    FalseObject = Colors.GetColor(ColorName.color_system_black),
                }));
        timeOfDayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty,
            new Binding(nameof(SelectableDateViewModel.FormattedTime)));

        if (DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait)
        {
            this.Add(timeOfDayLabel, 0, 2);
        }
        else
        {
            this.Add(timeOfDayLabel, 2);
        }
    }
}