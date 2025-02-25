using DIPS.Mobile.UI.Converters.ValueConverters;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

public class DateTimeView : DateViewBase
{
    protected override void OnViewCreated()
    {
        //Time of day label
        var timeOfDayLabel = CreateLabel(new Label(), SizeName.content_margin_medium);
        timeOfDayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_text_on_action),
            FalseObject = Colors.GetColor(ColorName.color_text_default),
        });
        
        timeOfDayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.FormattedTime);
        timeOfDayLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.FormattedTime);

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