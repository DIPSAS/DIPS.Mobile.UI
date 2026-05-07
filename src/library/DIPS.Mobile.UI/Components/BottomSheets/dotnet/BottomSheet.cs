using DIPS.Mobile.UI.Exceptions;

namespace DIPS.Mobile.UI.Components.BottomSheets;

public partial class BottomSheet
{
    private partial Task PlatformPushAsync(View content, string? title) =>
        throw new Only_Here_For_UnitTests();

    private partial Task PlatformPopAsync(BottomSheetNavigationEntry popped) =>
        throw new Only_Here_For_UnitTests();
}
