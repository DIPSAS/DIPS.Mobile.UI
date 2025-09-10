using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
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
    /// Sets the style of <see cref="TruncatedText"/>
    /// </summary>
    public Style TruncatedTextStyle
    {
        get => (Style)GetValue(TruncatedTextStyleProperty);
        set => SetValue(TruncatedTextStyleProperty, value);
    }

    /// <summary>
    /// Sets the formatted text of the Label
    /// </summary>
    public FormattedString FormattedText
    {
        get => (FormattedString)GetValue(FormattedTextProperty);
        set => SetValue(FormattedTextProperty, value);
    }

    /// <summary>
    /// Sets the text of the Label
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Sets the text color of the Label
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Sets the style of the Label
    /// </summary>
    /*public new Style Style
    {
        get => (Style)GetValue(StyleProperty);
        set => SetValue(StyleProperty, value);
    }*/

    /// <summary>
    /// Sets the maximum number of lines to display before truncating the text
    /// <remarks>TailTruncation is default enabled</remarks>
    /// </summary>
    public int MaxLines
    {
        get => (int)GetValue(MaxLinesProperty);
        set => SetValue(MaxLinesProperty, value);
    }

    /// <summary>
    /// Whether the text is currently truncated or not
    /// </summary>
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
    
    /*public new static readonly BindableProperty StyleProperty = BindableProperty.Create(
        nameof(Style),
        typeof(Style),
        typeof(CustomTruncationTextView),
        propertyChanged: (bindable, _, _) => ((CustomTruncationTextView)bindable).OnStyleChanged());*/
    
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
        defaultValue: Styles.GetLabelStyle(LabelStyle.UI100),
        propertyChanged: (bindable, _, _) => ((CustomTruncationTextView)bindable).OnTruncatedTextStyleChanged());
    
    public static readonly BindableProperty TruncatedTextProperty = BindableProperty.Create(
        nameof(TruncatedText),
        typeof(string),
        typeof(CustomTruncationTextView),
        defaultValue: DUILocalizedStrings.More);
}