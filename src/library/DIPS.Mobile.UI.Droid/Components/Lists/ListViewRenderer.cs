using Android.Content;
using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
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
        
        
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                Control.SetBottomEdgeEffectColor(Colors.GetColor(ColorName.color_primary_light_primary_80).ToAndroid());
            }
        }
    }
}