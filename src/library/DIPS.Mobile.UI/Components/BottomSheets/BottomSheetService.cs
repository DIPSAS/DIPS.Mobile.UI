namespace DIPS.Mobile.UI.Components.BottomSheets
{
    /// <summary>
    /// The bottom sheet service used to display a bottom sheet for people to see.
    /// </summary>
    public static partial class BottomSheetService
    {
        /// <summary>
        /// Presents a bottom sheet for people to see.
        /// </summary>
        /// <param name="bottomSheet">The view to display inside the bottom sheet.</param>
        /// <returns></returns>
        public static partial Task OpenBottomSheet(BottomSheet bottomSheet);

        /// <summary>
        /// Closes the current presented bottom sheet.
        /// </summary>
        /// <returns></returns>
        public static partial Task CloseCurrentBottomSheet(bool animated=true);

        public static partial bool IsBottomSheetOpen();
    }
}