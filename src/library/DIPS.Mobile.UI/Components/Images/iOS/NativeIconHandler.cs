using System.ComponentModel;
using DIPS.Mobile.UI.Resources.Icons;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using UIKit;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images;

public partial class NativeIconHandler : ViewHandler<NativeIcon, MauiImageView>
{
    protected override MauiImageView CreatePlatformView() => new();

    private partial void AppendPropertyMapper()
    {
        PropertyMapper.Add(nameof(NativeIcon.iOSSystemIconName), TrySetSystemImage);
    }

    private static partial void TrySetSystemImage(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon)
    {
        if (string.IsNullOrEmpty(nativeIcon.iOSSystemIconName))
        {
            return;
        }
        
        var systemImage = UIImage.FromFile(nativeIcon.iOSSystemIconName);
        nativeIconHandler.PlatformView.AdjustsImageSizeForAccessibilityContentSizeCategory = true;
        nativeIconHandler.PlatformView.Image = systemImage;
    }

    private static partial void TrySetImageColor(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon)
    {
        if (nativeIcon?.Color != null)
        {
            nativeIconHandler.PlatformView.TintColor = nativeIcon.Color.ToPlatform();
        }
    }
}