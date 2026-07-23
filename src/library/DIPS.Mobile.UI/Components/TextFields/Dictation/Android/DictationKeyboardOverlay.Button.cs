using Android.App;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField.Dictation;
using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.MVVM.Commands;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using ImageButton = DIPS.Mobile.UI.Components.Images.ImageButton.ImageButton;
using ShapeableImageView = Google.Android.Material.ImageView.ShapeableImageView;

namespace DIPS.Mobile.UI.Components.TextFields.Dictation;

internal sealed partial class DictationKeyboardOverlay
{
    private const double IconToButtonSizeRatio = 0.6;
    private const double StopSquareToButtonSizeRatio = 0.33;
    private const double StopSquareCornerToSizeRatio = 0.25;
    private const string MicrophoneButtonAutomationId = "DictationMicrophoneButton";

    private void CreateMicrophonePopup(Activity activity, IMauiContext mauiContext, float density)
    {
        var buttonSizeDips = Sizes.GetSize(SizeName.size_15);
        var buttonSizePixels = (int)(buttonSizeDips * density);
        var iconInsetPixels = (int)(buttonSizeDips * (1 - IconToButtonSizeRatio) / 2 * density);

        var microphoneButton = BuildMicrophoneButton();

        var iconView = HostNativeIcon(microphoneButton, mauiContext, iconInsetPixels);
        if (iconView is null)
            return;

        var whiteCircleButton = WrapInWhiteCircle(iconView, activity, buttonSizePixels, density);

        m_stopSquareSizePixels = (int)(buttonSizeDips * StopSquareToButtonSizeRatio * density);
        m_stopSquareDrawable = CreateStopSquareDrawable(m_stopSquareSizePixels);

        m_microphoneImageButton = microphoneButton;
        m_imageView = iconView;
        m_containerView = whiteCircleButton;
        m_popup = BuildKeyboardPopup(whiteCircleButton, activity, buttonSizePixels);
    }

    private ImageButton BuildMicrophoneButton()
    {
        var buttonSizeDips = Sizes.GetSize(SizeName.size_15);

        var microphoneButton = new ImageButton
        {
            Source = Icons.GetIcon(IconName.mic_ai_line),
            TintColor = Colors.GetColor(ColorName.color_icon_default),
            WidthRequest = buttonSizeDips,
            HeightRequest = buttonSizeDips,
            Command = new AsyncCommand(OnMicrophoneButtonTapped, LogMicrophoneTapFailure),
            AutomationId = MicrophoneButtonAutomationId
        };
        SemanticProperties.SetDescription(microphoneButton, DUILocalizedStrings.Dictation);
        return microphoneButton;
    }
    
    private static ShapeableImageView? HostNativeIcon(ImageButton microphoneButton, IMauiContext mauiContext, int iconInsetPixels)
    {
        microphoneButton.ToPlatform(mauiContext);
        if (microphoneButton.Handler?.PlatformView is not ShapeableImageView iconView)
            return null;

        (iconView.Parent as ViewGroup)?.RemoveView(iconView);
        iconView.SetPadding(iconInsetPixels, iconInsetPixels, iconInsetPixels, iconInsetPixels);
        iconView.SetScaleType(ImageView.ScaleType.FitCenter);
        return iconView;
    }
    
    private static FrameLayout WrapInWhiteCircle(ShapeableImageView iconView, Activity activity, int buttonSizePixels, float density)
    {
        var whiteCircleButton = new FrameLayout(activity)
        {
            LayoutParameters = new ViewGroup.LayoutParams(buttonSizePixels, buttonSizePixels),
            Background = CreateWhiteCircleBackground(buttonSizePixels, density),
            ClipToOutline = true
        };
        whiteCircleButton.AddView(iconView,
            new FrameLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent));
        return whiteCircleButton;
    }

    // A non-focusable popup that sits above the keyboard.
    private static PopupWindow BuildKeyboardPopup(FrameLayout content, Activity activity, int buttonSizePixels)
    {
        var popup = new PopupWindow(activity)
        {
            ContentView = content,
            Width = buttonSizePixels,
            Height = buttonSizePixels,
            // Never take focus, so the keyboard stays up
            Focusable = false,
            OutsideTouchable = false,
            Touchable = true,
            // Coexist with the keyboard instead of being placed behind it or dismissing it.
            InputMethodMode = Android.Widget.InputMethod.Needed
        };
        popup.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));
        return popup;
    }
    
    private static GradientDrawable CreateWhiteCircleBackground(int buttonSizePixels, float density)
    {
        var circle = new GradientDrawable();
        circle.SetShape(ShapeType.Rectangle);
        circle.SetColor(Colors.GetColor(ColorName.color_surface_default).ToPlatform());
        circle.SetCornerRadius(buttonSizePixels / 2f);
        circle.SetStroke((int)Math.Ceiling(density), Colors.GetColor(ColorName.color_border_button).ToPlatform());
        return circle;
    }
    
    private static GradientDrawable CreateStopSquareDrawable(int sizePixels)
    {
        var square = new GradientDrawable();
        square.SetShape(ShapeType.Rectangle);
        square.SetColor(Colors.GetColor(ColorName.color_palette_red_600).ToPlatform());
        square.SetCornerRadius((float)(sizePixels * StopSquareCornerToSizeRatio));
        square.SetSize(sizePixels, sizePixels);
        return square;
    }

    // Swaps the icon between the idle mic and the red stop square.
    private void SetMicrophoneActive(bool isActive)
    {
        var iconView = m_imageView;
        if (iconView is null)
            return;

        if (isActive)
        {
            m_microphoneIconDrawable ??= iconView.Drawable;
            iconView.SetScaleType(ImageView.ScaleType.Center);
            iconView.ClearColorFilter();
            iconView.SetImageDrawable(m_stopSquareDrawable);
            return;
        }

        iconView.SetScaleType(ImageView.ScaleType.FitCenter);
        iconView.SetColorFilter(Colors.GetColor(ColorName.color_icon_default).ToPlatform());
        if (m_microphoneIconDrawable is not null)
            iconView.SetImageDrawable(m_microphoneIconDrawable);
    }

    private async Task OnMicrophoneButtonTapped()
    {
        var consumer = DictationSessionCoordinator.Current.CurrentConsumer;
        if (consumer is null)
            return;

        if (m_isDictationActive)
        {
            m_isDictationActive = false;
            SetMicrophoneActive(false);
            DictationSessionCoordinator.Current.StopSession();
            return;
        }

        m_isDictationActive = true;
        SetMicrophoneActive(true);

        StartDictationResult startResult;
        try
        {
            startResult = await DictationSessionCoordinator.Current.StartSession(consumer);
        }
        catch (Exception error)
        {
            startResult = new StartDictationResult(error.Message);
        }

        m_isDictationActive = false;
        SetMicrophoneActive(false);

        if (startResult.IsError)
            DictationErrorDialog.Show(startResult.ErrorMessage);
    }

    private static void LogMicrophoneTapFailure(Exception error) =>
        DUILogService.LogError<DictationKeyboardOverlay>($"Dictation microphone tap failed: {error}");

    private void StopDictationIfActive()
    {
        if (!m_isDictationActive)
            return;

        m_isDictationActive = false;
        SetMicrophoneActive(false);
        DictationSessionCoordinator.Current.StopSession();
    }
}
