using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;
using DIPS.Mobile.UI.Components.Pickers.Platforms.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public partial class DatePickerHandler : ViewHandler<DatePicker, DUIDatePicker>
{
    private bool m_isOpen;

    protected override DUIDatePicker CreatePlatformView()
    {
        return new DUIDatePicker {PreferredDatePickerStyle = UIDatePickerStyle.Compact, Mode = UIDatePickerMode.Date, VirtualView = VirtualView};
    }
    
    protected override void ConnectHandler(DUIDatePicker platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetInLineLabelColors();
        
        platformView.ValueChanged += ValueChanged;
        platformView.EditingDidBegin += OnOpen;
        platformView.EditingDidEnd += OnClose;
        DUI.OnRemoveViewsLocatedOnTopOfPage += TryClose;
        
        platformView.Initialize();
    }

    private void ValueChanged(object? sender, EventArgs e)
    {
       OnDateSelected();
    }

    private void OnOpen(object? sender, EventArgs e)
    {
        m_isOpen = true;

        if (VirtualView.SelectedDate is null)
        {
            OnDateSelected();
        }
    }

    private void OnClose(object? sender, EventArgs e)
    {
        m_isOpen = false;
    }

    private void TryClose()
    {
        if (!m_isOpen)
            return;

        var currentPresentedUiViewController = Platform.GetCurrentUIViewController();
        currentPresentedUiViewController?.DismissViewController(false, null);
    }

    private partial void AppendPropertyMapper()
    {
        DatePickerPropertyMapper.Add(nameof(DatePicker.HorizontalOptions), MapHorizontalOptions);
        DatePickerPropertyMapper.Add(nameof(DatePicker.Background), MapOverrideBackground);
        DatePickerPropertyMapper.Add(nameof(DatePicker.IgnoreLocalTime), MapIgnoreLocalTime);
        DatePickerPropertyMapper.Add(nameof(DatePicker.MinimumDate), MapMinimumDate);
        DatePickerPropertyMapper.Add(nameof(DatePicker.MaximumDate), MapMaximumDate);
    }

    private static void MapMaximumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MaximumDate is null or null)
            return;

        handler.PlatformView.MaximumDate = ((DateTime)datePicker.MaximumDate).ConvertDate();
    }

    private static void MapMinimumDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MinimumDate is null or null)
            return;

        handler.PlatformView.MinimumDate = ((DateTime)datePicker.MinimumDate).ConvertDate();
    }

    private void MapOverrideBackground(DatePickerHandler handler, DatePicker datePicker)
    {
    }

    private static void MapHorizontalOptions(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.SetHorizontalAlignment(datePicker);
    }

    private void OnDateSelected()
    {
        var timeZone = PlatformView.TimeZone ?? NSTimeZone.LocalTimeZone;
        if (DateTime.TryParse(
                new NSDateFormatter() {DateFormat = "yyyy-MM-dd", TimeZone = timeZone}.StringFor(
                    PlatformView.Date),
                out var selectedDate))
        {
            VirtualView.SelectedDate = selectedDate;
        }

        VirtualView.SelectedDateCommand?.Execute(null);
    }

    private static void MapIgnoreLocalTime(DatePickerHandler handler, DatePicker datePicker)
    {
        handler.PlatformView.TimeZone = datePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    public static partial void MapSelectedDate(DatePickerHandler handler, DatePicker datePicker)
    {
        if(datePicker.SelectedDate is null)
            return;
        
        handler.PlatformView.SetDate(datePicker.SelectedDate.Value.ConvertDate(), true);
    }

    protected override void DisconnectHandler(DUIDatePicker platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.DisposeLayer();
        platformView.ValueChanged -= ValueChanged;
        platformView.EditingDidBegin -= OnOpen;
        platformView.EditingDidEnd -= OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage -= TryClose;
    }
}