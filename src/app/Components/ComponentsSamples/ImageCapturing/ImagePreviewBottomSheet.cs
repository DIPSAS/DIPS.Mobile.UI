using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.Components.BottomSheets;

namespace Components.ComponentsSamples.ImageCapturing;

public class ImagePreviewBottomSheet : BottomSheet
{
    private readonly CapturedImage m_capturedImage;

    public ImagePreviewBottomSheet(CapturedImage capturedImage)
    {
        m_capturedImage = capturedImage;
    }

    protected override void OnOpened()
    {
        Content = new Image() {Source = ImageSource.FromStream(() => new MemoryStream(m_capturedImage.AsByteArray)), HeightRequest = 400, WidthRequest = 400, BackgroundColor = Colors.Aqua};
        base.OnOpened();
    }
}