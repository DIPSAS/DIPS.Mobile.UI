using Android.Content;
using Android.Content.Res;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using DIPS.Mobile.UI.Components.Lists.Android;
using DIPS.Mobile.UI.Effects.Layout;
using DIPS.Mobile.UI.Extensions.Android;
using Microsoft.Maui.Controls.Handlers.Items;
using Color = Android.Graphics.Color;
using CornerRadius = Microsoft.Maui.CornerRadius;
using View = Microsoft.Maui.Controls.View;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler
{
    protected override RecyclerView CreatePlatformView()
    {
        return new MauiRecyclerView(Context, GetItemsLayout, CreateAdapter);
    }

    protected override ReorderableItemsViewAdapter<ReorderableItemsView, IGroupableItemsViewSource> CreateAdapter()
    {
        // Only use new adapter if the ItemsLayout is LinearItemsLayout
        return VirtualView is CollectionView { ItemsLayout: not LinearItemsLayout } ? base.CreateAdapter() : new ReorderableItemsViewAdapter(VirtualView);
    }

    protected override void ConnectHandler(RecyclerView platformView)
    {
        base.ConnectHandler(platformView);
        
        // If the list is horizontal, we don't want to do this
        if(VirtualView is CollectionView { ItemsLayout: not LinearItemsLayout }) 
            return;

        // Pads the RecyclerView elements left and right
        platformView.AddItemDecoration(new CellMarginDecoration(VirtualView));
        // Additional space at the end of the RecyclerView
        platformView.AddItemDecoration(new AdditionalEndSpaceDecoration(VirtualView));
    }

    private static partial void MapShouldBounce(CollectionViewHandler handler,
        Microsoft.Maui.Controls.CollectionView virtualView)
    {
        if (virtualView is CollectionView collectionView)
        {
            handler.PlatformView.OverScrollMode =
                collectionView.ShouldBounce ? OverScrollMode.Always : OverScrollMode.Never;
        }
    }

    internal partial void ReloadData(CollectionViewHandler handler)
    {
        if (handler.PlatformView is not MauiRecyclerView
            recyclerView)
        {
            return;
        }

        var adapter = recyclerView.GetAdapter();
        adapter?.NotifyDataSetChanged();
    }
}

public class ReorderableItemsViewAdapter : ReorderableItemsViewAdapter<ReorderableItemsView, IGroupableItemsViewSource>
{
    private readonly CollectionView m_collectionView;

    public ReorderableItemsViewAdapter(ReorderableItemsView reorderableItemsView, Func<View, Context, ItemContentView> createView = null) : base(reorderableItemsView, createView)
    {
        if(reorderableItemsView is CollectionView collectionView)
            m_collectionView = collectionView;
    }

    public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
    {
        base.OnBindViewHolder(holder, position);

        var (group, index) = ItemsSource.GetGroupAndIndex(position);
        
        IItemsViewSource currentGroup;
        
        try
        {
            currentGroup = ItemsSource.GetGroupItemsViewSource(group);
            
            if(currentGroup is null)
                return;
        }
        catch
        {
            // Can happen if list is refreshed while scrolling
            return;
        }
        
        var cornerRadius = new CornerRadius();

        // On Android, the group header -and footer is actual items in the RecyclerView, so we need to adjust the index
        var firstItemIndex = m_collectionView.GroupHeaderTemplate is null ? 0 : 1;
        var lastItemIndex = m_collectionView.GroupFooterTemplate is null ? currentGroup.Count - 1 : currentGroup.Count - 2;
        
        if ((!m_collectionView.FirstItemCornerRadius.IsEmpty() || m_collectionView.AutoCornerRadius) && index == firstItemIndex)
        {
            cornerRadius = m_collectionView.FirstItemCornerRadius.IsEmpty() ? new CornerRadius(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2), 0, 0) : m_collectionView.FirstItemCornerRadius;
        }
        else if ((!m_collectionView.LastItemCornerRadius.IsEmpty() || m_collectionView.AutoCornerRadius) && index == lastItemIndex)
        {
            cornerRadius = m_collectionView.LastItemCornerRadius.IsEmpty() ? new CornerRadius(0, 0, Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2)) : m_collectionView.LastItemCornerRadius;
        }
        
        if(!cornerRadius.IsEmpty())
            SetCellCornerRadius(holder, cornerRadius);
    }

    private static void SetCellCornerRadius(RecyclerView.ViewHolder holder, CornerRadius cornerRadius)
    {
        var materialShapeDrawable = MaterialShapeDrawableHelper.GetMaterialShapeDrawableFromCornerRadius(cornerRadius);
      
        holder.ItemView.ClipToOutline = true;

        // Removes ugly artifact from corners
        materialShapeDrawable.FillColor = ColorStateList.ValueOf(Color.Transparent);
        
        holder.ItemView.Background = materialShapeDrawable;
    }
}