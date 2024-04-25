using System.Globalization;
using DIPS.Mobile.UI.Components.Pickers.TimePicker.Android;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Chip = Google.Android.Material.Chip.Chip;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.TimePicker;

public partial class TimePickerHandler : ViewHandler<TimePicker, Chip>
{
    private Chips.Chip m_chip;

    protected override Chip CreatePlatformView()
    {
        m_chip = new DIPS.Mobile.UI.Components.Chips.Chip();
        return (Chip)m_chip.ToPlatform(MauiContext!);
    }

    protected override void ConnectHandler(Chip platformView)
    {
        base.ConnectHandler(platformView);

        platformView.Click += OnClicked;
    }

    private void OnClicked(object? sender, EventArgs e)
    {
        TimePickerService.OpenTimePicker(VirtualView);
    }

    private partial void AppendPropertyMapper()
    {
    }

    private static partial void MapSelectedTime(TimePickerHandler handler, TimePicker timePicker)
    {
        if (timePicker.IsDateTimeOrTimeSpanDefault)
        {
            handler.m_chip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            handler.PlatformView.Text = DUILocalizedStrings.Time;
            return;
        }

        handler.m_chip.Style = Styles.GetChipStyle(ChipStyle.Input);
        
        var convertedDisplayValue =
            new TimeConverter { Format = TimeConverter.TimeConverterFormat.Default }.Convert(timePicker.SelectedTime, null, null,
                CultureInfo.CurrentCulture);
        if (convertedDisplayValue is string displayItemAsString)
        {
            handler.PlatformView.Text = displayItemAsString; 
        }
    }
}