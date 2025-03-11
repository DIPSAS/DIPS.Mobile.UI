using System.Collections;
using DIPS.Mobile.UI.Components.Dividers;
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
            if (bindable is not CollectionView collectionView)
                return;

            if (collectionView.ItemsLayout is LinearItemsLayout linearItemsLayout)
            {
                collectionView.DataTemplateChanged(linearItemsLayout.Orientation == ItemsLayoutOrientation.Horizontal);
            }
            else
            {
                collectionView.DataTemplateChanged(true);
            }
        });

    private void DataTemplateChanged(bool useDefault = false)
    {
        base.ItemTemplate = useDefault ? ItemTemplate : new DataTemplateSelectorWrapper(ItemTemplate, this);
    }

    public new DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    private class DataTemplateSelectorWrapper(DataTemplate dataTemplate, CollectionView collectionView)
        : DataTemplateSelector
    {
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            int? index = 0;
            IList? itemsSourceList = null;
            if (collectionView.ItemsSource is IList list)
            {
                itemsSourceList = list;
                index = list.IndexOf(item);
            }

            // Run this through the extension method in case consumers' ItemTemplate is really a DataTemplateSelector
            var itemTemplate = dataTemplate.SelectDataTemplate(item, container);

            var consumerContent = itemTemplate.CreateContent() as View;

            var grid = new Grid
            {
                Children = { consumerContent },
                Padding = new Thickness(collectionView.Padding.Left, 0, collectionView.Padding.Right, 0)
            };

            if(index != itemsSourceList?.Count - 1)
                grid.Add(new Divider { VerticalOptions = LayoutOptions.End });

            // Here we wrap the consumer content in a grid to add padding
            return new DataTemplate(() => grid);
        }
    }
}