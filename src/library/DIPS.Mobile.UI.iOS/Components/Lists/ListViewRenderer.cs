using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ListView = DIPS.Mobile.UI.Components.Lists.ListView;
using ListViewRenderer = DIPS.Mobile.UI.iOS.Components.Lists.ListViewRenderer;

[assembly: ExportRenderer(typeof(ListView), typeof(ListViewRenderer))]

namespace DIPS.Mobile.UI.iOS.Components.Lists
{
    public class ListViewRenderer : Xamarin.Forms.Platform.iOS.ListViewRenderer
    {
        public ListViewRenderer()
        {
            
        }
        
        
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
        }
    }
}