using System.Collections.Specialized;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = Microsoft.Maui.Graphics.Colors;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;

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
    private readonly Button m_navigatePreviousImageButton;
    private readonly Button m_navigateNextImageButton;

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
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
        };
        m_numberOfImagesLabel.SetBinding(Label.TextColorProperty, static (Gallery gallery) => gallery.GalleryCustomizer.ImageCountTextColor, source: this);
        
        m_borderAroundNumberOfImages = new ContentView
        {
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
            Content = m_numberOfImagesLabel,
            Padding = new Thickness(Sizes.GetSize(SizeName.content_margin_small)),
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_4), 0, 0),
            Opacity = 0
        };
        m_borderAroundNumberOfImages.SetBinding(BackgroundColorProperty, static (Gallery gallery) => gallery.GalleryCustomizer.ImageCountBackgroundColor, source: this);

        UI.Effects.Layout.Layout.SetCornerRadius(m_borderAroundNumberOfImages, Sizes.GetSize(SizeName.radius_large));
        
        m_navigatePreviousImageButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.chevron_left_line),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconSmall),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Start,
            Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_large), 0, 0, 0),
            Command = new Command(() =>
            {
                if(carouselView.Position == 0)
                    return;

                carouselView.Position -= 1;
            }),
            Opacity = 0
        };
        m_navigatePreviousImageButton.SetBinding(Button.ImageTintColorProperty, static (Gallery gallery) => gallery.GalleryCustomizer.NavigateButtonsImageTintColor, source: this);
        m_navigatePreviousImageButton.SetBinding(BackgroundColorProperty, static (Gallery gallery) => gallery.GalleryCustomizer.NavigateButtonsBackgroundColor, source: this);
        
        m_navigateNextImageButton = new Button
        {
            ImageSource = Icons.GetIcon(IconName.chevron_right_line),
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconSmall),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.End,
            Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.content_margin_large), 0),
            Command = new Command(() =>
            {
                if(carouselView.Position == Images?.ToList().Count - 1)
                    return;

                carouselView.Position += 1;
            }),
            Opacity = 0
        };
        m_navigateNextImageButton.SetBinding(Button.ImageTintColorProperty, static (Gallery gallery) => gallery.GalleryCustomizer.NavigateButtonsImageTintColor, source: this);
        m_navigateNextImageButton.SetBinding(BackgroundColorProperty, static (Gallery gallery) => gallery.GalleryCustomizer.NavigateButtonsBackgroundColor, source: this);
        
        Add(carouselView);
        Add(m_borderAroundNumberOfImages);
        Add(m_navigatePreviousImageButton);
        Add(m_navigateNextImageButton);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        UpdateNavigationButtonsVisibility();
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
        
        OnImagesUpdated();
    }
    
    private void OnImagesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        OnImagesUpdated();
    }
    
    private void OnImagesUpdated()
    {
        if(Images is null)
            return;

        UpdateNumberOfImagesVisibility();
        UpdateNavigationButtonsVisibility();
        UpdateLabelText();
    }

    private void UpdateNavigationButtonsVisibility()
    {
        var navigatePreviousShouldBeVisible = CurrentImageIndex != 0 && !m_isAnyImageZoomed && Images?.ToList().Count > 0;
        var navigateNextShouldBeVisible = CurrentImageIndex != Images?.ToList().Count - 1 && !m_isAnyImageZoomed && Images?.ToList().Count > 0;

        if (navigatePreviousShouldBeVisible)
        {
            _ = SetViewVisibleAnimated(m_navigatePreviousImageButton);
        }
        else
        {
            _ = SetViewInvisibleAnimated(m_navigatePreviousImageButton);
        }

        if (navigateNextShouldBeVisible)
        {
            _ = SetViewVisibleAnimated(m_navigateNextImageButton); 
        }
        else
        {
            _ = SetViewInvisibleAnimated(m_navigateNextImageButton);
        }
    }

    private static async Task SetViewInvisibleAnimated(View view)
    {
        if(view.Opacity == 0f)
            return;
        
        await view.FadeTo(0);
    }

    private static async Task SetViewVisibleAnimated(View view)
    {
        if (Math.Abs(view.Opacity - 1) < 0.01f)
            return;
        
        view.Opacity = 0;
        await view.FadeTo(1);
    }

    private void OnCurrentImageIndexChanged()
    {
        UpdateLabelText();
        UpdateNavigationButtonsVisibility();
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

        if(!FadeOutOnZoom)
            return;
        
        m_isAnyImageZoomed = zoomContainer.IsZoomed;
        
        UpdateNumberOfImagesVisibility();
        UpdateNavigationButtonsVisibility();
    }

    private void UpdateNumberOfImagesVisibility()
    {
        var shouldShow = Images != null && !m_isAnyImageZoomed;

        if (m_borderAroundNumberOfImages.IsVisible && !shouldShow)
        {
            _ = SetViewInvisibleAnimated(m_borderAroundNumberOfImages);
        }
        else
        {
            _ = SetViewVisibleAnimated(m_borderAroundNumberOfImages);
        }
    }
}