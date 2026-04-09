using System;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Sizes;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using ImageButton = Android.Widget.ImageButton;
using View = Microsoft.Maui.Controls.View;

namespace DIPS.Mobile.UI.Components.FullScreenPresenter;

public static partial class FullScreenPresenterService
{
    private static FrameLayout? s_overlayView;
    private static AView? s_nativeView;
    private static View? s_mauiView;
    private static ViewGroup? s_originalParent;
    private static int s_originalIndex;
    private static ViewGroup.LayoutParams? s_originalLayoutParams;

    static partial void PresentOnPlatform(View content)
    {
        var mauiContext = DUI.GetCurrentMauiContext;
        if (mauiContext is null)
            return;
        
        s_mauiView = content;
        s_nativeView = content.ToPlatform(mauiContext);

        var activity = Platform.CurrentActivity;
        
        // When in a modal, MAUI uses a DialogFragment with its own window.
        // We must add the overlay to the Dialog's DecorView, not the Activity's.
        var isModal = Microsoft.Maui.Controls.Shell.Current.Navigation.ModalStack.Count > 0;
        ViewGroup? targetDecorView;
        
        if (isModal && FragmentLifeCycleCallback.CurrentDialogFragment?.Dialog?.Window?.DecorView is ViewGroup dialogDecorView)
        {
            targetDecorView = dialogDecorView;
        }
        else
        {
            targetDecorView = activity?.Window?.DecorView as ViewGroup;
        }
        
        if (targetDecorView is null || s_nativeView is null)
            return;

        // Save original position — keep the actual LayoutParameters reference
        s_originalParent = s_nativeView.Parent as ViewGroup;
        s_originalLayoutParams = s_nativeView.LayoutParameters;
        if (s_originalParent is not null)
        {
            s_originalIndex = s_originalParent.IndexOfChild(s_nativeView);
            s_originalParent.RemoveView(s_nativeView);
        }

        // Get status bar height for top padding
        var statusBarHeight = 0;
        var resourceId = activity!.Resources!.GetIdentifier("status_bar_height", "dimen", "android");
        if (resourceId > 0)
        {
            statusBarHeight = activity.Resources.GetDimensionPixelSize(resourceId);
        }

        // Create fullscreen overlay with status bar padding
        s_overlayView = new FrameLayout(activity)
        {
            LayoutParameters = new ViewGroup.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent)
        };
        s_overlayView.SetBackgroundColor(Colors.GetColor(ColorName.color_background_default).ToPlatform());
        s_overlayView.Elevation = 100f;
        s_overlayView.SetPadding(0, statusBarHeight, 0, 0);

        // Add the view to the overlay
        s_nativeView.LayoutParameters = new FrameLayout.LayoutParams(
            ViewGroup.LayoutParams.MatchParent,
            ViewGroup.LayoutParams.MatchParent);
        s_overlayView.AddView(s_nativeView);

        // Add close button
        var closeButton = CreateNativeCloseButton(activity);
        s_overlayView.AddView(closeButton);

        // Animate in
        s_overlayView.Alpha = 0;
        targetDecorView.AddView(s_overlayView);
        s_overlayView.Animate()?.Alpha(1).SetDuration(300).Start();
    }

    static partial void CloseOnPlatform()
    {
        if (s_overlayView is null || s_nativeView is null)
            return;

        var nativeView = s_nativeView;
        var mauiView = s_mauiView;
        var overlayView = s_overlayView;
        var originalParent = s_originalParent;
        var originalIndex = s_originalIndex;
        var originalLayoutParams = s_originalLayoutParams;

        s_overlayView = null;
        s_nativeView = null;
        s_mauiView = null;
        s_originalParent = null;
        s_originalLayoutParams = null;

        overlayView.Animate()?.Alpha(0).SetDuration(300).WithEndAction(new Java.Lang.Runnable(() =>
        {
            // Return view to original parent
            overlayView.RemoveView(nativeView);

            if (originalParent is not null)
            {
                if (originalLayoutParams is not null)
                {
                    nativeView.LayoutParameters = originalLayoutParams;
                }
                
                var index = Math.Min(originalIndex, originalParent.ChildCount);
                originalParent.AddView(nativeView, index);
                
                // Force native re-layout
                nativeView.RequestLayout();
                originalParent.RequestLayout();
            }

            (overlayView.Parent as ViewGroup)?.RemoveView(overlayView);

            // Force MAUI to re-measure and re-layout
            mauiView?.InvalidateMeasure();
            ((mauiView as VisualElement)?.Parent as VisualElement)?.InvalidateMeasure();
        }))?.Start();
    }

    private static AView CreateNativeCloseButton(Android.App.Activity activity)
    {
        var density = activity.Resources!.DisplayMetrics!.Density;
        var buttonSizeDp = Sizes.GetSize(SizeName.size_8);
        var buttonSizePx = (int)(buttonSizeDp * density);
        var marginPx = (int)(Sizes.GetSize(SizeName.content_margin_small) * density);

        var button = new ImageButton(activity);
        button.SetImageResource(Android.Resource.Drawable.IcMenuCloseClearCancel);
        var color = (Colors.GetColor(ColorName.color_icon_default)).ToPlatform();
        button.SetColorFilter(color);

        var bgDrawable = new Android.Graphics.Drawables.GradientDrawable();
        bgDrawable.SetShape(Android.Graphics.Drawables.ShapeType.Oval);
        bgDrawable.SetColor(Colors.GetColor(ColorName.color_surface_default).ToPlatform());
        button.Background = bgDrawable;

        button.ContentDescription = DUILocalizedStrings.Close;

        var lp = new FrameLayout.LayoutParams(buttonSizePx, buttonSizePx, GravityFlags.Top | GravityFlags.End);
        lp.TopMargin = marginPx;
        lp.RightMargin = marginPx;
        button.LayoutParameters = lp;

        button.Click += async (_, _) => await Close();

        return button;
    }
}
