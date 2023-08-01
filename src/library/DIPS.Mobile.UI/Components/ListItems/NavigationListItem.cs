using DIPS.Mobile.UI.Effects.Touch;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(CustomContentItem))]
public partial class NavigationListItem : ListItem
{
    private readonly Grid m_contentGrid;

    public NavigationListItem()
    {
        m_contentGrid = new Grid()
        {
            ColumnDefinitions = new ColumnDefinitionCollection() {new(GridLength.Auto), new(GridLength.Auto)}
        };
        
        if (CustomContentItem != null)
        {
            AddCustomContentItem();
        }
        
        m_contentGrid.Add(new Image {Source = Icons.GetIcon(IconName.arrow_right_s_line), VerticalOptions = LayoutOptions.Center}, 1);
        HorizontalContentItem = m_contentGrid;
    }

    private void AddCustomContentItem()
    {
        m_contentGrid.RemoveChildAt(0, 0);
        m_contentGrid.Add(CustomContentItem, 0);
    }

    private void OnCustomContentItemPropertyChanged() => AddCustomContentItem();
}