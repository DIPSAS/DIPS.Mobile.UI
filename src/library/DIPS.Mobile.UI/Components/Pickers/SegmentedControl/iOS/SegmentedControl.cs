using DIPS.Mobile.UI.Components.Pickers.ItemPicker;
using Microsoft.Maui.Controls.Handlers.Items;
using UIKit;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControl
{
    internal partial SelectableListItem? GetFirstItemVisible()
    {
        if (m_collectionView.Handler is not CollectionViewHandler collectionViewHandler) return null;
        if (collectionViewHandler.PlatformView is not { } uiView) return null;
        if (uiView.Subviews[0] is not UICollectionView uiCollectionView) return null;
        
        return m_allSelectableItems[uiCollectionView.IndexPathsForVisibleItems.First().Row];
    }

    internal partial SelectableListItem? GetLastItemVisible()
    {
        if (m_collectionView.Handler is not CollectionViewHandler collectionViewHandler) return null;
        if (collectionViewHandler.PlatformView is not { } uiView) return null;
        if (uiView.Subviews[0] is not UICollectionView uiCollectionView) return null;
        
        return m_allSelectableItems[uiCollectionView.IndexPathsForVisibleItems.Last().Row];
    }
}