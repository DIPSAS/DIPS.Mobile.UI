using DIPS.Mobile.UI.API.Camera.ImageCapturing.BottomSheets;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.TopToolbar;

internal class TopToolbarView : Grid
{
    public TopToolbarView(ImageCaptureSettings imageCaptureSettings, Action onBottomSheetSavedWithChanges)
    {
        var settingsButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            ImageSource = Icons.GetIcon(IconName.settings_fill),
            ImageTintColor = Colors.White,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            Command = new Command(() =>
            {
                new ImageCaptureSettingsBottomSheet(imageCaptureSettings, onBottomSheetSavedWithChanges).Open();
            })
        };

        var doneButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostLarge),
            Text = DUILocalizedStrings.Done,
            TextColor = Colors.White,
            Command = imageCaptureSettings.DoneButtonCommand,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
        };
        
        Add(settingsButton);
        
        if(imageCaptureSettings.DoneButtonCommand is not null)
            Add(doneButton);
    }
    
}