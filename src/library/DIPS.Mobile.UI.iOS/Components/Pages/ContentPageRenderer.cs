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
    }
}