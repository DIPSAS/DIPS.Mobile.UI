using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Extensions.Android;
using Rect = Android.Graphics.Rect;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Lists.Android;

internal class AdditionalEndSpaceDecoration : RecyclerView.ItemDecoration
{
    private readonly CollectionView m_collectionView;

    public AdditionalEndSpaceDecoration(ReorderableItemsView collectionView)
    {
        m_collectionView = (CollectionView)collectionView;
    }
    
    public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
    {
        base.GetItemOffsets(outRect, view, parent, state);
        
        var dataSize = state.ItemCount;
        var position = parent.GetChildAdapterPosition(view);
        if (dataSize > 0 && position == dataSize - 1) {
            var paddingBottom = (int)(m_collectionView.HasAdditionalSpaceAtTheEnd ? m_collectionView.Padding.Bottom.ToMauiPixel() + (parent.Height / 2) : (int)m_collectionView.Padding.Bottom.ToMauiPixel());
            outRect.Set(0, 0, 0, paddingBottom);
        } else {
            outRect.Set(0, 0, 0, 0);
        }
    }
}