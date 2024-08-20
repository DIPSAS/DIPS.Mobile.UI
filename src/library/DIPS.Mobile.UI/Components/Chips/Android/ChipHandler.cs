using Android.Text;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips.Android;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using TextAlignment = Android.Views.TextAlignment;


// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, Google.Android.Material.Chip.Chip>
{
    protected override Google.Android.Material.Chip.Chip CreatePlatformView() => new(Context);

    protected override void ConnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetPadding(8, 2, 8, 2);
        
        var fontManager = MauiContext?.Services.GetRequiredService<IFontManager>();
        platformView.UpdateFont(textStyle: new Label { Style = Styles.GetLabelStyle(LabelStyle.Body200) }, fontManager!);
        platformView.TextAlignment = TextAlignment.Center;
        platformView.SetTextColor(Colors.GetColor(ColorName.color_system_black).ToPlatform());
        platformView.ChipCornerRadius = 24;
        platformView.SetEnsureMinTouchTargetSize(false); //Remove extra margins around the chip, this is added to get more space to hit the chip but its not necessary : https://stackoverflow.com/a/57188310
        platformView.Click += OnChipTapped;
    }

    private void OnChipTapped(object? sender, EventArgs e)
    {
        OnChipTapped();
    }

    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.Text = chip.Title;
        handler.PlatformView.Ellipsize = TextUtils.TruncateAt.End;
    }

    private static partial void MapIsCloseable(ChipHandler handler, Chip chip)
    {
        if (handler.VirtualView.IsCloseable)
        {
            handler.PlatformView.CloseIconVisible = true;
            handler.PlatformView.SetOnCloseIconClickListener(new ChipCloseListener(handler));
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
        if (chip.Color == null) return;
        handler.PlatformView.ChipBackgroundColor = chip.Color.ToDefaultColorStateList();
    }

    private static partial void MapCloseButtonColor(ChipHandler handler, Chip chip)
    {
        if (chip.CloseButtonColor == null) return;
        handler.PlatformView.CloseIcon?.SetTint(chip.CloseButtonColor.ToPlatform());
    }

    private static partial void MapCornerRadius(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.ChipCornerRadius = (float) (handler.VirtualView.CornerRadius*DeviceDisplay.MainDisplayInfo.Density);
    }

    private static partial void MapBorderColor(ChipHandler handler, Chip chip)
    {
        if (chip.BorderColor == null) return;

        handler.PlatformView.ChipStrokeColor = chip.BorderColor.ToDefaultColorStateList();
    }

    private static partial void MapBorderWidth(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.ChipStrokeWidth = (float) chip.BorderWidth;
    }
    
    private static partial void MapTitleColor(ChipHandler handler, Chip chip)
    {
        if (chip.TitleColor is null) return;
        handler.PlatformView.SetTextColor(chip.TitleColor.ToPlatform());
    }

    private static partial void MapIsToggleable(ChipHandler handler, Chip chip)
    {
        if (chip.IsCloseable || !chip.IsToggleable)
            return;
        
        handler.PlatformView.Checkable = handler.PlatformView.CheckedIconVisible = true;
        handler.PlatformView.Checked = chip.IsToggled;
        
        DUI.TryGetResourceId(Icons.GetIconName(handler.ToggledIconName), out var id, defType:"drawable");
        if (id is not 0)
        {
#pragma warning disable CA1422
            var drawable = Platform.AppContext.Resources?.GetDrawable(id);
#pragma warning restore CA1422
            handler.PlatformView.CheckedIcon = drawable;
            handler.PlatformView.CheckedIconTint = chip.TitleColor?.ToDefaultColorStateList();
        }
        handler.PlatformView.SetOnCheckedChangeListener(new OnToggledChangedListener(handler));
    }
    
    private static partial void MapIsToggled(ChipHandler handler, Chip chip)
    {
        //Make sure not to mess with close button + check if chip actually is toggleable
        if (chip.IsCloseable || !chip.IsToggleable)
            return;
        
        handler.PlatformView.Checked = chip.IsToggled;
    }
    
    protected override void DisconnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.DisconnectHandler(platformView);
        
        platformView.SetOnCloseIconClickListener(null);
        platformView.SetOnCheckedChangeListener(null);
        platformView.Click -= OnChipTapped;
    }
}