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
        /// Pushes new content onto the bottom sheet's internal navigation stack.
        /// The content is displayed with an animated transition and a back button to return.
        /// </summary>
        /// <param name="content">The view to display.</param>
        /// <param name="title">The title displayed in the navigation bar for the pushed content.</param>
        public async Task PushAsync(View content, string? title = null)
        {
            var entry = new BottomSheetNavigationEntry(content, title);
            NavigationStack.Push(entry);
            try
            {
                await PlatformPushAsync(content, title);
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
        /// Called by platform code when the user interactively pops (e.g. iOS swipe-back gesture).
        /// Keeps the managed navigation stack in sync.
        /// </summary>
        internal void HandleInteractivePop(View content)
        {
            if (NavigationStack.TryPeek(out var top) && top.Content == content)
            {
                NavigationStack.Pop();
            }
        }

        private partial Task PlatformPushAsync(View content, string? title);
        private partial Task PlatformPopAsync(BottomSheetNavigationEntry popped);
    }
}

