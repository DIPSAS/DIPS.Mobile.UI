using System.Linq;
using DIPS.Mobile.UI.API.Camera.Preview;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace DIPS.Mobile.UI.UnitTests.API.Camera.Preview;

public class CameraPreviewTests
{
    [Fact]
    public void BottomToolbar_CompactScreen_DoesNotClipContent()
    {
        // Matches the specimen-scanner frame and bottom content measured on an iPhone SE.
        const double previewWidth = 375;
        const double previewHeight = 593;
        const double bottomContentHeight = 102;

        var cameraPreview = new CameraPreview();
        var root = cameraPreview.ConstructView();
        cameraPreview.Content = root;
        cameraPreview.Measure(previewWidth, previewHeight);
        cameraPreview.Arrange(new Rect(0, 0, previewWidth, previewHeight));
        cameraPreview.SetToolbarHeights((float)previewHeight);

        var bottomContent = new Grid
        {
            HeightRequest = bottomContentHeight
        };
        cameraPreview.AddBottomToolbarView(bottomContent);

        var bottomToolbar = root.Children
            .OfType<Grid>()
            .Single(grid => grid.Children.Contains(bottomContent));
        var expectedLetterboxHeight = previewHeight
                                      - previewWidth / CameraPreview.ThreeFourRatio
                                      - CameraPreview.ComputeTopToolbarHeight((float)previewWidth, (float)previewHeight);

        bottomToolbar.HeightRequest.Should().Be(-1);
        bottomToolbar.MinimumHeightRequest.Should().BeApproximately(expectedLetterboxHeight, 0.01);
        bottomContent.HeightRequest.Should().BeGreaterThan(bottomToolbar.MinimumHeightRequest);
    }
}
