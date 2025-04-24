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
            return new ReordeableItemsViewController(itemsView, layout, VirtualView as CollectionView);
            
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

internal class ReordeableItemsViewController(ReorderableItemsView reorderableItemsView, ItemsViewLayout layout, CollectionView mauiCollectionView)
    : ReorderableItemsViewController<ReorderableItemsView>(reorderableItemsView, layout)
{
    private readonly Dictionary<int, Divider> m_currentDividerSetToInvisibleInSection = new();
    private readonly Dictionary<int, UICollectionViewCell> m_currentLastCellWithCornerRadiusInSection = new();
    private readonly Dictionary<int, UICollectionViewCell> m_currentFirstCellWithCornerRadiusInSection = new();
    
    public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
    {
        var cell = base.GetCell(collectionView, indexPath);

        TrySetMarginOnCell(cell, mauiCollectionView.Padding);
        TrySetCornerRadiusOnCell(collectionView, indexPath, cell, mauiCollectionView, m_currentLastCellWithCornerRadiusInSection, m_currentFirstCellWithCornerRadiusInSection);
        TrySetDividerInvisible(collectionView, indexPath, cell, mauiCollectionView, m_currentDividerSetToInvisibleInSection);

        return cell;
    }

    internal static void TrySetMarginOnCell(UICollectionViewCell cell, Thickness collectionViewPadding)
    {
        var crossPlatformElement = GetCrossPlatformElementFromCell(cell);

        if (crossPlatformElement is null)
        {
            DUILogService.LogError<CollectionViewHandler>("Could not find cross platform element in cell");
            return;
        }
        
        crossPlatformElement.Margin = new Thickness(collectionViewPadding.Left, 
            crossPlatformElement.Margin.Top, 
            collectionViewPadding.Right, 
            crossPlatformElement.Margin.Bottom);
    }

    private static View? GetCrossPlatformElementFromCell(UICollectionViewCell cell)
    {
        var subView = cell.Subviews[1].Subviews[0];
        if (subView is LayoutView layoutView)
        {
            return (View?)layoutView.CrossPlatformLayout;
        }

        return null;
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

    internal static void TrySetCornerRadiusOnCell(UICollectionView collectionView, NSIndexPath indexPath, UICollectionViewCell cell, CollectionView mauiCollectionView, Dictionary<int, UICollectionViewCell> currentLastCellWithCornerRadiusInSection, Dictionary<int, UICollectionViewCell> currentFirstCellWithCornerRadiusInSection, bool useCache = true)
    {
        if(mauiCollectionView.LastItemCornerRadius.IsEmpty() && mauiCollectionView.FirstItemCornerRadius.IsEmpty() && !mauiCollectionView.AutoCornerRadius)
        {
            return;
        }
        
        var viewThatHasCrossPlatformElement = cell.Subviews[1].Subviews[0];
        // Uncomment to easier debug what cell you are currently debugging
        /*var debugText = viewThatReceivedMarginUnderCell.FindChildView<MauiLabel>().Text;*/
        var cornerRadius = new CornerRadius();
        
        if ((!mauiCollectionView.FirstItemCornerRadius.IsEmpty() || mauiCollectionView.AutoCornerRadius) && IsFirstItemInSection(indexPath))
        {
            if(currentFirstCellWithCornerRadiusInSection.TryGetValue(indexPath.Section, out var currentFirstCell))
            {
                if (!currentFirstCell.Equals(cell) && currentFirstCellWithCornerRadiusInSection.Remove(indexPath.Section))
                {
                    var newIndexPath = NSIndexPath.FromRowSection(indexPath.Row + 1, indexPath.Section);
                    TrySetCornerRadiusOnCell(collectionView, newIndexPath, currentFirstCell, mauiCollectionView, currentLastCellWithCornerRadiusInSection, currentFirstCellWithCornerRadiusInSection, false);
                }
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
            if (currentLastCellWithCornerRadiusInSection.TryGetValue(indexPath.Section, out var currentLastCell))
            {
                if (!currentLastCell.Equals(cell) && currentLastCellWithCornerRadiusInSection.Remove(indexPath.Section))
                {
                    var newIndexPath = NSIndexPath.FromRowSection(indexPath.Row - 1, indexPath.Section);
                    TrySetCornerRadiusOnCell(collectionView, newIndexPath, currentLastCell, mauiCollectionView, currentLastCellWithCornerRadiusInSection, currentFirstCellWithCornerRadiusInSection, false);
                }
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
            SetViewCornerRadius(viewThatHasCrossPlatformElement, cornerRadius);
        }
        else
        {
            // Reset the corner radius for all other cells, because of virtualization
            ResetCornerRadius(viewThatHasCrossPlatformElement);
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