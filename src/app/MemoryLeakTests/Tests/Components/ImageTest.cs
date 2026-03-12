using DIPS.Mobile.UI.Components.Images;
using DIPS.Mobile.UI.Resources.Icons;
using Image = DIPS.Mobile.UI.Components.Images.Image;

namespace MemoryLeakTests.Tests;

public class ImageTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        contentPage.Content = new VerticalStackLayout
        {
            Children =
            {
                new Image.Image { Source = Icons.GetIcon(IconName.check_line) },
                new ImageButton { Source = Icons.GetIcon(IconName.close_line) }
            }
        };
    }

    public override string Name => "Image";
}
