namespace DIPS.Mobile.UI.Components.BottomSheets
{
    /// <summary>
    /// Håndterer intern navigasjon i bottom sheet (push/pop av innhold).
    /// </summary>
    public partial class BottomSheet
    {
        internal Stack<BottomSheetNavigationEntry> NavigationStack { get; } = new();

        /// <summary>
        /// Om det finnes innhold som kan poppes fra navigasjonsstacken.
        /// </summary>
        public bool CanPopNavigation => NavigationStack.Count > 0;

        /// <summary>
        /// Pusher nytt innhold på bottom sheet sin interne navigasjonsstack.
        /// Innholdet vises med en animert overgang og en tilbake-knapp for å returnere.
        /// </summary>
        /// <param name="content">Viewet som skal vises.</param>
        /// <param name="title">Tittelen som vises i navigasjonsbaren for det pushede innholdet.</param>
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
        /// Popper gjeldende innhold fra bottom sheet sin interne navigasjonsstack og returnerer til forrige innhold.
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
        /// Kalles av plattformkode når brukeren interaktivt popper (f.eks. iOS swipe-back-gest).
        /// Holder den styrte navigasjonsstacken synkronisert.
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

