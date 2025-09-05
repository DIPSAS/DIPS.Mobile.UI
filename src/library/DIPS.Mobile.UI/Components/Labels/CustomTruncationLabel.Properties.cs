using DIPS.Mobile.UI.Resources.Styles.Span;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class CustomTruncationLabel
{
    /// <summary>
    /// <inheritdoc cref="Microsoft.Maui.Controls.Label.Text"/>
    /// </summary>
    public new string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
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

    /// <summary>
    /// Sets the style of <see cref="TruncatedText"/>. If set, this will override <see cref="TruncatedTextFontFamily"/> and <see cref="TruncatedTextColor"/>
    /// </summary>
    public SpanStyle TruncatedTextStyle
    {
        get => (SpanStyle)GetValue(TruncatedTextStyleProperty);
        set => SetValue(TruncatedTextStyleProperty, value);
    }
    
    public static readonly BindableProperty TruncatedTextColorProperty = BindableProperty.Create(
        nameof(TruncatedTextColor),
        typeof(Color),
        typeof(CustomTruncationLabel),
        defaultValue: Colors.GetColor(ColorName.color_text_action));

    public static readonly BindableProperty TruncatedTextStyleProperty = BindableProperty.Create(
        nameof(TruncatedTextStyle),
        typeof(SpanStyle),
        typeof(CustomTruncationLabel),
        defaultValue: SpanStyle.None);
    
    public static readonly BindableProperty TruncatedTextProperty = BindableProperty.Create(
        nameof(TruncatedText),
        typeof(string),
        typeof(CustomTruncationLabel));
    
    public static readonly BindableProperty IsTruncatedProperty = BindableProperty.Create(
        nameof(IsTruncated),
        typeof(bool),
        typeof(CustomTruncationLabel),
        defaultBindingMode: BindingMode.OneWayToSource);

    public new static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(CustomTruncationLabel), propertyChanged: ((bindable, _, _) =>
        {
            ((CustomTruncationLabel)bindable).OnTextChanged();
        }));
}