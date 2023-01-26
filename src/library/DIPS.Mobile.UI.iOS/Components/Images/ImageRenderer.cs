using System.ComponentModel;
using CoreGraphics;
using DIPS.Mobile.UI.Resources.Colors;
using UIKit;
using DUIImage = DIPS.Mobile.UI.Components.Images.Image;
using DUIImageRenderer = DIPS.Mobile.UI.iOS.Components.Images.ImageRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(DUIImage), typeof(DUIImageRenderer))]

namespace DIPS.Mobile.UI.iOS.Components.Images
{
    public class ImageRenderer : Xamarin.Forms.Platform.iOS.ImageRenderer
    {
        private DUIImage m_image;

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement is DUIImage image)
            {
                m_image = image;
                TrySetSystemImage(image);
                TrySetImageColor();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(UI.Components.Images.Image.Color):
                    TrySetImageColor();
                    break;
            }

            base.OnElementPropertyChanged(sender, e);
        }

        private void TrySetSystemImage(DUIImage image)
        {
            if (!string.IsNullOrEmpty(image.iOSProperties.SystemIconName))
            {
                var systemImage = UIImage.GetSystemImage(image.iOSProperties.SystemIconName);
                Control.AdjustsImageSizeForAccessibilityContentSizeCategory = true;
                Control.Image = systemImage;
            }
        }

        private void TrySetImageColor()
        {
            if (m_image?.Color != null)
            {
                Control.TintColor = m_image?.Color.ToUIColor();
            }
        }
    }
}