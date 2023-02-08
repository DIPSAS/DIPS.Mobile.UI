using System.Linq;
using Android.Content;
using AndroidX.AppCompat.App;
using DIPS.Mobile.UI.Droid.Components.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]

namespace DIPS.Mobile.UI.Droid.Components.Pages
{
    public class ContentPageRenderer : PageRenderer
    {
        private ContentPage? m_contentPage;

        public ContentPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is ContentPage contentPage)
                {
                    m_contentPage = contentPage;
                }
            }
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();
            m_contentPage?.SendOnContentAppearing();
        }
    }
}