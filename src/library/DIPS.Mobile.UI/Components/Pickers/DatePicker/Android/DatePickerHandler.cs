using System.Globalization;
using Android.Content.Res;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Chips.Android;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Android;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Converters.ValueConverters;
using Microsoft.Maui.Handlers;
using Chip = Google.Android.Material.Chip.Chip;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : ViewHandler<DatePicker, Chip>
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
    
    private partial void AppendPropertyMapper()
    {
    }

    private void OnClicked(object? sender, EventArgs e)
    {
        DatePickerService.Open(VirtualView);
    }

    
    public static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        var convertedDisplayValue =
            new DateConverter { Format = DateConverter.DateConverterFormat.Default }.Convert(datePicker.SelectedDate, null, null,
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            handler.PlatformView.Text = displayItemAsString; 
        }
    }
    
    protected override void DisconnectHandler(Chip platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.Click -= OnClicked;
    }
}