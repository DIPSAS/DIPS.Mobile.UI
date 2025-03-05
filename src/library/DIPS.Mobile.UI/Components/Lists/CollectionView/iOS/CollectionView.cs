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
        base.ItemTemplate = new DataTemplateSelectorWrapper(ItemTemplate, this);
    }

    public new DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    private class DataTemplateSelectorWrapper(DataTemplate dataTemplate, CollectionView collectionView) : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // Run this through the extension method in case consumers' ItemTemplate is really a DataTemplateSelector
            var itemTemplate = dataTemplate.SelectDataTemplate(item, container);
            
            var consumerContent = itemTemplate.CreateContent() as View;
            
            // Here we wrap the consumer content in a grid to add padding
            return new DataTemplate(() => new Grid
            {
                Children = { consumerContent },
                Padding = new Thickness(collectionView.Padding.Left, 0, collectionView.Padding.Right, 0)
            });
        }
    }
}