using System.Windows.Input;

namespace DIPS.Mobile.UI.API.Camera.ImageGallery;

public partial class ImageGallery
{
    /// <summary>
    /// The images to be displayed
    /// <remarks>The default bindingmode is <see cref="BindingMode.TwoWay"/></remarks>
    /// </summary>
    public List<byte[]> Images
    {
        get => (List<byte[]>)GetValue(ImagesProperty);
        set => SetValue(ImagesProperty, value);
    }

    /// <summary>
    /// The command to be executed when the user presses the done button.
    /// </summary>
    public ICommand? DoneCommand
    {
        get => (ICommand)GetValue(DoneCommandProperty);
        set => SetValue(DoneCommandProperty, value);
    }

    /// <summary>
    /// What index the <see cref="CarouselView"/> should start at
    /// </summary>
    public int StartingIndex
    {
        get => (int)GetValue(StartingIndexProperty);
        set => SetValue(StartingIndexProperty, value);
    }
    
    public static readonly BindableProperty DoneCommandProperty = BindableProperty.Create(
        nameof(DoneCommand),
        typeof(ICommand),
        typeof(ImageGallery));
    
    public static readonly BindableProperty ImagesProperty = BindableProperty.Create(
        nameof(Images),
        typeof(List<byte[]>),
        typeof(ImageGallery),
        defaultBindingMode: BindingMode.TwoWay,
        propertyChanged: (bindable, _, _) => ((ImageGallery)bindable).OnImagesChanged());
    
    public static readonly BindableProperty StartingIndexProperty = BindableProperty.Create(
        nameof(StartingIndex),
        typeof(int),
        typeof(ImageGallery));
}