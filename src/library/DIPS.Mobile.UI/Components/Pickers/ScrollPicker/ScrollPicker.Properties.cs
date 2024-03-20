namespace DIPS.Mobile.UI.Components.Pickers.ScrollPicker;

public partial class ScrollPicker
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(ScrollPicker));

    public static readonly BindableProperty ViewModelProperty = BindableProperty.Create(
        nameof(ViewModel),
        typeof(IScrollPickerViewModel),
        typeof(ScrollPicker),
        defaultBindingMode: BindingMode.OneWay);
    
    public static readonly BindableProperty SeparatorTextProperty = BindableProperty.Create(
        nameof(SeparatorText),
        typeof(string),
        typeof(ScrollPicker),
        defaultValue: "/");
    
    /// <summary>
    /// The ViewModel to configure the scroll picker
    /// </summary>
    public IScrollPickerViewModel ViewModel
    {
        get => (IScrollPickerViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
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