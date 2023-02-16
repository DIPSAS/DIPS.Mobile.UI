using System.ComponentModel;
using Android.Content;
using DIPS.Mobile.UI.Components.Progress;
using DIPS.Mobile.UI.Droid.Components.Progress;
using Google.Android.Material.ProgressIndicator;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(IndeterminateProgressBar), typeof(IndeterminateProgressBarRenderer))]

namespace DIPS.Mobile.UI.Droid.Components.Progress
{
    public class IndeterminateProgressBarRenderer : ViewRenderer
    {
        private LinearProgressIndicator m_control;
        private IndeterminateProgressBar m_indeterminateProgressBar;
        public IndeterminateProgressBarRenderer(Context context) : base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is IndeterminateProgressBar indeterminateProgressBar)
                {
                    m_indeterminateProgressBar = indeterminateProgressBar;
                    m_control = new LinearProgressIndicator(Context);
                    SetNativeControl(m_control);
                    ToggleAnimation();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(IndeterminateProgressBar.IsRunningProperty.PropertyName):
                    ToggleAnimation();
                    break;
            }
        }

        private void ToggleAnimation()
        {
            m_control.Indeterminate = m_indeterminateProgressBar.IsRunning;
            if (!m_control.Indeterminate)
            {
                m_control.Progress = 100;
            }
            else
            {
                m_control.Progress = 0;
            }
        }
    }    
}

