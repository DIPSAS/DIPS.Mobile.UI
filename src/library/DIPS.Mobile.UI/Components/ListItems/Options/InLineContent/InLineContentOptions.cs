using Label = DIPS.Mobile.UI.Components.Labels.Label;

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
        
        inLineContent.SetBinding(View.HorizontalOptionsProperty, new Binding(nameof(HorizontalOptions), source: this));
        inLineContent.SetBinding(View.VerticalOptionsProperty, new Binding(nameof(VerticalOptions), source: this));
    }

}