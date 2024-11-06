namespace DIPS.Mobile.UI.Components.Lists;

#if __IOS__
using CollectionViewHandlerImpl = Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2;
#else
using CollectionViewHandlerImpl = Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler;
#endif

public partial class CollectionViewHandler : CollectionViewHandlerImpl
{
    public CollectionViewHandler() : base(CollectionViewPropertyMapper)
    {
    }

    public static readonly PropertyMapper CollectionViewPropertyMapper = new PropertyMapper<Microsoft.Maui.Controls.CollectionView, CollectionViewHandler>(Mapper)
    {
        [nameof(CollectionView.ShouldBounce)] = MapShouldBounce
    };

    private static partial void MapShouldBounce(CollectionViewHandler handler, Microsoft.Maui.Controls.CollectionView virtualView);

    internal partial void ReloadData(CollectionViewHandler handler);
}