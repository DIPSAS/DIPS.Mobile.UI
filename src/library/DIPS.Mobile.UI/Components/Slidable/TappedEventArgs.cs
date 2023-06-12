namespace DIPS.Mobile.UI.Components.Slidable
{
    public class TappedEventArgs : EventArgs
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">The index relative to the current index that is in the center</param>
        public TappedEventArgs(int index)
        {
            Index = index;
        }

        public int AbsoluteIndex { get; }

        /// <summary>
        ///     Current index when pan event occurs.
        /// </summary>
        public int Index { get; }
    }
}