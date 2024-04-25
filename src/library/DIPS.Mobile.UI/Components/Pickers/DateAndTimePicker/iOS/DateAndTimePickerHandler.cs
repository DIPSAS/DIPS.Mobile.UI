using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;
using DIPS.Mobile.UI.Components.Pickers.Platforms.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DateAndTimePicker;

public partial class DateAndTimePickerHandler : ViewHandler<DateAndTimePicker, DUIDatePicker>
{
    private bool m_isOpen;

    protected override DUIDatePicker CreatePlatformView()
    {
        return new DUIDatePicker
        {
            Mode = UIDatePickerMode.DateAndTime, PreferredDatePickerStyle = UIDatePickerStyle.Compact, VirtualView = VirtualView
        };
    }
    
    protected override void ConnectHandler(DUIDatePicker platformView)
    {
        base.ConnectHandler(platformView);

        platformView.ValueChanged += OnDateSelected;
        platformView.SetInLineLabelColors();
        
        platformView.EditingDidBegin += OnOpen;
        platformView.EditingDidEnd += OnClose;
        DUI.OnRemoveViewsLocatedOnTopOfPage += TryClose;
    }

    private void OnOpen(object? sender, EventArgs e)
    {
        m_isOpen = true;
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
        DateAndTimePickerPropertyMapper.Add(nameof(DateAndTimePicker.HorizontalOptions), MapHorizontalOptions);
        DateAndTimePickerPropertyMapper.Add(nameof(DateAndTimePicker.Background), MapOverrideBackground);
        DateAndTimePickerPropertyMapper.Add(nameof(DateAndTimePicker.IgnoreLocalTime), MapIgnoreLocalTime);
    }

    private static partial void MapMaximumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MaximumDate is null or null)
            return;

        handler.PlatformView.MaximumDate = ((DateTime)dateAndTimePicker.MaximumDate).ConvertDate();
    }

    private static partial void MapMinimumDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        if(dateAndTimePicker.MinimumDate is null or null)
            return;
        
        handler.PlatformView.MinimumDate = ((DateTime)dateAndTimePicker.MinimumDate).ConvertDate();
    }

    private static void MapOverrideBackground(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
    }

    private static void MapHorizontalOptions(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.SetHorizontalAlignment(dateAndTimePicker);
    }

    private void OnDateSelected(object? sender, EventArgs e)
    {
        VirtualView.SelectedDateTime = (DateTime)PlatformView.Date;
        VirtualView.SelectedDateTimeCommand?.Execute(null);
    }

    private static partial void MapIgnoreLocalTime(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.TimeZone = dateAndTimePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    private static partial void MapSelectedDate(DateAndTimePickerHandler handler, DateAndTimePicker dateAndTimePicker)
    {
        handler.PlatformView.SetDate(dateAndTimePicker.SelectedDateTime.Value.ConvertDate(), true);
    }

    protected override void DisconnectHandler(DUIDatePicker platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.DisposeLayer();
        platformView.ValueChanged -= OnDateSelected;
        platformView.EditingDidBegin -= OnOpen;
        platformView.EditingDidEnd -= OnClose;
        DUI.OnRemoveViewsLocatedOnTopOfPage -= TryClose;
    }
}