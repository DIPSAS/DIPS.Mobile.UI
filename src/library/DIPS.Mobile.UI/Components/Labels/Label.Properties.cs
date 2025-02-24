using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class Label
{
    /// <summary>
    /// Determines whether the Label is truncated or not
    /// </summary>
    public bool IsTruncated
    {
        get => (bool)GetValue(IsTruncatedProperty);
        set => SetValue(IsTruncatedProperty, value);
    }

    /// <summary>
    /// Replaces the standard "..." truncate text with <see cref="TruncatedText"/>
    /// </summary>
    /// <remarks><b>NB!</b> Will completely break <see cref="FormattedString"/>, so make sure you have not set any spans on this Label</remarks>
    public string TruncatedText
    {
        get => (string)GetValue(TruncatedTextProperty);
        set => SetValue(TruncatedTextProperty, value);
    }

    /// <summary>
    /// Sets the color of <see cref="TruncatedText"/>
    /// </summary>
    public Color TruncatedTextColor
    {
        get => (Color)GetValue(TruncatedTextColorProperty);
        set => SetValue(TruncatedTextColorProperty, value);
    }
    
    public static readonly BindableProperty TruncatedTextColorProperty = BindableProperty.Create(
        nameof(TruncatedTextColor),
        typeof(Color),
        typeof(Label),
        // TODO: Lisa
        defaultValue: Colors.GetColor(ColorName.color_primary_90));
    
    public static readonly BindableProperty TruncatedTextProperty = BindableProperty.Create(
        nameof(TruncatedText),
        typeof(string),
        typeof(Label));
    
    public static readonly BindableProperty IsTruncatedProperty = BindableProperty.Create(
        nameof(IsTruncated),
        typeof(bool),
        typeof(Label),
        defaultBindingMode: BindingMode.OneWayToSource);

    public new static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(Label), propertyChanged: ((bindable, _, _) =>
        {
            ((Label)bindable).OnTextChanged();
        }));

    /// <summary>
    /// <inheritdoc cref="Microsoft.Maui.Controls.Label.Text"/>
    /// </summary>
    public new string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}