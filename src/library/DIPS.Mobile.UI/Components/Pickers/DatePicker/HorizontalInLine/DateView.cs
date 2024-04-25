using DIPS.Mobile.UI.Converters.ValueConverters;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Pickers.DatePicker.HorizontalInLine;

/// <summary>
/// TODO: Rewrite this so its easy to use publicly if its needed.
/// </summary>
public class DateView : DateViewBase
{
    protected override void OnViewCreated()
    {
        //Day shortname label
        var dayNameLabel = CreateLabel(new Label());
        dayNameLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty,
            new Binding(nameof(SelectableDateViewModel.IsSelected),
                converter: new BoolToObjectConverter()
                {
                    TrueObject = Colors.GetColor(ColorName.color_system_white),
                    FalseObject = Colors.GetColor(ColorName.color_system_black),
                }));
        dayNameLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty,
            new Binding(nameof(SelectableDateViewModel.DayName)));

        if (DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait)
        {
            this.Add(dayNameLabel, 0, 2);
        }
        else
        {
            this.Add(dayNameLabel, 2);
        }
    }
}