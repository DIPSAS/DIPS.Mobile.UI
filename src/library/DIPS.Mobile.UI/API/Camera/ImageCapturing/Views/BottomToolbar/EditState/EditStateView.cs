using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.EditState;

internal class EditStateView : Grid
{
    public EditStateView(IEditStateObserver editStateObserver)
    {
        VerticalOptions = LayoutOptions.Center;
        
        Add(new Button
        {
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Start,
            Text = DUILocalizedStrings.Cancel,
            Style = Styles.GetButtonStyle(ButtonStyle.GhostSmall),
            Command = new Command(editStateObserver.OnCancelButtonTapped)
        });
        
        Add(new Button
        {
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            Text = DUILocalizedStrings.Save,
            Style = Styles.GetButtonStyle(ButtonStyle.GhostSmall),
            Command = new Command(editStateObserver.OnSaveButtonTapped)
        });

        
    }
}