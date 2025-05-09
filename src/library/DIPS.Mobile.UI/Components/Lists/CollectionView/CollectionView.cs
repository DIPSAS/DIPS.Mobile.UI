using System.Collections;
using DIPS.Mobile.UI.Components.Dividers;
using Microsoft.Maui.Platform;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView : Microsoft.Maui.Controls.CollectionView
{
    private readonly List<WeakReference<VisualElement>> m_inputFields = [];
    private double m_previousHeightDifference;

    public CollectionView()
    {
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        SelectionMode = SelectionMode.None;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
        {
            Dispose();
            return;
        }

        
        if (!RemoveFocusOnScroll)
            return;

        var page = this.FindParentOfType<ContentPage>();
        RetrieveInputFields(page);
    }

    private void RetrieveInputFields(IVisualTreeElement? visualTreeElement)
    {
        foreach (var child in visualTreeElement?.GetVisualTreeDescendants() ?? [])
        {
            if(Equals(child, visualTreeElement))
                continue;
            
            switch (child)
            {
                case InputView editor:
                    m_inputFields.Add(new WeakReference<VisualElement>(editor));
                    break;
                case SearchBar searchBar:
                    m_inputFields.Add(new WeakReference<VisualElement>(searchBar));
                    break;
            }
            
            RetrieveInputFields(child);
        }
    }
    
    protected override void OnScrolled(ItemsViewScrolledEventArgs e)
    {
        base.OnScrolled(e);

#if __ANDROID__ 
        // OnScrolled gets kicked off when you change the collections item source for some reason, so we have to detect if its a scroll or not
        if (Handler is CollectionViewHandler {PlatformView.ScrollState: 0}) 
            return; //0 is idle
#endif
        TryCollapseOrExpandElements(e);
        TryRemoveScroll();
    }

    private void TryCollapseOrExpandElements(ItemsViewScrolledEventArgs e)
    {
        // Safety measure if user scrolls too fast and the element has not have time to expand completely
        if (e.VerticalOffset <= 0)
        {
            CollapsableElement?.Reset();
            return;
        }
        
        if (CollapsableElement is null || e.VerticalDelta == 0)
            return;

        CollapsableElement.TryInitialize();

        var curHeight = CollapsableElement.Element!.HeightRequest;

        var delta = e.VerticalDelta;

#if __ANDROID__
        delta = e.VerticalDelta - m_previousHeightDifference;
#endif
        
        var isScrollingDown = delta > 0;

        if (isScrollingDown && e.VerticalOffset < CollapsableElement.OffsetThreshold ||
            !isScrollingDown && e.VerticalOffset > CollapsableElement.OffsetThreshold)
        {
            return;
        }
        
        if (isScrollingDown && CollapsableElement.Element!.HeightRequest > 0)
        {
            CollapsableElement.Element.HeightRequest = Math.Max(0, CollapsableElement.Element.HeightRequest - delta);
        }
        else if (!isScrollingDown && CollapsableElement.Element!.HeightRequest < CollapsableElement.OriginalHeight)
        {
            CollapsableElement.Element.HeightRequest = Math.Min(CollapsableElement.OriginalHeight.Value, CollapsableElement.Element.HeightRequest - delta);
        }

        m_previousHeightDifference = CollapsableElement.Element.HeightRequest - curHeight;

        CollapsableElement.TryScale();
        CollapsableElement.TryFade();
        CollapsableElement.TrySetInputTransparent();
    }

    private void TryRemoveScroll()
    {
        if (!RemoveFocusOnScroll)
            return;

        foreach (var inputFieldReference in m_inputFields)
        {
            if (inputFieldReference.TryGetTarget(out var inputField))
            {
                if(inputField is SearchBar searchBar)
                    searchBar.Unfocus();
                else
                    inputField.Unfocus();
            }
        }
    }

    private void TrySetItemSpacing()
    {
        var oldItemsLayout = ItemsLayout;
        ItemsLayout = oldItemsLayout switch
        {
            LinearItemsLayout linearItemsLayout => new LinearItemsLayout(linearItemsLayout.Orientation)
            {
                ItemSpacing = ItemSpacing
            },
            GridItemsLayout gridItemsLayout => new GridItemsLayout(gridItemsLayout.Span, gridItemsLayout.Orientation)
            {
                HorizontalItemSpacing = ItemSpacing, VerticalItemSpacing = ItemSpacing,
            },
            _ => null
        };
    }
    
    private void Dispose()
    {
        CollapsableElement = null;
    }
}