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
        /// Sets the <see cref="ImageSource"/> to the right side of the text
        /// </summary>
        public bool ImageToRightSide
        {
            get => (bool)GetValue(ImageToRightSideProperty);
            set => SetValue(ImageToRightSideProperty, value);
        }
        
        public static readonly BindableProperty ImageTintColorProperty = BindableProperty.Create(
            nameof(ImageTintColor),
            typeof(Color),
            typeof(Button),
            defaultValue: Colors.GetColor(ColorName.color_system_black));
        
        public static readonly BindableProperty ImageToRightSideProperty = BindableProperty.Create(
            nameof(ImageToRightSide),
            typeof(bool),
            typeof(Button));
        
        public static readonly BindableProperty AdditionalHitBoxSizeProperty = BindableProperty.Create(
            nameof(AdditionalHitBoxSize),
            typeof(Thickness),
            typeof(Button));
    }
}