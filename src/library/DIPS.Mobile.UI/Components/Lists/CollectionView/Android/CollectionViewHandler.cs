using Android.Content;
using Android.Content.Res;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.Lists.Android;
using DIPS.Mobile.UI.Effects.Layout;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Controls.Handlers.Items;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;
using CornerRadius = Microsoft.Maui.CornerRadius;
using View = Microsoft.Maui.Controls.View;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler
{
    protected override RecyclerView CreatePlatformView()
    {
        return new MauiRecyclerView(Context, GetItemsLayout, CreateAdapter);
    }

    protected override ReorderableItemsViewAdapter<ReorderableItemsView, IGroupableItemsViewSource> CreateAdapter()
    {
        // Only use new adapter if the ItemsLayout is vertical LinearItemsLayout
        if(VirtualView is CollectionView { ItemsLayout: LinearItemsLayout {Orientation: ItemsLayoutOrientation.Vertical} })
            return new ReorderableItemsViewAdapter(VirtualView);

        return base.CreateAdapter();
    }

    protected override void ConnectHandler(RecyclerView platformView)
    {
        base.ConnectHandler(platformView);
        
        // If the list is horizontal, we don't want to do this
        if(VirtualView is not CollectionView { ItemsLayout: LinearItemsLayout {Orientation: ItemsLayoutOrientation.Vertical} }) 
            return;

        // Pads the RecyclerView elements left and right
        platformView.AddItemDecoration(new CellMarginDecoration());
        // Additional space at the end of the RecyclerView
        platformView.AddItemDecoration(new AdditionalEndSpaceDecoration(VirtualView));
    }

    private static partial void MapShouldBounce(CollectionViewHandler handler,
        Microsoft.Maui.Controls.CollectionView virtualView)
    {
        if (virtualView is CollectionView collectionView)
        {
            handler.PlatformView.OverScrollMode =
                collectionView.ShouldBounce ? OverScrollMode.Always : OverScrollMode.Never;
        }
    }

    internal partial void ReloadData(CollectionViewHandler handler)
    {
        if (handler.PlatformView is not MauiRecyclerView
            recyclerView)
        {
            return;
        }

        var adapter = recyclerView.GetAdapter();
        adapter?.NotifyDataSetChanged();
    }
}

public class ReorderableItemsViewAdapter : ReorderableItemsViewAdapter<ReorderableItemsView, IGroupableItemsViewSource>
{
    private readonly CollectionView m_collectionView;
    private readonly Dictionary<int, Divider> m_lastDividerSetToInvisibleInPosition = new();
    private readonly Dictionary<int, RecyclerView.ViewHolder> m_currentLastCellWithCornerRadiusInPosition = new();
    private readonly Dictionary<int, RecyclerView.ViewHolder> m_currentFirstCellWithCornerRadiusInPosition = new();
    private RecyclerView? m_recyclerView;

    public ReorderableItemsViewAdapter(ReorderableItemsView reorderableItemsView, Func<View, Context, ItemContentView> createView = null) : base(reorderableItemsView, createView)
    {
        if(reorderableItemsView is CollectionView collectionView)
            m_collectionView = collectionView;
        
        RegisterAdapterDataObserver(new DataChangedObserver(this));
    }

    public override void OnAttachedToRecyclerView(RecyclerView recyclerView)
    {
        base.OnAttachedToRecyclerView(recyclerView);
        m_recyclerView = recyclerView;
    }

    public override void OnDetachedFromRecyclerView(RecyclerView recyclerView)
    {
        base.OnDetachedFromRecyclerView(recyclerView);
        m_recyclerView = null;
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        base.OnBindViewHolder(holder, position);

        if (m_collectionView.IsGrouped)
        {
            if (!TryGetGroupAndGroupIndex(position, out position, out var group))
            {
                return;   
            }
            
            ModifyCell(holder, position, group.HasHeader, group.HasFooter, group.Count);
        }
        else
        {
            ModifyCell(holder, position, ItemsSource.HasHeader, ItemsSource.HasFooter, ItemsSource.Count);
        }
    }

