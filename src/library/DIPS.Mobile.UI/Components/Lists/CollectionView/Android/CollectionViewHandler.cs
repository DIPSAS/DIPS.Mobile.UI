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

    public ReorderableItemsViewAdapter(ReorderableItemsView reorderableItemsView, Func<View, Context, ItemContentView> createView = null) : base(reorderableItemsView, createView)
    {
        if(reorderableItemsView is CollectionView collectionView)
            m_collectionView = collectionView;
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
            
            ModifyCell(holder, position, group.HasHeader, group.HasHeader, group.Count);
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
            
            InternalModifyMarginIfNotHeaderAndFooter(rect, position, group.HasHeader, group.HasHeader, group.Count);
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
        if(!Effects.Layout.Layout.GetAutoHideLastDivider(m_collectionView) || (position != lastItemIndex))
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

        // We need to check if the last divider was set to invisible in the previous position (Because the list is flattened on Android even though it is grouped)
        if (m_lastDividerSetToInvisibleInPosition.Remove(position - 1, out var value))
        {
            value.IsVisible = true;
        }
        
        divider.IsVisible = false;
        m_lastDividerSetToInvisibleInPosition[position] = divider;
    }

    /// <summary>
    /// Here we set the Corner Radius on the cells
    /// </summary>
    /// <remarks>
    /// If the CollectionView is using ObservableCollection and the consumer adds an element,
    /// we have to cache which cell that has modified its corner radius, so we can reset it,
    /// because the new cell that has been added should be either the last or first one 
    /// </remarks>
    /// <param name="shouldCache">If we shall cache the cell that has modified its corner radius</param>
    private void TrySetCornerRadiusOnCell(RecyclerView.ViewHolder holder, int position, int firstItemIndex,
        int lastItemIndex, bool shouldCache = true)
    {
        var cornerRadius = new CornerRadius();
        
        if ((!m_collectionView.FirstItemCornerRadius.IsEmpty() || m_collectionView.AutoCornerRadius) && position == firstItemIndex)
        {
            if (m_currentFirstCellWithCornerRadiusInPosition.Remove(position + 1, out var value))
            {
                TrySetCornerRadiusOnCell(value, position + 1, firstItemIndex, lastItemIndex, false);
            }
            
            cornerRadius = m_collectionView.FirstItemCornerRadius.IsEmpty() ? new CornerRadius(Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small), 0, 0) : m_collectionView.FirstItemCornerRadius;
            if(shouldCache)
                m_currentFirstCellWithCornerRadiusInPosition[position] = holder;
        }
        
        if ((!m_collectionView.LastItemCornerRadius.IsEmpty() || m_collectionView.AutoCornerRadius) && position == lastItemIndex)
        {
            if (m_currentLastCellWithCornerRadiusInPosition.Remove(position - 1, out var value))
            {
                TrySetCornerRadiusOnCell(value, position - 1, firstItemIndex, lastItemIndex, false);
            }
            
            cornerRadius = m_collectionView.LastItemCornerRadius.IsEmpty() ? new CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small)) : m_collectionView.LastItemCornerRadius;
            if(shouldCache)
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
        var materialShapeDrawable = MaterialShapeDrawableHelper.GetMaterialShapeDrawableFromCornerRadius(cornerRadius);
      
        holder.ItemView.ClipToOutline = true;

        // Removes ugly artifact from corners
        materialShapeDrawable.FillColor = ColorStateList.ValueOf(Color.Transparent);
        
        holder.ItemView.Background = materialShapeDrawable;
    }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        m_lastDividerSetToInvisibleInPosition.Clear();
        m_currentLastCellWithCornerRadiusInPosition.Clear();
        m_currentFirstCellWithCornerRadiusInPosition.Clear();
    }
}