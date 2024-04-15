using DIPS.Mobile.UI.Components.VirtualListView.Adapters;

namespace DIPS.Mobile.UI.Components.VirtualListView;

public partial class VirtualListViewHandler
{
    public static new IPropertyMapper<IVirtualListView, VirtualListViewHandler> PropertyMapper = new PropertyMapper<IVirtualListView, VirtualListViewHandler>(ViewMapper)
	{
		[nameof(IVirtualListView.Adapter)] = MapAdapter,
		[nameof(IVirtualListView.Header)] = MapHeader,
		[nameof(IVirtualListView.IsHeaderVisible)] = MapIsHeaderVisible,
		[nameof(IVirtualListView.Footer)] = MapFooter,
		[nameof(IVirtualListView.IsFooterVisible)] = MapIsFooterVisible,
		[nameof(IVirtualListView.ViewSelector)] = MapViewSelector,
		[nameof(IVirtualListView.SelectionMode)] = MapSelectionMode,
		[nameof(IVirtualListView.Orientation)] = MapOrientation,
		[nameof(IVirtualListView.RefreshAccentColor)] = MapRefreshAccentColor,
		[nameof(IVirtualListView.IsRefreshEnabled)] = MapIsRefreshEnabled,
		[nameof(IVirtualListView.EmptyView)] = MapEmptyView,
		[nameof(IVirtualListView.SelectedItems)] = MapSelectedItems,
	};

	public static CommandMapper<IVirtualListView, VirtualListViewHandler> CommandMapper = new(ViewCommandMapper)
	{
		[nameof(IVirtualListView.ScrollToItem)] = MapScrollToItem,
	};

	public static void MapScrollToItem(VirtualListViewHandler handler, IVirtualListView view, object parameter)
	{
		if (parameter is ItemPosition itemPosition)
		{
			handler.PlatformScrollToItem(itemPosition, true);
		}
		else if (parameter is object[] parameters)
		{
			if (parameters?[0] is ItemPosition p)
			{
				var animated = true;
				if (parameters?[1] is bool a)
					animated = a;

				handler.PlatformScrollToItem(p, animated);
			}
		}
	}

	public VirtualListViewHandler() : base(PropertyMapper, CommandMapper)
	{

	}

	internal PositionalViewSelector PositionalViewSelector { get; private set; }

	bool ShouldShowEmptyView
	{
		get
		{
			var sections = PositionalViewSelector?.Adapter?.GetNumberOfSections() ?? 0;

			if (sections <= 0)
				return true;

			return (PositionalViewSelector?.Adapter?.GetNumberOfItemsInSection(0) ?? 0) <= 0;
		}
	}

	public static void MapAdapter(VirtualListViewHandler handler, IVirtualListView virtualListView)
	{
		if (handler.currentAdapter != null)
			handler.currentAdapter.OnDataInvalidated -= handler.Adapter_OnDataInvalidated;

		if (virtualListView?.Adapter != null)
			virtualListView.Adapter.OnDataInvalidated += handler.Adapter_OnDataInvalidated;

		handler.currentAdapter = virtualListView.Adapter;
		handler?.InvalidateData(new InvalidateTypeEventArgs());
	}

	IVirtualListViewAdapter currentAdapter = default;

	void Adapter_OnDataInvalidated(object? sender, InvalidateTypeEventArgs invalidateTypeEventArgs)
	{
		InvalidateData(invalidateTypeEventArgs);
	}

	public bool IsItemSelected(int sectionIndex, int itemIndex)
	{
		if (VirtualView is null)
			return false;

		if (VirtualView.SelectionMode == SelectionMode.None)
			return false;

		return previousSelections.Contains(new ItemPosition(sectionIndex, itemIndex));
	}

	ItemPosition[] previousSelections = Array.Empty<ItemPosition>();

	public static void MapSelectedItems(VirtualListViewHandler handler, IVirtualListView virtualListView)
	{
		if (handler is null)
			return;

		var newSelections = virtualListView?.SelectedItems ?? Array.Empty<ItemPosition>();

		if (virtualListView.SelectionMode == SelectionMode.None)
			newSelections = Array.Empty<ItemPosition>();
		else if (virtualListView.SelectionMode == SelectionMode.Single && newSelections.Count > 1)
			newSelections = newSelections.Take(1).ToArray();

		// First deselect any previously selected items that aren't in the new set
		foreach (var itemPosition in handler.previousSelections)
		{
			if (!newSelections.Contains(itemPosition))
				handler.PlatformUpdateItemSelection(itemPosition, false);
		}

		// Set all the new state selected to true
		foreach (var itemPosition in newSelections)
		{
			if (!handler.previousSelections.Contains(itemPosition))
				handler.PlatformUpdateItemSelection(itemPosition, true);
		}

		// Keep track of the new state for next time it changes
		handler.previousSelections = newSelections.ToArray();
	}

	public static void MapIsHeaderVisible(VirtualListViewHandler handler, IVirtualListView virtualListView)
	{
		handler?.InvalidateData(new InvalidateTypeEventArgs());
	}

	public static void MapIsFooterVisible(VirtualListViewHandler handler, IVirtualListView virtualListView)
	{
		handler?.InvalidateData(new InvalidateTypeEventArgs());
	}
}