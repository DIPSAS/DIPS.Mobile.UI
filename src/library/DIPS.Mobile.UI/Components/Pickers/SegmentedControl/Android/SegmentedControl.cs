using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Components.Pickers.ItemPicker;

namespace DIPS.Mobile.UI.Components.Pickers.SegmentedControl;

public partial class SegmentedControl
{
    internal partial SelectableListItem? GetFirstItemVisible()
    {
        if (m_collectionView.Handler != null)
        {
            if (m_collectionView.Handler.PlatformView is RecyclerView recyclerView)
            {
                var layoutManager = recyclerView.GetLayoutManager();
                if (layoutManager is LinearLayoutManager linearLayoutManager)
                {
               
                    return m_allSelectableItems[linearLayoutManager.FindFirstVisibleItemPosition()];
                }
            }
        }

        return null;
    }
    
    internal partial SelectableListItem? GetLastItemVisible()
    {
        if (m_collectionView.Handler != null)
        {
            if (m_collectionView.Handler.PlatformView is RecyclerView recyclerView)
            {
                var layoutManager = recyclerView.GetLayoutManager();
                if (layoutManager is LinearLayoutManager linearLayoutManager)
                {
                    return m_allSelectableItems[linearLayoutManager.FindLastVisibleItemPosition()];
                }
            }
        }

        return null;
    }
}