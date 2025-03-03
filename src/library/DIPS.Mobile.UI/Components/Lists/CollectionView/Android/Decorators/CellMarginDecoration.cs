using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Extensions.Android;
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
        
        outRect.Left = (int)m_collectionView.Padding.Left.ToMauiPixel()!;
        outRect.Right = (int)m_collectionView.Padding.Right.ToMauiPixel()!;
    }
}