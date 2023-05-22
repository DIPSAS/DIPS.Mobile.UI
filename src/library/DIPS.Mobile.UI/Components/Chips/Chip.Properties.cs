namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(Chip));

    /// <summary>
    /// Sets the text inside of the chip that people will see
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}