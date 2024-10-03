using DIPS.Mobile.UI.API.Camera.ImageCapturing;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;

namespace DIPS.Mobile.UI.API.Camera.Shared;

public class CapturedImageInfoBottomSheet : BottomSheet
{
    public CapturedImageInfoBottomSheet(CapturedImage capturedImage)
    {
        var sizeListItem = new ListItem
        {
            Title = DUILocalizedStrings.Size,
            InLineContent = new Label { Text = $"{capturedImage.Size.SizeInMegaBytesWithTwoDecimals} mb" },
            HasBottomDivider = true
        };
        
        // Should we add a long press command to copy the image to the clipboard?
        // Maybe too much text hehe
        /*Touch.SetLongPressCommand(sizeListItem, new Command(() =>
        {
            Clipboard.SetTextAsync(byte array);
        }));*/
        
        var resolutionListItem = new ListItem
        {
            Title = DUILocalizedStrings.Resolution,
            InLineContent = new Label { Text = $"{capturedImage.Size.Width} x {capturedImage.Size.Height}" },
            HasBottomDivider = true
        };
        
        var orientationListItem = new ListItem
        {
            Title = DUILocalizedStrings.Orientation,
            InLineContent = new Label { Text = capturedImage.Transformation.OrientationDisplayName }
        };

        Content = new VerticalStackLayout
        {
            Spacing = 0, Children = { sizeListItem, resolutionListItem, orientationListItem }
        };
    }
}