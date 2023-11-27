using DIPS.Mobile.UI.Exceptions;

namespace DIPS.Mobile.UI.Components.BottomSheets
{
    /// <summary>
    /// The bottom sheet service used to display a bottom sheet for people to see.
    /// </summary>
    public static partial class BottomSheetService
    {
        internal static partial Task PlatformOpen(BottomSheet bottomSheet) => throw new Only_Here_For_UnitTests();

        public static partial Task Close(BottomSheet bottomSheet, bool animated) =>
            throw new Only_Here_For_UnitTests();
    }
}