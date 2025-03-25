namespace DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;

public partial class InLineContentOptions : ListItemOptions
{
    internal static void SetupDefaults(ListItem listItem)
    {
        listItem.ColumnDefinitions[2].Width = (GridLength)WidthProperty.DefaultValue;
        
        if (listItem.InLineContent is not View inLineContent)
        {
            return;
        }
        
        inLineContent.HorizontalOptions = (LayoutOptions)HorizontalOptionsProperty.DefaultValue;
        inLineContent.VerticalOptions = (LayoutOptions)VerticalOptionsProperty.DefaultValue;
        
        if ((bool)SpanOverUnderlyingContentProperty.DefaultValue)
        {
            Grid.SetRowSpan(inLineContent, 2);
        }
    }
    
    protected override void DoBind(ListItem listItem)
    { 
        listItem.ColumnDefinitions[2].Width = Width;
        
        if (listItem.InLineContent is not View inLineContent)
        {
            return;
        }

        if (SpanOverUnderlyingContent)
        {
            Grid.SetRowSpan(inLineContent, 2);
        }
        
        inLineContent.SetBinding(View.HorizontalOptionsProperty, static (InLineContentOptions inLineContentOptions) => inLineContentOptions.HorizontalOptions, source: this);
        inLineContent.SetBinding(View.VerticalOptionsProperty, static (InLineContentOptions inLineContentOptions) => inLineContentOptions.VerticalOptions, source: this);
    }

}