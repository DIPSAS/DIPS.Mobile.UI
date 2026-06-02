using System.Collections;
using System.Collections.Specialized;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Effects.Layout;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using Microsoft.Maui.Controls.Handlers.Items2;
using Microsoft.Maui.Platform;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler
{
    protected override ItemsViewController2<ReorderableItemsView> CreateController(ReorderableItemsView itemsView,
        UICollectionViewLayout layout)
    {
        // ONLY create a ReorderableItemsViewController if the CollectionView is using a vertical LinearItemsLayout, otherwise use the default controller.
        if (VirtualView is CollectionView { ItemsLayout: LinearItemsLayout { Orientation: ItemsLayoutOrientation.Vertical } })
        {
            return new ReorderableItemsViewController(itemsView, layout, (VirtualView as CollectionView)!);
        }

        return base.CreateController(itemsView, layout);
    }

    private static partial void MapShouldBounce(CollectionViewHandler handler,
        Microsoft.Maui.Controls.CollectionView virtualView)
    {
        if (handler.PlatformView.Subviews[0] is not UICollectionView uiCollectionView)
            return;

        if (virtualView is CollectionView collectionView)
        {
            if (virtualView.ItemsLayout is ItemsLayout { Orientation: ItemsLayoutOrientation.Horizontal })
            {
                uiCollectionView.AlwaysBounceHorizontal = collectionView.ShouldBounce;
            }
            else
            {
                uiCollectionView.AlwaysBounceVertical = collectionView.ShouldBounce;
            }
        }
    }

    private static partial void MapRemoveFocusOnScroll(CollectionViewHandler handler,
        Microsoft.Maui.Controls.CollectionView virtualView)
    {
        if (handler.PlatformView.Subviews[0] is not UICollectionView uiCollectionView)
            return;

        if (virtualView is CollectionView collectionView)
        {
            uiCollectionView.KeyboardDismissMode = collectionView.RemoveFocusOnScroll
                ? UIScrollViewKeyboardDismissMode.OnDrag
                : UIScrollViewKeyboardDismissMode.None;
        }
    }

    internal partial void ReloadData(CollectionViewHandler handler)
    {
        if (handler.PlatformView.Subviews[0] is UICollectionView uiCollectionView)
        {
            uiCollectionView.ReloadData();
        }
    }
}

