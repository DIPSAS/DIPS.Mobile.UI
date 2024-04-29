using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Pickers.DatePicker.iOS;
using DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;
using DIPS.Mobile.UI.Components.Pickers.Platforms.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton.ImageButton;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Pickers.DatePicker;

public abstract partial class BaseDatePickerHandler : ViewHandler<DatePicker, LayoutView>
{
    private bool m_isOpen;
    private UIStackView m_uiStackView;
    private DUIDatePicker m_datePicker;
    private ImageButton m_clearButton;
    private InternalDatePicker m_internalDatePicker;
    private HorizontalStackLayout m_horizontalStackLayout;

    protected override LayoutView CreatePlatformView()
    {
        m_clearButton = new ImageButton 
        { 
            Source = Icons.GetIcon(IconName.close_circle_line), 
            IsVisible = true,
            Command = new Command(() =>
            {
                SetClearButtonVisibility(false);
                OnClearButtonTapped();
                VirtualView.SelectedDate = new DateTime();
                //VirtualView.SelectedTime = TimeSpan.Zero;
            }),
          
        };

        
        m_internalDatePicker = new InternalDatePicker { Mode = UIDatePickerMode.Date, DateTimePicker = VirtualView};
        m_internalDatePicker.SetBinding(View.HorizontalOptionsProperty, new Binding(nameof(VirtualView.HorizontalOptions), source: VirtualView));

        m_datePicker = m_internalDatePicker.ToPlatform(DUI.GetCurrentMauiContext!) as DUIDatePicker;
        
        m_horizontalStackLayout = [m_internalDatePicker, m_clearButton];
        return (m_horizontalStackLayout.ToPlatform(DUI.GetCurrentMauiContext!) as LayoutView)!;
    }

    protected abstract void OnClearButtonTapped();

    private void SetClearButtonVisibility(bool state)
    {
        m_clearButton.IsVisible = state;
    }

    protected override void ConnectHandler(LayoutView platformView)
    {
        base.ConnectHandler(platformView);
        
        m_datePicker.SetInLineLabelColors();
        
        m_datePicker.ValueChanged += OnValueChanged;
        m_datePicker.EditingDidBegin += OnOpen;
        m_datePicker.EditingDidEnd += OnClose;
        DUI.OnRemoveViewsLocatedOnTopOfPage += TryClose;
    }

    private void OnValueChanged(object? sender, EventArgs e)
    {
       OnDateSelected();
    }

    private void OnOpen(object? sender, EventArgs e)
    {
        m_isOpen = true;

        OnDateSelected();
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
     //   DatePickerPropertyMapper.Add(nameof(DatePicker.HorizontalOptions), MapHorizontalOptions);
        //DatePickerPropertyMapper.Add(nameof(DatePicker.Background), MapOverrideBackground);
        DatePickerPropertyMapper.Add(nameof(DatePicker.IgnoreLocalTime), MapIgnoreLocalTime);
        DatePickerPropertyMapper.Add(nameof(DatePicker.MinimumDate), MapMinimumDate);
        DatePickerPropertyMapper.Add(nameof(DatePicker.MaximumDate), MapMaximumDate);
    }

    private static void MapMaximumDate(BaseDatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MaximumDate is null or null)
            return;

        handler.m_datePicker.MaximumDate = ((DateTime)datePicker.MaximumDate).ConvertDate();
    }

    private static void MapMinimumDate(BaseDatePickerHandler handler, DatePicker datePicker)
    {
        if (datePicker.MinimumDate is null or null)
            return;

        handler.m_datePicker.MinimumDate = ((DateTime)datePicker.MinimumDate).ConvertDate();
    }

    private void MapOverrideBackground(BaseDatePickerHandler handler, DatePicker datePicker)
    {
    }

    private static void MapHorizontalOptions(BaseDatePickerHandler handler, DatePicker datePicker)
    {
        handler.m_datePicker.SetHorizontalAlignment(datePicker);
    }

    private void OnDateSelected()
    {
        if (VirtualView.IsNullable && VirtualView.SelectedDate == default)
        {
            VirtualView.SelectedDate = DateTime.Now;
            VirtualView.SelectedDateCommand?.Execute(null);
            return;
        }
        
        var timeZone = m_datePicker.TimeZone ?? NSTimeZone.LocalTimeZone;
        if (DateTime.TryParse(
                new NSDateFormatter() {DateFormat = "yyyy-MM-dd", TimeZone = timeZone}.StringFor(
                    m_datePicker.Date),
                out var selectedDate))
        {
            VirtualView.SelectedDate = selectedDate;
        }
        VirtualView.SelectedDateCommand?.Execute(null);
    }

    private static void MapIgnoreLocalTime(BaseDatePickerHandler handler, DatePicker datePicker)
    {
        handler.m_datePicker.TimeZone = datePicker.IgnoreLocalTime ? new NSTimeZone("UTC") : NSTimeZone.LocalTimeZone;
    }

    public static partial void MapSelectedDate(BaseDatePickerHandler handler, DatePicker datePicker)
    {
        if(!datePicker.SelectedDate.HasValue)
            return;
        
        handler.m_datePicker.SetDate(datePicker.SelectedDate.Value.ConvertDate(), true);
        handler.m_datePicker.UpdatePlaceholders();
    }

    protected override void DisconnectHandler(LayoutView platformView)
    {
        base.DisconnectHandler(platformView);

        m_datePicker.DisposeLayer();
        m_datePicker.ValueChanged -= OnValueChanged;
        m_datePicker.EditingDidBegin -= OnOpen;
        m_datePicker.EditingDidEnd -= OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage -= TryClose;
    }
}