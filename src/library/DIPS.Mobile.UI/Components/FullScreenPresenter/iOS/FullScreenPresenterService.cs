using System.Runtime.InteropServices;
using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FullScreenPresenter;

public static partial class FullScreenPresenterService
{
    private static UIView? s_overlayView;
    private static UIView? s_nativeView;
    private static UIView? s_originalSuperview;
    private static nint s_originalIndex;
    private static CGRect s_originalFrame;

    static partial void PresentOnPlatform(View content)
    {
        var mauiContext = DUI.GetCurrentMauiContext;
        if (mauiContext is null)
            return;

        s_nativeView = content.ToPlatform(mauiContext);

        var window = GetKeyWindow();
        if (window is null || s_nativeView is null)
            return;

        // Save original position
        s_originalSuperview = s_nativeView.Superview;
        s_originalFrame = s_nativeView.Frame;
        s_originalIndex = s_originalSuperview is not null
            ? Array.IndexOf(s_originalSuperview.Subviews, s_nativeView)
            : 0;

        // Create fullscreen overlay
        s_overlayView = new UIView(window.Bounds)
        {
            BackgroundColor = Colors.GetColor(ColorName.color_background_default).ToPlatform(),
            AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight
        };

        // Remove from original parent and add to overlay
        s_nativeView.RemoveFromSuperview();
        s_nativeView.Frame = s_overlayView.Bounds;
        s_nativeView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight;
        s_overlayView.AddSubview(s_nativeView);

        // Add close button
        var closeButton = CreateNativeCloseButton();
        s_overlayView.AddSubview(closeButton);

        // Position close button in top-right with safe area
        closeButton.TranslatesAutoresizingMaskIntoConstraints = false;
        NSLayoutConstraint.ActivateConstraints(
        [
            closeButton.TopAnchor.ConstraintEqualTo(s_overlayView.SafeAreaLayoutGuide.TopAnchor, (NFloat)Sizes.GetSize(SizeName.content_margin_small)),
            closeButton.TrailingAnchor.ConstraintEqualTo(s_overlayView.SafeAreaLayoutGuide.TrailingAnchor, (NFloat)(-Sizes.GetSize(SizeName.content_margin_small))),
            closeButton.WidthAnchor.ConstraintEqualTo((NFloat)Sizes.GetSize(SizeName.size_8)),
            closeButton.HeightAnchor.ConstraintEqualTo((NFloat)Sizes.GetSize(SizeName.size_8))
        ]);

        // Animate in
        s_overlayView.Alpha = 0;
        window.AddSubview(s_overlayView);

        UIView.Animate(0.3, () =>
        {
            s_overlayView.Alpha = 1;
        });
    }

    static partial void CloseOnPlatform()
    {
        if (s_overlayView is null || s_nativeView is null)
            return;

        UIView.Animate(0.3, () =>
        {
            s_overlayView!.Alpha = 0;
        }, () =>
        {
            // Return view to original parent
            s_nativeView!.RemoveFromSuperview();
            s_nativeView.Frame = s_originalFrame;
            s_nativeView.AutoresizingMask = UIViewAutoresizing.None;

            if (s_originalSuperview is not null)
            {
                var index = (int)Math.Min(s_originalIndex, s_originalSuperview.Subviews.Length);
                s_originalSuperview.InsertSubview(s_nativeView, index);
            }

            s_overlayView!.RemoveFromSuperview();

            s_overlayView = null;
            s_nativeView = null;
            s_originalSuperview = null;
        });
    }

    private static UIButton CreateNativeCloseButton()
    {
        var buttonSize = Sizes.GetSize(SizeName.size_8);

        var button = new UIButton(UIButtonType.Custom);
        button.SetImage(
            UIImage.GetSystemImage("xmark")?
                .ApplyTintColor(Colors.GetColor(ColorName.color_icon_default).ToPlatform()),
            UIControlState.Normal);
        button.BackgroundColor = Colors.GetColor(ColorName.color_surface_default).ToPlatform();
        button.Layer.CornerRadius = (NFloat)(buttonSize / 2);
        button.ClipsToBounds = true;
        button.AccessibilityLabel = DUILocalizedStrings.Close;

        button.TouchUpInside += async (_, _) => await Close();

        return button;
    }

    private static UIWindow? GetKeyWindow()
    {
        return UIApplication.SharedApplication
            .ConnectedScenes
            .OfType<UIWindowScene>()
            .SelectMany(s => s.Windows)
            .FirstOrDefault(w => w.IsKeyWindow);
    }
}
