using System.Collections;
using DIPS.Mobile.UI.Components.Dividers;
using Microsoft.Maui.Platform;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView : Microsoft.Maui.Controls.CollectionView
{
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
    }

    private void TryCollapseOrExpandElements(ItemsViewScrolledEventArgs e)
    {
        if (IsBouncing(e))
            return;
        
        // Safety measure if user scrolls too fast and the element has not have time to expand completely
        if (e.VerticalOffset <= 0)
        {
            CollapsibleElement?.Reset();
            return;
        }
        
        if (CollapsibleElement is null || e.VerticalDelta == 0)
            return;

        CollapsibleElement.TryInitialize();

        var curHeight = CollapsibleElement.Element!.HeightRequest;

        var delta = e.VerticalDelta;

#if __ANDROID__
        delta = e.VerticalDelta - m_previousHeightDifference;
#endif
        
        var isScrollingDown = delta > 0;

        if (isScrollingDown && e.VerticalOffset < CollapsibleElement.OffsetThreshold ||
            !isScrollingDown && e.VerticalOffset > CollapsibleElement.OffsetThreshold)
        {
            return;
        }
        
        if (isScrollingDown && CollapsibleElement.Element!.HeightRequest > 0)
        {
            CollapsibleElement.Element.HeightRequest = Math.Max(0, CollapsibleElement.Element.HeightRequest - delta);
        }
        else if (!isScrollingDown && CollapsibleElement.Element!.HeightRequest < CollapsibleElement.OriginalHeight)
        {
            CollapsibleElement.Element.HeightRequest = Math.Min(CollapsibleElement.OriginalHeight.Value, CollapsibleElement.Element.HeightRequest - delta);
        }

        m_previousHeightDifference = CollapsibleElement.Element.HeightRequest - curHeight;

        CollapsibleElement.TryScale();
        CollapsibleElement.TryFade();
        CollapsibleElement.TrySetInputTransparent();
    }

    private bool IsBouncing(ItemsViewScrolledEventArgs e)
    {
#if __IOS__
        if(Handler?.PlatformView is not UIKit.UIView uiView)
            return true;

        if(uiView.Subviews[0] is not UIKit.UICollectionView uiCollectionView)
            return true;

        if (e.VerticalOffset >= uiCollectionView.ContentSize.Height - uiCollectionView.Bounds.Height - 20)
            return true;

        return false;
#else
        return false;
#endif
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
        CollapsibleElement = null;
    }
}