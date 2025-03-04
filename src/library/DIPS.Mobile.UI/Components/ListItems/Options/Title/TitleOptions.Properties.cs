using System.ComponentModel;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Title;

public partial class TitleOptions
{
    /// <summary>
    /// Sets the font attributes
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
    
    [TypeConverter(typeof(GridLengthTypeConverter))]
    public GridLength Width
    {
        get => (GridLength)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    public TextAlignment HorizontalTextAlignment
    {
        get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
        set => SetValue(HorizontalTextAlignmentProperty, value);
    }

    public TextAlignment VerticalTextAlignment
    {
        get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
        set => SetValue(VerticalTextAlignmentProperty, value);
    }
    
    public Thickness Margin
    {
        get => (Thickness)GetValue(MarginProperty);
        set => SetValue(MarginProperty, value);
    }

    public int MaxLines
    {
        get => (int)GetValue(MaxLinesProperty);
        set => SetValue(MaxLinesProperty, value);
    }

    public LineBreakMode LineBreakMode
    {
        get => (LineBreakMode)GetValue(LineBreakModeProperty);
        set => SetValue(LineBreakModeProperty, value);
    }

    public FormattedString FormattedText
    {
        get => (FormattedString)GetValue(FormattedTextProperty);
        set => SetValue(FormattedTextProperty, value);
    }

    public double MaxWidth
    {
        get => (double)GetValue(MaxWidthProperty);
        set => SetValue(MaxWidthProperty, value);
    }

    public static readonly BindableProperty FormattedTextProperty = BindableProperty.Create(
        nameof(FormattedText),
        typeof(FormattedString),
        typeof(TitleOptions));
    
    public static readonly BindableProperty MaxLinesProperty = BindableProperty.Create(
        nameof(MaxLines),
        typeof(int),
        typeof(TitleOptions), defaultValue: Label.MaxLinesProperty.DefaultValue);
    
    public static readonly BindableProperty LineBreakModeProperty = BindableProperty.Create(
        nameof(LineBreakMode),
        typeof(LineBreakMode),
        typeof(TitleOptions), defaultValue: Label.LineBreakModeProperty.DefaultValue,
        defaultBindingMode: BindingMode.OneTime);
    
    public static readonly BindableProperty MarginProperty = BindableProperty.Create(
        nameof(Margin),
        typeof(Thickness),
        typeof(TitleOptions),
        defaultValue: new Thickness(0, 0, Sizes.GetSize(SizeName.content_margin_large), 0),
        defaultBindingMode: BindingMode.OneTime);
    
    public static readonly BindableProperty WidthProperty = BindableProperty.Create(
        nameof(Width),
        typeof(GridLength),
        typeof(TitleOptions),
        defaultValue:GridLength.Auto,
        defaultBindingMode: BindingMode.OneTime);
    
    public static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(ListItem),
        defaultValue:Colors.GetColor(ColorName.color_text_default));
    
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
        nameof(FontAttributes),
        typeof(FontAttributes),
        typeof(TitleOptions),
        defaultBindingMode: BindingMode.OneTime);
    
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(
        nameof(HorizontalTextAlignment),
        typeof(TextAlignment),
        typeof(TitleOptions),
        defaultValue:TextAlignment.Start,
        defaultBindingMode: BindingMode.OneTime);
    
    public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(
        nameof(VerticalTextAlignment),
        typeof(TextAlignment),
        typeof(TitleOptions),
        defaultValue:TextAlignment.End,
        defaultBindingMode: BindingMode.OneTime);
    
    public static readonly BindableProperty StyleProperty = BindableProperty.Create(
        nameof(Style),
        typeof(Style),
        typeof(TitleOptions),
        defaultValue:Styles.GetLabelStyle(LabelStyle.Body300),
        defaultBindingMode: BindingMode.OneTime);

    public static readonly BindableProperty MaxWidthProperty = BindableProperty.Create(
        nameof(MaxWidth),
        typeof(double),
        typeof(TitleOptions),
        double.PositiveInfinity,
        defaultBindingMode: BindingMode.OneTime);

}