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
        
        m_contentGrid.Add(new Image {Source = Icons.GetIcon(IconName.arrow_right_s_line)}, 1);
        ContentItem = m_contentGrid;
    }

    private void CustomizeMainContent()
    {
        if(Icon is null)
            return;
        
        foreach (var view in MainContent.Children)
        {
            MainContent.SetColumn(view, MainContent.GetColumn(view) + 1);
        }
        
        MainContent.Add(new Images.Image.Image
        {
            Source = Icon,
            TintColor = IconColor,
            Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.size_4), 0)
        }, 0);
        MainContent.ColumnDefinitions.Insert(0, new ColumnDefinition(GridLength.Auto));
        
    }

    private void AddCustomContentItem()
    {
        m_contentGrid.RemoveChildAt(0, 0);
        m_contentGrid.Add(CustomContentItem, 0);
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
        
        CustomizeMainContent();
    }

    private void OnCustomContentItemPropertyChanged() => AddCustomContentItem();
}