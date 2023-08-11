using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.Components.ListItems.ExtendedListItems;

[ContentProperty(nameof(InLineContent))]
public partial class NavigationListItem : ListItem
{
    private readonly Grid m_contentGrid = new()
    {
        ColumnDefinitions = new ColumnDefinitionCollection
        {
            new (GridLength.Star),
            new(GridLength.Auto)
        },
        RowDefinitions = new RowDefinitionCollection
        {
            new (GridLength.Auto)
        },
        VerticalOptions = LayoutOptions.Center
    };
    
    

    public NavigationListItem()
    {
        m_contentGrid.Add(new Image
        {
            Source = Icons.GetIcon(IconName.arrow_right_s_line), 
            VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.End
        }, 1);
    }

    protected override void AddInLineContent()
    {
        var newInLineContent = InLineContent;
            
        m_contentGrid.Insert(0, newInLineContent);

        SetInLineContent(m_contentGrid);
    }
}