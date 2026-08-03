using System.Linq;
using DIPS.Mobile.UI.API.Camera.Gallery;
using DIPS.Mobile.UI.API.Camera.ImageCapturing;

namespace DIPS.Mobile.UI.UnitTests.API.Camera.Gallery;

public class GalleryThumbnailsTests
{
    [Fact]
    public void UpdateImages_MultipleEditsFromOpenGallery_RetainsEveryEdit()
    {
        var firstImage = new CapturedImage();
        var secondImage = new CapturedImage();
        var firstEditedImage = new CapturedImage();
        var secondEditedImage = new CapturedImage();
        var imagesBeingEdited = new List<CapturedImage> { firstImage, secondImage };
        var gallery = new GalleryThumbnails { Images = imagesBeingEdited.ToList() };

        imagesBeingEdited[0] = firstEditedImage;
        gallery.UpdateImages(imagesBeingEdited);
        imagesBeingEdited[1] = secondEditedImage;
        gallery.UpdateImages(imagesBeingEdited);

        gallery.Images.Should().Equal(firstEditedImage, secondEditedImage);
    }
}
