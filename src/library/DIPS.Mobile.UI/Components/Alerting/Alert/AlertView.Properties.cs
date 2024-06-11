using System.ComponentModel;

namespace DIPS.Mobile.UI.Components.Alerting.Alert;

public partial class AlertView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(AlertView));

    /// <summary>
    /// The title of the alert.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description),
        typeof(string),
        typeof(AlertView));

    /// <summary>
    /// The description of the alert.
    /// </summary>
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(AlertView));

    /// <summary>
    /// The icon of the alert.
    /// </summary>
    /// <remarks>Use <see cref="Style"/> instead of settings this manually.</remarks>
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }
}