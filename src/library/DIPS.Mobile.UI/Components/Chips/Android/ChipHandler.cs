using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips.Android;
using Java.Interop;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;


// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, Google.Android.Material.Chip.Chip>
{
    protected override Google.Android.Material.Chip.Chip CreatePlatformView() => new Google.Android.Material.Chip.Chip(Context);

    protected override void ConnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetDefaultChipAttributes();
        platformView.Click += OnChipTapped;
    }

    private void OnChipTapped(object? sender, EventArgs e)
    {
        OnChipTapped();
    }

    protected override void DisconnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.DisconnectHandler(platformView);
        platformView.Click -= OnChipTapped;
    }

    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.Text = chip.Title;
    }

    private static partial void MapHasCloseButton(ChipHandler handler, Chip chip)
    {
        if (handler.VirtualView.HasCloseButton)
        {
            handler.PlatformView.CloseIconVisible = true;
            handler.PlatformView.SetOnCloseIconClickListener(new OnCloseListener(handler));
            DUI.TryGetResourceId(Icons.GetIconName(iconName: IconName.placeholdericon_fill), out var id, defType:"drawable");
            if (id != 0)
            {
                var drawable = Platform.AppContext.Resources?.GetDrawable(id);
                handler.PlatformView.CloseIcon = drawable;
            }
        }
        else
        {
            handler.PlatformView.CloseIconVisible = false;
            handler.PlatformView.SetOnCloseIconClickListener(null);
        }
        
    }

    private static partial void MapColor(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.SetBackgroundColor(handler.VirtualView.Color);
    }

    private static partial void MapCloseButtonColor(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.CloseIcon?.SetTint(handler.VirtualView.CloseButtonColor.ToPlatform());
    }
    
    public class OnCloseListener : Java.Lang.Object, global::Android.Views.View.IOnClickListener
    {
        private readonly ChipHandler m_chipHandler;

        public OnCloseListener(ChipHandler chipHandler)
        {
            m_chipHandler = chipHandler;
        }

        public void OnClick(global::Android.Views.View? v)
        {
            m_chipHandler.OnCloseTapped();
        }
    }
}