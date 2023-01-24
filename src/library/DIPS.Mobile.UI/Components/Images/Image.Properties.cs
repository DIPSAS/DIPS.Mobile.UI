using DIPS.Mobile.UI.Resources.Colors;
using Xamarin.Forms;

namespace DIPS.Mobile.UI.Components.Images
{
    public partial class Image
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color),
            typeof(Color),
            typeof(Image));

        /// <summary>
        /// The color of the image.
        /// </summary>
        public Color? Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public PlatformImageProperties PlatformImageProperties { get; } = new();
    }

    public class PlatformImageProperties
    {
        public AndroidImageProperties Android { get; } = new();

        public iOSImageProperties iOS { get; } = new();
    }

    public class AndroidImageProperties
    {
        /// <summary>
        /// /// Set this to override the <see cref="Image.Source"/> icon with a Android Resource  
        /// </summary>
        /// <remarks>This can be any resource in your Resources drawable, but you can also check out Android.Resource.Drawable.icon-name which is built in</remarks>
        public string? IconResourceName { get; set; }
    }

    /// <summary>
    /// The iOS specific context menu item options
    /// </summary>
    public class iOSImageProperties : BindableObject
    {
        /// <summary>
        /// Set this to override the <see cref="Image.Source"/> icon with a SF Symbol 
        /// </summary>
        /// <remarks>To see all SF Symbols go to https://developer.apple.com/sf-symbols/</remarks>
        public string? SystemIconName { get; set; }
    }
}