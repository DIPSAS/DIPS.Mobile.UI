using Microsoft.Maui.Controls.Internals;

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
            // Run this through the extension method in case it's really a DataTemplateSelector
            var itemTemplate = ItemTemplate.SelectDataTemplate(BindingContext, this);
            
            var consumerContent = itemTemplate.CreateContent() as View;
            
            // Here we wrap the consumer content in a grid to add padding
            return new Grid { Children = { consumerContent }, Padding = new Thickness(12, 0)};
        });
    }

    public new DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }
}