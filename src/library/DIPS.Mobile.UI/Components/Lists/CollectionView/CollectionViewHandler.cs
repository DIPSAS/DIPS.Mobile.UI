#if __IOS__
using CollectionViewHandlerImpl = Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2;
#else
using CollectionViewHandlerImpl = Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler;
#endif

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler : CollectionViewHandlerImpl
{
    static CollectionViewHandler()
    {
        CollectionViewPropertyMapper.ModifyMapping(
            nameof(Microsoft.Maui.Controls.ItemsView.ItemsSource),
            static (CollectionViewHandler handler, Microsoft.Maui.Controls.CollectionView collectionView, Action<IElementHandler, IElement>? map) =>
            {
                handler.BeforeItemsSourceMapped();
                map?.Invoke(handler, collectionView);
                handler.OnItemsSourceMapped();
            });
    }

    public CollectionViewHandler() : base(CollectionViewPropertyMapper)
    {
    }

    public static readonly PropertyMapper<Microsoft.Maui.Controls.CollectionView, CollectionViewHandler> CollectionViewPropertyMapper = new PropertyMapper<Microsoft.Maui.Controls.CollectionView, CollectionViewHandler>(Mapper)
    {
        [nameof(CollectionView.ShouldBounce)] = MapShouldBounce,
        [nameof(CollectionView.RemoveFocusOnScroll)] = MapRemoveFocusOnScroll
    };

    partial void BeforeItemsSourceMapped();

    partial void OnItemsSourceMapped();
    
    private static partial void MapShouldBounce(CollectionViewHandler handler, Microsoft.Maui.Controls.CollectionView virtualView);
    
    private static partial void MapRemoveFocusOnScroll(CollectionViewHandler handler, Microsoft.Maui.Controls.CollectionView virtualView);

    internal partial void ReloadData(CollectionViewHandler handler);
}