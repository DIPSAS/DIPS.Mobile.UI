using AndroidX.RecyclerView.Widget;

namespace DIPS.Mobile.UI.Components.Lists.Android;

public class CollectionViewHandler : Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler
{
    protected override RecyclerView CreatePlatformView()
    {
        return new MauiRecyclerView(Context, GetItemsLayout, CreateAdapter);
    }
}