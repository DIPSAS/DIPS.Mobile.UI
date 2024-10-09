using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.ConfirmState;

internal class ConfirmStateView : Grid
{
    public ConfirmStateView(Action? usePhoto, Action? retakePhoto)
    {
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        
        VerticalOptions = LayoutOptions.Center;

        var doneButton =
            new ButtonWithText(DUILocalizedStrings.UsePicture, Icons.GetIcon(IconName.check_line), usePhoto)
            {
                HorizontalOptions = LayoutOptions.End
            };

        var retakeButton =
            new ButtonWithText(DUILocalizedStrings.ReTakePhoto, Icons.GetIcon(IconName.arrow_back_line), retakePhoto)
            {
                HorizontalOptions = LayoutOptions.Start
            };
        
        Add(retakeButton);
        this.Add(doneButton, 1);
    }
}