    internal void ModifyMarginIfNotHeaderAndFooter(global::Android.Graphics.Rect rect, int position)
    {
        if (m_collectionView.IsGrouped)
        {
            if (!TryGetGroupAndGroupIndex(position, out position, out var group))
            {
                return;   
            }
            
            InternalModifyMarginIfNotHeaderAndFooter(rect, position, group.HasHeader, group.HasFooter, group.Count);
        }
        else
        {
            InternalModifyMarginIfNotHeaderAndFooter(rect, position, ItemsSource.HasHeader, ItemsSource.HasFooter, ItemsSource.Count);
        }
    }

    private void InternalModifyMarginIfNotHeaderAndFooter(global::Android.Graphics.Rect rect, int position, bool hasHeader, bool hasFooter, int count)
    {
        // Don't set margin on header and footer
        if(hasHeader && position == 0 || hasFooter && position == count - 1)
            return;
        
        rect.Left = (int)m_collectionView.Padding.Left.ToMauiPixel();
        rect.Right = (int)m_collectionView.Padding.Right.ToMauiPixel();
    }

    /// <summary>
    /// Here we shall modify the cell
    /// </summary>
    /// <remarks>This is called for each section,
    /// if the CollectionView has Header and footer,
    /// and CollectionView has 3 sections,
    /// this will be called for all items in the sections including section header/footer
    /// and twice more because of global header and footer,
    /// so we have to check if the ViewHolder is actually an element in the list
    /// </remarks>
    private void ModifyCell(RecyclerView.ViewHolder holder, int position, bool hasHeader, bool hasFooter, int count)
    {
        // Header and footer is included as elements
        var firstItemIndex = hasHeader ? 1 : 0;
        var lastItemIndex = count - (hasFooter ? 2 : 1);

        TrySetCornerRadiusOnCell(holder, position, firstItemIndex, lastItemIndex);
        TrySetLastDividerToInvisible(holder, position, lastItemIndex);
    }

    private void TrySetLastDividerToInvisible(RecyclerView.ViewHolder holder, int position, int lastItemIndex)
    {
        if(!Effects.Layout.Layout.GetAutoHideLastDivider(m_collectionView))
            return;
        
        Divider? divider = null;
        holder.ItemView.BreadthFirstSearchChildrenUntilMatch(view =>
        {
            if (view is not ContentViewGroup { CrossPlatformLayout: Divider crossPlatformLayout })
                return false;

            divider = crossPlatformLayout;
            return true;
        });

        if (divider is null)
            return;

        if (position == lastItemIndex)
        {
            divider.IsVisible = false;
            m_lastDividerSetToInvisibleInPosition[position] = divider;
        }
        else
        {
            // Reset the divider for non-last items, because of virtualization/recycling
            divider.IsVisible = true;
        }
    }

    /// <summary>
    /// Here we set the Corner Radius on the cells
    /// </summary>
    /// <remarks>
    /// If the CollectionView is using ObservableCollection and the consumer adds or removes an element,
    /// we clear the cache of which cell had its corner radius modified, and reset the old cells directly.
    /// </remarks>
    private void TrySetCornerRadiusOnCell(RecyclerView.ViewHolder holder, int position, int firstItemIndex,
        int lastItemIndex)
    {
        var cornerRadius = new CornerRadius();
        
        if ((!m_collectionView.FirstItemCornerRadius.IsEmpty() || m_collectionView.AutoCornerRadius) && position == firstItemIndex)
        {
            // Reset corner radius on the previously cached first cell if it's different from the current one
            foreach (var entry in m_currentFirstCellWithCornerRadiusInPosition)
            {
                if (!entry.Value.Equals(holder))
                    SetCellCornerRadius(entry.Value, new CornerRadius());
            }
            m_currentFirstCellWithCornerRadiusInPosition.Clear();
            
            cornerRadius = m_collectionView.FirstItemCornerRadius.IsEmpty() ? new CornerRadius(Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small), 0, 0) : m_collectionView.FirstItemCornerRadius;
            m_currentFirstCellWithCornerRadiusInPosition[position] = holder;
        }
        
