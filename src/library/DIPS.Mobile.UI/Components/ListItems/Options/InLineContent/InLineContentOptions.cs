namespace DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;

public partial class InLineContentOptions : ListItemOptions
{
    public override void DoBind(ListItem listItem)
    { 
        listItem.ContainerGrid.ColumnDefinitions[2].Width = Width;
        
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