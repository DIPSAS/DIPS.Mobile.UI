namespace DIPS.Mobile.UI.Components.BottomSheets
{
    /// <summary>
    /// The bottom sheet service used to display a bottom sheet for people to see.
    /// </summary>
    public static partial class BottomSheetService
    {
        public static List<BottomSheet>? BottomSheetStack { get; internal set; }

        /// <summary>
        /// Presents a bottom sheet for people to see.
        /// </summary>
        /// <param name="bottomSheet">The view to display inside the bottom sheet.</param>
        /// <returns></returns>
        public static Task Open(BottomSheet bottomSheet)
        {
            BottomSheetStack ??= [];
            BottomSheetStack.Add(bottomSheet);
            return PlatformOpen(bottomSheet);
        }

        /// <summary>
        /// Close the presentation of the all bottom sheets in the <see cref="BottomSheetStack"/>.
        /// </summary>
        /// <returns></returns>
        public static async Task CloseAll(bool animated = true)
        {
            if (BottomSheetStack == null) return;

            //Make a copy and reverse it.
            //If its not reversed, it wont work on iOS because of sheets being presented on top of each other.
            //If its not a copy, exception will be thrown because we remove from the stack during iterations of the stack.
            var stack = BottomSheetStack.ToArray().Reverse();
            foreach (var bottomSheet in stack)
            {
                await bottomSheet.Close(animated);
            }
        }

        /// <summary>
        /// Determines if any bottom sheets is open.
        /// </summary>
        /// <returns></returns>
        public static bool IsOpen()
        {
            return BottomSheetStack?.Count > 0;
        }

        /// <summary>
        /// Close the presentation of the the bottom sheet.
        /// </summary>
        /// <param name="bottomSheet">The bottom sheet to close</param>
        /// <param name="animated">Determines if the bottom sheet should animate when its closing.</param>
        /// <returns></returns>
        public static partial Task Close(BottomSheet bottomSheet, bool animated = true);

        /// <summary>
        /// Sets the positioning of the last opened BottomSheet
        /// </summary>
        /// <returns>returns false if there is no BottomSheet opened</returns>
        public static bool TrySetPositionOfLastOpenedBottomSheet(Positioning positioning, out Positioning fromPosition)
        {
            fromPosition = positioning;
            
            var lastBottomSheetOpened = BottomSheetStack?.LastOrDefault();
            
            if(lastBottomSheetOpened is null)
                return false;

            if (lastBottomSheetOpened.Positioning == positioning)
            {
                return true;
            }

            fromPosition = lastBottomSheetOpened.Positioning;
            lastBottomSheetOpened.Positioning = positioning;
            return true;
        }
        
        internal static partial Task PlatformOpen(BottomSheet bottomSheet);
        internal static void RemoveFromStack(BottomSheet bottomSheet) => BottomSheetStack?.Remove(bottomSheet);
    }
}