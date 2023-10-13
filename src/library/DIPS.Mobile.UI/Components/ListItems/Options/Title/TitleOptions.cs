namespace DIPS.Mobile.UI.Components.ListItems.Options.Title;

public partial class TitleOptions : ListItemOptions
{
    public override void DoBind(ListItem listItem)
    {
        listItem.MainContent.ColumnDefinitions[1].Width = Width;
        
        if(listItem.TitleLabel is null)
            return;

        listItem.TitleLabel.SetBinding(Label.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        listItem.TitleLabel.SetBinding(Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        listItem.TitleLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));
        listItem.TitleLabel.SetBinding(Label.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        listItem.TitleLabel.SetBinding(Label.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        listItem.TitleLabel.SetBinding(Label.MaxLinesProperty, new Binding(nameof(MaxLines), source: this));
        listItem.TitleLabel.SetBinding(Label.LineBreakModeProperty, new Binding(nameof(LineBreakMode), source: this));
        listItem.TitleLabel.SetBinding(View.MarginProperty, new Binding(nameof(Margin), source: this));
        
    }

}