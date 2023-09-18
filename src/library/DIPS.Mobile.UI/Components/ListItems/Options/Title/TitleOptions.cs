namespace DIPS.Mobile.UI.Components.ListItems.Options.Title;

public partial class TitleOptions : ListItemOptions
{
    public override void DoBind(ListItem listItem)
    {
        if(listItem.TitleLabel is null)
            return;

        listItem.TitleLabel.SetBinding(Labels.Label.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        listItem.TitleLabel.SetBinding(Labels.Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        listItem.TitleLabel.SetBinding(Labels.Label.TextColorProperty, new Binding(nameof(TextColor), source: this));
        listItem.TitleLabel.SetBinding(Labels.Label.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        listItem.TitleLabel.SetBinding(Labels.Label.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        listItem.TitleLabel.SetBinding(View.MarginProperty, new Binding(nameof(Margin), source: this));
        listItem.MainContent.ColumnDefinitions[1].Width = Width;
        
    }

}