namespace DIPS.Mobile.UI.Components.Buttons
{
    public partial class Button
    {
        public static readonly BindableProperty AdditionalHitBoxSizeProperty = BindableProperty.Create(
            nameof(AdditionalHitBoxSize),
            typeof(Thickness),
            typeof(Button));

        public Thickness AdditionalHitBoxSize
        {
            get => (Thickness)GetValue(AdditionalHitBoxSizeProperty);
            set => SetValue(AdditionalHitBoxSizeProperty, value);
        }
    }
}