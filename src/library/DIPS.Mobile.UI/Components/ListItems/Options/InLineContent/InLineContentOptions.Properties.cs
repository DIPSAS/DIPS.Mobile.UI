using System.ComponentModel;

namespace DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;

public partial class InLineContentOptions
{
    public LayoutOptions VerticalOptions
    {
        get => (LayoutOptions)GetValue(VerticalOptionsProperty);
        set => SetValue(VerticalOptionsProperty, value);
    }
    
    public LayoutOptions HorizontalOptions
    {
        get => (LayoutOptions)GetValue(HorizontalOptionsProperty);
        set => SetValue(HorizontalOptionsProperty, value);
    }
    
    [TypeConverter(typeof(GridLengthTypeConverter))]
    public GridLength Width
    {
        get => (GridLength)GetValue(WidthProperty);
        set => SetValue(WidthProperty, value);
    }

    /// <summary>
    /// Set the RowSpan
    /// </summary>
    /// <remarks>Use 1 to be in the same row as Title. Use 2 to span over underlying content</remarks>
    public bool SpanOverUnderlyingContent
    {
        get => (bool)GetValue(SpanOverUnderlyingContentProperty);
        set => SetValue(SpanOverUnderlyingContentProperty, value);
    }
    
    public static readonly BindableProperty SpanOverUnderlyingContentProperty = BindableProperty.Create(
        nameof(SpanOverUnderlyingContent),
        typeof(bool),
        typeof(InLineContentOptions));
    
    public static readonly BindableProperty WidthProperty = BindableProperty.Create(
        nameof(Width),
        typeof(GridLength),
        typeof(InLineContentOptions),
        defaultValue: GridLength.Star);
    
    public static readonly BindableProperty HorizontalOptionsProperty = BindableProperty.Create(
        nameof(HorizontalOptions),
        typeof(LayoutOptions),
        typeof(InLineContentOptions),
        defaultValue: LayoutOptions.End);

    public static readonly BindableProperty VerticalOptionsProperty = BindableProperty.Create(
        nameof(VerticalOptions),
        typeof(LayoutOptions),
        typeof(InLineContentOptions),
        defaultValue: LayoutOptions.Center);
}