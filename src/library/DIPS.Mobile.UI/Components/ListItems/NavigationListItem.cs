using DIPS.Mobile.UI.Effects.AwesomeTouchEffect;
using DIPS.Mobile.UI.Resources.Icons;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class NavigationListItem : ListItem
{
    public NavigationListItem()
    {
        var checkMark = new Image
        {
            Source = IconLookup.GetIcon(IconName.arrow_right_s_line)
        };

        ContentItem = checkMark;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        DUITouchEffect.SetCommandAccessibilityContentDescription(this, string.Join(".", Title, SubTitle));
        DUITouchEffect.SetCommand(this, Command);
    }
    
}