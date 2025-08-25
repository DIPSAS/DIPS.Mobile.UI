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
        public static async Task Open(BottomSheet bottomSheet)
        {
            BottomSheetStack ??= [];
            BottomSheetStack.Add(bottomSheet);
            
            // Log the stack
            for (var i = 0; i < BottomSheetStack.Count; i++)
            {
                Console.WriteLine($"[{i}] {BottomSheetStack[i].GetType().Name}");
            }

            var shouldFocusSearchBarOnOpen = bottomSheet is { HasSearchBar: true, ShouldAutoFocusSearchBar: true };
            if (shouldFocusSearchBarOnOpen)
            {
                bottomSheet.Positioning = Positioning.Large;
            }
            
            await PlatformOpen(bottomSheet);

            if (shouldFocusSearchBarOnOpen)
            {
                await Task.Delay(1);
                bottomSheet.SearchBar.Focus();
            }
        }

        /// <summary>
        /// Close the presentation of the all bottom sheets in the <see cref="BottomSheetStack"/>.
        /// </summary>
        /// <returns></returns>
        public static async Task CloseAll(bool animated = true)
        {
            if (BottomSheetStack == null) return;

            // Log the stack
            for (var i = 0; i < BottomSheetStack.Count; i++)
            {
                var bottomSheet = BottomSheetStack[i];
                Console.WriteLine($"[{i}] {bottomSheet.GetType().Name}");
            }
            
            // Log the reversed stack
            var reversedStack = BottomSheetStack.ToList();
            reversedStack.Reverse();
            for (var i = 0; i < BottomSheetStack.Count; i++)
            {
                var bottomSheet = reversedStack[i];
                Console.WriteLine($"[{i}] {bottomSheet.GetType().Name}");
            }
            
            //Make a copy and reverse it.
            //If its not reversed, it wont work on iOS because of sheets being presented on top of each other.
            //If its not a copy, exception will be thrown because we remove from the stack during iterations of the stack.
            var stack = BottomSheetStack.ToArray().Reverse();
            foreach (var bottomSheet in stack)
            {
                Console.WriteLine($"Closing bottom sheet: {bottomSheet.GetType().Name}");
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

        internal static partial Task PlatformOpen(BottomSheet bottomSheet);
        internal static void RemoveFromStack(BottomSheet bottomSheet) 
        {
            BottomSheetStack?.Remove(bottomSheet);
            for (var i = 0; i < BottomSheetStack?.Count; i++)
            {
                Console.WriteLine($"[{i}] {BottomSheetStack[i].GetType().Name}");
            }
        } 
    }
}