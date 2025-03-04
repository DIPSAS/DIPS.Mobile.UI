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
            SetContentInset();
            CollectionView.SetNeedsLayout();
            CollectionView.LayoutIfNeeded();
        }
        catch(Exception e)
        {
            // Safety measures
            DUILogService.LogError<CollectionViewHandler>($"Failed to set content inset for CollectionView: {e.Message}");
        }
    }

    private void SetContentInset()
    {
        var bottomPadding = mauiCollectionView.HasAdditionalSpaceAtTheEnd ? (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.ContentInset.Bottom + CollectionView.Frame.Height / 2 : (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.ContentInset.Bottom;
        CollectionView.ContentInset = new UIEdgeInsets((nfloat)mauiCollectionView.Padding.Top + CollectionView.ContentInset.Top, (nfloat)mauiCollectionView.Padding.Left, bottomPadding, (nfloat)mauiCollectionView.Padding.Right);
    }

    public override void ViewDidLayoutSubviews()
    {
        base.ViewDidLayoutSubviews();
        
        try
        {
            // Maui implementation of CollectionView resets the contentinset, so we need to set it again, if it has changed (Maui never set left and right padding)
            if (Math.Abs(CollectionView.ContentInset.Left - mauiCollectionView.Padding.Left) > 0.01f ||
                Math.Abs(CollectionView.ContentInset.Right - mauiCollectionView.Padding.Right) > 0.01f)
            {
                SetContentInset();
                // Need to call this to fix a bug where only the right side padding is applied
                CollectionView.ReloadData();
            }
        }
        catch
        {
            // ignored            
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