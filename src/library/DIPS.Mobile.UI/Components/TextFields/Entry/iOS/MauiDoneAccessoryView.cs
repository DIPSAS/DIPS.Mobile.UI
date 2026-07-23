using CoreGraphics;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Platform;
using UIKit;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TextFields.Entry.iOS;

/// <summary>
/// Based on https://github.com/dotnet/maui/blob/main/src/Core/src/Platform/iOS/MauiDoneAccessoryView.cs#L8
/// 
/// Adds an optional dictation microphone button to the left of the Done button. The microphone is hidden by
/// default.
/// </summary>
internal sealed class MauiDoneAccessoryView : UIToolbar
{
    private const string IdleMicrophoneSymbolName = "mic";
    private const double StopIndicatorSizePoints = 16;
    private const double StopIndicatorCornerRadiusPoints = 4;
    private const string MicrophoneButtonAccessibilityIdentifier = "DictationMicrophoneButton";

    private readonly BarButtonItemProxy m_proxy;
    private readonly UIBarButtonItem m_microphoneButton;
    private readonly UIBarButtonItem m_doneButton;
    private readonly UIBarButtonItem m_flexibleSpace = new(UIBarButtonSystemItem.FlexibleSpace);
    private readonly UIBarButtonItem m_microphoneToDoneSpacing =
        new(UIBarButtonSystemItem.FixedSpace) { Width = (nfloat)Sizes.GetSize(SizeName.size_2) };

    private bool m_isMicrophoneButtonVisible;

    // iOS 26 renders the Done button as a larger circular floating button
    // that requires extra height to avoid overlapping with the keyboard edge
    private static nfloat ToolbarHeight => UIDevice.CurrentDevice.CheckSystemVersion(26, 0) ? 60 : 44;

    public MauiDoneAccessoryView() : base(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, ToolbarHeight))
    {
        m_proxy = new BarButtonItemProxy();
        m_doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, m_proxy.OnDataClicked);
        m_microphoneButton = CreateMicrophoneButton(m_proxy.OnMicrophoneDataClicked);
        InitializeToolbar();
    }

    public MauiDoneAccessoryView(Action doneClicked) : base(new CGRect(0, 0, UIScreen.MainScreen.Bounds.Width, ToolbarHeight))
    {
        m_proxy = new BarButtonItemProxy(doneClicked);
        m_doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, m_proxy.OnClicked);
        m_microphoneButton = CreateMicrophoneButton(m_proxy.OnMicrophoneDataClicked);
        InitializeToolbar();
    }

    internal void SetDoneClicked(Action<object>? value) => m_proxy.SetDoneClicked(value);

    internal void SetMicrophoneClicked(Action<object>? value) => m_proxy.SetMicrophoneClicked(value);

    internal void SetDataContext(object? dataContext) => m_proxy.SetDataContext(dataContext);

    /// <summary>Shows or hides the microphone button. Hidden leaves the toolbar as Done only.</summary>
    internal void SetMicrophoneButtonVisible(bool isVisible)
    {
        if (m_isMicrophoneButtonVisible == isVisible)
            return;

        m_isMicrophoneButtonVisible = isVisible;
        
        UpdateToolbarItems();
    }

    /// <summary>
    /// Switches the microphone between its idle mic icon and the red stop square shown while dictating.
    /// </summary>
    internal void SetMicrophoneButtonActive(bool isActive)
    {
        if (isActive)
        {
            m_microphoneButton.Image = CreateStopIndicatorImage();
            return;
        }

        m_microphoneButton.Image = UIImage.GetSystemImage(IdleMicrophoneSymbolName);
        m_microphoneButton.TintColor = Colors.GetColor(ColorName.color_icon_default).ToPlatform();
    }

    // Should be changed to stop-filled once the icon has been added.
    private static UIImage CreateStopIndicatorImage()
    {
        var squareBounds = new CGRect(0, 0, StopIndicatorSizePoints, StopIndicatorSizePoints);
        var renderer = new UIGraphicsImageRenderer(squareBounds.Size);

        var redSquare = renderer.CreateImage(_ =>
        {
            var roundedSquare = UIBezierPath.FromRoundedRect(squareBounds, (nfloat)StopIndicatorCornerRadiusPoints);
            Colors.GetColor(ColorName.color_palette_red_600).ToPlatform().SetFill();
            roundedSquare.Fill();
        });

        return redSquare.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
    }

    private void InitializeToolbar()
    {
        BarStyle = UIBarStyle.Default;
        Translucent = true;
        SetMicrophoneButtonActive(false);
        UpdateToolbarItems();
    }

    private static UIBarButtonItem CreateMicrophoneButton(EventHandler clicked)
    {
        var microphoneButton =
            new UIBarButtonItem(UIImage.GetSystemImage(IdleMicrophoneSymbolName), UIBarButtonItemStyle.Plain, clicked)
            {
                AccessibilityIdentifier = MicrophoneButtonAccessibilityIdentifier,
                AccessibilityLabel = DUILocalizedStrings.Dictation
            };
        
        return microphoneButton;
    }

    private void UpdateToolbarItems()
    {
        var items = m_isMicrophoneButtonVisible
            ? new[] { m_flexibleSpace, m_microphoneButton, m_microphoneToDoneSpacing, m_doneButton }
            : new[] { m_flexibleSpace, m_doneButton };

        SetItems(items, false);
    }

    private sealed class BarButtonItemProxy
    {
        private readonly Action? m_doneClicked;
        private Action<object>? m_doneWithDataClicked;
        private Action<object>? m_microphoneWithDataClicked;
        private WeakReference<object>? m_data;

        public BarButtonItemProxy()
        {
        }

        public BarButtonItemProxy(Action doneClicked)
        {
            m_doneClicked = doneClicked;
        }

        public void SetDoneClicked(Action<object>? value) => m_doneWithDataClicked = value;

        public void SetMicrophoneClicked(Action<object>? value) => m_microphoneWithDataClicked = value;

        public void SetDataContext(object? dataContext) => m_data = dataContext is null ? null : new(dataContext);

        public void OnDataClicked(object? sender, EventArgs e)
        {
            if (m_data is not null && m_data.TryGetTarget(out var data))
                m_doneWithDataClicked?.Invoke(data);
        }

        public void OnMicrophoneDataClicked(object? sender, EventArgs e)
        {
            if (m_data is not null && m_data.TryGetTarget(out var data))
                m_microphoneWithDataClicked?.Invoke(data);
        }

        public void OnClicked(object? sender, EventArgs e) => m_doneClicked?.Invoke();
    }
}
