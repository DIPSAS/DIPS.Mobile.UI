using DIPS.Mobile.UI.Components.VirtualListView.Adapters;
using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Handlers;

namespace DIPS.Mobile.UI.Components.VirtualListView;

public partial class VirtualListViewHandler : ViewHandler<IVirtualListView, Only_Here_For_UnitTests>
{
    protected override Only_Here_For_UnitTests CreatePlatformView()
    {
        throw new NotImplementedException();
    }
    
    void PlatformScrollToItem(ItemPosition itemPosition, bool animated)
    {
    }

    public static void MapHeader(VirtualListViewHandler handler, IVirtualListView virtualListView)
    {
    }

    public static void MapFooter(VirtualListViewHandler handler, IVirtualListView virtualListView)
    {
    }

    public static void MapViewSelector(VirtualListViewHandler handler, IVirtualListView virtualListView)
    {
    }

    public static void MapSelectionMode(VirtualListViewHandler handler, IVirtualListView virtualListView)
    {
    }

    public static void MapInvalidateData(VirtualListViewHandler handler, IVirtualListView virtualListView,
        object? parameter)
    {
    }
    
    public static void MapOrientation(VirtualListViewHandler handler, IVirtualListView virtualListView)
    {
    }

    public static void MapRefreshAccentColor(VirtualListViewHandler handler, IVirtualListView virtualListView)
    {
    
    }

    public static void MapIsRefreshEnabled(VirtualListViewHandler handler, IVirtualListView virtualListView)
    {
     
    }
    
    public static void MapEmptyView(VirtualListViewHandler handler, IVirtualListView virtualListView)
    {
    }
    
    public void InvalidateData(InvalidateTypeEventArgs invalidateTypeEventArgs)
    {
    }
    
    void PlatformUpdateItemSelection(ItemPosition itemPosition, bool selected)
    {
    }
}