using System;
using CoreGraphics;
using DIPS.Mobile.UI.iOS.Extensions;
using UIKit;
using DUIButton = DIPS.Mobile.UI.Components.Buttons.Button;
using DUIButtonRenderer = DIPS.Mobile.UI.iOS.Components.Buttons.ButtonRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(DUIButton), typeof(DUIButtonRenderer))]
namespace DIPS.Mobile.UI.iOS.Components.Buttons
{
    public class ButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
        private DUIButton? m_button;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is DUIButton button)
                {
                    m_button = button;
                }
            }
        }

        public override void Draw(CGRect rect)
        {
            if (m_button != null)
            {
                Control.AddCornerRadius(m_button.CornerRadius, m_button.BackgroundColor);    
            } 
            
            base.Draw(rect);
        }
    }
}