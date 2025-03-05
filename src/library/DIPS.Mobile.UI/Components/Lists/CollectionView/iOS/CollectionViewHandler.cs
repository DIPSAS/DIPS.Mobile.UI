using CoreGraphics;
using DIPS.Mobile.UI.Effects.Layout;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using Microsoft.Maui.Controls.Handlers.Items;
using UIKit;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler
{
    protected override ItemsViewController<ReorderableItemsView> CreateController(ReorderableItemsView itemsView, ItemsViewLayout layout)
    {
        // Only use new controller if the ItemsLayout is LinearItemsLayout
        return VirtualView is CollectionView { ItemsLayout: not LinearItemsLayout } ? base.CreateController(itemsView, layout) : new ReordableItemsViewController(itemsView, layout, (VirtualView as CollectionView)!);
    }

    protected override void UpdateLayout()
    {
        base.UpdateLayout();
    }
    protected override ItemsViewLayout SelectLayout()
    {
        // Only use new layout if the ItemsLayout is LinearItemsLayout
        return VirtualView is CollectionView { ItemsLayout: not LinearItemsLayout } ? base.SelectLayout() : new ListViewLayout(new LinearItemsLayout(ItemsLayoutOrientation.Vertical), ItemsView.ItemSizingStrategy, (VirtualView as CollectionView)!);
    }


    
    private static partial void MapShouldBounce(CollectionViewHandler handler,
        Microsoft.Maui.Controls.CollectionView virtualView)
    {
        if (handler.PlatformView.Subviews[0] is UICollectionView uiCollectionView)
        {
            if (virtualView is CollectionView collectionView)
            {
                if (virtualView.ItemsLayout is ItemsLayout {Orientation: ItemsLayoutOrientation.Horizontal})
                {
                    uiCollectionView.AlwaysBounceHorizontal = collectionView.ShouldBounce;
                }
                else
                {
                    uiCollectionView.AlwaysBounceVertical = collectionView.ShouldBounce;
                    
                }
                 
            }
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

internal class ListViewLayout : Microsoft.Maui.Controls.Handlers.Items.ListViewLayout
{
    private CollectionView? m_collectionView;

    public ListViewLayout(LinearItemsLayout itemsLayout, ItemSizingStrategy itemSizingStrategy, CollectionView collectionView) : base(itemsLayout, itemSizingStrategy)
    {
        m_collectionView = collectionView;
        
    }
    
    

    

    /*public override void ConstrainTo(CGSize size)
    {
        size.Width -= 24;
        ConstrainedDimension = size.Width;
        DetermineCellSize();
        /*InvalidateLayout();#1#
        /*EstimatedItemSize = new CGSize(40, 100);#1#
    }*/
    
    

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);

        m_collectionView = null;
    }
}

internal class ReordableItemsViewController(ReorderableItemsView reorderableItemsView, ItemsViewLayout layout, CollectionView mauiCollectionView)
    : ReorderableItemsViewController<ReorderableItemsView>(reorderableItemsView, layout)
{
    private UIEdgeInsets? m_overridenContentInset;
    
    private void SetContentInset()
    {
        var bottomPadding = mauiCollectionView.HasAdditionalSpaceAtTheEnd ? (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.Frame.Height / 2 : (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.ContentInset.Bottom;
        CollectionView.ContentInset = new UIEdgeInsets(CollectionView.ContentInset.Top, CollectionView.ContentInset.Left, bottomPadding, CollectionView.ContentInset.Right);
        
        m_overridenContentInset = CollectionView.ContentInset;
    }

    /*public override void UpdateItemsSource()
    {
        base.UpdateItemsSource();
        
        MainThread.BeginInvokeOnMainThread(() => CollectionView.ReloadData());
    }*/

    protected override UICollectionViewDelegateFlowLayout CreateDelegator()
    {
        return new Test123(ItemsViewLayout, this);
        
    }

    /// <summary>
    /// Maui sets the ContentInset in this function, so we need to override it to set the ContentInset after Maui has set it.
    /// </summary>
    public override void ViewDidLayoutSubviews()
    {
        base.ViewDidLayoutSubviews();

        if(m_overridenContentInset is null)
        {
            SetContentInset();
        }
        else if(CollectionView.ContentInset != m_overridenContentInset)
        {
            SetContentInset();
        }
    }

    /*protected override void CacheCellAttributes(NSIndexPath indexPath, CGSize size)
    {
        size.Width -= 24;
        base.CacheCellAttributes(indexPath, size);
    }*/

    
    
    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var cell = base.GetCell(collectionView, indexPath);

        var paddingWrapper = cell.Subviews[1]?.Subviews[0];

        if (paddingWrapper is null)
        {
            DUILogService.LogDebug<ReorderableItemsView>("Could not find padding wrapper in cell; could not modify cell");
            return cell;
        }
        
        var consumerItem = paddingWrapper.Subviews[0];

        if (consumerItem is null)
        {
            DUILogService.LogDebug<ReorderableItemsView>("Could not find consumer item in cell; could not modify cell");
            return cell;
        }
        
        if(mauiCollectionView.LastItemCornerRadius.IsEmpty() && mauiCollectionView.FirstItemCornerRadius.IsEmpty() && !mauiCollectionView.AutoCornerRadius)
            return cell;
        
        if ((!mauiCollectionView.FirstItemCornerRadius.IsEmpty() || mauiCollectionView.AutoCornerRadius) && indexPath.Row == 0)
        {
            // Apply corner radius to the cell if its the first cell
            SetViewCornerRadius(consumerItem, mauiCollectionView.FirstItemCornerRadius.IsEmpty() ? new CornerRadius(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2), 0, 0) : mauiCollectionView.FirstItemCornerRadius);

            return cell;
        }
        
        if ((!mauiCollectionView.LastItemCornerRadius.IsEmpty() || mauiCollectionView.AutoCornerRadius) && indexPath.Row == collectionView.NumberOfItemsInSection(indexPath.Section) - 1)
        {
            // Apply corner radius to the cell if its the last cell
            SetViewCornerRadius(consumerItem, mauiCollectionView.LastItemCornerRadius.IsEmpty() ? new CornerRadius(0, 0, Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2)) : mauiCollectionView.LastItemCornerRadius);
            return cell;
        }

        // Reset the corner radius for all other cells, because of virtualization
        consumerItem.ClipsToBounds = false;
        consumerItem.Layer.CornerRadius = 0;
        consumerItem.Layer.MaskedCorners = 0;

        return cell;
    }

    private static void SetViewCornerRadius(UIView cell, CornerRadius cornerRadius)
    {
        cell.ClipsToBounds = true;
        cell.Layer.CornerRadius = (nfloat)cornerRadius.HighestCornerRadius();
        cell.Layer.MaskedCorners = CACornerMaskHelper.GetCACornerMask(cornerRadius);
    }
}

