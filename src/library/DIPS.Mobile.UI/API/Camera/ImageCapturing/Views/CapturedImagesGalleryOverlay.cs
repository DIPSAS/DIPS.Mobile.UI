using System.Collections.ObjectModel;
using DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.BottomToolbar;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Internal.Logging;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views;

/// <summary>
/// The in-camera gallery for multi capture.
/// </summary>
internal sealed class CapturedImagesGalleryOverlay : Grid
{
    private readonly ObservableCollection<ImageSource> m_imageSources;
    private readonly Action<int> m_onRemoveImageAtAction;
    private readonly Components.Gallery.Gallery m_gallery;
    private readonly Label m_emptyStateLabel;
    private readonly ButtonWithText m_removeButton;

    private bool m_hasAnimatedIn;

    public CapturedImagesGalleryOverlay(IReadOnlyList<CapturedImage> images, int startingIndex, Action<int> onRemoveImageAtAction, Action onCloseAction)
    {
        ArgumentNullException.ThrowIfNull(images);
        ArgumentNullException.ThrowIfNull(onRemoveImageAtAction);
        ArgumentNullException.ThrowIfNull(onCloseAction);
        if (images.Count == 0)
            throw new ArgumentException("The captured-images gallery needs at least one image to open.", nameof(images));

        m_onRemoveImageAtAction = onRemoveImageAtAction;

        BackgroundColor = Colors.GetColor(ColorName.color_palette_base_black);
        
        Opacity = 0;

        m_imageSources = new ObservableCollection<ImageSource>(images.Select(ToImageSource));

        m_gallery = new Components.Gallery.Gallery
        {
            CurrentImageIndex = startingIndex,
            Images = m_imageSources,
            FadeOutOnZoom = true
        };

        m_emptyStateLabel = new Label
        {
            Text = DUILocalizedStrings.NoCapturedImages,
            TextColor = Colors.GetColor(ColorName.color_palette_base_white),
            Style = Styles.GetLabelStyle(LabelStyle.Body300),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            IsVisible = false
        };

        m_removeButton = new ButtonWithText(DUILocalizedStrings.Delete, Icons.GetIcon(IconName.delete_line), OnRemoveImageTapped)
        {
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.Start
        };

        var doneButton = new ButtonWithText(DUILocalizedStrings.Done, Icons.GetIcon(IconName.check_line), onCloseAction)
        {
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.End
        };

        var bottomToolbar = new Grid
        {
            VerticalOptions = LayoutOptions.End,
            Margin = new Thickness(Sizes.GetSize(SizeName.size_5), 0, Sizes.GetSize(SizeName.size_5), Sizes.GetSize(SizeName.size_5)),
            Children = { m_removeButton, doneButton }
        };

        Add(m_gallery);
        Add(m_emptyStateLabel);
        Add(bottomToolbar);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is null || m_hasAnimatedIn)
            return;
        
        m_hasAnimatedIn = true;
        
        _ = this.FadeToAsync(1, 150);
    }

    public async Task FadeOutAsync()
    {
        await this.FadeToAsync(0, 150);
    }

    private async void OnRemoveImageTapped()
    {
        var index = m_gallery.CurrentImageIndex;
        if (index < 0 || index >= m_imageSources.Count)
            return;

        try
        {
            var dialogResult = await DialogService.ShowMessage(configurator => configurator
                .SetTitle(DUILocalizedStrings.RemoveImageTitle)
                .SetDescription(DUILocalizedStrings.RemoveImageDescription)
                .SetActionTitle(DUILocalizedStrings.Remove)
                .SetCancelTitle(DUILocalizedStrings.Cancel)
                .SetDestructive());

            if (dialogResult == DialogAction.Closed)
                return;

            m_imageSources.RemoveAt(index);
            m_onRemoveImageAtAction(index);

            if (m_imageSources.Count == 0)
                ShowEmptyState();
        }
        catch (Exception e)
        {
            DUILogService.LogError<CapturedImagesGalleryOverlay>(e.Message);
        }
    }

    private void ShowEmptyState()
    {
        m_gallery.IsVisible = false;
        m_removeButton.IsVisible = false;
        m_emptyStateLabel.IsVisible = true;
    }

    private static ImageSource ToImageSource(CapturedImage capturedImage)
    {
        var imageBytes = capturedImage.AsByteArray;
        return ImageSource.FromStream(() => new MemoryStream(imageBytes));
    }
}
