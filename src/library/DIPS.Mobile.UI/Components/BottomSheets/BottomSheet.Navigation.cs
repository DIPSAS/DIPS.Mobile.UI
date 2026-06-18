namespace DIPS.Mobile.UI.Components.BottomSheets
{
    /// <summary>
    /// Handles internal navigation in the bottom sheet (push/pop of content).
    /// </summary>
    public partial class BottomSheet
    {
        internal Stack<BottomSheetNavigationEntry> NavigationStack { get; } = new();

        /// <summary>
        /// Whether there is content that can be popped from the navigation stack.
        /// </summary>
        public bool CanPopNavigation => NavigationStack.Count > 0;

        /// <summary>
        /// Pushes a ContentPage onto the bottom sheet's internal navigation stack.
        /// The page's Content is displayed with an animated transition and a back button to return.
        /// The page's Title is used in the navigation bar.
        /// </summary>
        /// <param name="page">The page to push.</param>
        public async Task PushAsync(ContentPage page)
        {
            var entry = new BottomSheetNavigationEntry(page);
            NavigationStack.Push(entry);
            try
            {
                await PlatformPushAsync(page);
            }
            catch
            {
                NavigationStack.Pop();
                throw;
            }
        }

        /// <summary>
        /// Pops the current content from the bottom sheet's internal navigation stack and returns to the previous content.
        /// </summary>
        public async Task PopAsync()
        {
            if (NavigationStack.Count == 0) return;
            var popped = NavigationStack.Pop();
            try
            {
                await PlatformPopAsync(popped);
            }
            catch
            {
                NavigationStack.Push(popped);
                throw;
            }
        }

        /// <summary>
        /// Pops all content from the bottom sheet's internal navigation stack and returns to the root content with a single animated transition.
        /// Does nothing if the stack is already at the root.
        /// </summary>
        public async Task PopToRootAsync()
        {
            if (NavigationStack.Count == 0) return;

            // Snapshot in pop-order (top first). Stack.ToArray() already returns top-to-bottom.
            var popped = NavigationStack.ToArray();
            NavigationStack.Clear();
            try
            {
                await PlatformPopToRootAsync(popped);
            }
            catch
            {
                // Restore stack in original order (bottom-to-top)
                for (var i = popped.Length - 1; i >= 0; i--)
                {
                    NavigationStack.Push(popped[i]);
                }
                throw;
            }
        }

        /// <summary>
        /// Called by platform code when the user interactively pops (e.g. iOS swipe-back gesture).
        /// Keeps the managed navigation stack in sync.
        /// </summary>
        internal void HandleInteractivePop(ContentPage page)
        {
            if (NavigationStack.TryPeek(out var top) && top.Page == page)
            {
                NavigationStack.Pop();
            }
        }

        private partial Task PlatformPushAsync(ContentPage page);
        private partial Task PlatformPopAsync(BottomSheetNavigationEntry popped);
        private partial Task PlatformPopToRootAsync(IReadOnlyList<BottomSheetNavigationEntry> popped);
    }
}

