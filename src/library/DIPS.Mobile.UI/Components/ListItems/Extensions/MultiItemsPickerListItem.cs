using DIPS.Mobile.UI.Internal;

namespace DIPS.Mobile.UI.Components.ListItems.Extensions;

[ContentProperty(nameof(Pickers.MultiItemsPicker.MultiItemsPicker))]
public partial class MultiItemsPickerListItem : ListItem
{
    private readonly Grid m_inlineContentGrid;

    public MultiItemsPickerListItem()
    {
        m_inlineContentGrid = new Grid()
        {
            AutomationId = "InlineContentGrid".ToDUIAutomationId<MultiItemsPickerListItem>(),
            RowDefinitions = new RowDefinitionCollection(new RowDefinition(GridLength.Auto)),
            ColumnDefinitions = new ColumnDefinitionCollection(new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto))
        };

        m_inlineContentGrid.Add(
            new Image() {Source = Icons.GetIcon(IconName.arrow_right_s_line), HorizontalOptions = LayoutOptions.End},
            1);

        InLineContentOptions.Width = GridLength.Star;
        InLineContentOptions.HorizontalOptions = LayoutOptions.Fill;
        InLineContentOptions.VerticalOptions = LayoutOptions.Fill;
        TitleOptions.Width = GridLength.Auto;

        InLineContent = m_inlineContentGrid;
    }
    
    private void MultiItemPickerPropertyChanged()
    {
        if (MultiItemsPicker == null) return;
        Command = MultiItemsPicker.OpenCommand;
        MultiItemsPicker.HorizontalOptions = LayoutOptions.End; //Place it to the right in a list item
        m_inlineContentGrid.Add(MultiItemsPicker, 0);
    }
}