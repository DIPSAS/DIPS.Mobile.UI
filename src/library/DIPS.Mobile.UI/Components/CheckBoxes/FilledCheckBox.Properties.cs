using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.CheckBoxes;

public partial class FilledCheckBox
{
    public static readonly BindableProperty IsNotCheckedOpacityProperty = BindableProperty.Create(
        nameof(IsNotCheckedOpacity),
        typeof(float),
        typeof(FilledCheckBox),
        defaultValue: .25f);

    public float IsNotCheckedOpacity
    {
        get => (float)GetValue(IsNotCheckedOpacityProperty);
        set => SetValue(IsNotCheckedOpacityProperty, value);
    }
    
    /// <summary>
    ///     Sets the background color of the checkbox when it's unchecked
    /// </summary>
    public Color UnCheckedBackgroundColor
    {
        get => (Color)GetValue(UnCheckedBackgroundColorProperty);
        set => SetValue(UnCheckedBackgroundColorProperty, value);
    }
    
    /// <summary>
    ///     The command to run when the checkbox is tapped
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    ///     The command to be executed when the checkbox is finished animating
    /// </summary>
    public ICommand? CompletedCommand
    {
        get => (ICommand)GetValue(CompletedCommandProperty);
        set => SetValue(CompletedCommandProperty, value);
    }

    /// <summary>
    /// The vent to invoke when the animating
    /// </summary>
    public event EventHandler Completed;

    /// <summary>
    ///     The corner radius of the checkbox
    /// </summary>
    public CornerRadius CornerRadius { get; set; }

    /// <summary>
    /// Sets the checkbox to a checked state
    /// </summary>
    public bool IsChecked
    {
        get => (bool)GetValue(IsCheckedProperty);
        set => SetValue(IsCheckedProperty, value);
    }

    /// <summary>
    /// Depicts a going on process
    /// </summary>
    public bool IsProgressing
    {
        get => (bool)GetValue(IsProgressingProperty);
        set => SetValue(IsProgressingProperty, value);
    }
    
    public static readonly BindableProperty CompletedCommandProperty = BindableProperty.Create(
        nameof(CompletedCommand),
        typeof(ICommand),
        typeof(FilledCheckBox));
    
    public static readonly BindableProperty IsCheckedProperty = BindableProperty.Create(
        nameof(IsChecked),
        typeof(bool),
        typeof(FilledCheckBox),
        propertyChanged:OnIsCheckedChanged);
    
    public static readonly BindableProperty IsProgressingProperty = BindableProperty.Create(
        nameof(IsProgressing),
        typeof(bool),
        typeof(FilledCheckBox),
        propertyChanged:OnIsProgressingChanged);
    
    public static readonly BindableProperty CommandProperty = BindableProperty.Create(
        nameof(Command),
        typeof(ICommand),
        typeof(FilledCheckBox), propertyChanged:((bindable, value, newValue) => ((FilledCheckBox)bindable).OnCommandChanged()));
    
    public static readonly BindableProperty UnCheckedBackgroundColorProperty = BindableProperty.Create(
        nameof(UnCheckedBackgroundColor),
        typeof(Color),
        typeof(FilledCheckBox),
        defaultValue:Colors.GetColor(ColorName.color_system_white));
    
}