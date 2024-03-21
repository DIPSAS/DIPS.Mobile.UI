namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public partial class ScrollPicker
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ScrollPicker));

    public static readonly BindableProperty ComponentsProperty = BindableProperty.Create(
        nameof(Components),
        typeof(List<IScrollPickerComponent>),
        typeof(ScrollPicker),
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty SeparatorTextProperty = BindableProperty.Create(
        nameof(SeparatorText),
        typeof(string),
        typeof(ScrollPicker),
        defaultValue: "/");
    
    /// <summary>
    /// The components (wheels) in the scroll picker
    /// </summary>
    public List<IScrollPickerComponent> Components
    {
        get => (List<IScrollPickerComponent>)GetValue(ComponentsProperty);
        set => SetValue(ComponentsProperty, value);
    }

    /// <summary>
    /// The text to separate the components in the scroll picker, default is '/'
    /// </summary>
    /// <remarks>Only valid if there are 1> components in the scroll picker</remarks>
    public string SeparatorText
    {
        get => (string)GetValue(SeparatorTextProperty);
        set => SetValue(SeparatorTextProperty, value);
    }
    
    /// <summary>
    /// Only for Android
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    
}