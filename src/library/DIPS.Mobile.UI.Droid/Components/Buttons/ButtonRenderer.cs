using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using ButtonRenderer = DIPS.Mobile.UI.Droid.Components.Buttons.ButtonRenderer;

[assembly: ExportRenderer(typeof(Button), typeof(ButtonRenderer))]
namespace DIPS.Mobile.UI.Droid.Components.Buttons
{
    public class ButtonRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}