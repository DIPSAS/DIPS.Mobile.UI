using Android.Content;
using DIPS.Mobile.UI.Components.Progress;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ProgressBar = DIPS.Mobile.UI.Components.Progress.ProgressBar;
using ProgressBarRenderer = DIPS.Mobile.UI.Droid.Components.Progress.ProgressBarRenderer;

[assembly: ExportRenderer(typeof(ProgressBar), typeof(ProgressBarRenderer))]

namespace DIPS.Mobile.UI.Droid.Components.Progress
{
    public class ProgressBarRenderer : Xamarin.Forms.Platform.Android.ProgressBarRenderer
    {
        public ProgressBarRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.ProgressBar> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is ProgressBar progressBar)
                {
                    if (progressBar.Mode == ProgressBarMode.Indeterminate)
                    {
                        Control.Indeterminate = true;    
                    }
                }
            }
        }
    }    
}

