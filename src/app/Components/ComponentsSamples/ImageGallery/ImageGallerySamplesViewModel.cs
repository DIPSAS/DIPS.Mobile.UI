using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.ImageGallery;

public class ImageGallerySamplesViewModel : ViewModel
{
    private List<byte[]> m_images;
    private bool m_isDisplayingGallery;
    private int m_startingIndex;
    public static List<byte[]> StoredImages { get; } = [];

    public ImageGallerySamplesViewModel()
    {
        Images = StoredImages;
        CameraButtonTappedCommand = new Command(() =>
        {
            DialogService.ShowMessage("Camera button tapped",
                "It would be natural to open the camera when pressing this button", "OK");
        });
    }

    private void OnTappedImage(int index)
    {
        StartingIndex = index;
        IsDisplayingGallery = true;
    }

    public List<byte[]> Images
    {
        get => m_images;
        set => RaiseWhenSet(ref m_images, value);
    }

    public bool IsDisplayingGallery
    {
        get => m_isDisplayingGallery;
        set => RaiseWhenSet(ref m_isDisplayingGallery, value);
    }

    public int StartingIndex
    {
        get => m_startingIndex;
        set => RaiseWhenSet(ref m_startingIndex, value);
    }

    public ICommand CameraButtonTappedCommand { get; }
}