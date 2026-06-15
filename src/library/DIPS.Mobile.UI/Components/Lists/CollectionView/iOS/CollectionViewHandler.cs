using System.Collections;
using System.Collections.Specialized;
using DIPS.Mobile.UI.Components;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Effects.Accessibility.Effects;
using DIPS.Mobile.UI.Effects.Layout;
using DIPS.Mobile.UI.Extensions;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using Microsoft.Maui.Controls.Handlers.Items2;
using Microsoft.Maui.Platform;
using UIKit;
using AccessibilityTrait = DIPS.Mobile.UI.Effects.Accessibility.Trait;
using ContentView = Microsoft.Maui.Platform.ContentView;
using DUIAccessibility = DIPS.Mobile.UI.Effects.Accessibility.Accessibility;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler
{
    private const int MaxPendingFocusRestoreLayoutPasses = 3;
    private IReloadFocusPreservable? m_focusedHeaderOrFooterElementBeforeItemsSourceMapping;
    private IReloadFocusPreservable? m_pendingFocusRestore;
    private int m_pendingFocusRestoreLayoutPasses;
    private INotifyCollectionChanged? m_focusPreservingItemsSource;
    private readonly List<INotifyCollectionChanged> m_focusPreservingGroups = [];

    protected override ItemsViewController2<ReorderableItemsView> CreateController(ReorderableItemsView itemsView,
        UICollectionViewLayout layout)
    {
        // ONLY create a ReorderableItemsViewController if the CollectionView is using a vertical LinearItemsLayout, otherwise use the default controller.
        if (VirtualView is CollectionView { ItemsLayout: LinearItemsLayout { Orientation: ItemsLayoutOrientation.Vertical } })
        {
            return new ReorderableItemsViewController(itemsView, layout, (VirtualView as CollectionView)!, this, CompletePendingFocusRestoreAfterLayout);
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
        if (handler.GetUICollectionView() is not { } uiCollectionView)
            return;

        if (virtualView is CollectionView collectionView)
            SetKeyboardDismissMode(uiCollectionView, collectionView);
    }

    partial void BeforeItemsSourceMapped()
    {
        if (VirtualView is CollectionView collectionView)
            SubscribeToFocusPreservingItemsSource(collectionView);

        m_focusedHeaderOrFooterElementBeforeItemsSourceMapping = CaptureFocusedHeaderOrFooterElement();
    }

    protected override void DisconnectHandler(UIView platformView)
    {
        UnsubscribeFromFocusPreservingItemsSource();
        base.DisconnectHandler(platformView);
    }

    partial void OnItemsSourceMapped()
    {
        if (VirtualView is CollectionView collectionView && GetUICollectionView() is { } uiCollectionView)
            SetKeyboardDismissMode(uiCollectionView, collectionView);

        var focusedElement = m_focusedHeaderOrFooterElementBeforeItemsSourceMapping;
        m_focusedHeaderOrFooterElementBeforeItemsSourceMapping = null;
        ScheduleFocusRestoreAfterNextLayout(focusedElement);
    }

    private UICollectionView? GetUICollectionView()
    {
        if (PlatformView.Subviews.Length == 0)
            return null;

        return PlatformView.Subviews[0] as UICollectionView;
    }

    private static void SetKeyboardDismissMode(UICollectionView uiCollectionView, CollectionView collectionView)
    {
        uiCollectionView.KeyboardDismissMode = collectionView.RemoveFocusOnScroll
            ? UIScrollViewKeyboardDismissMode.OnDrag
            : UIScrollViewKeyboardDismissMode.None;
    }

    internal partial void ReloadData(CollectionViewHandler handler)
    {
        if (handler.PlatformView.Subviews[0] is UICollectionView uiCollectionView)
        {
            var focusedElement = handler.CaptureFocusedHeaderOrFooterElement();
            uiCollectionView.ReloadData();
            handler.ScheduleFocusRestoreAfterNextLayout(focusedElement);
        }
    }

    private IReloadFocusPreservable? CaptureFocusedHeaderOrFooterElement()
    {
        return FindFocusedHeaderOrFooterElement();
    }

    private IReloadFocusPreservable? FindFocusedHeaderOrFooterElement()
    {
        if (VirtualView is not CollectionView collectionView)
            return null;

        return FindFocusedElement(collectionView.Header) ?? FindFocusedElement(collectionView.Footer);
    }

    private static IReloadFocusPreservable? FindFocusedElement(object? candidate)
    {
        if (candidate is IReloadFocusPreservable { HasPreservedFocus: true } focusPreservableElement)
        {
            return focusPreservableElement;
        }

        if (candidate is Microsoft.Maui.Controls.VisualElement { IsFocused: true } focusedElement)
            return new VisualElementFocusPreserver(focusedElement);

        if (candidate is not IVisualTreeElement visualTreeElement)
            return null;

        foreach (var child in visualTreeElement.GetVisualChildren())
        {
            if (FindFocusedElement(child) is { } focusedChild)
                return focusedChild;
        }

        return null;
    }

    private void ScheduleFocusRestoreAfterNextLayout(IReloadFocusPreservable? focusedElement)
    {
        if (focusedElement is null)
            return;

        m_pendingFocusRestore = focusedElement;
        m_pendingFocusRestoreLayoutPasses = 0;
    }

    private void CompletePendingFocusRestoreAfterLayout()
    {
        var focusedElement = m_pendingFocusRestore;
        if (focusedElement is null)
            return;

        if (GetUICollectionView() is { Tracking: true } or { Dragging: true } or { Decelerating: true })
        {
            m_pendingFocusRestore = null;
            m_pendingFocusRestoreLayoutPasses = 0;
            return;
        }

        var restoredFocus = focusedElement.TryRestoreFocus();
        if (restoredFocus)
        {
            m_pendingFocusRestore = null;
            m_pendingFocusRestoreLayoutPasses = 0;
            return;
        }

        m_pendingFocusRestoreLayoutPasses++;
        if (m_pendingFocusRestoreLayoutPasses >= MaxPendingFocusRestoreLayoutPasses)
        {
            m_pendingFocusRestore = null;
            m_pendingFocusRestoreLayoutPasses = 0;
        }
    }

    private void SubscribeToFocusPreservingItemsSource(CollectionView collectionView)
    {
        UnsubscribeFromFocusPreservingItemsSource();

        if (collectionView.ItemsSource is INotifyCollectionChanged observableSource)
        {
            m_focusPreservingItemsSource = observableSource;
            observableSource.CollectionChanged += OnFocusPreservingItemsSourceCollectionChanged;
        }

        RefreshFocusPreservingGroupSubscriptions(collectionView);
    }

    private void UnsubscribeFromFocusPreservingItemsSource()
    {
        if (m_focusPreservingItemsSource is not null)
        {
            m_focusPreservingItemsSource.CollectionChanged -= OnFocusPreservingItemsSourceCollectionChanged;
            m_focusPreservingItemsSource = null;
        }

        foreach (var group in m_focusPreservingGroups)
        {
            group.CollectionChanged -= OnFocusPreservingItemsSourceCollectionChanged;
        }

        m_focusPreservingGroups.Clear();
    }

    private void RefreshFocusPreservingGroupSubscriptions(CollectionView collectionView)
    {
        foreach (var group in m_focusPreservingGroups)
        {
            group.CollectionChanged -= OnFocusPreservingItemsSourceCollectionChanged;
        }

        m_focusPreservingGroups.Clear();

        if (!collectionView.IsGrouped || collectionView.ItemsSource is not IEnumerable groups)
            return;

        foreach (var group in groups.OfType<INotifyCollectionChanged>())
        {
            group.CollectionChanged += OnFocusPreservingItemsSourceCollectionChanged;
            m_focusPreservingGroups.Add(group);
        }
    }

    private void OnFocusPreservingItemsSourceCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (m_pendingFocusRestore is null)
            ScheduleFocusRestoreAfterNextLayout(CaptureFocusedHeaderOrFooterElement());

        if (VirtualView is CollectionView collectionView)
            RefreshFocusPreservingGroupSubscriptions(collectionView);
    }

    private sealed class VisualElementFocusPreserver(Microsoft.Maui.Controls.VisualElement visualElement) : IReloadFocusPreservable
    {
        public bool HasPreservedFocus => visualElement.IsFocused;

        public bool TryRestoreFocus()
        {
            if (visualElement.IsFocused)
                return true;

            if (visualElement.Handler is null)
                return false;

            visualElement.Focus();
            return visualElement.IsFocused;
        }
    }
}

