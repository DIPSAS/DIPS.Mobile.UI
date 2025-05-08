using System.Collections;
using DIPS.Mobile.UI.Components.Dividers;
using Microsoft.Maui.Platform;
using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView : Microsoft.Maui.Controls.CollectionView
{
    private readonly List<WeakReference<VisualElement>> m_inputFields = [];
    private bool m_suppressScroll;

    public CollectionView()
    {
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        SelectionMode = SelectionMode.None;
    }

#if __ANDROID__
    /// <summary>
    /// The Scroll event will be invoked on Android if the CollectionView's size is changed
    /// So we need to make sure to suppress the next scroll event
    /// </summary>
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        m_suppressScroll = true;
    }
#endif

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
            return;

        
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
        if (CollapsableElements.Count == 0)
            return;

        foreach (var collapsableElement in CollapsableElements)
        {
#if __ANDROID__
            // For some reason, Android bugs out if scrolling up and down at the start, so force reset when reaching start
            if (e.VerticalOffset == 0)
            {
                collapsableElement.Reset();
                continue;
            }
            
            if (m_suppressScroll)
            {
                m_suppressScroll = false;
                return;
            }
#endif

            collapsableElement.TryInitialize();

            if (IsBouncing(e))
                continue;
            
            var isScrollingDown = e.VerticalDelta > 0;
            // Shrink the search bar when scrolling down
            if (isScrollingDown && collapsableElement.Element.HeightRequest > 0)
            {
                collapsableElement.Element.HeightRequest = Math.Max(0, collapsableElement.Element.HeightRequest - e.VerticalDelta);
            }
            // Expand the search bar when scrolling up
            else if (!isScrollingDown && collapsableElement.Element.HeightRequest < collapsableElement.OriginalHeight)
            {
                collapsableElement.Element.HeightRequest = Math.Min(collapsableElement.OriginalHeight.Value, collapsableElement.Element.HeightRequest - e.VerticalDelta);
            }
            
            collapsableElement.TryScale();
            collapsableElement.TryFade();
            collapsableElement.TrySetInputTransparent();
        }
    }

    private bool IsBouncing(ItemsViewScrolledEventArgs e)
    {
#if __IOS__
        if(Handler?.PlatformView is not UIKit.UIView uiView)
            return true;
            
        if(uiView.Subviews[0] is not UIKit.UICollectionView uiCollectionView)
            return true;
            
        // Prevent adjustments if the list is bouncing (at the start or end)
        if (e.VerticalOffset <= 0 || e.VerticalOffset >= uiCollectionView.ContentSize.Height - uiCollectionView.ContentInset.Bottom - 20)
        {
            return true;
        }

        return false;
#else
        return false;
#endif
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
}