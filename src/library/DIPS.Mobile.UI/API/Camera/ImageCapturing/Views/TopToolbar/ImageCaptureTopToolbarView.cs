using DIPS.Mobile.UI.API.Camera.ImageCapturing.BottomSheets;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.API.Camera.Shared;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.MemoryManagement;
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
    private readonly Button m_doneButton;
    private OrientationDegree m_currentOrientationDegree;
    private bool m_isConfirmState;

    public ImageCaptureTopToolbarView(ImageCaptureSettings imageCaptureSettings, Action onDoneButtonTapped)
    {
        m_imageCaptureSettings = imageCaptureSettings;

        m_doneButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostLarge),
            Text = imageCaptureSettings.DoneButtonText,
            TextColor = Colors.White,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
            Command = new Command(onDoneButtonTapped)
        };
        
        Add(m_doneButton);
        
        DUI.OrientationChanged += OnOrientationChanged;
    }

    private void OnOrientationChanged(OrientationDegree orientationDegree)
    {
        if (!m_isConfirmState)
        {
            m_doneButton.RotateTo(orientationDegree.OrientationDegreeToMauiRotation());
            m_settingsButton?.RotateTo(orientationDegree.OrientationDegreeToMauiRotation());
        }
        
        m_currentOrientationDegree = orientationDegree;
    }

    private Button SettingsButton => new()
    {
        Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
        ImageSource = m_imageCaptureSettings.CanChangeMaxHeightOrWidth ? Icons.GetIcon(IconName.settings_fill) : Icons.GetIcon(IconName.information_line),
        ImageTintColor = Colors.White,
        BackgroundColor = Colors.Transparent,
        HorizontalOptions = LayoutOptions.Start,
        VerticalOptions = LayoutOptions.Center
    };
    
    private static Button InfoButton => new()
    {
        Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
        ImageSource = Icons.GetIcon(IconName.information_line),
        ImageTintColor = Colors.White,
        BackgroundColor = Colors.Transparent,
        HorizontalOptions = LayoutOptions.Start,
        VerticalOptions = LayoutOptions.Center
    };

    public void GoToConfirmState(CapturedImage capturedImage)
    {
        m_isConfirmState = true;
        
        m_infoButton = InfoButton;

        m_doneButton.RotateTo(0);

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
        m_isConfirmState = false;
        m_settingsButton = SettingsButton;

        m_doneButton.RotateTo(m_currentOrientationDegree.OrientationDegreeToMauiRotation());

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

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
        {
            DUI.OrientationChanged -= OnOrientationChanged;
        }
    }
}