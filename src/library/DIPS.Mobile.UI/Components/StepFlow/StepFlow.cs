using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using DIPS.Mobile.UI.Resources.Sizes;
using MauiScrollView = Microsoft.Maui.Controls.ScrollView;

namespace DIPS.Mobile.UI.Components.StepFlow;

/// <summary>
/// Animated, accordion-style multi-step flow component. Hosts a sequence of
/// <see cref="StepFlowItem"/>s and orchestrates expand/collapse, activation, completion and
/// reset choreography in concert with a <see cref="StepFlowController"/>.
/// </summary>
[ContentProperty(nameof(Items))]
public partial class StepFlow : ContentView
{
    private readonly VerticalStackLayout m_stack = new();
    private readonly ObservableCollection<StepFlowItem> m_items = new();
    private StepFlowController? m_attachedController;

    // Cached closest ancestor scroller. Resolved lazily on first activation and invalidated
    // whenever this StepFlow is re-parented.
    private MauiScrollView? m_cachedScroller;
    private bool m_scrollerResolved;

    public StepFlow()
    {
        m_stack.Spacing = Sizes.GetSize(SizeName.size_3);
        base.Content = m_stack;

        m_items.CollectionChanged += OnItemsCollectionChanged;
        ParentChanged += OnParentChangedInvalidateScroller;
    }

    /// <summary>The steps in display order. This is the XAML default ContentProperty.</summary>
    public IList<StepFlowItem> Items => m_items;

