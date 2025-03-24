namespace DIPS.Mobile.UI.Components.ListItems.Options.Title;

public partial class TitleOptions : ListItemOptions
{
    internal static void SetDefaultValues(ListItem listItem)
    {
        listItem.ColumnDefinitions[1].Width = (GridLength)WidthProperty.DefaultValue;
        
        if(listItem.TitleLabel is null)
            return;
        
        listItem.TitleLabel.Style = (Style?)StyleProperty.DefaultValue;
        listItem.TitleLabel.FontAttributes = (FontAttributes)FontAttributesProperty.DefaultValue;
        listItem.TitleLabel.TextColor = (Color)TextColorProperty.DefaultValue;
        listItem.TitleLabel.LineBreakMode = (LineBreakMode)LineBreakModeProperty.DefaultValue;
        listItem.TitleLabel.Margin = (Thickness)MarginProperty.DefaultValue;
        listItem.TitleLabel.MaximumWidthRequest = (double)MaxWidthProperty.DefaultValue;
    }
    
    protected override void DoBind(ListItem listItem)
    {
        listItem.ColumnDefinitions[1].Width = Width;

        if (listItem.TitleLabel is null)
            return;
        
        listItem.TitleLabel.SetBinding(VisualElement.StyleProperty, static (TitleOptions options) => options.Style, source: this);
        listItem.TitleLabel.SetBinding(Label.FontAttributesProperty, static (TitleOptions options) => options.FontAttributes, source: this);
        listItem.TitleLabel.SetBinding(Label.TextColorProperty, static (TitleOptions options) => options.TextColor, source: this);
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