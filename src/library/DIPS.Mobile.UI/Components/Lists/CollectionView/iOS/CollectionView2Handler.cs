using CoreGraphics;
using Foundation;
using Microsoft.Maui.Controls.Handlers.Items2;
using Microsoft.Maui.Platform;
using UIKit;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView2Handler
{
    protected override ItemsViewController2<ReorderableItemsView> CreateController(ReorderableItemsView itemsView, UICollectionViewLayout layout)
    {
        return VirtualView is CollectionView { ItemsLayout: not LinearItemsLayout } ? base.CreateController(itemsView, layout) : new ReorderableItemsViewController(itemsView, layout, (VirtualView as CollectionView)!);
    }
}

public class ReorderableItemsViewController : ReorderableItemsViewController2<ReorderableItemsView>
{
    public ReorderableItemsViewController(ReorderableItemsView itemsView, UICollectionViewLayout layout, CollectionView virtualView) : base(itemsView, layout)
    {
    }

    protected override UICollectionViewDelegateFlowLayout CreateDelegator()
    {
        return new TestDelegator(ItemsViewLayout, this);
    }

    public override async void ViewDidLayoutSubviews()
    {
        base.ViewDidLayoutSubviews();
        /*ItemsViewLayout
        CollectionView.ContentInset = new UIEdgeInsets(12, 0, 100, 0);
        ItemsViewLayout.SectionInset*/
        
    }

    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var cell = base.GetCell(collectionView, indexPath);

        cell.Frame = cell.Bounds.Inset(12, 0);
        /*ItemsViewLayout*/
        /*cell.Frame = new CGRect(cell.Frame.X + 12, cell.Frame.Y, cell.Frame.Width - 24, cell.Frame.Height + 1);*/

        return cell;
    }
}

public class TestDelegator : ReorderableItemsViewDelegator2<ReorderableItemsView, ReorderableItemsViewController2<ReorderableItemsView>>
{
    public TestDelegator(UICollectionViewLayout itemsViewLayout, ReorderableItemsViewController2<ReorderableItemsView> itemsViewController2) : base(itemsViewLayout, itemsViewController2)
    {
    }

    public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, IntPtr section)
    {
        return new UIEdgeInsets(0, 12, 0, 12);
    }
    

    public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
    {
        return new CGSize(100, 100);
    }
}