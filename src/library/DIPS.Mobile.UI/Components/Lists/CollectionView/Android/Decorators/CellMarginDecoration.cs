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
        
        // We have only implemented uniform horizontal padding on iOS, so we do the same for Android 
        var horizontalPadding = 0;
        if(m_collectionView.Padding.Left >= m_collectionView.Padding.Right)
        {
            horizontalPadding = (int)m_collectionView.Padding.Left * 2;
        }
        else if(m_collectionView.Padding.Right > m_collectionView.Padding.Left)
        {
            horizontalPadding = (int)m_collectionView.Padding.Right * 2;
        }
        
        outRect.Left = (horizontalPadding / 2).ToMauiPixel();
        outRect.Right = (horizontalPadding / 2).ToMauiPixel();
    }
}