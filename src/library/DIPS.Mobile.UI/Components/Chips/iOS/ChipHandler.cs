using DIPS.Mobile.UI.Components.Buttons;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Platforms.iOS;
using Foundation;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using ButtonHandler = DIPS.Mobile.UI.Components.Buttons.ButtonHandler;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, UIButton>
{
    protected override UIButton CreatePlatformView()
    {
        return new ChipButton { OnTapped = OnTappedButtonChip };
    }

    private void OnTappedButtonChip(NSSet touches)
    {
        if (VirtualView.IsToggleable)
        {
            VirtualView.IsToggled = !VirtualView.IsToggled;
            OnChipTapped();
            return;
        }
        
        if (!VirtualView.IsCloseable)
        {
            OnChipTapped();
            return;
        }

        var firstObject = touches.First();
        if (firstObject is not UITouch uiTouch) return;
        var uiButton = PlatformView;
        var touchLocationInView = uiTouch.LocationInView(uiButton);
        var didTouchInsideImage =
            touchLocationInView.X > uiButton.TitleLabel.Frame.X + uiButton.TitleLabel.Frame.Width;

        if (didTouchInsideImage)
        {
            OnCloseTapped();
        }
        else
        {
            OnChipTapped();
        }
    }

    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SetTitleColor(Colors.GetColor(ColorName.color_system_black).ToPlatform(), UIControlState.Normal);
        // Here we style the button as close as possible to native compact datepicker in iOS
        // We do not use the design system here so this does not diverge at a later point
        var fontManager = MauiContext?.Services.GetRequiredService<IFontManager>();
        platformView.Layer.CornerRadius = 6;
        platformView.UpdateFont(new Label { FontSize = 17 }, fontManager!);
        platformView.UpdatePadding(new Thickness(12, 6, 12, 6));
    }
    
    //TODO: .NET 8 vNext, workaround developed by looking at : https://github.com/dotnet/maui/pull/18521
    //TODO. .NET 8 vNext, Remove when its confirmed that this is released: https://github.com/dotnet/maui/issues/18504
    public override Size GetDesiredSize(double widthConstraint, double heightConstraint)
    {
        var desiredSize = base.GetDesiredSize(widthConstraint, heightConstraint);
        if (PlatformView is not UIButton uiButton)
        {
            return desiredSize;
        }

        if (uiButton.ImageView.Image is not null && string.IsNullOrEmpty(uiButton.CurrentTitle)) //A chip with only a image
        {
            return desiredSize;
        }
        
        if (uiButton.ImageView.Image is null && !string.IsNullOrEmpty(uiButton.CurrentTitle)) //A chip with only a title
        {
            return desiredSize;
        }

        //A button with both title and image, which has the bug
        return new Size(uiButton.IntrinsicContentSize.Width, uiButton.IntrinsicContentSize.Height);
    }
    
    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.TranslatesAutoresizingMaskIntoConstraints = false;
        handler.PlatformView.SetTitle(chip.Title, UIControlState.Normal);
        handler.PlatformView.TitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
    }

    private static partial void MapIsCloseable(ChipHandler handler, Chip chip)
    {
        if (handler.VirtualView.IsCloseable)
        {
            if (Icons.TryGetUIImage(iconName: handler.CloseIconName, out var image))
            {
                handler.PlatformView.ContentMode = UIViewContentMode.ScaleAspectFit;
                var resizedImage = image!.ResizeImage(handler.PlatformView.TitleLabel.Font.PointSize);
                handler.PlatformView.SetImage(resizedImage, UIControlState.Normal);
                ShiftImageToTheRight(handler.PlatformView);
            }
            else
            {
                handler.PlatformView.SetImage(null, UIControlState.Normal);
            }
        }
    }

    private static partial void MapColor(ChipHandler handler, Chip chip)
    {
        if (handler.VirtualView.Color == null) return;
        handler.PlatformView.BackgroundColor = handler.VirtualView.Color.ToPlatform();
    }

    private static partial void MapCloseButtonColor(ChipHandler handler, Chip chip)
    {
        if (!handler.VirtualView.IsCloseable) return;
        
        if (handler.VirtualView.CloseButtonColor == null) return;
        
        if (handler.PlatformView.ImageView.Image == null) //Close button is the UIButton imageview
        {
            return;
        }
        
        var imageToTint =
            handler.PlatformView.ImageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        handler.PlatformView.SetImage(imageToTint, UIControlState.Normal);
        handler.PlatformView.TintColor = handler.VirtualView.CloseButtonColor.ToPlatform();
    }
    
    private static partial void MapCornerRadius(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.Layer.CornerRadius = chip.CornerRadius;
    }

    private static void ShiftImageToTheRight(UIButton uiButton)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(14, 1))
        {
            return;
        }

        var imageWidth = uiButton.ImageView.IntrinsicContentSize.Width;
        uiButton.TitleEdgeInsets = new UIEdgeInsets(0, -imageWidth, 0, imageWidth);

        var titleWidth = uiButton.TitleLabel.IntrinsicContentSize.Width;
        var titleImageSpacing = Sizes.GetSize(SizeName.size_2);
        var spacing = titleWidth + titleImageSpacing;
        uiButton.ImageEdgeInsets = new UIEdgeInsets(0, spacing, 0, -spacing);

        var oldContentEdgeInsets = uiButton.ContentEdgeInsets;
        uiButton.ContentEdgeInsets = new UIEdgeInsets(oldContentEdgeInsets.Top, oldContentEdgeInsets.Left,
            oldContentEdgeInsets.Bottom, oldContentEdgeInsets.Right + titleImageSpacing);
    }
    
    
    private static partial void MapBorderColor(ChipHandler handler, Chip chip)
    {
        if (chip.BorderColor == null) return;
        handler.PlatformView.Layer.BorderColor = chip.BorderColor.ToCGColor();
    }

    private static partial void MapBorderWidth(ChipHandler handler, Chip chip)
    {
        handler.PlatformView.Layer.BorderWidth = (nfloat)chip.BorderWidth;
    }

    private static partial void MapTitleColor(ChipHandler handler, Chip chip)
    {
        if (chip.TitleColor is null) return;
        handler.PlatformView.SetTitleColor(chip.TitleColor.ToPlatform(), UIControlState.Normal);
        
        if (!handler.VirtualView.IsToggleable || !handler.VirtualView.IsToggled) return; //Do not change close icon color
        var imageToTint = handler.PlatformView.ImageView.Image?.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
        handler.PlatformView.SetImage(imageToTint, UIControlState.Normal);
        handler.PlatformView.TintColor = chip.TitleColor.ToPlatform();
    }

    private static partial void MapIsToggleable(ChipHandler handler, Chip chip)
    {
        if (chip.IsCloseable || !chip.IsToggleable) return;
        
        if (!OperatingSystem.IsIOSVersionAtLeast(14, 1)) return;
        
        if (Icons.TryGetUIImage(iconName: handler.ToggledIconName, out var image))
        {
            var resizedImage = image!.ResizeImage(handler.PlatformView.TitleLabel.Font.PointSize);
            var imageWidth = resizedImage.Size.Width;
            
            var oldContentEdgeInsets = handler.PlatformView.ContentEdgeInsets;
            handler.PlatformView.ContentEdgeInsets = new UIEdgeInsets(oldContentEdgeInsets.Top,  oldContentEdgeInsets.Left + imageWidth,
                oldContentEdgeInsets.Bottom, oldContentEdgeInsets.Right + imageWidth);
        }
    }

    private static partial void MapIsToggled(ChipHandler handler, Chip chip)
    {
        //Make sure not to mess with close button
        if (chip.IsCloseable || !chip.IsToggleable) return;
        
        var uiButton = handler.PlatformView;
        if (chip.IsToggled)
        {
            if (!Icons.TryGetUIImage(iconName: handler.ToggledIconName, out var image)) return;

            uiButton.ContentMode = UIViewContentMode.ScaleAspectFit;
            var resizedImage = image!.ResizeImage(handler.PlatformView.TitleLabel.Font.PointSize);
            uiButton.ContentEdgeInsets = new UIEdgeInsets(uiButton.ContentEdgeInsets.Top, uiButton.ContentEdgeInsets.Left-resizedImage.Size.Width, uiButton.ContentEdgeInsets.Bottom, uiButton.ContentEdgeInsets.Right-resizedImage.Size.Width);            
            uiButton.SetImage(resizedImage, UIControlState.Normal);
            CenterContent(uiButton, true);
        }
        else
        {
            if (uiButton.ImageView.Image is null) return; 
            
            CenterContent(uiButton);
            uiButton.SetImage(null, UIControlState.Normal);
        }
    }
    
    private static void CenterContent(UIButton uiButton, bool imageIsDisplayed = false)
    {
        if (!OperatingSystem.IsIOSVersionAtLeast(14, 1)) return;
         
        var imageWidth = uiButton.ImageView.IntrinsicContentSize.Width;
        var oldContentEdgeInsets = uiButton.ContentEdgeInsets;
        
        uiButton.TitleEdgeInsets = imageIsDisplayed 
            ? new UIEdgeInsets(0, 0, 0, -imageWidth) 
            : new UIEdgeInsets(0, -imageWidth, 0, -imageWidth);
        
        uiButton.ImageEdgeInsets = imageIsDisplayed
            ? new UIEdgeInsets(0, -imageWidth, 0, -imageWidth)
            : new UIEdgeInsets(0, 0, 0, 0);
        
        uiButton.ContentEdgeInsets = imageIsDisplayed 
            ? new UIEdgeInsets(oldContentEdgeInsets.Top,  oldContentEdgeInsets.Left,
            oldContentEdgeInsets.Bottom, oldContentEdgeInsets.Right + imageWidth) 
            : new UIEdgeInsets(oldContentEdgeInsets.Top,  oldContentEdgeInsets.Left + imageWidth,
                oldContentEdgeInsets.Bottom, oldContentEdgeInsets.Right) ;
    }
}