using CoreGraphics;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Effects.Layout;
using DIPS.Mobile.UI.Extensions.iOS;
using DIPS.Mobile.UI.Internal.Logging;
using Foundation;
using Microsoft.Maui.Controls.Handlers.Items;
using Microsoft.Maui.Platform;
using UIKit;
using ContentView = Microsoft.Maui.Platform.ContentView;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler
{
    protected override ItemsViewController<ReorderableItemsView> CreateController(ReorderableItemsView itemsView, ItemsViewLayout layout)
    {
        // Only use new controller if the ItemsLayout is vertical LinearItemsLayout
        if(VirtualView is CollectionView { ItemsLayout: LinearItemsLayout {Orientation: ItemsLayoutOrientation.Vertical} })
            return new ReordableItemsViewController(itemsView, layout, VirtualView as CollectionView);
            
        return base.CreateController(itemsView, layout);
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
    
    private readonly Dictionary<int, Divider> m_currentDividerSetToInvisibleInSection = new();
    private readonly Dictionary<int, UICollectionViewCell> m_currentLastCellWithCornerRadiusInSection = new();
    private readonly Dictionary<int, UICollectionViewCell> m_currentFirstCellWithCornerRadiusInSection = new();

    private void SetContentInset()
    {
        var bottomPadding = mauiCollectionView.HasAdditionalSpaceAtTheEnd ? (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.Frame.Height / 2 : (nfloat)mauiCollectionView.Padding.Bottom + CollectionView.ContentInset.Bottom;
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

        TrySetMarginOnCell(cell, mauiCollectionView);
        TrySetCornerRadiusOnCell(collectionView, indexPath, cell, mauiCollectionView, m_currentLastCellWithCornerRadiusInSection, m_currentFirstCellWithCornerRadiusInSection);
        TrySetDividerInvisible(collectionView, indexPath, cell, mauiCollectionView, m_currentDividerSetToInvisibleInSection);

        return cell;
    }

    internal static void TrySetDividerInvisible(UICollectionView collectionView, NSIndexPath indexPath,
        UICollectionViewCell cell, CollectionView mauiCollectionView,
        Dictionary<int, Divider> currentDividerSetToInvisibleInSection)
    {
        var autoDividerVisibilityEnabled = Effects.Layout.Layout.GetAutoHideLastDivider(mauiCollectionView);
        if(!autoDividerVisibilityEnabled)
            return;
        
        Divider? divider = null;
        cell.BreadthFirstSearchChildrenUntilMatch(view =>
        {
            if (view is not ContentView { CrossPlatformLayout: Divider crossPlatformLayout })
                return false;

            divider = crossPlatformLayout;
            return true;
        });

        if(divider is null)
            return;
        
        if (IsLastItemInSection(collectionView, indexPath))
        {
            if(currentDividerSetToInvisibleInSection.TryGetValue(indexPath.Section, out var value))
                value.IsVisible = true;
            divider.IsVisible = false;
            currentDividerSetToInvisibleInSection[indexPath.Section] = divider;
        }
        else
        {
            // Reset the divider for all other cells, because of virtualization
            divider.IsVisible = true;
        }
    }

    internal static void TrySetMarginOnCell(UICollectionViewCell cell, CollectionView collectionView)
    {
        if(collectionView.Padding.HorizontalThickness == 0)
            return;
        
        var directSubViewUnderCellThatContainsCrossPlatformView = cell.Subviews[1];
        UpdateConstraintWithConstant(directSubViewUnderCellThatContainsCrossPlatformView, NSLayoutAttribute.Trailing, (nfloat)(collectionView.Padding.Right));
        UpdateConstraintWithConstant(directSubViewUnderCellThatContainsCrossPlatformView, NSLayoutAttribute.Leading, (nfloat)(-collectionView.Padding.Left));
    }
    
    private static void UpdateConstraintWithConstant(UIView uiView, NSLayoutAttribute attribute, nfloat constant)
    {
        var constraint = uiView.Constraints.FirstOrDefault(constraint => constraint.FirstAttribute == attribute);
        if (constraint is not null)
            constraint.Constant = constant;
    }

    internal static void TrySetCornerRadiusOnCell(UICollectionView collectionView, NSIndexPath indexPath, UICollectionViewCell cell, CollectionView mauiCollectionView, Dictionary<int, UICollectionViewCell> currentLastCellWithCornerRadiusInSection, Dictionary<int, UICollectionViewCell> currentFirstCellWithCornerRadiusInSection, bool useCache = true)
    {
        if(mauiCollectionView.LastItemCornerRadius.IsEmpty() && mauiCollectionView.FirstItemCornerRadius.IsEmpty() && !mauiCollectionView.AutoCornerRadius)
        {
            return;
        }
        
        var viewThatReceivedMarginUnderCell = cell.Subviews[1].Subviews[0];

        var cornerRadius = new CornerRadius();
        
        if ((!mauiCollectionView.FirstItemCornerRadius.IsEmpty() || mauiCollectionView.AutoCornerRadius) && IsFirstItemInSection(indexPath))
        {
            if (currentFirstCellWithCornerRadiusInSection.Remove(indexPath.Section, out var firstCell))
            {
                var newIndexPath = NSIndexPath.FromRowSection(indexPath.Row + 1, indexPath.Section);
                TrySetCornerRadiusOnCell(collectionView, newIndexPath, firstCell, mauiCollectionView, currentLastCellWithCornerRadiusInSection, currentFirstCellWithCornerRadiusInSection, false);
            }
            
            // Apply corner radius to the cell if its the first cell
            cornerRadius = mauiCollectionView.FirstItemCornerRadius.IsEmpty()
                ? new CornerRadius(Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small), 0, 0)
                : mauiCollectionView.FirstItemCornerRadius;
            
            if(useCache)
                currentFirstCellWithCornerRadiusInSection[indexPath.Section] = cell;
        }
        
        if ((!mauiCollectionView.LastItemCornerRadius.IsEmpty() || mauiCollectionView.AutoCornerRadius) && IsLastItemInSection(collectionView, indexPath))
        {
            if (currentLastCellWithCornerRadiusInSection.Remove(indexPath.Section, out var lastCell))
            {
                var newIndexPath = NSIndexPath.FromRowSection(indexPath.Row - 1, indexPath.Section);
                TrySetCornerRadiusOnCell(collectionView, newIndexPath, lastCell, mauiCollectionView, currentLastCellWithCornerRadiusInSection, currentFirstCellWithCornerRadiusInSection, false);
            }
            // Apply corner radius to the cell if its the last cell
            cornerRadius = mauiCollectionView.LastItemCornerRadius.IsEmpty()
                ? new CornerRadius(cornerRadius.TopLeft, cornerRadius.TopRight, Sizes.GetSize(SizeName.radius_small), Sizes.GetSize(SizeName.radius_small))
                : mauiCollectionView.LastItemCornerRadius;
            
            if(useCache)
                currentLastCellWithCornerRadiusInSection[indexPath.Section] = cell;
        }

        if (!cornerRadius.IsEmpty())
        {
            SetViewCornerRadius(viewThatReceivedMarginUnderCell, cornerRadius);
        }
        else
        {
            // Reset the corner radius for all other cells, because of virtualization
            ResetCornerRadius(viewThatReceivedMarginUnderCell);
        }
    }

    private static void ResetCornerRadius(UIView? test)
    {
        if(test is null)
            return;
        
        test.ClipsToBounds = false;
        test.Layer.CornerRadius = 0;
        test.Layer.MaskedCorners = 0;
    }

    internal static bool IsLastItemInSection(UICollectionView collectionView, NSIndexPath indexPath)
    {
        return indexPath.Row == collectionView.NumberOfItemsInSection(indexPath.Section) - 1;
    }

    internal static bool IsFirstItemInSection(NSIndexPath indexPath)
    {
        return indexPath.Row == 0;
    }

    private static void SetViewCornerRadius(UIView cell, CornerRadius cornerRadius)
    {
        cell.ClipsToBounds = true;
        cell.Layer.CornerRadius = (nfloat)cornerRadius.HighestCornerRadius();
        cell.Layer.MaskedCorners = CACornerMaskHelper.GetCACornerMask(cornerRadius);
    }
    
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        
        m_currentDividerSetToInvisibleInSection.Clear();
        m_currentLastCellWithCornerRadiusInSection.Clear();
        m_currentFirstCellWithCornerRadiusInSection.Clear();
    }
}