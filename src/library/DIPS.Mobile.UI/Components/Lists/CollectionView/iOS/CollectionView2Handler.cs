using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Extensions.iOS;
using Foundation;
using Microsoft.Maui.Controls.Handlers.Items2;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView2Handler
{
    public CollectionView2Handler() : base(CollectionViewPropertyMapper)
    {
    }

    public static readonly PropertyMapper CollectionViewPropertyMapper =
        new PropertyMapper<CollectionView2, CollectionViewHandler2>(Mapper)
        {
            [nameof(CollectionView2.ShouldBounce)] = MapShouldBounce
        };

    protected override ItemsViewController2<ReorderableItemsView> CreateController(ReorderableItemsView itemsView,
        UICollectionViewLayout layout)
    {
        return VirtualView is CollectionView { ItemsLayout: not LinearItemsLayout }
            ? base.CreateController(itemsView, layout)
            : new ReorderableItemsViewController(itemsView, layout, (VirtualView as CollectionView)!);
    }

    private static void MapShouldBounce(CollectionViewHandler2 handler,
        CollectionView2 virtualView)
    {
        if (handler.PlatformView.Subviews[0] is not UICollectionView uiCollectionView)
            return;
            
        if (virtualView.ItemsLayout is ItemsLayout { Orientation: ItemsLayoutOrientation.Horizontal })
        {
            uiCollectionView.AlwaysBounceHorizontal = virtualView.ShouldBounce;
        }
        else
        {
            uiCollectionView.AlwaysBounceVertical = virtualView.ShouldBounce;
        }
    }

    internal void ReloadData(CollectionView2Handler handler)
    {
        if (handler.PlatformView.Subviews[0] is UICollectionView uiCollectionView)
        {
            uiCollectionView.ReloadData();
        }
    }
}

public class ReorderableItemsViewController(
    ReorderableItemsView itemsView,
    UICollectionViewLayout layout,
    CollectionView mauiCollectionView)
    : ReorderableItemsViewController2<ReorderableItemsView>(itemsView, layout)
{
    private UIEdgeInsets? m_overridenContentInset;

    private readonly Dictionary<int, Divider> m_currentDividerSetToInvisibleInSection = new();
    private readonly Dictionary<int, UICollectionViewCell> m_currentLastCellWithCornerRadiusInSection = new();
    private readonly Dictionary<int, UICollectionViewCell> m_currentFirstCellWithCornerRadiusInSection = new();
    
    private void SetContentInset()
    {
        var bottomPadding = mauiCollectionView.HasAdditionalSpaceAtTheEnd
            ? (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.Frame.Height / 2
            : (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.ContentInset.Bottom;
        CollectionView.ContentInset = new UIEdgeInsets(CollectionView.ContentInset.Top,
            CollectionView.ContentInset.Left, bottomPadding, CollectionView.ContentInset.Right);

        m_overridenContentInset = CollectionView.ContentInset;
    }

    /// <summary>
    /// Maui sets the ContentInset in this function, so we need to override it to set the ContentInset after Maui has set it.
    /// </summary>
    public override void ViewDidLayoutSubviews()
    {
        base.ViewDidLayoutSubviews();

        if (m_overridenContentInset is null)
        {
            SetContentInset();
        }
        else if (CollectionView.ContentInset != m_overridenContentInset)
        {
            SetContentInset();
        }
    }

    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var cell = base.GetCell(collectionView, indexPath);

        ReordeableItemsViewController.TrySetMarginOnCell(cell, mauiCollectionView.Padding);
        ReordeableItemsViewController.TrySetCornerRadiusOnCell(collectionView, indexPath, cell, mauiCollectionView, m_currentLastCellWithCornerRadiusInSection, m_currentFirstCellWithCornerRadiusInSection);
        ReordeableItemsViewController.TrySetDividerInvisible(collectionView, indexPath, cell, mauiCollectionView, m_currentDividerSetToInvisibleInSection);
        
        return cell;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        m_currentDividerSetToInvisibleInSection.Clear();
        m_currentFirstCellWithCornerRadiusInSection.Clear();
        m_currentLastCellWithCornerRadiusInSection.Clear();
    }
}