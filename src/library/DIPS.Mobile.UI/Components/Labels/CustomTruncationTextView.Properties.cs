using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Labels;

public partial class CustomTruncationTextView
{
    /// <summary>
    /// Places <see cref="TruncatedText"/> at the end of the Label if it is truncated
    /// </summary>
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
    public Style TruncatedTextStyle
    {
        get => (Style)GetValue(TruncatedTextStyleProperty);
        set => SetValue(TruncatedTextStyleProperty, value);
    }

    public FormattedString FormattedText
    {
        get => (FormattedString)GetValue(FormattedTextProperty);
        set => SetValue(FormattedTextProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public new Style Style
    {
        get => (Style)GetValue(StyleProperty);
        set => SetValue(StyleProperty, value);
    }

    public int MaxLines
    {
        get => (int)GetValue(MaxLinesProperty);
        set => SetValue(MaxLinesProperty, value);
    }

    public bool IsTruncated
    {
        get => (bool)GetValue(IsTruncatedProperty);
        set => SetValue(IsTruncatedProperty, value);
    }
    
    public static readonly BindableProperty IsTruncatedProperty = BindableProperty.Create(
        nameof(IsTruncated),
        typeof(bool),
        typeof(CustomTruncationTextView));
    
    public static readonly BindableProperty MaxLinesProperty = BindableProperty.Create(
        nameof(MaxLines),
        typeof(int),
        typeof(CustomTruncationTextView));
    
    public new static readonly BindableProperty StyleProperty = BindableProperty.Create(
        nameof(Style),
        typeof(Style),
        typeof(CustomTruncationTextView),
        propertyChanged: (bindable, _, _) => ((CustomTruncationTextView)bindable).OnStyleChanged());
    
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(CustomTruncationTextView));
    
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(CustomTruncationTextView));
    
    public static readonly BindableProperty FormattedTextProperty = BindableProperty.Create(
        nameof(FormattedText),
        typeof(FormattedString),
        typeof(CustomTruncationTextView));
    
    public static readonly BindableProperty TruncatedTextColorProperty = BindableProperty.Create(
        nameof(TruncatedTextColor),
        typeof(Color),
        typeof(CustomTruncationTextView),
        defaultValue: Colors.GetColor(ColorName.color_text_on_fill_information));

    public static readonly BindableProperty TruncatedTextStyleProperty = BindableProperty.Create(
        nameof(TruncatedTextStyle),
        typeof(Style),
        typeof(CustomTruncationTextView),
        propertyChanged: (bindable, _, _) => ((CustomTruncationTextView)bindable).OnTruncatedTextStyleChanged());
    
    public static readonly BindableProperty TruncatedTextProperty = BindableProperty.Create(
        nameof(TruncatedText),
        typeof(string),
        typeof(CustomTruncationTextView),
        defaultValue: DUILocalizedStrings.More);
}