using DIPS.Mobile.UI.API.Camera.ImageCapturing.Settings;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Entry = DIPS.Mobile.UI.Components.TextFields.Entry.Entry;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.BottomSheets;

internal class ImageCaptureSettingsBottomSheet : BottomSheet
{
    private static string NotSetText => DUILocalizedStrings.None;

    private readonly string m_startingEntryText;
    private readonly ImageCaptureSettings m_imageCaptureSettings;
    private readonly Entry m_targetHeightOrWidthEntry;
    private readonly Action? m_onSavedWithChanges;

    public ImageCaptureSettingsBottomSheet(ImageCaptureSettings imageCaptureSettings, Action onSavedWithChanges)
    {
        m_imageCaptureSettings = imageCaptureSettings;
        m_onSavedWithChanges = onSavedWithChanges;

        Title = DUILocalizedStrings.Settings;

        /*TODO:ToolbarItems.Add(new ToolbarItem
        {
            Text = imageCaptureSettings.CanChangeMaxHeightOrWidth ? DUILocalizedStrings.Save : DUILocalizedStrings.Close,
            Command = new Command(OnSave)
        });*/

        m_startingEntryText = imageCaptureSettings.MaxHeightOrWidth?.ToString() ?? NotSetText; 
        m_targetHeightOrWidthEntry = new Entry
        {
            Text = m_startingEntryText,
            Keyboard = Keyboard.Numeric,
            HasBorder = imageCaptureSettings.CanChangeMaxHeightOrWidth,
            ShouldSelectAllTextOnFocused = true,
            HorizontalTextAlignment = TextAlignment.Center,
            IsReadOnly = !imageCaptureSettings.CanChangeMaxHeightOrWidth,
            MinimumWidthRequest = Sizes.GetSize(SizeName.size_20)
        };
        
        m_targetHeightOrWidthEntry.TextChanged += TargetHeightOrWidthEntryOnTextChanged;
        
        var targetHeightOrWidthListItem = new ListItem
        {
            Title = DUILocalizedStrings.MaxHeightOrWidth,
            HasBottomDivider = true,
            InLineContent = m_targetHeightOrWidthEntry 
        };

        var actualResolutionListItem = new ListItem
        {
            Title = DUILocalizedStrings.CurrentCameraResolution,
            InLineContent = new Label
            {
                Text =
                    $"{imageCaptureSettings.CameraInfo.CurrentCameraResolution.Width} x {imageCaptureSettings.CameraInfo.CurrentCameraResolution.Height}"
            }
        };

        Content = new VerticalStackLayout
        {
            Spacing = 0,
            Children = { targetHeightOrWidthListItem, actualResolutionListItem }
        };
    }

    private void TargetHeightOrWidthEntryOnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (!int.TryParse(e.NewTextValue, out var value))
            return;

        if (value <= 0)
        {
            m_targetHeightOrWidthEntry.Text = NotSetText;
        }
    }

    private void OnSave()
    {
        if(m_targetHeightOrWidthEntry.Text == m_startingEntryText)
        {
            Close();
            return;
        }
        
        if(string.IsNullOrEmpty(m_targetHeightOrWidthEntry.Text))
        {
            m_imageCaptureSettings.MaxHeightOrWidth = null;
            m_onSavedWithChanges?.Invoke();
            Close();
            return;
        }
        
        if(!int.TryParse(m_targetHeightOrWidthEntry.Text, out var value))
        {
            _ = DialogService.ShowMessage(DUILocalizedStrings.InvalidInput, DUILocalizedStrings.PleaseInputValidInteger, "Ok");
            return;
        }

        if (value == m_imageCaptureSettings.MaxHeightOrWidth)
        {
            Close();            
            return;
        }

        m_imageCaptureSettings.MaxHeightOrWidth = value;
        m_onSavedWithChanges?.Invoke();

        Close();
    }

    protected override void OnClosed()
    {
        base.OnClosed();
        
        m_targetHeightOrWidthEntry.TextChanged -= TargetHeightOrWidthEntryOnTextChanged;
    }
}