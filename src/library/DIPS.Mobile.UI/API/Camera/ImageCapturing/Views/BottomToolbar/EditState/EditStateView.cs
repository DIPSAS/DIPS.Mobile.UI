using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.EditState;

internal class EditStateView : Grid
{
    public EditStateView(Action onDoneButtonTapped, Action onCancelButtonTapped, Action onRotateButtonTapped)
    {
        VerticalOptions = LayoutOptions.Center;
        
        Add(new ButtonWithText(DUILocalizedStrings.Cancel, Icons.GetIcon(IconName.close_line), onCancelButtonTapped)
        {
            HorizontalOptions = LayoutOptions.Start
        });

        Add(new ButtonWithText(DUILocalizedStrings.Rotate, Icons.GetIcon(IconName.arrow_back_line), onRotateButtonTapped)
        {
            HorizontalOptions = LayoutOptions.Center
        });
        
        Add(new ButtonWithText(DUILocalizedStrings.Done, Icons.GetIcon(IconName.check_line), onDoneButtonTapped)
        {
            HorizontalOptions = LayoutOptions.End
        });
    }
}