namespace DIPS.Mobile.UI.Components.BottomSheets
{
    /// <summary>
    /// The bottom sheet service used to display a bottom sheet for people to see.
    /// </summary>
    public static partial class BottomSheetService
    {
        public static BottomSheet? LatestBottomSheet { get; set; } 

        [Obsolete("Will get removed in future releases, do use Open instead")]
        public static Task OpenBottomSheet(BottomSheet bottomSheet) => Open(bottomSheet);

        /// <summary>
        /// Presents a bottom sheet for people to see.
        /// </summary>
        /// <param name="bottomSheet">The view to display inside the bottom sheet.</param>
        /// <returns></returns>
        public async static Task Open(BottomSheet bottomSheet)
        {
            if (IsBottomSheetOpen())
            {
                await CloseCurrentBottomSheet(true);
            }

            LatestBottomSheet = bottomSheet;
            await PlatformOpen(bottomSheet);
        }

        /// <summary>
        /// Closes the current presented bottom sheet.
        /// </summary>
        /// <returns></returns>
        public static Task CloseCurrentBottomSheet(bool animated=true)
        {
            return LatestBottomSheet != null ? Close(LatestBottomSheet, animated) : Task.CompletedTask;
        }

        public static bool IsBottomSheetOpen()
        {
            return LatestBottomSheet != null;
        }

        public static partial Task Close(BottomSheet bottomSheet, bool animated = true);
        internal static partial Task PlatformOpen(BottomSheet bottomSheet);
    }
}