using System.ComponentModel;
using Android.Widget;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

// ReSharper disable once CheckNamespace
namespace DIPS.Mobile.UI.Components.Images;

public partial class NativeIconHandler : ViewHandler<NativeIcon, ImageView>
{
    protected override ImageView CreatePlatformView() => new(Platform.AppContext);

    private static partial void TrySetSystemImage(NativeIconHandler nativeIconHandler, NativeIcon nativeIcon)
    {
        if (string.IsNullOrEmpty(nativeIcon.AndroidIconResourceName))
        {
            return;
        }

        var androidResource = 
            API.Library.Android.DUI.GetResourceId(nativeIcon.AndroidIconResourceName, "drawable");
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