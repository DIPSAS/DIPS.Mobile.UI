namespace DIPS.Mobile.UI.Components.Buttons
{
    public partial class Button
    {
        public static readonly BindableProperty AdditionalHitBoxSizeProperty = BindableProperty.Create(
            nameof(AdditionalHitBoxSize),
            typeof(Thickness),
            typeof(Button));

        /// <summary>
        /// Sets the value to make the hitbox of the button larger
        /// </summary>
        public Thickness AdditionalHitBoxSize
        {
            get => (Thickness)GetValue(AdditionalHitBoxSizeProperty);
            set => SetValue(AdditionalHitBoxSizeProperty, value);
        }
    }
}