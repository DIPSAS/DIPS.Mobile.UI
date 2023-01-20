using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DUIListView = DIPS.Mobile.UI.Components.Lists.ListView;
using DUIListViewRenderer = DIPS.Mobile.UI.iOS.Components.Lists.ListViewRenderer;

[assembly: ExportRenderer(typeof(DUIListView), typeof(DUIListViewRenderer))]

namespace DIPS.Mobile.UI.iOS.Components.Lists
{
    public class ListViewRenderer : Xamarin.Forms.Platform.iOS.ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (Element is DUIListView listView)
                {
                    SetSelectionMode(listView);
                }
            }
        }

        private void SetSelectionMode(DUIListView listView)
        {
            if (listView.SelectionMode == (ListViewSelectionMode) SelectionMode.None)
            {
                Control.AllowsSelection = false;
            }
        }
    }
}