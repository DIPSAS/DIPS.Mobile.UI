using System.Globalization;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Chips.Android;
using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Microsoft.Maui.Handlers;
using Chip = Google.Android.Material.Chip.Chip;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler : ViewHandler<TimePicker, Chip>
{
    protected override Chip CreatePlatformView()
    {
        return new Chip(Context);
    }

    protected override void ConnectHandler(Chip platformView)
    {
        base.ConnectHandler(platformView);

        platformView.SetDefaultChipAttributes();
        platformView.Click += OnClicked;
    }

    private void OnClicked(object? sender, EventArgs e)
    {
        TimePickerService.OpenTimePicker(VirtualView);
    }

    private partial void AppendPropertyMapper()
    {
    }

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker datePicker)
    {
        var convertedDisplayValue =
            new TimeConverter { Format = TimeConverter.TimeConverterFormat.Default }.Convert(datePicker.SelectedTime, null, null,
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            handler.PlatformView.Text = displayItemAsString; 
        }
    }
}