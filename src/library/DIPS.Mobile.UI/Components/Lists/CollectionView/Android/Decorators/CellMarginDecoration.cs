using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Controls.Handlers.Items;
using Rect = Android.Graphics.Rect;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Lists.Android;

internal class CellMarginDecoration : RecyclerView.ItemDecoration
{
    private readonly CollectionView m_collectionView;

    public CellMarginDecoration(ReorderableItemsView collectionView)
    {
        m_collectionView = (CollectionView)collectionView;
    }

    public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
    {
        base.GetItemOffsets(outRect, view, parent, state);

        var position = parent.GetChildAdapterPosition(view);

        if(parent.GetAdapter() is not ReorderableItemsViewAdapter adapter)
        {
            return;
        }
        
        if (!adapter.TryGetGroupAndGroupIndex(position, out var indexInGroup, out var group))
        {
            return;   
        }
        
        // Header is an element in the list
        if(m_collectionView.HeaderTemplate is not null && position == 0)
        {
            return;
        }
        
        if(m_collectionView.FooterTemplate is not null && position == parent.GetAdapter().ItemCount - 1)
        {
            return;
        }
        
        if(group.IsHeader(indexInGroup))
        {
            return;
        }
        
        if(group.IsFooter(indexInGroup))
        {
            return;
        }
        
        outRect.Left = (int)m_collectionView.Padding.Left.ToMauiPixel();
        outRect.Right = (int)m_collectionView.Padding.Right.ToMauiPixel();
    }
}