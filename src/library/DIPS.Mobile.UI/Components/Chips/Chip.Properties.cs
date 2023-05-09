namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip
{
    public static readonly new BindableProperty CornerRadiusProperty = BindableProperty.Create(
        nameof(CornerRadius),
        typeof(CornerRadius),
        typeof(Chip));

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }
}