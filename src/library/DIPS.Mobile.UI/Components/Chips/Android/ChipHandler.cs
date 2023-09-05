using Android.Content.Res;
using DIPS.Mobile.UI.API.Library;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using TextAlignment = Android.Views.TextAlignment;


// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, Google.Android.Material.Chip.Chip>
{
    protected override Google.Android.Material.Chip.Chip CreatePlatformView() => new Google.Android.Material.Chip.Chip(Context);

    protected override void ConnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetPadding(8, 2, 8, 2);
        platformView.TextAlignment = (TextAlignment) Microsoft.Maui.TextAlignment.Center;
        platformView.SetTextColor(Colors.GetColor(ColorName.color_system_black).ToPlatform());
        platformView.TextSize = Sizes.GetSize(SizeName.size_4);
        platformView.ChipCornerRadius = 24;
        platformView.SetEnsureMinTouchTargetSize(false); //Remove extra margins around the chip, this is added to get more space to hit the chip but its not necessary : https://stackoverflow.com/a/57188310
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
        var cornerRadius = handler.PlatformView.ChipCornerRadius;
    }

    private static partial void MapHasCloseButton(ChipHandler handler, Chip chip)
    {
        if (handler.VirtualView.HasCloseButton)
        {
            handler.PlatformView.CloseIconVisible = true;
            handler.PlatformView.SetOnCloseIconClickListener(new OnCloseListener(handler));
            DUI.TryGetResourceId(Icons.GetIconName(handler.CloseIconName), out var id, defType:"drawable");
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
        var color = handler.VirtualView.Color;
        var states = new[]
        {
            new[] { global::Android.Resource.Attribute.StateEnabled}, // enabled
            new[] {-global::Android.Resource.Attribute.StateEnabled}, // disabled
            new[] {-global::Android.Resource.Attribute.StateChecked}, // unchecked
            new[] { global::Android.Resource.Attribute.StateChecked } // pressed
        };

        var colors = new int[] 
        {
            color.ToPlatform(),
            color.ToPlatform(),
            color.ToPlatform(),
            color.ToPlatform()
        };
        
        handler.PlatformView.ChipBackgroundColor = new ColorStateList(states, colors);
    }

    private static partial void MapCloseButtonColor(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.CloseIcon?.SetTint(handler.VirtualView.CloseButtonColor.ToPlatform());
    }

    private static partial void MapCornerRadius(ChipHandler handler, Chip arg2)
    {
        handler.PlatformView.ChipCornerRadius = (float) (handler.VirtualView.CornerRadius*DeviceDisplay.MainDisplayInfo.Density);
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