internal class Test123 : ReorderableItemsViewDelegator<ReorderableItemsView, ReorderableItemsViewController<ReorderableItemsView>>
{
    public Test123(ItemsViewLayout itemsViewLayout, ReorderableItemsViewController<ReorderableItemsView> itemsViewController) : base(itemsViewLayout, itemsViewController)
    {
    }

    /*public override CGSize GetReferenceSizeForFooter(UICollectionView collectionView, UICollectionViewLayout layout, IntPtr section)
    {
        var test = base.GetReferenceSizeForFooter(collectionView, layout, section);
        return test;
    }*/

    /*public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
    {
        var size = base.GetSizeForItem(collectionView, layout, indexPath);
        size.Width -= 24;
        return size;
    }*/
}

/*internal class AwesomeCell : TemplatedCell
{
    public static NSString ReuseId = new NSString("DIPS.Mobile.UI.AwesomeCell");
    public AwesomeCell(CGRect frame) : base(frame)
    {
    }


    public override CGSize Measure()
    {
        // Get property with reflection
        
        
        
        var measure = PlatformHandler.VirtualView.Measure(ConstrainedDimension, double.PositiveInfinity);

        return new CGSize(ConstrainedDimension, measure.Height);
    }

    protected override (bool, Size) NeedsContentSizeUpdate(Size currentSize)
    {
        
    }

    protected override bool AttributesConsistentWithConstrainedDimension(UICollectionViewLayoutAttributes attributes)
    {
        
    }
}*/