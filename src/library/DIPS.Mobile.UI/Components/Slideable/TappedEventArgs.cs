namespace DIPS.Mobile.UI.Components.Slideable
{
    /// <summary>
    ///     Used in <see cref="SlidableLayout.PanEnded" /> and <see cref="SlidableLayout.PanStarted" />
    /// </summary>
    /// <param name="currentIndex"></param>
    public class TappedEventArgs : EventArgs
    {
        /// <summary>
        /// </summary>
        /// <param name="currentIndex"></param>
        public TappedEventArgs(int currentIndex)
        {
            CurrentIndex = currentIndex;
        }

        /// <summary>
        ///     Current index when pan event occurs.
        /// </summary>
        public int CurrentIndex { get; }
    }
}