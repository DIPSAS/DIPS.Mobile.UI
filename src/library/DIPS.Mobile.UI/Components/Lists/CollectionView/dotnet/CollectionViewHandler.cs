

using DIPS.Mobile.UI.Exceptions;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionViewHandler
{
    private static partial void MapShouldBounce(CollectionViewHandler handler,
        Microsoft.Maui.Controls.CollectionView virtualView) =>
        throw new Only_Here_For_UnitTests();
}