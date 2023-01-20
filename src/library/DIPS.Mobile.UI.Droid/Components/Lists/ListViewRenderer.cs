using Android.Content;
using Xamarin.Forms;
using ListView = DIPS.Mobile.UI.Components.Lists.ListView;
using ListViewRenderer = DIPS.Mobile.UI.Droid.Components.Lists.ListViewRenderer;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewRenderer))]

namespace DIPS.Mobile.UI.Droid.Components.Lists
{
    public class ListViewRenderer : Xamarin.Forms.Platform.Android.ListViewRenderer
    {
        public ListViewRenderer(Context context):base(context)
        {
            
        }
    }
}