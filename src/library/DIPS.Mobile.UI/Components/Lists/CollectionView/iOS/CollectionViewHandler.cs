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

    protected override ItemsViewLayout SelectLayout()
    {
        // Try ths shit
        return base.SelectLayout();
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

class ReordableItemsViewController(ReorderableItemsView reorderableItemsView, ItemsViewLayout layout, CollectionView mauiCollectionView)
    : ReorderableItemsViewController<ReorderableItemsView>(reorderableItemsView, layout)
{
    public override async void LoadView()
    {
        base.LoadView();
        
        try
        { 
            // For some reason we need this delay, or the app will crash :D 
            await Task.Delay(1);
            // TODO: Add standard padding to bottom 
            CollectionView.ContentInset = new UIEdgeInsets((nfloat)mauiCollectionView.Padding.Top, (nfloat)mauiCollectionView.Padding.Left, (nfloat)mauiCollectionView.Padding.Bottom, (nfloat)mauiCollectionView.Padding.Right);
            CollectionView.SetNeedsLayout();
            CollectionView.LayoutIfNeeded();
        }
        catch(Exception e)
        {
            // Safety measures
            DUILogService.LogError<CollectionViewHandler>($"Failed to set content inset for CollectionView: {e.Message}");
        }
    }
    

    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var cell = base.GetCell(collectionView, indexPath);

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

    private static void SetCellCornerRadius(UICollectionViewCell cell, CornerRadius cornerRadius)
    {
        cell.ClipsToBounds = true;
        cell.Layer.CornerRadius = (nfloat)cornerRadius.HighestCornerRadius();
        cell.Layer.MaskedCorners = CACornerMaskHelper.GetCACornerMask(cornerRadius);
    }
}