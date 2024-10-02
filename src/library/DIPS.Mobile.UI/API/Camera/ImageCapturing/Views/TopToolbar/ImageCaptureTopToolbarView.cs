using DIPS.Mobile.UI.API.Camera.ImageCapturing.BottomSheets;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.MemoryManagement;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.TopToolbar;

internal class ImageCaptureTopToolbarView : Grid
{
    private readonly ImageCaptureSettings m_imageCaptureSettings;
    private Button? m_settingsButton;
    private Button? m_infoButton;

    public ImageCaptureTopToolbarView(ImageCaptureSettings imageCaptureSettings, Action onDoneButtonTapped)
    {
        m_imageCaptureSettings = imageCaptureSettings;

        var doneButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostLarge),
            Text = DUILocalizedStrings.Done,
            TextColor = Colors.White,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
            Command = new Command(onDoneButtonTapped)
        };
        
        Add(doneButton);
    }
    
    private Button SettingsButton => new()
    {
        Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
        ImageSource = m_imageCaptureSettings.CanChangeMaxHeightOrWidth ? Icons.GetIcon(IconName.settings_fill) : Icons.GetIcon(IconName.information_fill),
        ImageTintColor = Colors.White,
        BackgroundColor = Colors.Transparent,
        HorizontalOptions = LayoutOptions.Start,
        VerticalOptions = LayoutOptions.Center
    };
    
    private static Button InfoButton => new()
    {
        Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
        ImageSource = Icons.GetIcon(IconName.information_fill),
        ImageTintColor = Colors.White,
        BackgroundColor = Colors.Transparent,
        HorizontalOptions = LayoutOptions.Start,
        VerticalOptions = LayoutOptions.Center
    };

    public void GoToConfirmState(CapturedImage capturedImage)
    {
        m_infoButton = InfoButton;

        if (m_settingsButton is not null)
        {
            Remove(m_settingsButton);
            new VisualTreeMemoryResolver().TryResolveMemoryLeakCascading(m_settingsButton);
        }
        Add(m_infoButton);
        
        m_infoButton.Command = new Command(() =>
        {
            new CapturedImageInfoBottomSheet(capturedImage).Open();
        });
    }

    public void GoToStreamingState(Action onBottomSheetSavedWithChanges)
    {
        m_settingsButton = SettingsButton;

        if (m_infoButton is not null)
        {
            Remove(m_infoButton);
            new VisualTreeMemoryResolver().TryResolveMemoryLeakCascading(m_infoButton);
        }
        Add(m_settingsButton);

        m_settingsButton.Command = new Command(() =>
        {
            new ImageCaptureSettingsBottomSheet(m_imageCaptureSettings, onBottomSheetSavedWithChanges).Open();
        });
    }
}