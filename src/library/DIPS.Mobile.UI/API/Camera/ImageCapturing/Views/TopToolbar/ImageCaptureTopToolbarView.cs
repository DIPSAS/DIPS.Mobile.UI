using DIPS.Mobile.UI.API.Camera.ImageCapturing.BottomSheets;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Observers;
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
    private readonly Button m_doneButton;
    private readonly HorizontalStackLayout m_upperLeftColumn;
    
    private Button? m_settingsButton;
    private Button? m_infoButton;
    private Button? m_editButton;
    
    private OrientationDegree m_currentOrientationDegree;
    
    private bool m_isConfirmState;

    public ImageCaptureTopToolbarView(ImageCaptureSettings imageCaptureSettings, Action onDoneButtonTapped)
    {
        m_imageCaptureSettings = imageCaptureSettings;

        m_doneButton = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostLarge),
            Text = imageCaptureSettings.CancelButtonText,
            TextColor = Colors.White,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
            Command = new Command(onDoneButtonTapped)
        };

        m_upperLeftColumn = new HorizontalStackLayout
        {
            Spacing = Sizes.GetSize(SizeName.content_margin_xsmall),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Start
        };
        
        Add(m_doneButton);
        Add(m_upperLeftColumn);
        
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
        ImageTintColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_icon_on_action),
        BackgroundColor = Colors.Transparent,
        HorizontalOptions = LayoutOptions.Start,
        VerticalOptions = LayoutOptions.Center
    };

    private static Button EditButton => new()
    {
        Style = Styles.GetButtonStyle(ButtonStyle.GhostIconButtonLarge),
        ImageSource = Icons.GetIcon(IconName.filter_fill),
        ImageTintColor = DIPS.Mobile.UI.Resources.Colors.Colors.GetColor(ColorName.color_icon_on_action),
        BackgroundColor = Colors.Transparent,
        HorizontalOptions = LayoutOptions.Start,
        VerticalOptions = LayoutOptions.Center
    };

    public void GoToConfirmState(CapturedImage capturedImage, IConfirmStateObserver confirmStateObserver)
    {
        m_doneButton.IsVisible = true;
        
        m_isConfirmState = true;
        
        m_infoButton = InfoButton;
        m_editButton = EditButton;

        m_doneButton.RotateTo(0);

        ResolveUpperLeftColumn();
        
        m_upperLeftColumn.Add(m_infoButton);
        m_upperLeftColumn.Add(m_editButton);
        
        m_infoButton.Command = new Command(() =>
        {
            new CapturedImageInfoBottomSheet(capturedImage).Open();
        });

        m_editButton.Command = new Command(confirmStateObserver.OnEditButtonTapped);
    }

    public void GoToStreamingState(IStreamingStateObserver streamingStateObserver)
    {
        m_isConfirmState = false;
        m_settingsButton = SettingsButton;

        m_doneButton.RotateTo(m_currentOrientationDegree.OrientationDegreeToMauiRotation());

        ResolveUpperLeftColumn();
        m_upperLeftColumn.Add(m_settingsButton);

        m_settingsButton.Command = new Command(() =>
        {
            new ImageCaptureSettingsBottomSheet(m_imageCaptureSettings, streamingStateObserver.OnSettingsChanged).Open();
        });
    }

    public void GoToEditState()
    {
        m_doneButton.IsVisible = false;
        ResolveUpperLeftColumn();
    }

    private void ResolveUpperLeftColumn()
    {
        var children = m_upperLeftColumn.Children.ToList();
        
        foreach (var view in children)
        {
            if(view is not View viewToRemove)
                continue;
            
            m_upperLeftColumn.Remove(viewToRemove);
            viewToRemove.DisconnectHandlers();
        }
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