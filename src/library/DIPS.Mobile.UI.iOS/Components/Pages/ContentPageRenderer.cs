using DIPS.Mobile.UI.iOS.Components.Pages;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DUIContentPage = DIPS.Mobile.UI.Components.Pages.ContentPage;


[assembly: ExportRenderer(typeof(DUIContentPage), typeof(ContentPageRenderer))]
namespace DIPS.Mobile.UI.iOS.Components.Pages
{
    public class ContentPageRenderer : PageRenderer
    {
        private DUIContentPage? m_contentPage;
        private bool IsPartOfShell => (Element?.Parent is BaseShellItem || (
            Element?.Parent is TabbedPage && Element?.Parent?.Parent is BaseShellItem));
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is DUIContentPage contentPage)
                {
                    m_contentPage = contentPage;
                }
            }
        }
        
        
        //Somehow, when in a shell context, this gets ran and nullifies the actual padding set by the content page
        public override void ViewSafeAreaInsetsDidChange()
        {
            
            var window = UIApplication.SharedApplication.KeyWindow;
            if (window != null && m_contentPage != null)
            {
                var shouldUpdatePadding = m_contentPage.SendViewSafeAreaInsetsDidChange(new Thickness(window.SafeAreaInsets.Left,
                    window.SafeAreaInsets.Top, window.SafeAreaInsets.Right, window.SafeAreaInsets.Bottom));

                if (shouldUpdatePadding)
                {
                    base.ViewSafeAreaInsetsDidChange();    
                }
            }
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            m_contentPage?.SendOnContentAppearing();
        }
    }
}