public class ReorderableItemsViewController(
    ReorderableItemsView itemsView,
    UICollectionViewLayout layout,
    CollectionView mauiCollectionView)
    : ReorderableItemsViewController2<ReorderableItemsView>(itemsView, layout)
{
    private INotifyCollectionChanged? m_observableSource;
    private readonly List<INotifyCollectionChanged> m_observableGroups = [];

    #region Lifecycle

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();
        SubscribeToItemsSource();
        mauiCollectionView.PropertyChanged += OnMauiPropertyChanged;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            UnsubscribeFromItemsSource();
            mauiCollectionView.PropertyChanged -= OnMauiPropertyChanged;
        }
        base.Dispose(disposing);
    }

    #endregion

    #region Cell binding

    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var cell = base.GetCell(collectionView, indexPath);

        var itemCount = (int)collectionView.NumberOfItemsInSection(indexPath.Section);
        var isFirst = indexPath.Row == 0;
        var isLast = indexPath.Row == itemCount - 1;

        TrySetMarginOnCell(GetCrossPlatformElement(cell), mauiCollectionView.Padding);
        ApplyCornerRadius(GetContentView(cell), isFirst, isLast);
        ApplyDividerVisibility(GetCrossPlatformElement(cell), isLast);

        return cell;
    }

    #endregion

    #region Position helpers

    private bool IsFirstItemInSection(NSIndexPath indexPath)
    {
        return indexPath.Row == 0;
    }

    private bool IsLastItemInSection(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var itemCount = (int)collectionView.NumberOfItemsInSection(indexPath.Section);
        return indexPath.Row == itemCount - 1;
    }

    #endregion

    #region Divider visibility

    private void ApplyDividerVisibility(Microsoft.Maui.Controls.View? crossPlatformElement, bool isLast)
    {
        if (crossPlatformElement is null)
            return;

        var divider = crossPlatformElement.FindChildOfTypeClosestToRoot<Divider>();
        if (divider is null)
            return;

        var shouldHideDivider = Effects.Layout.Layout.GetAutoHideLastDivider(mauiCollectionView) && isLast;
        divider.Opacity = shouldHideDivider ? 0 : 1;
    }

    #endregion

    #region Corner radius

    private void ApplyCornerRadius(UIView? contentView, bool isFirst, bool isLast)
    {
        if (contentView is null)
            return;

        var hasCustomFirstCornerRadius = !mauiCollectionView.FirstItemCornerRadius.IsEmpty();
        var hasCustomLastCornerRadius = !mauiCollectionView.LastItemCornerRadius.IsEmpty();
        var hasAutoCornerRadius = mauiCollectionView.AutoCornerRadius;

        if (!hasCustomFirstCornerRadius && !hasCustomLastCornerRadius && !hasAutoCornerRadius)
            return;

        var cornerRadius = GetCornerRadius(isFirst, isLast, hasCustomFirstCornerRadius, hasCustomLastCornerRadius, hasAutoCornerRadius);

        if (!cornerRadius.IsEmpty())
        {
            contentView.ClipsToBounds = true;
            contentView.Layer.CornerRadius = (nfloat)cornerRadius.HighestCornerRadius();
            contentView.Layer.MaskedCorners = CACornerMaskHelper.GetCACornerMask(cornerRadius);
        }
        else
        {
            contentView.ClipsToBounds = false;
            contentView.Layer.CornerRadius = 0;
            contentView.Layer.MaskedCorners = 0;
        }
    }

    private CornerRadius GetCornerRadius(bool isFirst, bool isLast, bool hasCustomFirstCornerRadius, bool hasCustomLastCornerRadius, bool hasAutoCornerRadius)
    {
        var cornerRadius = new CornerRadius();

        if ((hasCustomFirstCornerRadius || hasAutoCornerRadius) && isFirst)
        {
            cornerRadius = hasCustomFirstCornerRadius
                ? mauiCollectionView.FirstItemCornerRadius
                : new CornerRadius(Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small), 0, 0);
        }

        if ((hasCustomLastCornerRadius || hasAutoCornerRadius) && isLast)
        {
            cornerRadius = hasCustomLastCornerRadius
                ? mauiCollectionView.LastItemCornerRadius
                : new CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small));
        }

        return cornerRadius;
    }

    #endregion

    #region Margins

    private static void TrySetMarginOnCell(Microsoft.Maui.Controls.View? crossPlatformElement, Thickness collectionViewPadding)
    {
        if (crossPlatformElement is null)
        {
            DUILogService.LogError<CollectionViewHandler>("Could not find cross platform element in cell");
            return;
        }

        crossPlatformElement.Margin = new Thickness(
            collectionViewPadding.Left,
            crossPlatformElement.Margin.Top,
            collectionViewPadding.Right,
            crossPlatformElement.Margin.Bottom);
    }

    #endregion

    #region View helpers

    private static UIView? GetContentView(UICollectionViewCell cell)
    {
        if (cell.Subviews.Length <= 1 || cell.Subviews[1].Subviews.Length == 0)
            return null;

        return cell.Subviews[1].Subviews[0];
    }

    private static Microsoft.Maui.Controls.View? GetCrossPlatformElement(UICollectionViewCell cell)
    {
        var subView = GetContentView(cell);
        return subView switch
        {
            LayoutView layoutView => (Microsoft.Maui.Controls.View?)layoutView.CrossPlatformLayout,
            ContentView contentView => (Microsoft.Maui.Controls.View?)contentView.CrossPlatformLayout,
            _ => null
        };
    }

    #endregion

    #region Data change observer

    /// <summary>
    /// When items are inserted, removed, or moved, the boundary items (first/last) may change.
    /// Visible cells that were previously first/last need their corner radius and divider state updated.
    /// This observer defers the update to ensure the collection view state is consistent.
    /// </summary>
    private void UpdateAllVisibleCells()
    {
        foreach (var cell in CollectionView.VisibleCells)
        {
            var indexPath = CollectionView.IndexPathForCell(cell);
            if (indexPath == null)
                continue;

            var isFirst = IsFirstItemInSection(indexPath);
            var isLast = IsLastItemInSection(CollectionView, indexPath);

            ApplyCornerRadius(GetContentView(cell), isFirst, isLast);
            ApplyDividerVisibility(GetCrossPlatformElement(cell), isLast);
        }
    }

    private void SubscribeToItemsSource()
    {
        UnsubscribeFromItemsSource();
        m_observableSource = mauiCollectionView.ItemsSource as INotifyCollectionChanged;
        if (m_observableSource != null)
            m_observableSource.CollectionChanged += OnItemsSourceCollectionChanged;

        RefreshGroupSubscriptions();
    }

    private void UnsubscribeFromItemsSource()
    {
        if (m_observableSource != null)
        {
            m_observableSource.CollectionChanged -= OnItemsSourceCollectionChanged;
            m_observableSource = null;
        }

        UnsubscribeFromGroups();
    }

    private void OnMauiPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "ItemsSource")
            SubscribeToItemsSource();
    }

    private void RefreshGroupSubscriptions()
    {
        UnsubscribeFromGroups();

        if (!mauiCollectionView.IsGrouped || mauiCollectionView.ItemsSource is not IEnumerable groups)
            return;

        foreach (var group in groups.OfType<INotifyCollectionChanged>())
        {
            group.CollectionChanged += OnGroupCollectionChanged;
            m_observableGroups.Add(group);
        }
    }

    private void UnsubscribeFromGroups()
    {
        foreach (var group in m_observableGroups)
        {
            group.CollectionChanged -= OnGroupCollectionChanged;
        }

        m_observableGroups.Clear();
    }

    private void OnItemsSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        RefreshGroupSubscriptions();
        ScheduleUpdateAllVisibleCells();
    }

    private void OnGroupCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        ScheduleUpdateAllVisibleCells();
    }

    private void ScheduleUpdateAllVisibleCells()
    {
        CoreFoundation.DispatchQueue.MainQueue.DispatchAsync(UpdateAllVisibleCells);
    }

    #endregion
}
