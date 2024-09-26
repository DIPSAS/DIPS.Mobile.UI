using DIPS.Mobile.UI.API.Camera.ImageCapturing.BottomSheets;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.TopToolbar;

internal class TopToolbarView : Grid
{
    public TopToolbarView(ImageCaptureSettings imageCaptureSettings, Action onBottomSheetSavedWithChanges)
    {
        Margin = new Thickness(Sizes.GetSize(SizeName.size_5), 0);
        
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
        
        Add(settingsButton);
    }
    
}