using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.ConfirmState;

internal class ConfirmStateView : Grid
{
    public ConfirmStateView(IConfirmStateObserver confirmStateObserver)
    {
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        AddColumnDefinition(new ColumnDefinition(GridLength.Star));
        
        VerticalOptions = LayoutOptions.Center;

        var doneButton =
            new ButtonWithText(DUILocalizedStrings.UsePicture, Icons.GetIcon(IconName.check_line), confirmStateObserver.OnUsePhotoButtonTapped)
            {
                HorizontalOptions = LayoutOptions.End
            };

        var retakeButton =
            new ButtonWithText(DUILocalizedStrings.ReTakePhoto, Icons.GetIcon(IconName.arrow_back_line), confirmStateObserver.OnRetakePhotoButtonTapped)
            {
                HorizontalOptions = LayoutOptions.Start
            };
        
        Add(retakeButton);
        this.Add(doneButton, 1);
    }
}