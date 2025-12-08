using System.Collections.Specialized;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Gallery;

/// <summary>
/// A gallery component that displays a collection of images with swipe-to-navigate functionality.
/// </summary>
public partial class Gallery : Grid
{
    private readonly Label m_numberOfImagesLabel;
    private readonly ContentView m_borderAroundNumberOfImages;
    private bool m_isAnyImageZoomed;
    private INotifyCollectionChanged? m_previousCollection;

    public Gallery()
    {
        BackgroundColor = Colors.Black;

        var carouselView = new CarouselView.CarouselView { HorizontalScrollBarVisibility = ScrollBarVisibility.Never };
        carouselView.SetBinding(ItemsView.ItemsSourceProperty, static (Gallery gallery) => gallery.Images, source: this);
        
        carouselView.ItemTemplate = new DataTemplate(() =>
        {
            var zoomContainer = new PanZoomContainer.PanZoomContainer();
            zoomContainer.SetBinding(PanZoomContainer.PanZoomContainer.SourceProperty, static (ImageSource imageSource) => imageSource);
            zoomContainer.PropertyChanged += OnZoomContainerPropertyChanged;
            return zoomContainer;
        });
        
        carouselView.Loop = false;
        carouselView.PeekAreaInsets = new Thickness(0);
        this.SetBinding(CurrentImageIndexProperty, static (CarouselView.CarouselView carouselView) => carouselView.Position, source: carouselView);
        
        m_numberOfImagesLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            TextColor = Colors.White,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
        };
        
        m_borderAroundNumberOfImages = new ContentView
        {
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
            Content = m_numberOfImagesLabel,
            Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_small)),
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_4), 0, 0),
            IsVisible = false,
            BackgroundColor = Colors.Black.WithAlpha(.5f)
        };

        UI.Effects.Layout.Layout.SetCornerRadius(m_borderAroundNumberOfImages, Sizes.GetSize(SizeName.radius_large));
        
        Add(carouselView);
        Add(m_borderAroundNumberOfImages);
    }
    
    private void OnImagesChanged()
    {
        // Unsubscribe from previous collection
        if (m_previousCollection is not null)
        {
            m_previousCollection.CollectionChanged -= OnImagesCollectionChanged;
        }
        
        // Subscribe to new collection if it supports notifications
        if (Images is INotifyCollectionChanged notifyCollection)
        {
            notifyCollection.CollectionChanged += OnImagesCollectionChanged;
            m_previousCollection = notifyCollection;
        }
        else
        {
            m_previousCollection = null;
        }
        
        UpdateImagesDisplay();
    }
    
    private void OnImagesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        UpdateImagesDisplay();
    }
    
    private void UpdateImagesDisplay()
    {
        if(Images is null)
            return;

        _ = UpdateNumberOfImagesVisibility();
        UpdateLabelText();
    }
    
    private void OnCurrentImageIndexChanged()
    {
        UpdateLabelText();
    }
    
    private void UpdateLabelText()
    {
        if (Images is null)
            return;
            
        m_numberOfImagesLabel.Text = $"{CurrentImageIndex + 1}/{Images.Count()}";
    }

    private void OnZoomContainerPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(PanZoomContainer.PanZoomContainer.IsZoomed))
        {
            return;
        }

        if (sender is not PanZoomContainer.PanZoomContainer zoomContainer)
        {
            return;
        }

        m_isAnyImageZoomed = zoomContainer.IsZoomed;
        _ = UpdateNumberOfImagesVisibility();
    }

    private async Task UpdateNumberOfImagesVisibility()
    {
        var shouldShow = Images != null && !m_isAnyImageZoomed;

        if (m_borderAroundNumberOfImages.IsVisible && !shouldShow)
        {
            await m_borderAroundNumberOfImages.FadeTo(0);
            m_borderAroundNumberOfImages.IsVisible = false;
        }
        else
        {
            m_borderAroundNumberOfImages.Opacity = 0;
            m_borderAroundNumberOfImages.IsVisible = true;
            await m_borderAroundNumberOfImages.FadeTo(1);
        }
    }
}