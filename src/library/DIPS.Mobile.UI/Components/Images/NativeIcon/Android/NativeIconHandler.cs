using System.ComponentModel;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Resources.Icons;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images.NativeIcon;

public partial class NativeIconHandler : ViewHandler<NativeIcon, ImageView>
{
    protected override ImageView CreatePlatformView() => new(Platform.AppContext);

    private partial void AppendPropertyMapper()
    {
        PropertyMapper.Add(nameof(NativeIcon.AndroidIconResourceName), TrySetSystemImage);
    }
    
    private static void TrySetSystemImage(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon)
    {
        if (string.IsNullOrEmpty(nativeIcon.AndroidIconResourceName))
        {
            return;
        }

        var androidResource = DUI.GetResourceId(nativeIcon.AndroidIconResourceName, "drawable");
        if (androidResource != null)
        {
            nativeIconHandler.PlatformView.SetImageResource((int)androidResource);    
        }
    }

    private static partial void TrySetImageColor(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon)
    {
        if (nativeIcon is {Color: not null })
        {
            nativeIconHandler.PlatformView.SetColorFilter(nativeIcon.Color.ToPlatform());
        }
    }
}