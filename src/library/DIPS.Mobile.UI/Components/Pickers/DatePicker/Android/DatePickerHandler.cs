using System.Globalization;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.Service;
using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Chip;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Chip = Google.Android.Material.Chip.Chip;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : ViewHandler<DatePicker, Chip>
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

    private partial void AppendPropertyMapper()
    {
    }

    private void OnClicked(object? sender, EventArgs e)
    {
        DatePickerService.Open(VirtualView);
    }

    public static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if(datePicker is { IsDateTimeOrTimeSpanDefault: true, IsNullable: true })
        {
            handler.m_chip.Style = Styles.GetChipStyle(ChipStyle.EmptyInput);
            handler.PlatformView.Text = DUILocalizedStrings.Date;
            return;
        }

        handler.m_chip.Style = Styles.GetChipStyle(ChipStyle.Input);
        
        var convertedDisplayValue =
            new DateConverter {Format = DateConverter.DateConverterFormat.Default}.Convert(datePicker.SelectedDate,
                null, null,
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