        if ((!m_collectionView.LastItemCornerRadius.IsEmpty() || m_collectionView.AutoCornerRadius) && position == lastItemIndex)
        {
            // Reset corner radius on the previously cached last cell if it's different from the current one
            foreach (var entry in m_currentLastCellWithCornerRadiusInPosition)
            {
                if (!entry.Value.Equals(holder))
                    SetCellCornerRadius(entry.Value, new CornerRadius());
            }
            m_currentLastCellWithCornerRadiusInPosition.Clear();
            
            cornerRadius = m_collectionView.LastItemCornerRadius.IsEmpty() ? new CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small)) : m_collectionView.LastItemCornerRadius;
            m_currentLastCellWithCornerRadiusInPosition[position] = holder;
        }
        
        SetCellCornerRadius(holder, cornerRadius);
    }

    public bool TryGetGroupAndGroupIndex(int position, out int index, out IItemsViewSource currentGroup)
    {
        (var group, index) = ItemsSource.GetGroupAndIndex(position);

        try
        {
            currentGroup = ItemsSource.GetGroupItemsViewSource(group);
            
            if(currentGroup is null)
                return false;
        }
        catch
        {
            // Can happen if list is refreshed while scrolling
            currentGroup = null!;
            return false;
        }

        return true;
    }

    private static void SetCellCornerRadius(RecyclerView.ViewHolder holder, CornerRadius cornerRadius)
    {
        var materialShapeDrawable = MaterialShapeDrawableHelper.CreateDrawable(cornerRadius);
      
        holder.ItemView.ClipToOutline = true;

        // Removes ugly artifact from corners
        materialShapeDrawable.FillColor = ColorStateList.ValueOf(Color.Transparent);
        
        holder.ItemView.Background = materialShapeDrawable;
    }

    private void ClearCaches()
    {
        m_lastDividerSetToInvisibleInPosition.Clear();
        m_currentLastCellWithCornerRadiusInPosition.Clear();
        m_currentFirstCellWithCornerRadiusInPosition.Clear();
    }

    /// <summary>
    /// Re-applies divider visibility and corner radius modifications to all currently visible cells.
    /// Called after data changes to ensure boundary cells (first/last) get the correct state,
    /// since RecyclerView does not rebind cells whose data hasn't changed.
    /// </summary>
    private void ReapplyModificationsToVisibleCells()
    {
        if (m_recyclerView == null)
            return;
        
        for (var i = 0; i < m_recyclerView.ChildCount; i++)
        {
            var child = m_recyclerView.GetChildAt(i);
            if (child == null) continue;
            
            var holder = m_recyclerView.GetChildViewHolder(child);
            if (holder == null) continue;
            
            var position = holder.AdapterPosition;
            if (position < 0) continue;
            
            if (m_collectionView.IsGrouped)
            {
                if (TryGetGroupAndGroupIndex(position, out var groupPosition, out var group))
                {
                    ModifyCell(holder, groupPosition, group.HasHeader, group.HasFooter, group.Count);
                }
            }
            else
            {
                ModifyCell(holder, position, ItemsSource.HasHeader, ItemsSource.HasFooter, ItemsSource.Count);
            }
        }
    }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        ClearCaches();
        m_recyclerView = null;
    }

    private class DataChangedObserver(ReorderableItemsViewAdapter adapter) : RecyclerView.AdapterDataObserver
    {
        public override void OnChanged()
        {
            adapter.OnDataChanged();
        }

        public override void OnItemRangeInserted(int positionStart, int itemCount)
        {
            adapter.OnDataChanged();
        }

        public override void OnItemRangeRemoved(int positionStart, int itemCount)
        {
            adapter.OnDataChanged();
        }

        public override void OnItemRangeMoved(int fromPosition, int toPosition, int itemCount)
        {
            adapter.OnDataChanged();
        }
    }

    private void OnDataChanged()
    {
        ClearCaches();
        
        // Post a deferred re-application of modifications to visible cells.
        // This ensures boundary cells (first/last) get correct divider and corner radius state
        // after items are added/removed, since RecyclerView does not rebind unaffected cells.
        m_recyclerView?.Post(ReapplyModificationsToVisibleCells);
    }
}