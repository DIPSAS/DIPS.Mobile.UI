using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Controls.Handlers.Items;
using Rect = Android.Graphics.Rect;
using View = Android.Views.View;

namespace DIPS.Mobile.UI.Components.Lists.Android;

internal class CellMarginDecoration : RecyclerView.ItemDecoration
{
    public override void GetItemOffsets(Rect outRect, View view, RecyclerView parent, RecyclerView.State state)
    {
        base.GetItemOffsets(outRect, view, parent, state);

        var position = parent.GetChildAdapterPosition(view);

        if(parent.GetAdapter() is not ReorderableItemsViewAdapter adapter)
        {
            return;
        }

        adapter.ModifyMarginIfNotHeaderAndFooter(outRect, position);
    }
}