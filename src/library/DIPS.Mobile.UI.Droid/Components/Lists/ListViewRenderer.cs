using Android.Content;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = DIPS.Mobile.UI.Components.Lists.ListView;
using ListViewRenderer = DIPS.Mobile.UI.Droid.Components.Lists.ListViewRenderer;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewRenderer))]

namespace DIPS.Mobile.UI.Droid.Components.Lists
{
    public class ListViewRenderer : Xamarin.Forms.Platform.Android.ListViewRenderer
    {
        private ListView m_listView;

        public ListViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is ListView listView)
                {
                    m_listView = listView;
                }
            }
        }
        private bool CanScrollVertically () {
            var canScroll = false;

            if (Control !=null && Control.ChildCount > 0) {
                var isOnTop = Control.FirstVisiblePosition != 0 || Control.GetChildAt(0).Top != 0;
                var isAllItemsVisible = isOnTop && Control.LastVisiblePosition == Control.ChildCount;

                if (isOnTop || isAllItemsVisible) {
                    canScroll = true;
                }
            }

            return  canScroll;
        }
    }
}