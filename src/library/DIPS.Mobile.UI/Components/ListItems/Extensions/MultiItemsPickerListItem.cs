namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

public partial class MultiItemsPickerListItem : ListItem
{
    private readonly Grid m_inlineContentGrid;

    public MultiItemsPickerListItem()
    {
        m_inlineContentGrid = new Grid()
        {
            RowDefinitions = new RowDefinitionCollection(new RowDefinition(GridLength.Auto)),
            ColumnDefinitions = new ColumnDefinitionCollection(new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto))
        };

        m_inlineContentGrid.Add(
            new Image() {Source = Icons.GetIcon(IconName.arrow_right_s_line), HorizontalOptions = LayoutOptions.End},
            1);

        HorizontalContentItemColumnWidth = GridLength.Star;
        TitleColumnWidth = GridLength.Auto;
        ShouldOverrideContentItemLayoutOptions = false;
        HorizontalContentItem = m_inlineContentGrid;
    }
    
    private void MultiItemPickerPropertyChanged()
    {
        if (MultiItemPicker == null) return;
        Command = MultiItemPicker.OpenCommand;
        MultiItemPicker.HorizontalOptions = LayoutOptions.End; //Place it to the right in a list item
        m_inlineContentGrid.Add(MultiItemPicker, 0);
    }
}