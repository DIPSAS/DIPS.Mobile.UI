using System.Windows.Input;
using DIPS.Mobile.UI.Components.Alerting.Dialog;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.MVVM;

namespace Components.ComponentsSamples.ImageCapturing.ImageGallery;

public class ImageGallerySamplesViewModel : ViewModel
{
    private List<byte[]> m_images;
    private bool m_isDisplayingGallery;
    private int m_startingIndex;
    public static List<byte[]> StoredImages { get; set; } = [];

    public ImageGallerySamplesViewModel()
    {
        Images = StoredImages;
        CameraButtonTappedCommand = new Command(() =>
        {
            _ = BottomSheetService.CloseAll();
        });
    }

    public List<byte[]> Images
    {
        get => m_images;
        set
        {
            RaiseWhenSet(ref m_images, value);
            StoredImages = value;
        }
    }

    public ICommand CameraButtonTappedCommand { get; }
}