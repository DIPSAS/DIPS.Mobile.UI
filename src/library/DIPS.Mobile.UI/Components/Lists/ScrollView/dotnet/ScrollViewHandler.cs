using DIPS.Mobile.UI.Exceptions;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollViewHandler
{
    private static partial void MapShouldBounce(ScrollViewHandler handler,
        Microsoft.Maui.Controls.ScrollView virtualView) =>
        throw new Only_Here_For_UnitTests();
}