    private void OnItemsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.OldItems is not null)
        {
            foreach (StepFlowItem item in e.OldItems)
            {
                DetachItem(item);
            }
        }

        if (e.NewItems is not null)
        {
            foreach (StepFlowItem item in e.NewItems)
            {
                AttachItem(item);
            }
        }

        ReindexAndSyncStack();
        InitializeControllerIfReady();
    }

    private void AttachItem(StepFlowItem item)
    {
        item.HeaderTapped += OnItemHeaderTapped;
        // Watching the item's State directly works for both controller-driven activation
        // (controller mutates State on the item) and the controller-less escape hatch where
        // the consumer assigns State manually.
        item.PropertyChanged += OnItemPropertyChanged;
    }

    private void DetachItem(StepFlowItem item)
    {
        item.HeaderTapped -= OnItemHeaderTapped;
        item.PropertyChanged -= OnItemPropertyChanged;
        m_stack.Children.Remove(item);
        // Manually disconnect handlers so the native handler chain on the item and its children
        // does not keep the visual tree alive after removal.
        item.DisconnectHandlers();
    }

    private void ReindexAndSyncStack()
    {
        // Sync the internal stack with the Items list. We rebuild rather than diff because
        // the operation is rare and item count is small.
        var current = m_stack.Children.OfType<StepFlowItem>().ToList();
        foreach (var c in current)
        {
            if (!m_items.Contains(c)) m_stack.Children.Remove(c);
        }

        for (var i = 0; i < m_items.Count; i++)
        {
            var item = m_items[i];
            item.Index = i;
            item.DisplayNumber = i + 1;
            item.TotalSteps = m_items.Count;
            item.RefreshTitleText();
            if (!m_stack.Children.Contains(item))
            {
                m_stack.Children.Insert(Math.Min(i, m_stack.Children.Count), item);
            }
        }
    }

    private void OnControllerChanged(StepFlowController? oldController, StepFlowController? newController)
    {
        DetachController(oldController);
        AttachController(newController);
        InitializeControllerIfReady();
    }

    private void AttachController(StepFlowController? controller)
    {
        if (controller is null) return;
        controller.StateChanged += OnControllerStateChanged;
        controller.FlowCompleted += OnControllerFlowCompleted;
        m_attachedController = controller;
    }

    private void DetachController(StepFlowController? controller)
    {
        if (controller is null) return;
        controller.StateChanged -= OnControllerStateChanged;
        controller.FlowCompleted -= OnControllerFlowCompleted;
        if (ReferenceEquals(controller, m_attachedController))
        {
            m_attachedController = null;
        }
    }

    private void InitializeControllerIfReady()
    {
        if (m_attachedController is null) return;
        if (m_items.Count == 0) return;

        m_attachedController.Initialize(m_items.Count);
        for (var i = 0; i < m_items.Count; i++)
        {
            m_items[i].State = m_attachedController.States[i];
        }
    }

    private void OnControllerStateChanged(object? sender, StepFlowEventArgs e)
    {
        if (e.Index < 0 || e.Index >= m_items.Count) return;
        var controller = m_attachedController;
        if (controller is null) return;
        m_items[e.Index].State = controller.States[e.Index];
    }

    private void OnControllerFlowCompleted(object? sender, EventArgs e)
    {
        FlowCompleted?.Invoke(this, EventArgs.Empty);
    }

    private void OnItemHeaderTapped(object? sender, EventArgs e)
    {
        if (!IsEnabled) return;
        if (sender is not StepFlowItem item) return;

        var controller = m_attachedController;
        if (controller is null)
        {
            // Escape hatch (no controller): manually enforce single-active.
            if (item.State == StepFlowItemState.Completed && item.LockWhenCompleted) return;
            foreach (var other in m_items)
            {
                if (!ReferenceEquals(other, item) && other.State == StepFlowItemState.Active)
                {
                    other.State = StepFlowItemState.Disabled;
                }
            }
            item.State = StepFlowItemState.Active;
            return;
        }

        if (item.Index < 0) return;
        controller.GoTo(item.Index);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);
        if (args.NewHandler is null)
        {
            DetachController(m_attachedController);
            foreach (var item in m_items.ToList())
            {
                DetachItem(item);
            }
            m_items.CollectionChanged -= OnItemsCollectionChanged;
            ParentChanged -= OnParentChangedInvalidateScroller;
            m_cachedScroller = null;
            m_scrollerResolved = false;
        }
    }

    private void OnItemPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(StepFlowItem.State)) return;
        if (sender is not StepFlowItem item) return;
        if (item.State != StepFlowItemState.Active) return;
        if (!AutoScrollIntoView) return;

        _ = ScrollItemIntoViewAsync(item);
    }

    private void OnParentChangedInvalidateScroller(object? sender, EventArgs e)
    {
        m_cachedScroller = null;
        m_scrollerResolved = false;
    }

    private MauiScrollView? ResolveScroller()
    {
        if (m_scrollerResolved) return m_cachedScroller;
        m_scrollerResolved = true;

        Element? walker = Parent;
        while (walker is not null)
        {
            if (walker is MauiScrollView sv)
            {
                m_cachedScroller = sv;
                return m_cachedScroller;
            }
            // ContentPage whose root is a ScrollView is covered by the parent walk above —
            // the StepFlow's Parent chain already passes through that ScrollView before
            // reaching the ContentPage.
            walker = walker.Parent;
        }

        m_cachedScroller = null;
        return null;
    }

    private async Task ScrollItemIntoViewAsync(StepFlowItem item)
    {
        var scroller = ResolveScroller();
        if (scroller is null) return;

        // Wait for the expand animation to finish so the body's measured height is settled
        // before we ask the ScrollView to scroll. Otherwise we would target a too-short rect
        // and the freshly-revealed body would still be off-screen once the animation completes.
        await Task.Delay((int)StepFlowItem.ExpandDurationMs + 20);

        // The item or its scroller may have been torn down while we were waiting.
        if (Handler is null || item.Handler is null || scroller.Handler is null) return;
        if (!ReferenceEquals(scroller, ResolveScroller())) return;

        try
        {
            // Pin the active step to the top of the scroller. MakeVisible only scrolls the
            // minimum needed to bring the item on-screen, which leaves the header low on the
            // screen and most of the freshly expanded body still below the fold.
            await scroller.ScrollToAsync(item, ScrollToPosition.Start, animated: true);
        }
        catch
        {
            // ScrollToAsync can throw if the item is detached mid-scroll — swallow because
            // there is nothing the consumer or this component can do about it.
        }
    }
}
