using AndroidX.RecyclerView.Widget;

namespace DIPS.Mobile.UI.Components.VirtualListView;

public partial class VirtualListViewHandler
{
    class RvScrollListener : RecyclerView.OnScrollListener
    {
        private bool m_firstScroll = true;
        
        public RvScrollListener(Action<RecyclerView, int, int> scrollHandler)
        {
            ScrollHandler = scrollHandler;
        }

        Action<RecyclerView, int, int> ScrollHandler { get; }

        public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
        {
            base.OnScrolled(recyclerView, dx, dy);

            // This is a workaround for the first scroll event that is triggered when the view is created
            if(m_firstScroll)
            {
                m_firstScroll = false;
                return;
            }
            
            ScrollHandler?.Invoke(recyclerView, dx, dy);
        }
    }
}