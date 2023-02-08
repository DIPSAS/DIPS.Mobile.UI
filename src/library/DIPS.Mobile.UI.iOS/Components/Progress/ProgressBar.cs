using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using ProgressBar = DIPS.Mobile.UI.Components.Progress.ProgressBar;
using ProgressBarRenderer = DIPS.Mobile.UI.iOS.Components.Progress.ProgressBarRenderer;

[assembly: ExportRenderer(typeof(ProgressBar), typeof(ProgressBarRenderer))]

namespace DIPS.Mobile.UI.iOS.Components.Progress
{
    public class ProgressBarRenderer : Xamarin.Forms.Platform.iOS.ProgressBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ProgressBar> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is ProgressBar progressBar)
                {
                
                }
            }
        }
    }   
}