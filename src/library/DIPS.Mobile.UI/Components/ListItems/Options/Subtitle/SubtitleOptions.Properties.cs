using System.ComponentModel;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;

public partial class SubtitleOptions
{
    
    /// <summary>
    /// Sets the font attributes of the <see cref="Text"/>
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    /// <summary>
    /// Sets the text color
    /// </summary>
    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    /// <summary>
    /// Sets the style
    /// </summary>
    public Style Style
    {
        get => (Style)GetValue(StyleProperty);
        set => SetValue(StyleProperty, value);
    }

    public LineBreakMode LineBreakMode
    {
        get => (LineBreakMode)GetValue(LineBreakModeProperty);
        set => SetValue(LineBreakModeProperty, value);
    }

    public int MaxLines
    {
        get => (int)GetValue(MaxLinesProperty);
        set => SetValue(MaxLinesProperty, value);
    }
    
    public FormattedString? FormattedText
    {
        get => (FormattedString?)GetValue(FormattedTextProperty);
        set => SetValue(FormattedTextProperty, value);
    }

    public static readonly BindableProperty FormattedTextProperty = BindableProperty.Create(
        nameof(FormattedText),
        typeof(FormattedString),
        typeof(SubtitleOptions));
    
    public static readonly BindableProperty LineBreakModeProperty = BindableProperty.Create(
        nameof(LineBreakMode),
        typeof(LineBreakMode),
        typeof(SubtitleOptions), defaultValue: Label.LineBreakModeProperty.DefaultValue,
        defaultBindingMode: BindingMode.OneTime);
    
    public static readonly BindableProperty MaxLinesProperty = BindableProperty.Create(
        nameof(MaxLines),
        typeof(int),
        typeof(SubtitleOptions), defaultValue: Label.MaxLinesProperty.DefaultValue);
    
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
        nameof(FontAttributes),
        typeof(FontAttributes),
        typeof(SubtitleOptions),
        defaultBindingMode: BindingMode.OneTime);
    
    public static readonly BindableProperty StyleProperty = BindableProperty.Create(
        nameof(Style),
        typeof(Style),
        typeof(SubtitleOptions),
        defaultValue: Styles.GetLabelStyle(LabelStyle.Body200),
        defaultBindingMode: BindingMode.OneTime);
    
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(ListItem),
        defaultValue:Colors.GetColor(ColorName.color_text_subtle_small));
   
}