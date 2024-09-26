using DIPS.Mobile.UI.API.Camera.ImageCapturing.BottomSheets;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.TopToolbar;

internal class TopToolbarView : Grid
{
    private readonly Button m_settingsButton;
    private readonly Button m_infoButton;

    public TopToolbarView(ImageCaptureSettings imageCaptureSettings, Action onBottomSheetSavedWithChanges)
    {
        m_settingsButton = new Button
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

        m_infoButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
            ImageSource = Icons.GetIcon(IconName.information_fill),
            ImageTintColor = Colors.White,
            BackgroundColor = Colors.Transparent,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
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
        
        Add(m_settingsButton);
        Add(m_infoButton);
        
        if(imageCaptureSettings.DoneButtonCommand is not null)
            Add(doneButton);
    }

    public void SwitchToConfirmState(CapturedImage capturedImage)
    {
        m_settingsButton.IsVisible = false;
        m_infoButton.IsVisible = true;
        
        m_infoButton.Command = new Command(() =>
        {
            new CapturedImageInfoBottomSheet(capturedImage).Open();
        });
    }

    public void SwitchToStreamingState()
    {
        m_settingsButton.IsVisible = true;
        m_infoButton.IsVisible = false;
    }
    
}