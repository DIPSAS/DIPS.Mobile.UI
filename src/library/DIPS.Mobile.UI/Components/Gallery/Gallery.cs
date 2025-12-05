using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.Gallery;

/// <summary>
/// A gallery component that displays a collection of images with swipe-to-navigate functionality.
/// </summary>
public partial class Gallery : Grid
{
    private readonly CarouselView.CarouselView m_carouselView;

    public Gallery()
    {
        BackgroundColor = Colors.Black;
        
        m_carouselView = new CarouselView.CarouselView();
        m_carouselView.SetBinding(ItemsView.ItemsSourceProperty, static (Gallery gallery) => gallery.Images, source: this);
        
        m_carouselView.ItemTemplate = new DataTemplate(() =>
        {
            var zoomContainer = new PanZoomContainer.PanZoomContainer();
            zoomContainer.SetBinding(PanZoomContainer.PanZoomContainer.SourceProperty, static (ImageSource imageSource) => imageSource);
            return zoomContainer;
        });
        
        m_carouselView.Loop = false;
        m_carouselView.PeekAreaInsets = new Thickness(0);
        
        m_carouselView.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(CarouselView.CarouselView.Position))
            {
                OnPropertyChanged(nameof(Position));
            }
        };
        
        Add(m_carouselView);
    }

    /// <summary>
    /// Gets or sets the current position in the gallery.
    /// </summary>
    public int Position
    {
        get => m_carouselView.Position;
        set => m_carouselView.Position = value;
    }
}