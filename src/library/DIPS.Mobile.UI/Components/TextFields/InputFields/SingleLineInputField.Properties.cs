using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.TextFields.InputFields;

public partial class SingleLineInputField
{
    /// <summary>
    /// The header text to be displayed above the input field 
    /// </summary>
    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    /// <summary>
    /// Help text to be displayed below the input field 
    /// </summary>
    public string HelpText
    {
        get => (string)GetValue(HelpTextProperty);
        set => SetValue(HelpTextProperty, value);
    }

    /// <summary>
    /// Sets the text color of the <see cref="HelpText"/>
    /// </summary>
    public Color HelpTextColor
    {
        get => (Color)GetValue(HelpTextColorProperty);
        set => SetValue(HelpTextColorProperty, value);
    }

    /// <summary>
    /// Sets the text color of the input field 
    /// </summary>
    public Color InputTextColor
    {
        get => (Color)GetValue(InputTextColorProperty);
        set => SetValue(InputTextColorProperty, value);
    }

    /// <summary>
    /// Sets the thickness of the border around input field and <see cref="HeaderText"/>
    /// </summary>
    public int BorderThickness
    {
        get => (int)GetValue(BorderThicknessProperty);
        set => SetValue(BorderThicknessProperty, value);
    }

    /// <summary>
    /// Sets the color of the border around input field and <see cref="HeaderText"/>
    /// </summary>
    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }
    
    /// <summary>
    /// Sets the corner radius of the border around input field and <see cref="HeaderText"/>
    /// </summary>
    public int BorderCornerRadius
    {
        get => (int)GetValue(BorderCornerRadiusProperty);
        set => SetValue(BorderCornerRadiusProperty, value);
    }

    /// <summary>
    /// A two-way bindable property to set or get the text from the input field
    /// </summary>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Determines whether the input field is focused or not
    /// </summary>
    /// <remarks>Read-only</remarks>
    public new bool IsFocused
    {
        get => (bool)GetValue(IsFocusedProperty);
        set => SetValue(IsFocusedProperty, value);
    }

    public new event EventHandler<FocusEventArgs>? Focused;
    public new event EventHandler<FocusEventArgs>? Unfocused;

    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(SingleLineInputField),
        propertyChanged: (bindable, _, _) => ((SingleLineInputField)bindable).OnTextChanged(),
        defaultValue:string.Empty,
        defaultBindingMode:BindingMode.TwoWay);
    
    public static new readonly BindableProperty IsFocusedProperty = BindableProperty.Create(
        nameof(IsFocused),
        typeof(bool),
        typeof(SingleLineInputField),
        defaultBindingMode:BindingMode.OneWayToSource);
    
    public static readonly BindableProperty BorderCornerRadiusProperty = BindableProperty.Create(
        nameof(BorderCornerRadius),
        typeof(int),
        typeof(SingleLineInputField),
        propertyChanged:(bindable, _, _) => ((SingleLineInputField)bindable).OnBorderCornerRadiusChanged());
    
    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor),
        typeof(Color),
        typeof(SingleLineInputField));
    
    public static readonly BindableProperty BorderThicknessProperty = BindableProperty.Create(
        nameof(BorderThickness),
        typeof(int),
        typeof(SingleLineInputField));
    
    public static readonly BindableProperty InputTextColorProperty = BindableProperty.Create(
        nameof(InputTextColor),
        typeof(Color),
        typeof(SingleLineInputField),
        defaultValue:Colors.GetColor(ColorName.color_text_default));
    
    public static readonly BindableProperty HelpTextProperty = BindableProperty.Create(
        nameof(HelpText),
        typeof(string),
        typeof(SingleLineInputField),
        propertyChanged: (bindable, _, _) => ((SingleLineInputField)bindable).OnHelpTextChanged());
    
    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
        nameof(HeaderText),
        typeof(string),
        typeof(SingleLineInputField),
        propertyChanged: (bindable, _, _) => ((SingleLineInputField)bindable).OnHeaderTextChanged());
    
    public static readonly BindableProperty HelpTextColorProperty = BindableProperty.Create(
        nameof(HelpTextColor),
        typeof(Color),
        typeof(SingleLineInputField));
}