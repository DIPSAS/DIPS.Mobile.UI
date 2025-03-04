using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Buttons
{
    public partial class Button
    {
       

        /// <summary>
        /// Sets the value to make the hitbox of the button larger
        /// </summary>
        public Thickness AdditionalHitBoxSize
        {
            get => (Thickness)GetValue(AdditionalHitBoxSizeProperty);
            set => SetValue(AdditionalHitBoxSizeProperty, value);
        }

        /// <summary>
        /// Sets the color of the <see cref="ImageSource"/>
        /// </summary>
        public Color ImageTintColor
        {
            get => (Color)GetValue(ImageTintColorProperty);
            set => SetValue(ImageTintColorProperty, value);
        }

        /// <summary>
        /// Sets the placement of <see cref="ImageSource"/>
        /// </summary>
        public ImagePlacement ImagePlacement
        {
            get => (ImagePlacement)GetValue(ImagePlacementProperty);
            set => SetValue(ImagePlacementProperty, value);
        }
        
        public static readonly BindableProperty ImageTintColorProperty = BindableProperty.Create(
            nameof(ImageTintColor),
            typeof(Color),
            typeof(Button),
            defaultValue: Colors.GetColor(ColorName.color_icon_default));
        
        public static readonly BindableProperty ImagePlacementProperty = BindableProperty.Create(
            nameof(ImagePlacement),
            typeof(ImagePlacement),
            typeof(Button));
        
        public static readonly BindableProperty AdditionalHitBoxSizeProperty = BindableProperty.Create(
            nameof(AdditionalHitBoxSize),
            typeof(Thickness),
            typeof(Button));
    }

    public enum ImagePlacement
    {
        Left = 0,
        Right = 1
    }
}