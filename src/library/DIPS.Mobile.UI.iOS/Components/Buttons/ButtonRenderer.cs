using DUIButton = DIPS.Mobile.UI.Components.Buttons.Button;
using DUIButtonRenderer = DIPS.Mobile.UI.iOS.Components.Buttons.ButtonRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DUIButton), typeof(DUIButtonRenderer))]
namespace DIPS.Mobile.UI.iOS.Components.Buttons
{
    public class ButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}