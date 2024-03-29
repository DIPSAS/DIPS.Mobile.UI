namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler : Microsoft.Maui.Controls.Handlers.Items.CollectionViewHandler
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