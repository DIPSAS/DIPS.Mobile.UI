using DIPS.Mobile.UI.Components.Images;
using Image = DIPS.Mobile.UI.Components.Images.Image;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class NavigationListItem : ListItem
{
    public NavigationListItem()
    {
        var checkMark = new Image
        {
            //TODO: Change to svg icon
            iOSProperties = new iOSImageProperties { SystemIconName = "chevron.right" },
            AndroidProperties =
                new AndroidImageProperties { IconResourceName = "abc_ic_arrow_drop_right_black_24dp" }
        };

        ContentItem = checkMark;

        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, new Binding(nameof(Command), source: this));

        GestureRecognizers.Add(tapGestureRecognizer);
    }
    
    
    
}