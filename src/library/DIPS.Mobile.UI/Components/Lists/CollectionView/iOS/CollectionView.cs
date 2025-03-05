namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView
{
    public static new readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
        nameof(ItemTemplate),
        typeof(DataTemplate),
        typeof(CollectionView),
        propertyChanged: (bindable, _, _) =>
        {
            if (bindable is CollectionView collectionView)
            {
                collectionView.DataTemplateChanged();
            }
        });

    private void DataTemplateChanged()
    {
        base.ItemTemplate = new DataTemplate(() =>
        {
            var consumerContent = ItemTemplate.CreateContent() as View;
            
            return new Grid { Children = { consumerContent }, Padding = new Thickness(12, 0)};
        });
    }

    public new DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
}