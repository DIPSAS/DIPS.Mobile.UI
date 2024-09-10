using System.Windows.Input;

namespace Components.ComponentsSamples.ImageCapturing.ImageGallery;

public partial class ImageGallerySamples
{
    public ImageGallerySamples()
    {
        InitializeComponent();

        CameraButtonTappedCommand = new Command(() =>
        {
            Close();
        });
    }

    public ICommand CameraButtonTappedCommand { get; }
}