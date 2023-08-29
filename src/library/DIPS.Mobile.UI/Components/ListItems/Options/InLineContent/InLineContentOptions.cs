using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;

public partial class InLineContentOptions : BindableObject, IListItemOptions
{
    public void Bind(ListItem listItem)
    {
        BindingContext = listItem.BindingContext;
        
        if(listItem.InLineContent is not View inLineContent)
            return;

        inLineContent.SetBinding(View.HorizontalOptionsProperty, new Binding(nameof(HorizontalOptions), source: this));
        inLineContent.SetBinding(View.VerticalOptionsProperty, new Binding(nameof(VerticalOptions), source: this));
        listItem.MainContent.ColumnDefinitions[2].Width = Width;
    }

}