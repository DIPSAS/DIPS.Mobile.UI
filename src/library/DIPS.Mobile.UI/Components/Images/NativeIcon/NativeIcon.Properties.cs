using DIPS.Mobile.UI.Resources.Colors;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Images.NativeIcon
{
    public partial class NativeIcon
    {

        public static readonly BindableProperty AndroidIconResourceNameProperty = BindableProperty.Create(
            nameof(AndroidIconResourceName),
            typeof(string),
            typeof(NativeIcon));

        public string AndroidIconResourceName
        {
            get => (string)GetValue(AndroidIconResourceNameProperty);
            set => SetValue(AndroidIconResourceNameProperty, value);
        }

        public static readonly BindableProperty iOSSystemNameProperty = BindableProperty.Create(
            nameof(iOSSystemIconName),
            typeof(string),
            typeof(NativeIcon));

        public string iOSSystemIconName
        {
            get => (string)GetValue(iOSSystemNameProperty);
            set => SetValue(iOSSystemNameProperty, value);
        }


        public static readonly BindableProperty ColorProperty = BindableProperty.Create(
            nameof(Color),
            typeof(Color),
            typeof(NativeIcon), defaultValue: Colors.GetColor(ColorName.color_system_black));

        /// <summary>
        /// The color of the image.
        /// </summary>
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }
    }
}