using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using HorizontalStackLayout = DIPS.Mobile.UI.Components.Lists.HorizontalStackLayout;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton.ImageButton;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

public abstract class BaseDatePickerHandler : ViewHandler<INullableDatePicker, LayoutView>
{
    private bool m_isOpen;
    
    private ImageButton m_clearButton;
    
    private InternalDatePicker m_internalDatePicker;
    internal DUIDatePicker m_nativeDatePicker;

    protected BaseDatePickerHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }
    
    protected override LayoutView CreatePlatformView()
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

        var view = VirtualView as View;
        m_internalDatePicker = new InternalDatePicker { Mode = UIDatePickerMode.Date, DatePicker = VirtualView};
        m_internalDatePicker.SetBinding(View.HorizontalOptionsProperty, new Binding(nameof(view.HorizontalOptions), source: this));

        m_nativeDatePicker = (m_internalDatePicker.ToPlatform(DUI.GetCurrentMauiContext!) as DUIDatePicker)!;
        m_nativeDatePicker.Mode = GetMode();
        
        HorizontalStackLayout horizontalStackLayout = [m_internalDatePicker, m_clearButton];
        return (horizontalStackLayout.ToPlatform(DUI.GetCurrentMauiContext!) as LayoutView)!;
    }
    
    protected override void ConnectHandler(LayoutView platformView)
    {
        base.ConnectHandler(platformView);
        
        m_nativeDatePicker.SetInLineLabelColors();
        
        m_nativeDatePicker.ValueChanged += OnValueChanged;
        m_nativeDatePicker.EditingDidBegin += OnOpen;
        m_nativeDatePicker.EditingDidEnd += OnClose;
        DUI.OnRemoveViewsLocatedOnTopOfPage += TryClose;
    }

    protected abstract UIDatePickerMode GetMode();
    protected abstract void OnDateSelected();

    protected void OnDateOrTimeChanged()
    {
        m_nativeDatePicker.UpdatePlaceholders();
        SetClearButtonVisibility();
    }

    protected void SetClearButtonVisibility()
    {
        m_clearButton.IsVisible = VirtualView is { IsDateOrTimeNull: false, IsNullable: true };
    }

    private void OnValueChanged(object? sender, EventArgs e)
    {
        OnDateSelected();
    }

    private void OnOpen(object? sender, EventArgs e)
    {
        m_isOpen = true;

        switch (VirtualView)
        {
            case DatePicker.DatePicker { IsNullable: true, SelectedDate: null } datePicker:
                datePicker.SelectedDate = DateTime.Now;
                datePicker.SelectedDateCommand?.Execute(null);
                break;
            case DateAndTimePicker.DateAndTimePicker { IsNullable: true, SelectedDateTime: null } dateAndTimePicker:
                dateAndTimePicker.SelectedDateTime = DateTime.Now;
                dateAndTimePicker.SelectedDateTimeCommand?.Execute(null);
                break;
            case TimePicker.TimePicker { IsNullable: true, SelectedTime: null } timePicker:
                timePicker.SelectedTime = DateTime.Now.TimeOfDay;
                timePicker.SelectedTimeCommand?.Execute(null);
                break;
        }
        
        SetClearButtonVisibility();
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

    protected override void DisconnectHandler(LayoutView platformView)
    {
        base.DisconnectHandler(platformView);

        m_nativeDatePicker.DisposeLayer();
        m_nativeDatePicker.ValueChanged -= OnValueChanged;
        m_nativeDatePicker.EditingDidBegin -= OnOpen;
        m_nativeDatePicker.EditingDidEnd -= OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage -= TryClose;
    }
}