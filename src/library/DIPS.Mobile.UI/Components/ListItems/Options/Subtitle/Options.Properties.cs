using System.ComponentModel;

namespace DIPS.Mobile.UI.Components.ListItems.Options.Subtitle;

public partial class Options
{
    
    /// <summary>
    /// Sets the font attributes of the <see cref="Text"/>
    /// </summary>
    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
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
    
    public static readonly BindableProperty FontAttributesProperty = BindableProperty.Create(
        nameof(FontAttributes),
        typeof(FontAttributes),
        typeof(Options));
    
    public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(
        nameof(HorizontalTextAlignment),
        typeof(TextAlignment),
        typeof(Options),
        defaultValue:TextAlignment.Start);
    
    public static readonly BindableProperty VerticalTextAlignmentProperty = BindableProperty.Create(
        nameof(VerticalTextAlignment),
        typeof(TextAlignment),
        typeof(Options),
        defaultValue:TextAlignment.Start);
   
}