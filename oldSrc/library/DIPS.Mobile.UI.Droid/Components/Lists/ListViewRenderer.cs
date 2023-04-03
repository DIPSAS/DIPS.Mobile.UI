using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Widget;
using Com.Google.Android.Material.Divider;
using DIPS.Mobile.UI.Droid.Extensions;
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
        private ListView? m_listView;

        public ListViewRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ListView> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is ListView listView)
                {
                    m_listView = listView;
                    UpdateBackgroundColor();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(ListView.CornerRadius):
                case nameof(VisualElement.BackgroundColor):
                    UpdateBackgroundColor();
                    break;
            }
        }

        public void UpdateBackgroundColor()
        {
            if (m_listView == null) return;
            
            Control.SetRoundedRectangularBackground(m_listView.CornerRadius, m_listView.BackgroundColor);
        }
    }
}