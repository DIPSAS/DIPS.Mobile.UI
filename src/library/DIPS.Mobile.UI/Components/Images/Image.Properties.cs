using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Images
{
    public partial class Image
    {
        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color),
            typeof(Color),
            typeof(Image), defaultValue:Colors.GetColor(ColorName.color_system_black));

        /// <summary>
        /// The color of the image.
        /// </summary>
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
        public AndroidImageProperties AndroidProperties { get; set; } = new();

        public iOSImageProperties iOSProperties { get; set; } = new();
    }

    public class AndroidImageProperties : BindableObject
    {
        public static readonly BindableProperty IconResourceNameProperty = BindableProperty.Create(
            nameof(IconResourceName),
            typeof(string),
            typeof(AndroidImageProperties));

        /// <summary>
        /// /// Set this to override the <see cref="Image.Source"/> icon with a Android Resource  
        /// </summary>
        /// <remarks>This can be any resource in your Resources drawable, but you can also check out Android.Resource.Drawable.icon-name which is built in</remarks>
        public string IconResourceName
        {
            get => (string)GetValue(IconResourceNameProperty);
            set => SetValue(IconResourceNameProperty, value);
        }
    }

    /// <summary>
    /// The iOS specific context menu item options
    /// </summary>
    public class iOSImageProperties : BindableObject
    {
        public static readonly BindableProperty SystemIconNameProperty = BindableProperty.Create(
            nameof(SystemIconName),
            typeof(string),
            typeof(iOSImageProperties));

        /// <summary>
        /// Set this to override the <see cref="Image.Source"/> icon with a SF Symbol 
        /// </summary>
        /// <remarks>To see all SF Symbols go to https://developer.apple.com/sf-symbols/</remarks>
        public string SystemIconName
        {
            get => (string)GetValue(SystemIconNameProperty);
            set => SetValue(SystemIconNameProperty, value);
        }
    }
}