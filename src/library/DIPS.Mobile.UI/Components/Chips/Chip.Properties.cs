namespace DIPS.Mobile.UI.Components.Chips;

public partial class Chip
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(Chip));

    /// <summary>
    /// Sets the title of the Chip
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
}