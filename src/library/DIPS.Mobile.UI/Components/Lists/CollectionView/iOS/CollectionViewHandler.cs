using CoreGraphics;
using DIPS.Mobile.UI.Effects.Layout;
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

internal class ReordableItemsViewController(ReorderableItemsView reorderableItemsView, ItemsViewLayout layout, CollectionView mauiCollectionView)
    : ReorderableItemsViewController<ReorderableItemsView>(reorderableItemsView, layout)
{
    private UIEdgeInsets? m_overridenContentInset;
    
    private void SetContentInset()
    {
        var bottomPadding = mauiCollectionView.HasAdditionalSpaceAtTheEnd ? (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.ContentInset.Bottom + CollectionView.Frame.Height / 2 : (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.ContentInset.Bottom;
        CollectionView.ContentInset = new UIEdgeInsets(CollectionView.ContentInset.Top, CollectionView.ContentInset.Left, bottomPadding, CollectionView.ContentInset.Right);
        
        m_overridenContentInset = CollectionView.ContentInset;
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

    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var cell = base.GetCell(collectionView, indexPath);

        SetCellMargin(collectionView, cell);

        if(mauiCollectionView.LastItemCornerRadius.IsEmpty() && mauiCollectionView.FirstItemCornerRadius.IsEmpty() && !mauiCollectionView.AutoCornerRadius)
            return cell;
        
        if ((!mauiCollectionView.FirstItemCornerRadius.IsEmpty() || mauiCollectionView.AutoCornerRadius) && indexPath.Row == 0)
        {
            // Apply corner radius to the cell if its the first cell
            SetCellCornerRadius(cell, mauiCollectionView.FirstItemCornerRadius.IsEmpty() ? new CornerRadius(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2), 0, 0) : mauiCollectionView.FirstItemCornerRadius);

            return cell;
        }
        
        if ((!mauiCollectionView.LastItemCornerRadius.IsEmpty() || mauiCollectionView.AutoCornerRadius) && indexPath.Row == collectionView.NumberOfItemsInSection(indexPath.Section) - 1)
        {
            // Apply corner radius to the cell if its the last cell
            SetCellCornerRadius(cell, mauiCollectionView.LastItemCornerRadius.IsEmpty() ? new CornerRadius(0, 0, Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2)) : mauiCollectionView.LastItemCornerRadius);
            return cell;
        }

        // Reset the corner radius for all other cells, because of virtualization
        cell.ClipsToBounds = false;
        cell.Layer.CornerRadius = 0;
        cell.Layer.MaskedCorners = 0;

        return cell;
    }

    private void SetCellMargin(UICollectionView collectionView, UICollectionViewCell cell)
    {
        var horizontalPadding = 0;
        if(mauiCollectionView.Padding.Left > mauiCollectionView.Padding.Right)
        {
            horizontalPadding = (int)mauiCollectionView.Padding.Left * 2;
        }
        else if(mauiCollectionView.Padding.Right > mauiCollectionView.Padding.Left)
        {
            horizontalPadding = (int)mauiCollectionView.Padding.Right * 2;
        }
        
        cell.Frame = new CGRect(x: horizontalPadding / 2, y: cell.Frame.Y, width: collectionView.Frame.Width - horizontalPadding, height: cell.Frame.Size.Height);
    }

    private static void SetCellCornerRadius(UICollectionViewCell cell, CornerRadius cornerRadius)
    {
        cell.ClipsToBounds = true;
        cell.Layer.CornerRadius = (nfloat)cornerRadius.HighestCornerRadius();
        cell.Layer.MaskedCorners = CACornerMaskHelper.GetCACornerMask(cornerRadius);
    }
}