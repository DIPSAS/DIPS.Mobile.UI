using DIPS.Mobile.UI.Components.Buttons;
using DIPS.Mobile.UI.Extensions.iOS;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Chips;

public partial class ChipHandler : ViewHandler<Chip, UIButton>
{
    private Components.Buttons.Button m_button;

    protected override UIButton CreatePlatformView()
    {
        m_button = new Components.Buttons.Button();
        var uiView = m_button.ToPlatform(MauiContext!);
        return (uiView.Subviews[0] as UIButton)!;
    }
    
    protected override void ConnectHandler(UIButton platformView)
    {
        base.ConnectHandler(platformView);
        m_button.TextColor = Colors.GetColor(ColorName.color_system_black);
        // Here we style the button as close as possible to native compact datepicker in iOS
        // We do not use the design system here so this does not diverge at a later point
        m_button.FontSize = 17;
        m_button.CornerRadius = 6;
        m_button.Padding = new Thickness(12, 6, 12, 6);
        platformView.AddGestureRecognizer(new ChipCloseGestureRecognizer(this));
        platformView.AddGestureRecognizer(new ChipToggleGestureRecognizer(this));
    }
    
    private static partial void MapTitle(ChipHandler handler, Chip chip)
    {
        handler.m_button.Text = chip.Title;
        handler.PlatformView.TitleLabel.LineBreakMode = UILineBreakMode.TailTruncation;
    }

    private static partial void MapIsCloseable(ChipHandler handler, Chip chip)
    {
        var uiButton = handler.PlatformView;
        if (handler.VirtualView.IsCloseable)
        {
            if (Icons.TryGetUIImage(iconName: handler.CloseIconName, out var image))
            {
                uiButton.ContentMode = UIViewContentMode.ScaleAspectFit;
                var resizedImage = image!.ResizeImage(handler.PlatformView.TitleLabel.Font.PointSize);
                uiButton.SetImage(resizedImage, UIControlState.Normal);
                handler.m_button.ImagePlacement = ImagePlacement.Right;
                //ShiftImageToTheRight(handler, uiButton);
            }
            else
            {
                uiButton.SetImage(null, UIControlState.Normal);
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
    
    private static partial void MapCornerRadius(ChipHandler handler, Chip arg2)
    {
        handler.PlatformView.Layer.CornerRadius = handler.VirtualView.CornerRadius;
    }

    private static void ShiftImageToTheRight(ChipHandler handler, UIButton uiButton)
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
        if (handler.VirtualView.IsCloseable || !handler.VirtualView.IsToggleable) return;
        
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
        if (handler.VirtualView.IsCloseable || !handler.VirtualView.IsToggleable) return;
        
        var uiButton = handler.PlatformView;
        if (handler.VirtualView.IsToggled)
        {
            if (!Icons.TryGetUIImage(iconName: handler.ToggledIconName, out var image)) return;

            uiButton.ContentMode = UIViewContentMode.ScaleAspectFit;
            var resizedImage = image!.ResizeImage(handler.PlatformView.TitleLabel.Font.PointSize);
            uiButton.ContentEdgeInsets = new UIEdgeInsets(uiButton.ContentEdgeInsets.Top, uiButton.ContentEdgeInsets.Left-resizedImage.Size.Width, uiButton.ContentEdgeInsets.Bottom, uiButton.ContentEdgeInsets.Right-resizedImage.Size.Width);            
            uiButton.SetImage(resizedImage, UIControlState.Normal);
            CenterContent(handler, uiButton, true);
        }
        else
        {
            if (uiButton.ImageView.Image is null) return; 
            
            CenterContent(handler, uiButton);
            uiButton.SetImage(null, UIControlState.Normal);
        }
    }
    
    private static void CenterContent(ChipHandler handler, UIButton uiButton, bool imageIsDisplayed = false)
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