using System.Collections;
using Android.Content;
using Android.Content.Res;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Components.Lists.Android;
using DIPS.Mobile.UI.Effects.Layout;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Controls.Handlers.Items;
using Microsoft.Maui.Platform;
using Color = Android.Graphics.Color;
using CornerRadius = Microsoft.Maui.CornerRadius;
using View = Microsoft.Maui.Controls.View;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler
{
    private KeyboardDismissOnScrollListener? m_keyboardDismissOnScrollListener;
    private RecyclerView? m_listenerRegisteredOn;

    protected override RecyclerView CreatePlatformView()
    {
        return new MauiRecyclerView(Context, GetItemsLayout, CreateAdapter);
    }

    protected override ReorderableItemsViewAdapter<ReorderableItemsView, IGroupableItemsViewSource> CreateAdapter()
    {
        // Only use new adapter if the ItemsLayout is vertical LinearItemsLayout
        if (VirtualView is CollectionView { ItemsLayout: LinearItemsLayout { Orientation: ItemsLayoutOrientation.Vertical } })
            return new ReorderableItemsViewAdapter(VirtualView);

        return base.CreateAdapter();
    }

    protected override void ConnectHandler(RecyclerView platformView)
    {
        base.ConnectHandler(platformView);

        // If the list is horizontal, we don't want to do this
        if (VirtualView is not CollectionView { ItemsLayout: LinearItemsLayout { Orientation: ItemsLayoutOrientation.Vertical } })
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

    private static partial void MapRemoveFocusOnScroll(CollectionViewHandler handler,
        Microsoft.Maui.Controls.CollectionView virtualView)
    {
        if (virtualView is not CollectionView collectionView)
            return;

        handler.RemoveKeyboardDismissListener();

        if (collectionView.RemoveFocusOnScroll)
        {
            handler.AddKeyboardDismissListener();
        }
    }

    partial void OnItemsSourceMapped()
    {
        if (VirtualView is not CollectionView { RemoveFocusOnScroll: true })
            return;

        // MAUI's MauiRecyclerView.UpdateItemsSource() calls ClearOnScrollListeners(),
        // which removes ALL scroll listeners — including ours.
        // This runs via AppendToMapping, guaranteeing it executes AFTER MAUI's mapper.
        RemoveKeyboardDismissListener();
        AddKeyboardDismissListener();
    }

    private void AddKeyboardDismissListener()
    {
        m_keyboardDismissOnScrollListener = new KeyboardDismissOnScrollListener();
        PlatformView.AddOnScrollListener(m_keyboardDismissOnScrollListener);
        m_listenerRegisteredOn = PlatformView;
    }

    private void RemoveKeyboardDismissListener()
    {
        if (m_keyboardDismissOnScrollListener == null)
            return;

        if (m_listenerRegisteredOn != null)
        {
            m_listenerRegisteredOn.RemoveOnScrollListener(m_keyboardDismissOnScrollListener);
        }

        m_keyboardDismissOnScrollListener = null;
        m_listenerRegisteredOn = null;
    }

    protected override void DisconnectHandler(RecyclerView platformView)
    {
        RemoveKeyboardDismissListener();

        base.DisconnectHandler(platformView);
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

    private RecyclerView? m_recyclerView;
    private CellStateObserver? m_cellStateObserver;

    public ReorderableItemsViewAdapter(ReorderableItemsView reorderableItemsView, Func<View, Context, ItemContentView> createView = null) : base(reorderableItemsView, createView)
    {
        if (reorderableItemsView is CollectionView collectionView)
            m_collectionView = collectionView;
    }

    #region Lifecycle

    public override void OnAttachedToRecyclerView(RecyclerView recyclerView)
    {
        base.OnAttachedToRecyclerView(recyclerView);

        m_recyclerView = recyclerView;
        m_cellStateObserver = new CellStateObserver(this);
        RegisterAdapterDataObserver(m_cellStateObserver);
    }

    public override void OnDetachedFromRecyclerView(RecyclerView recyclerView)
    {
        if (m_cellStateObserver != null)
        {
            UnregisterAdapterDataObserver(m_cellStateObserver);
            m_cellStateObserver = null;
        }

        m_recyclerView = null;
        base.OnDetachedFromRecyclerView(recyclerView);
    }

    #endregion

    #region Cell binding

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        base.OnBindViewHolder(holder, position);

        ApplyCellStyling(holder, position);
        ScheduleCellStyling(holder);
    }

    private void ScheduleCellStyling(RecyclerView.ViewHolder holder)
    {
        holder.ItemView.Post(() =>
        {
            var currentPosition = holder.AdapterPosition;
            if (currentPosition != RecyclerView.NoPosition)
                ApplyCellStyling(holder, currentPosition);
        });
    }

    private void ApplyCellStyling(RecyclerView.ViewHolder holder, int position)
    {
        if (!TryGetItemPosition(position, out var itemPosition, out var count))
        {
            ResetCornerRadius(holder);
            return;
        }

        var isFirst = IsFirstItem(itemPosition);
        var isLast = IsLastItem(itemPosition, count);

        ApplyCornerRadius(holder, isFirst, isLast);
        ApplyDividerVisibility(holder, isLast);
    }

    #endregion

    #region Position helpers

    private bool TryGetItemPosition(int adapterPosition, out int itemPosition, out int count)
    {
        if (m_collectionView.IsGrouped)
        {
            if (!TryGetGroupAndGroupIndex(adapterPosition, out var groupItemPosition, out var group))
            {
                itemPosition = 0;
                count = 0;
                return false;
            }

            var headerCount = group.HasHeader ? 1 : 0;
            var footerCount = group.HasFooter ? 1 : 0;
            itemPosition = groupItemPosition - headerCount;
            count = group.Count - headerCount - footerCount;
            return IsItemPosition(itemPosition, count);
        }

        itemPosition = adapterPosition - (ItemsSource.HasHeader ? 1 : 0);
        count = GetItemsCount();
        return IsItemPosition(itemPosition, count);
    }

    private static bool IsItemPosition(int position, int count)
    {
        return position >= 0 && position < count;
    }

    private static bool IsFirstItem(int position)
    {
        return position == 0;
    }

    private static bool IsLastItem(int position, int count)
    {
        return position == count - 1;
    }

    private int GetItemsCount()
    {
        if (m_collectionView.ItemsSource is ICollection collection)
            return collection.Count;

        if (m_collectionView.ItemsSource is not IEnumerable enumerable)
            return 0;

        var count = 0;
        foreach (var _ in enumerable)
        {
            count++;
        }

        return count;
    }

    public bool TryGetGroupAndGroupIndex(int position, out int index, out IItemsViewSource currentGroup)
    {
        (var group, index) = ItemsSource.GetGroupAndIndex(position);

        try
        {
            currentGroup = ItemsSource.GetGroupItemsViewSource(group);
            if (currentGroup is null)
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

    #endregion

    #region Divider visibility

    private void ApplyDividerVisibility(RecyclerView.ViewHolder holder, bool isLast)
    {
        var crossPlatformElement = GetCrossPlatformElement(holder);
        if (crossPlatformElement is null)
            return;

        var divider = crossPlatformElement.FindChildOfTypeClosestToRoot<Divider>();
        if (divider is null)
            return;

        var shouldHideDivider = Effects.Layout.Layout.GetAutoHideLastDivider(m_collectionView) && isLast;
        divider.Opacity = shouldHideDivider ? 0 : 1;
    }

    #endregion

    #region Corner radius

    private void ApplyCornerRadius(RecyclerView.ViewHolder holder, bool isFirst, bool isLast)
    {
        var hasCustomFirstCornerRadius = !m_collectionView.FirstItemCornerRadius.IsEmpty();
        var hasCustomLastCornerRadius = !m_collectionView.LastItemCornerRadius.IsEmpty();
        var hasAutoCornerRadius = m_collectionView.AutoCornerRadius;

        if (!hasCustomFirstCornerRadius && !hasCustomLastCornerRadius && !hasAutoCornerRadius)
        {
            ResetCornerRadius(holder);
            return;
        }

        var cornerRadius = GetCornerRadius(isFirst, isLast, hasCustomFirstCornerRadius, hasCustomLastCornerRadius, hasAutoCornerRadius);

        ApplyCornerRadius(holder, cornerRadius);
    }

    private static void ApplyCornerRadius(RecyclerView.ViewHolder holder, CornerRadius cornerRadius)
    {
        var materialShapeDrawable = MaterialShapeDrawableHelper.CreateDrawable(cornerRadius);
        materialShapeDrawable.FillColor = ColorStateList.ValueOf(Color.Transparent);
        holder.ItemView.ClipToOutline = true;
        holder.ItemView.OutlineProvider = ViewOutlineProvider.Background;
        holder.ItemView.Background = materialShapeDrawable;
    }

    private static void ResetCornerRadius(RecyclerView.ViewHolder holder)
    {
        ApplyCornerRadius(holder, new CornerRadius());
    }

    private CornerRadius GetCornerRadius(bool isFirst, bool isLast, bool hasCustomFirstCornerRadius, bool hasCustomLastCornerRadius, bool hasAutoCornerRadius)
    {
        var cornerRadius = new CornerRadius();

        if ((hasCustomFirstCornerRadius || hasAutoCornerRadius) && isFirst)
        {
            cornerRadius = hasCustomFirstCornerRadius
                ? m_collectionView.FirstItemCornerRadius
                : new CornerRadius(Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small), 0, 0);
        }

        if ((hasCustomLastCornerRadius || hasAutoCornerRadius) && isLast)
        {
            cornerRadius = hasCustomLastCornerRadius
                ? m_collectionView.LastItemCornerRadius
                : new CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small));
        }

        return cornerRadius;
    }

    #endregion

    #region Margins

    internal void ModifyMarginIfNotHeaderAndFooter(global::Android.Graphics.Rect rect, int position)
    {
        if (!TryGetItemPosition(position, out _, out _))
            return;

        rect.Left = (int)m_collectionView.Padding.Left.ToMauiPixel();
        rect.Right = (int)m_collectionView.Padding.Right.ToMauiPixel();
    }

    #endregion

    #region View helpers

    private static View? GetCrossPlatformElement(RecyclerView.ViewHolder holder)
    {
        if (holder is TemplatedItemViewHolder templatedHolder)
            return templatedHolder.View;

        return null;
    }

    #endregion

    #region Data change observer

    /// <summary>
    /// When items are inserted, removed, or moved, the boundary items (first/last) may change.
    /// Visible cells that were previously first/last need their corner radius and divider state updated.
    /// This observer defers the update to the next frame to ensure the adapter state is consistent.
    /// </summary>
    private void UpdateAllVisibleCells()
    {
        if (m_recyclerView == null)
            return;

        for (var i = 0; i < m_recyclerView.ChildCount; i++)
        {
            var child = m_recyclerView.GetChildAt(i);
            if (child == null)
                continue;

            var holder = m_recyclerView.GetChildViewHolder(child);
            var position = holder.AdapterPosition;
            if (position == RecyclerView.NoPosition)
                continue;

            ApplyCellStyling(holder, position);
        }
    }

    private class CellStateObserver(ReorderableItemsViewAdapter adapter) : RecyclerView.AdapterDataObserver
    {
        public override void OnChanged() => ScheduleUpdate();
        public override void OnItemRangeInserted(int positionStart, int itemCount) => ScheduleUpdate();
        public override void OnItemRangeRemoved(int positionStart, int itemCount) => ScheduleUpdate();
        public override void OnItemRangeMoved(int fromPosition, int toPosition, int itemCount) => ScheduleUpdate();
        public override void OnItemRangeChanged(int positionStart, int itemCount) => ScheduleUpdate();

        private void ScheduleUpdate()
        {
            adapter.m_recyclerView?.Post(() =>
            {
                adapter.m_recyclerView.InvalidateItemDecorations();
                adapter.UpdateAllVisibleCells();
                adapter.m_recyclerView?.Post(() =>
                {
                    adapter.m_recyclerView.InvalidateItemDecorations();
                    adapter.UpdateAllVisibleCells();
                });
            });
        }
    }

    #endregion
}
