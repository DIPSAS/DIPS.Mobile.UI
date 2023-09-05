using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Effects.Touch;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Sorting;

public partial class SortControl : HorizontalStackLayout
{
    public SortControl()
    {
        Spacing = Sizes.GetSize(SizeName.size_1);
        
        Touch.SetCommand(this, new Command(OpenBottomSheet));
        
        var label = new Label
        {
            Text = "Sort",
            FontAttributes = FontAttributes.Bold,
            FontSize = 11,
            TextTransform = TextTransform.Uppercase,
            TextColor = Colors.GetColor(ColorName.color_primary_90),
            VerticalTextAlignment = TextAlignment.Center
        };

        var sortImage = new Image
        {
            Source = Icons.GetIcon(IconName.ascending_fill),
            TintColor = Colors.GetColor(ColorName.color_primary_90),
            VerticalOptions = LayoutOptions.Center,
            WidthRequest = Sizes.GetSize(SizeName.size_5),
            HeightRequest = Sizes.GetSize(SizeName.size_5)
        };
        
        Add(label);
        Add(sortImage);
    }

    private void OpenBottomSheet()
    {
        BottomSheetService.OpenBottomSheet(new SortControlBottomSheet(this));
    }

}