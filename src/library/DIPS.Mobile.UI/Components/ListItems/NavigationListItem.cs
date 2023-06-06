using DIPS.Mobile.UI.Effects.Touch;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class NavigationListItem : ListItem
{
    public NavigationListItem()
    {
        ContentItem = new Image
        {
            Source = Icons.GetIcon(IconName.arrow_right_s_line)
        };;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        Touch.SetAccessibilityContentDescription(this, string.Join(".", Title, Subtitle));
        Touch.SetCommand(this, Command);
    }
    
}