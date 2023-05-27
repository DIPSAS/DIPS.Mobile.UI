using DIPS.Mobile.UI.Effects.DUITouchEffect;
using DIPS.Mobile.UI.Resources.Icons;

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
        
        DUITouchEffect.SetAccessibilityContentDescription(this, string.Join(".", Title, Subtitle));
        DUITouchEffect.SetCommand(this, Command);
    }
    
}