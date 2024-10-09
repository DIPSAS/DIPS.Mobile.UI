using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.EditState;

internal class EditStateView : Grid
{
    public EditStateView(IEditStateObserver editStateObserver)
    {
        VerticalOptions = LayoutOptions.Center;
        
        Add(new ButtonWithText(DUILocalizedStrings.Cancel, Icons.GetIcon(IconName.close_line), editStateObserver.OnCancelButtonTapped)
        {
            HorizontalOptions = LayoutOptions.Start
        });

        Add(new ButtonWithText(DUILocalizedStrings.Rotate, Icons.GetIcon(IconName.arrow_back_line), () => _ = editStateObserver.OnRotateButtonTapped())
        {
            HorizontalOptions = LayoutOptions.Center
        });
        
        Add(new ButtonWithText(DUILocalizedStrings.Done, Icons.GetIcon(IconName.check_line), editStateObserver.OnDoneButtonTapped)
        {
            HorizontalOptions = LayoutOptions.End
        });
    }
}