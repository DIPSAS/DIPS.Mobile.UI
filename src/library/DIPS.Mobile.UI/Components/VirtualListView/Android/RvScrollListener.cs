using AndroidX.RecyclerView.Widget;

namespace DIPS.Mobile.UI.Components.VirtualListView;

public partial class VirtualListViewHandler
{
    class RvScrollListener : RecyclerView.OnScrollListener
    {
        private bool m_haveScrolled;
        
        public RvScrollListener(Action<RecyclerView, int, int> scrollHandler)
        {
            ScrollHandler = scrollHandler;
        }

        Action<RecyclerView, int, int> ScrollHandler { get; }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            // This function gets called when GlobalHeader, ItemTemplate etc. is drawn, so we have to check here if the user has scrolled, if not, never invoke the ScrollHandler
            if (dx != 0 || dy != 0)
            {
                m_haveScrolled = true;
            }
            
            if(m_haveScrolled)
                ScrollHandler?.Invoke(recyclerView, dx, dy);
        }
    }
}