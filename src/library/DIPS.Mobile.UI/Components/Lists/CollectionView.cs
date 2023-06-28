using DIPS.Mobile.UI.Resources.Sizes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView : Microsoft.Maui.Controls.CollectionView
{
    private readonly Border m_extraSpaceBorder;

    public CollectionView()
    {
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        //Adds a extra space in the bottom to make sure the last item is not placed at the very bottom of the page, this makes the last item more accessible for people.
        m_extraSpaceBorder ??= new Border()
        {
            BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent
        };
        Footer = m_extraSpaceBorder;
        SelectionMode = SelectionMode.None;
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

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        if (HasAdditionalSpaceAtTheEnd && Footer == m_extraSpaceBorder)
        {
            AddExtraSpaceAtTheEnd(height);
        }
    }

    private void AddExtraSpaceAtTheEnd(double height)
    {
#if __IOS__ //Collectionviews height is bigger than the visual height of the collection view
        var visibleSize = height - Bounds.Y;
        var nonVisibleSize = height - visibleSize;
        m_extraSpaceBorder.HeightRequest =
            nonVisibleSize +
            ((visibleSize) /
             3); //The border has to be as big as the non visible size + one third of the visible size
#elif __ANDROID__
        m_extraSpaceBorder.HeightRequest = height/3; //The border has to be the one third of the visible size
#endif
    }
}