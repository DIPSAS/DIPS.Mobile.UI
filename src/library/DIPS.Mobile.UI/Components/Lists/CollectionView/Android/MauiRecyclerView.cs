using Android.Content;
using Microsoft.Maui.Controls.Handlers.Items;

namespace DIPS.Mobile.UI.Components.Lists.Android;

public class MauiRecyclerView : MauiRecyclerView<ReorderableItemsView,
    GroupableItemsViewAdapter<ReorderableItemsView, IGroupableItemsViewSource>, IGroupableItemsViewSource>
{
    public MauiRecyclerView(Context context, Func<IItemsLayout> getItemsLayout,
        Func<GroupableItemsViewAdapter<ReorderableItemsView, IGroupableItemsViewSource>> getAdapter) : base(context,
        getItemsLayout, getAdapter)
    {
    }

    protected override void UpdateItemSpacing()
    {
        base.UpdateItemSpacing();

        // MAUI tries to set negative padding so that it seems like the first and last elements doesn't have padding, however,
        // this creates a bug where recyclerview tries to snap to elements
        // https://github.com/dotnet/maui/blob/ace9fe5e7d8d9bd16a2ae0b2fe2b888ad681433e/src/Controls/src/Core/Handlers/Items/Android/MauiRecyclerView.cs#L459C10-L459C10
        SetPadding(0, 0, 0, 0);
    }
}