using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Pickers.DatePickerShared.iOS;

public abstract class BaseDatePickerHandler : ViewHandler<IDatePicker, UIDatePicker>
{
    private bool m_isOpen;
    
    protected BaseDatePickerHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }
    
    public static readonly IPropertyMapper<IDatePicker, BaseDatePickerHandler> BasePropertyMapper = new PropertyMapper<IDatePicker, BaseDatePickerHandler>(ViewMapper)
    {
    };

    protected abstract override UIDatePicker CreatePlatformView();

    protected override void ConnectHandler(UIDatePicker platformView)
    {
        base.ConnectHandler(platformView);

        platformView.ValueChanged += OnValueChanged;
        platformView.EditingDidBegin += OnOpen;
        platformView.EditingDidEnd += OnClose;
        DUI.OnRemoveViewsLocatedOnTopOfPage += TryClose;
        
        platformView.TintColor = Colors.GetColor(ColorName.color_primary_90).ToPlatform();
    }

    protected virtual void OnValueChanged(object? sender, EventArgs e)
    {
        var newDateTime = DateTime.SpecifyKind((DateTime)PlatformView.Date, DateTimeKind.Utc)
            .ConvertDate(VirtualView.GetDateTimeKind());

        VirtualView.SetSelectedDateTime(newDateTime);
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

    protected override void DisconnectHandler(UIDatePicker platformView)
    {
        base.DisconnectHandler(platformView);

        platformView.EditingDidBegin -= OnOpen;
        platformView.EditingDidEnd -= OnClose;

        DUI.OnRemoveViewsLocatedOnTopOfPage -= TryClose;
    }
}