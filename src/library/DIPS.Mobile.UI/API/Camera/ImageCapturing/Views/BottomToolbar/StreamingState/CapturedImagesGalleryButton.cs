using System.ComponentModel;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar.StreamingState;

/// <summary>
/// The most recently captured image, shown in the camera's bottom-left during multi capture. Tapping it opens the captured-images gallery.
/// </summary>
internal sealed partial class CapturedImagesGalleryButton : Grid
{
    private const float ThumbnailSize = 48;

    private readonly BoxView m_placeholder;
    private readonly Image m_thumbnail;
    private readonly Label m_countLabel;
    private readonly ContentView m_countBadge;
    private readonly Action m_onTapped;

    private bool m_isAwaitingCapturedImage;

    public CapturedImagesGalleryButton(Action onTapped)
    {
        ArgumentNullException.ThrowIfNull(onTapped);

        m_onTapped = onTapped;

        IsVisible = false;
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Start;
        WidthRequest = ThumbnailSize;
        HeightRequest = ThumbnailSize;

        m_placeholder = new BoxView
        {
            Color = Colors.GetColor(ColorName.color_palette_base_white, 0.2f),
            CornerRadius = Sizes.GetSize(SizeName.radius_small),
            WidthRequest = ThumbnailSize,
            HeightRequest = ThumbnailSize
        };

        m_thumbnail = new Image
        {
            Aspect = Aspect.AspectFill,
            Opacity = 0,
            WidthRequest = ThumbnailSize,
            HeightRequest = ThumbnailSize
        };
        UI.Effects.Layout.Layout.SetCornerRadius(m_thumbnail, Sizes.GetSize(SizeName.radius_small));
        UI.Effects.Layout.Layout.SetStroke(m_thumbnail, Colors.GetColor(ColorName.color_palette_base_white, 0.15f));
        UI.Effects.Layout.Layout.SetStrokeThickness(m_thumbnail, 1);
        m_thumbnail.PropertyChanged += OnThumbnailPropertyChanged;

        m_countLabel = new Label
        {
            TextColor = Colors.GetColor(ColorName.color_palette_base_white),
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        m_countBadge = new ContentView
        {
            Content = m_countLabel,
            BackgroundColor = Colors.GetColor(ColorName.color_palette_base_black, 0.6f),
            Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_xsmall), 0),
            MinimumWidthRequest = Sizes.GetSize(SizeName.size_5),
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(0, -Sizes.GetSize(SizeName.content_margin_xsmall), -Sizes.GetSize(SizeName.content_margin_xsmall), 0)
        };
        UI.Effects.Layout.Layout.SetCornerRadius(m_countBadge, Sizes.GetSize(SizeName.size_5) / 2);

        Touch.SetCommand(this, new Command(OnTapped));
        SemanticProperties.SetDescription(this, DUILocalizedStrings.Accessibility_OpenCapturedImages);

        Add(m_placeholder);
        Add(m_thumbnail);
        Add(m_countBadge);

        this.RotateWithDeviceOrientation();
    }

    /// <summary>
    /// Shows the button the moment the shutter fires, before the captured image is processed.
    /// Bumps the count and blurs the previous thumbnail until the new one has decoded.
    /// </summary>
    public void ShowPendingCapture(int pendingImageCount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(pendingImageCount);

        IsVisible = true;
        m_countLabel.Text = pendingImageCount.ToString();
        m_isAwaitingCapturedImage = true;

        SetThumbnailBlurred(true);
    }

    /// <summary>
    /// Rolls back the optimistic count and placeholder from <see cref="ShowPendingCapture" />.
    /// </summary>
    public void CancelPendingCapture(IReadOnlyList<CapturedImage> images)
    {
        ArgumentNullException.ThrowIfNull(images);

        if (!m_isAwaitingCapturedImage)
            return;

        ShowMostRecentImageAndCount(images);
    }

    public void ShowMostRecentImageAndCount(IReadOnlyList<CapturedImage> images)
    {
        ArgumentNullException.ThrowIfNull(images);

        var wasAwaitingCapturedImage = m_isAwaitingCapturedImage;
        m_isAwaitingCapturedImage = false;

        if (images.Count == 0)
        {
            SetThumbnailBlurred(false);
            IsVisible = false;
            return;
        }

        IsVisible = true;
        
        if (!wasAwaitingCapturedImage)
            m_thumbnail.Opacity = 0;

        var mostRecentImage = images[^1];
        var thumbnailBytes = mostRecentImage.ThumbnailAsByteArray ?? mostRecentImage.AsByteArray;
        m_thumbnail.Source = ImageSource.FromStream(() => new MemoryStream(thumbnailBytes));

        m_countLabel.Text = images.Count.ToString();
    }

    private void OnTapped()
    {
        if (m_isAwaitingCapturedImage)
            return;

        m_onTapped();
    }

    private void OnThumbnailPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        var hasFinishedLoading =
            e.PropertyName == Microsoft.Maui.Controls.Image.IsLoadingProperty.PropertyName && !m_thumbnail.IsLoading;
        if (!hasFinishedLoading)
            return;
        
        SetThumbnailBlurred(false);

        if (m_thumbnail.Opacity < 1)
            _ = m_thumbnail.FadeToAsync(1, 150);
    }
    
    /// <summary>
    /// Blurs the previous thumbnail natively while a new capture is in flight.
    /// </summary>
    private partial void SetThumbnailBlurred(bool isBlurred);
}
