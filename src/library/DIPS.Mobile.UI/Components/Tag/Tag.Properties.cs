using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Tag;

public partial class Tag
{
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(Tag));

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(Tag));

    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(Tag));

    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
        nameof(IconColor),
        typeof(Color),
        typeof(Tag));

    /// <summary>
    /// Sets the color of the icon in the tag.
    /// </summary>
    public Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }
    
    /// <summary>
    /// Sets the icon to be displayed in the tag.
    /// <remarks>Is optional</remarks>
    /// </summary>
    public ImageSource? Icon
    {
        get => (ImageSource?)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
    
    /// <summary>
    /// Sets the color of the text in the tag.
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }
    
    /// <summary>
    /// Sets the text to be displayed in the tag.
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}