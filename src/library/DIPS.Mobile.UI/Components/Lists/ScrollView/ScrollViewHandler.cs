namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollViewHandler : Microsoft.Maui.Handlers.ScrollViewHandler
{
    public ScrollViewHandler() : base(ScrollViewPropertyMapper)
    {
    }

    public static readonly PropertyMapper ScrollViewPropertyMapper = new PropertyMapper<Microsoft.Maui.Controls.ScrollView, ScrollViewHandler>(Mapper)
    {
        [nameof(CollectionView.ShouldBounce)] = MapShouldBounce
    };
    
    private static partial void MapShouldBounce(ScrollViewHandler handler, Microsoft.Maui.Controls.ScrollView virtualView);
}