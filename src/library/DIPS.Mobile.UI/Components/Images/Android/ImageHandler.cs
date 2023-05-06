using System.ComponentModel;
using Android.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images;

public partial class ImageHandler : ViewHandler<Image, ImageView>
{
    protected override ImageView CreatePlatformView() => new ImageView(Platform.AppContext);

    protected override void ConnectHandler(ImageView platformView)
    {
        base.ConnectHandler(platformView);
        
        VirtualView.AndroidProperties.PropertyChanged += AndroidPropertiesOnPropertyChanged;
    }

    private void AndroidPropertiesOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(VirtualView.AndroidProperties.IconResourceName))
        {
            TrySetSystemImage(this, VirtualView);
        }
    }

    private static partial void TrySetSystemImage(ImageHandler imageHandler, Image image)
    {
        if (string.IsNullOrEmpty(image.AndroidProperties.IconResourceName))
        {
            return;
        }

        var androidResource = 
            API.Library.Android.DUI.GetResourceId(image.AndroidProperties.IconResourceName, "drawable");
        if (androidResource != null)
        {
            imageHandler.PlatformView.SetImageResource((int)androidResource);    
        }
    }

    private static partial void TrySetImageColor(ImageHandler imageHandler, Image image)
    {
        if (image is {Color: not null })
        {
            imageHandler.PlatformView.SetColorFilter(image.Color.ToPlatform());
        }
    }

    protected override void DisconnectHandler(ImageView platformView)
    {
        base.DisconnectHandler(platformView);

        VirtualView.AndroidProperties.PropertyChanged -= AndroidPropertiesOnPropertyChanged;
    }
}