public class ReorderableItemsViewController(
    ReorderableItemsView itemsView,
    UICollectionViewLayout layout,
    CollectionView mauiCollectionView,
    CollectionViewHandler collectionViewHandler,
    Action collectionViewDidLayoutSubviews)
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

    public override void ViewDidLayoutSubviews()
    {
        base.ViewDidLayoutSubviews();
        collectionViewDidLayoutSubviews();
    }

    #endregion

    #region Cell binding

    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var cell = base.GetCell(collectionView, indexPath);

        var itemCount = (int)collectionView.NumberOfItemsInSection(indexPath.Section);
        var isFirst = indexPath.Row == 0;
        var isLast = indexPath.Row == itemCount - 1;

        var crossPlatformElement = GetCrossPlatformElement(cell);

        TrySetMarginOnCell(crossPlatformElement, mauiCollectionView.Padding);
        ApplyCornerRadius(GetContentView(cell), isFirst, isLast);
        ApplyDividerVisibility(crossPlatformElement, isLast);
        ApplyAccessibilityTraits(cell, crossPlatformElement);

        return cell;
    }

    #endregion

    #region Accessibility

    private static void ApplyAccessibilityTraits(UICollectionViewCell cell, Microsoft.Maui.Controls.View? crossPlatformElement)
    {
        if (crossPlatformElement is null)
            return;

        var trait = DUIAccessibility.GetTrait(crossPlatformElement);
        ApplyAccessibilityTraits(cell, trait);
        ApplyAccessibilityTraits(cell.ContentView, trait);

        if (GetContentView(cell) is { } contentView)
        {
            ApplyAccessibilityTraits(contentView, trait);
        }

        if (trait != AccessibilityTrait.None && crossPlatformElement.Handler?.PlatformView is UIView platformView)
        {
            ApplyAccessibilityTraits(platformView, trait);
        }
    }

    private static void ApplyAccessibilityTraits(UIView view, AccessibilityTrait trait)
    {
        view.AccessibilityTraits = TraitPlatformEffect.ApplyTraits(view.AccessibilityTraits, trait);
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
        {
            ResetCornerRadius(contentView);
            return;
        }

        var cornerRadius = GetCornerRadius(isFirst, isLast, hasCustomFirstCornerRadius, hasCustomLastCornerRadius, hasAutoCornerRadius);

        if (!cornerRadius.IsEmpty())
        {
            contentView.ClipsToBounds = true;
            contentView.Layer.CornerRadius = (nfloat)cornerRadius.HighestCornerRadius();
            contentView.Layer.MaskedCorners = CACornerMaskHelper.GetCACornerMask(cornerRadius);
        }
        else
        {
            ResetCornerRadius(contentView);
        }
    }

    private static void ResetCornerRadius(UIView contentView)
    {
        contentView.ClipsToBounds = false;
        contentView.Layer.CornerRadius = 0;
        contentView.Layer.MaskedCorners = 0;
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
        if (e.PropertyName == nameof(mauiCollectionView.ItemsSource))
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
