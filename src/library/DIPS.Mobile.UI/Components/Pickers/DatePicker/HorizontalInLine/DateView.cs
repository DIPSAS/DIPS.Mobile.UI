using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
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
        var dayNameLabel = CreateLabel(new Label()
        {
            Style = Styles.GetLabelStyle(LabelStyle.UI200)
        });
        dayNameLabel.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.IsSelected, converter: new BoolToObjectConverter()
        {
            TrueObject = Colors.GetColor(ColorName.color_text_default_inverted),
            FalseObject = Colors.GetColor(ColorName.color_text_default),
        });
        dayNameLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (SelectableDateViewModel selectableDateViewModel) => selectableDateViewModel.DayName);
        
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