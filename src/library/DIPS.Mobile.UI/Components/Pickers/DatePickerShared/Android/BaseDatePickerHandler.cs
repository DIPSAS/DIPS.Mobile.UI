using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.Android;

public class BaseDatePickerHandler : ViewHandler<INullableDatePicker, AView>
{
    private ImageButton m_clearButton;
    protected Chip Chip;
    private Google.Android.Material.Chip.Chip m_nativeChip;
    private HorizontalStackLayout m_horizontalStackLayout;

    public BaseDatePickerHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }

    protected override AView CreatePlatformView()
    {
        m_clearButton = new ImageButton 
        { 
            Source = Icons.GetIcon(IconName.close_circle_line), 
            IsVisible = true,
            Command = new Command(() =>
            {
                VirtualView.SetDateOrTimeNull();
                SetClearButtonVisibility();
            })
        };
        
        Chip = new Chip();
        m_nativeChip = (Chip.ToPlatform(DUI.GetCurrentMauiContext!) as Google.Android.Material.Chip.Chip)!;
        
        m_horizontalStackLayout = [Chip, m_clearButton];
        return m_horizontalStackLayout.ToPlatform(DUI.GetCurrentMauiContext!);
    }
    
    protected override void ConnectHandler(AView platformView)
    {
        base.ConnectHandler(platformView);
        
        m_nativeChip.Click += OnClicked;
    }

    private void OnClicked(object? sender, EventArgs e)
    {
        switch (VirtualView)
        {
            case DatePicker.DatePicker datePicker:
                DatePickerService.Open(datePicker);
                break;
            case TimePicker.TimePicker timePicker:
                TimePickerService.OpenTimePicker(timePicker);
                break;
        }
    }

    protected void SetClearButtonVisibility()
    {
        m_clearButton.IsVisible = VirtualView is { IsDateOrTimeNull: false, IsNullable: true };
    }

    internal void RemoveClearButton()
    {
        m_horizontalStackLayout.Remove(m_clearButton);
    }

    protected override void DisconnectHandler(AView platformView)
    {
        base.DisconnectHandler(platformView);
        
        m_nativeChip.Click -= OnClicked;
    }
}