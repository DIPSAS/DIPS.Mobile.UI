using DIPS.Mobile.UI.Sizes.Sizes;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView : Microsoft.Maui.Controls.CollectionView
{
    public CollectionView()
    {
        BackgroundColor = Colors.Transparent;
        //Adds a extra space in the bottom to make sure the last item is not placed at the very bottom of the page, this makes the last item more accessible for people.
        Footer = new BoxView() { HeightRequest = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_24) };
        SelectionMode = SelectionMode.None;
        ItemSpacing = UI.Resources.Sizes.Sizes.GetSize(SizeName.size_1);
    }
    
    private static void OnItemSpacingPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
    {
        if (bindable is not CollectionView collectionView || newvalue is not double itemsSpacing) return;

        switch (collectionView.ItemsLayout)
        {
            case LinearItemsLayout linearItemsLayout:
                linearItemsLayout.ItemSpacing = itemsSpacing;
                break;
            case GridItemsLayout gridItemsLayout:
                gridItemsLayout.HorizontalItemSpacing = itemsSpacing;
                gridItemsLayout.VerticalItemSpacing = itemsSpacing;
                break;
        }
    }
}
