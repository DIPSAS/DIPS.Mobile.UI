using Android.Text;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips.Android;
using DIPS.Mobile.UI.Extensions.Android;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using TextAlignment = Microsoft.Maui.Controls.PlatformConfiguration.Android.Views.TextAlignment;


// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public class ChipHandler : ViewHandler<Chip, Google.Android.Material.Chip.Chip>
{
    public ChipHandler() : base(PropertyMapper)
    {
    }

    public static readonly IPropertyMapper<Chip, ChipHandler> PropertyMapper = new PropertyMapper<Chip, ChipHandler>(ViewMapper)
    {
        [nameof(Chip.Title)] = MapTitle,
        [nameof(Chip.IsCloseable)] = MapIsCloseable,
        [nameof(Chip.Color)] = MapColor,
        [nameof(Chip.CloseButtonColor)] = MapCloseButtonColor,
        [nameof(Chip.CornerRadius)] = MapCornerRadius,
        [nameof(Chip.BorderWidth)] = MapBorderWidth,
        [nameof(Chip.BorderColor)] = MapBorderColor,
        [nameof(Chip.IsToggled)] = MapIsToggled,
        [nameof(Chip.TitleColor)] = MapTitleColor,
        [nameof(Chip.IsToggleable)] = MapIsToggleable,
        [nameof(Chip.CustomIcon)] = MapCustomIcon,
        [nameof(Chip.CustomIconTintColor)] = MapCustomIconTintColor,
        [nameof(Chip.CustomRightIcon)] = MapCustomRightIcon,
        [nameof(Chip.TitleTextAlignment)] = MapTitleTextAlignment,
        [nameof(Chip.InnerPadding)] = MapPadding
    };

    protected override Google.Android.Material.Chip.Chip CreatePlatformView() => new(Context);

    protected override void ConnectHandler(Google.Android.Material.Chip.Chip platformView)
    {
        base.ConnectHandler(platformView);
        
        var fontManager = MauiContext?.Services.GetRequiredService<IFontManager>();
        platformView.UpdateFont(textStyle: new Label { Style = Styles.GetLabelStyle(LabelStyle.Body200) }, fontManager!);
        platformView.SetTextColor(Colors.GetColor(ColorName.color_text_default).ToPlatform());
        platformView.SetEnsureMinTouchTargetSize(false); //Remove extra margins around the chip, this is added to get more space to hit the chip but its not necessary : https://stackoverflow.com/a/57188310
        platformView.Click += OnChipTapped;
        VirtualView.SizeChanged += VirtualViewOnSizeChanged;
    }

    private void VirtualViewOnSizeChanged(object? sender, EventArgs e)
    {
        // This is a workaround because setting padding top and bottom does not work as expected on Android.
        VirtualView.MinimumHeightRequest = VirtualView.InnerPadding.Bottom + VirtualView.InnerPadding.Top + VirtualView.Height;
        VirtualView.SizeChanged -= VirtualViewOnSizeChanged;
    }

    private static void MapPadding(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.ChipStartPadding = chip.InnerPadding.Left.ToMauiPixel();
        handler.PlatformView.ChipEndPadding = chip.InnerPadding.Right.ToMauiPixel();
    }

    private static void MapTitleTextAlignment(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.TextAlignment = chip.TitleTextAlignment switch
        {
            Microsoft.Maui.TextAlignment.Start => TextAlignment.ViewStart,
            Microsoft.Maui.TextAlignment.Center => TextAlignment.Center,
            Microsoft.Maui.TextAlignment.End => TextAlignment.ViewEnd,
            _ => TextAlignment.ViewStart
        };
    }

    private static void MapCustomRightIcon(ChipHandler handler, Chip chip)
    {
        if(chip.IsCloseable)
            return;

        if (chip.CustomRightIcon is not FileImageSource fileImageSource)
        {
            handler.PlatformView.CloseIcon = null;
            return;
        }
        
        handler.PlatformView.CloseIconVisible = true;
        DUI.TryGetResourceId(fileImageSource.File.Replace(".png", string.Empty), out var id, defType:"drawable");
        
        if (id == 0)
            return;

        var drawable = Platform.AppContext.Resources?.GetDrawable(id);
        handler.PlatformView.CloseIcon = drawable;
    }

    private static void MapCustomIcon(ChipHandler handler, Chip chip)
    {
        if(chip.IsToggleable || chip.CustomIcon is not FileImageSource fileImageSource)
            return;

        var id = DUI.GetResourceId(fileImageSource.File.Replace(".png", string.Empty), "drawable");
        var icon = Platform.AppContext.GetDrawable((int)id);
        handler.PlatformView.ChipIcon = icon;
    }
    
    private static void MapCustomIconTintColor(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.ChipIconTint = chip.CustomIconTintColor.ToDefaultColorStateList();
    }

    internal void OnChipTapped()
    {
        VirtualView.SendTapped();
    }

    internal void OnCloseTapped()
    {
        VirtualView.SendCloseTapped();
    }

    private void OnChipTapped(object? sender, EventArgs e)
    {
        OnChipTapped();
    }

    private static void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.Text = chip.Title;
        handler.PlatformView.Ellipsize = TextUtils.TruncateAt.End;
        handler.PlatformView.SetMaxLines(1);
    }

    private static void MapIsCloseable(ChipHandler handler, Chip chip)
    {
        if (handler.VirtualView.IsCloseable)
        {
            handler.PlatformView.CloseIconVisible = true;
            handler.PlatformView.SetOnCloseIconClickListener(new ChipCloseListener(handler));
            
            DUI.TryGetResourceId(Icons.GetIconName(Chip.CloseIconName), out var id, defType:"drawable");
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

    private static void MapColor(ChipHandler handler, Chip chip)
    {
        if (chip.Color == null) return;
        handler.PlatformView.ChipBackgroundColor = chip.Color.ToDefaultColorStateList();
    }

    private static void MapCloseButtonColor(ChipHandler handler, Chip chip)
    {
        if (chip.CloseButtonColor == null) return;
        handler.PlatformView.CloseIcon?.SetTint(chip.CloseButtonColor.ToPlatform());
    }

    private static void MapCornerRadius(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.ChipCornerRadius = handler.VirtualView.CornerRadius.ToMauiPixel();
    }

    private static void MapBorderColor(ChipHandler handler, Chip chip)
    {
        if (chip.BorderColor == null) return;

        handler.PlatformView.ChipStrokeColor = chip.BorderColor.ToDefaultColorStateList();
    }

    private static void MapBorderWidth(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.ChipStrokeWidth = (float) chip.BorderWidth;
    }
    
    private static void MapTitleColor(ChipHandler handler, Chip chip)
    {
        if (chip.TitleColor is null) return;
        handler.PlatformView.SetTextColor(chip.TitleColor.ToPlatform());
    }

    private static void MapIsToggleable(ChipHandler handler, Chip chip)
    {
        if (chip.IsCloseable || !chip.IsToggleable)
            return;
        
        handler.PlatformView.Checkable = handler.PlatformView.CheckedIconVisible = true;
        handler.PlatformView.Checked = chip.IsToggled;
        
        DUI.TryGetResourceId(Icons.GetIconName(Chip.ToggledIconName), out var id, defType:"drawable");
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
    
    private static void MapIsToggled(ChipHandler handler, Chip chip)
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