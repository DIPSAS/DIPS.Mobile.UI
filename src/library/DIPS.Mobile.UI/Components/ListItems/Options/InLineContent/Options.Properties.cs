using System.ComponentModel;

namespace DIPS.Mobile.UI.Components.ListItems.Options.InLineContent;

public partial class Options
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
    
    public static readonly BindableProperty WidthProperty = BindableProperty.Create(
        nameof(Width),
        typeof(GridLength),
        typeof(Options),
        defaultValue: GridLength.Star);
    
    public static readonly BindableProperty HorizontalOptionsProperty = BindableProperty.Create(
        nameof(HorizontalOptions),
        typeof(LayoutOptions),
        typeof(Options),
        defaultValue: LayoutOptions.End);

    public static readonly BindableProperty VerticalOptionsProperty = BindableProperty.Create(
        nameof(VerticalOptions),
        typeof(LayoutOptions),
        typeof(Options),
        defaultValue: LayoutOptions.Center);
}