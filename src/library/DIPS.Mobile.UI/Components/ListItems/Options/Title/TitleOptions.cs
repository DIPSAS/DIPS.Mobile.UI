namespace DIPS.Mobile.UI.Components.ListItems.Options.Title;

public partial class TitleOptions : ListItemOptions
{
    public override void DoBind(ListItem listItem)
    {
        
        listItem.ContainerGrid.ColumnDefinitions[1].Width = Width;
        
        if(listItem.TitleLabel is null)
            return;
        
        listItem.TitleLabel.SetBinding(Label.TextProperty, static (ListItem listItem) => listItem.Title, source: listItem);
        listItem.TitleLabel.SetBinding(VisualElement.StyleProperty, static (TitleOptions options) => options.Style, source: this);
        listItem.TitleLabel.SetBinding(Label.FontAttributesProperty, static (TitleOptions options) => options.FontAttributes, source: this);
        listItem.TitleLabel.SetBinding(Label.TextColorProperty, static (TitleOptions options) => options.TextColor, source: this);
        listItem.TitleLabel.SetBinding(Label.HorizontalTextAlignmentProperty, static (TitleOptions options) => options.HorizontalTextAlignment, source: this);
        listItem.TitleLabel.SetBinding(Label.VerticalTextAlignmentProperty, static (TitleOptions options) => options.VerticalTextAlignment, source: this);
        listItem.TitleLabel.SetBinding(Label.LineBreakModeProperty, static (TitleOptions options) => options.LineBreakMode, source: this);
        listItem.TitleLabel.SetBinding(View.MarginProperty, static (TitleOptions options) => options.Margin, source: this);
        listItem.TitleLabel.SetBinding(Label.FormattedTextProperty, static (TitleOptions options) => options.FormattedText, source: this);
        listItem.TitleLabel.SetBinding(VisualElement.MaximumWidthRequestProperty, static (TitleOptions options) => options.MaxWidth, source: this);
        
        if (MaxLines > -1) //We can not trigger property changed for this if its -1 because it causes bugs on Android.
        {
            listItem.TitleLabel.MaxLines = MaxLines;
        }
    }

}