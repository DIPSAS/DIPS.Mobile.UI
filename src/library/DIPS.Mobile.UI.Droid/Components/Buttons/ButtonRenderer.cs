using System.ComponentModel;
using DIPS.Mobile.UI.Droid.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using ButtonRenderer = DIPS.Mobile.UI.Droid.Components.Buttons.ButtonRenderer;

[assembly: ExportRenderer(typeof(Button), typeof(ButtonRenderer))]
namespace DIPS.Mobile.UI.Droid.Components.Buttons
{
    public class ButtonRenderer : Xamarin.Forms.Platform.Android.ButtonRenderer
    {
        private Button? m_button;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is Button button)
                {
                    m_button = button;
                    UpdateBackground();
                }
            }
        }

        private void UpdateBackground()
        {
            if (m_button == null) return;
            
            Control.SetRoundedRectangularBackground(m_button.CornerRadius, m_button.BackgroundColor);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            switch (e.PropertyName)
            {
                case nameof(Button.CornerRadius):
                    UpdateBackground();
                    break;
            }
        }
    }
}