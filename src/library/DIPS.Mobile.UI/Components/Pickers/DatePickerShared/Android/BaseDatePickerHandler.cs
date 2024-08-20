using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using TimePickerService = DIPS.Mobile.UI.Components.Pickers.TimePicker.TimePickerService;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.Android;

public class BaseDatePickerHandler : ViewHandler<IDatePicker, Google.Android.Material.Chip.Chip>
{
    public BaseDatePickerHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }

    protected override Google.Android.Material.Chip.Chip CreatePlatformView()
    {
        return (new Chip().ToPlatform(DUI.GetCurrentMauiContext!) as Google.Android.Material.Chip.Chip)!;
    }
    
    protected override void ConnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.ConnectHandler(platformView);
        
        platformView.Click += OnClicked;
    }

    private void OnClicked(object? sender, EventArgs e)
    {
        switch (VirtualView)
        {
            case DatePicker.DatePicker datePicker:
                DatePickerService.Open(datePicker);
                break;
            case TimePicker.TimePicker timePicker:
                TimePickerService.Open(timePicker);
                break;
        }
    }

    protected override void DisconnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.Click -= OnClicked;
    }
}