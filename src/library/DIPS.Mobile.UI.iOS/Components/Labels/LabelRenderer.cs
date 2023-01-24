using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using DUILabel = DIPS.Mobile.UI.Components.Labels.Label;
using DUILabelRenderer = DIPS.Mobile.UI.iOS.Components.Labels.LabelRenderer;

[assembly: ExportRenderer(typeof(Label), typeof(DUILabelRenderer))]
namespace DIPS.Mobile.UI.iOS.Components.Labels
{
    public class LabelRenderer : Xamarin.Forms.Platform.iOS.LabelRenderer
    {
    }
}