using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views;

public class ConfirmStateGrid : Grid
{
    public ConfirmStateGrid(Action usePhoto, Action retakePhoto)
    {
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        
        var retakeButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.arrow_back_line),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            BackgroundColor = Colors.GetColor(ColorName.color_neutral_90),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            Command = new Command(retakePhoto)
        };
        
        var doneButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.check_line),
            ImageTintColor = Colors.GetColor(ColorName.color_system_white),
            BackgroundColor = Colors.GetColor(ColorName.color_neutral_90),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            Command = new Command(usePhoto)
        };
        
        var doneLabel = new Label
        {
            Text = DUILocalizedStrings.UsePicture,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            TextColor = Colors.GetColor(ColorName.color_system_white),
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_3), 0, 0)
        };
        
        var retakeLabel = new Label
        {
            Text = DUILocalizedStrings.TakeNewPhoto,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            TextColor = Colors.GetColor(ColorName.color_system_white),
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_3), 0, 0)
        };

        var leftColumn = new VerticalStackLayout
        {
            Spacing = 0,
            Children = { retakeButton, retakeLabel },
            HorizontalOptions = LayoutOptions.Start
        };
        
        var rightColumn = new VerticalStackLayout
        {
            Spacing = 0,
            Children = { doneButton, doneLabel },
            HorizontalOptions = LayoutOptions.End
        };
        
        Add(leftColumn);
        this.Add(rightColumn, 1);
    }
}