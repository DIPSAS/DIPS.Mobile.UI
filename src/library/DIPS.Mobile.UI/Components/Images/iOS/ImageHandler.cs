using System.ComponentModel;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images;

public partial class ImageHandler : ViewHandler<Image, UIImageView>
{
    protected override UIImageView CreatePlatformView() => new UIImageView();

    protected override void ConnectHandler(UIImageView platformView)
    {
        base.ConnectHandler(platformView);
        
        VirtualView.iOSProperties.PropertyChanged += IOSPropertiesOnPropertyChanged;
    }

    private void IOSPropertiesOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(VirtualView.iOSProperties.SystemIconName))
        {
            TrySetSystemImage(this, VirtualView);
        }
    }

    private static partial void TrySetSystemImage(ImageHandler imageHandler, Image image)
    {
        if (string.IsNullOrEmpty(image.iOSProperties.SystemIconName))
        {
            return;
        }

        var systemImage = UIImage.GetSystemImage(image.iOSProperties.SystemIconName);
            imageHandler.PlatformView.AdjustsImageSizeForAccessibilityContentSizeCategory = true;
            imageHandler.PlatformView.Image = systemImage;
    }

    private static partial void TrySetImageColor(ImageHandler imageHandler, Image image)
    {
        if (image?.Color != null)
        {
            imageHandler.PlatformView.TintColor = image.Color.ToPlatform();
        }
    }
    
    protected override void DisconnectHandler(UIImageView platformView)
    {
        base.DisconnectHandler(platformView);

        VirtualView.iOSProperties.PropertyChanged -= IOSPropertiesOnPropertyChanged;
    }
}