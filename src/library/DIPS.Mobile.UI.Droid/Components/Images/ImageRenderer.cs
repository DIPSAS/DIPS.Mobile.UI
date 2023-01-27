using System.ComponentModel;
using Android.Content;
using Android.Widget;
using DUIImage = DIPS.Mobile.UI.Components.Images.Image;
using DUIImageRenderer = DIPS.Mobile.UI.Droid.Components.Images.ImageRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DUIImage), typeof(DUIImageRenderer))]
namespace DIPS.Mobile.UI.Droid.Components.Images
{
    public class ImageRenderer : Xamarin.Forms.Platform.Android.ImageRenderer
    {
        private DUIImage? m_image;
        public ImageRenderer(Context context):base(context) { }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                if (e.NewElement is DUIImage duiImage)
                {
                    m_image = duiImage;
                    m_image.AndroidProperties.PropertyChanged += OnElementPropertyChanged;
                    TrySetColors();
                    TrySetAndroidResourceIcon();
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (m_image != null)
            {
                m_image.AndroidProperties.PropertyChanged -= OnElementPropertyChanged;
            }

            base.Dispose(disposing);
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(DUIImage.Color):
                    TrySetColors();
                    break;
                case nameof(DUIImage.AndroidProperties.IconResourceName):
                    TrySetAndroidResourceIcon();
                    break;

            }
            base.OnElementPropertyChanged(sender, e);
        }
        
        private void TrySetAndroidResourceIcon()
        {
            if (!string.IsNullOrEmpty(m_image?.AndroidProperties.IconResourceName))
            {
                var androidResource =
                    DUI.GetResourceId(m_image!.AndroidProperties.IconResourceName!, "drawable");
                if (androidResource != null)
                {
                    Control.SetImageResource((int)androidResource);    
                }
            }
        }

        private void TrySetColors()
        {
            if (m_image is {Color: { }})
            {
                Control.SetColorFilter(m_image.Color.ToAndroid());
            }
        }
    }
}