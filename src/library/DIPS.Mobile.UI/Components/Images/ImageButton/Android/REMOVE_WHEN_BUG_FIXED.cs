using Android.Graphics.Drawables;
using Android.Widget;
using Google.Android.Material.ImageView;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Images.ImageButton.Android;

//Remove To fix bug: https://github.com/dotnet/maui/pull/14905
//TODO .NET8: Remove
public static class REMOVE_WHEN_BUG_FIXED
{
    internal static void SetContentPadding(this ShapeableImageView platformButton, IImageButton imageButton)
    {
        var imageView = platformButton as ImageView;

        if (imageView is not null)
        {
            var bitmapDrawable = imageView.Drawable as BitmapDrawable;

            // Without ImageSource we do not apply Padding, although since there is no content
            // there are no differences.
            if (bitmapDrawable is null)
                return;

            var backgroundBounds = bitmapDrawable.Bounds;

            var padding = imageButton.Padding;

            bitmapDrawable.SetBounds(
                backgroundBounds.Left + (int)platformButton.Context.ToPixels(padding.Left),
                backgroundBounds.Top + (int)platformButton.Context.ToPixels(padding.Top),
                backgroundBounds.Right - (int)platformButton.Context.ToPixels(padding.Right),
                backgroundBounds.Bottom - (int)platformButton.Context.ToPixels(padding.Bottom));
        }
    }
}