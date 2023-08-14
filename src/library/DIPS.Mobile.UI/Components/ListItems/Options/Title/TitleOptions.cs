using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Title;

public partial class TitleOptions : BindableObject, IListItemOptions
{
    public void Bind(ListItem listItem)
    {
        if(listItem.TitleLabel is null)
            return;

        listItem.TitleLabel.SetBinding(Label.FontAttributesProperty, new Binding(nameof(FontAttributes), source: this));
        listItem.TitleLabel.SetBinding(Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));
        listItem.TitleLabel.SetBinding(Label.TextColorProperty, new Binding(nameof(TextColor), source: this));
        listItem.TitleLabel.SetBinding(Label.HorizontalTextAlignmentProperty, new Binding(nameof(HorizontalTextAlignment), source: this));
        listItem.TitleLabel.SetBinding(Label.VerticalTextAlignmentProperty, new Binding(nameof(VerticalTextAlignment), source: this));
        listItem.TitleLabel.SetBinding(View.MarginProperty, new Binding(nameof(Margin), source: this));
        listItem.TitleLabel.Margin = new Thickness(0, 0, Sizes.GetSize(SizeName.size_3), 0);
        listItem.MainContent.ColumnDefinitions[1].Width = Width;
    }

}