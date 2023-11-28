using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.TextFields.InputFields.MultiLineInputField;

public partial class MultiLineInputField
{
    /// <summary>
    /// Determines whether the input is truncated or not
    /// </summary>
    public bool IsTruncated
    {
        get => (bool)GetValue(IsTruncatedProperty);
        set => SetValue(IsTruncatedProperty, value);
    }

    /// <summary>
    /// Sets the number of lines before the text is truncated
    /// </summary>
    /// <remarks>NB: Is only truncated when it is not focused</remarks>
    public int MaxLines
    {
        get => (int)GetValue(MaxLinesProperty);
        set => SetValue(MaxLinesProperty, value);
    }

    /// <summary>
    /// Executed when the Save Button is tapped
    /// </summary>
    /// <remarks>The text gets sent as a string to the <see cref="Command"/> as a CommandParameter</remarks>
    public ICommand? SaveCommand
    {
        get => (ICommand?)GetValue(SaveCommandProperty);
        set => SetValue(SaveCommandProperty, value);
    }
    
    public static readonly BindableProperty SaveCommandProperty = BindableProperty.Create(
        nameof(SaveCommand),
        typeof(ICommand),
        typeof(MultiLineInputField));

    public static readonly BindableProperty MaxLinesProperty = BindableProperty.Create(
        nameof(MaxLines),
        typeof(int),
        typeof(MultiLineInputField),
        defaultValue:int.MaxValue);
    
    public static readonly BindableProperty IsTruncatedProperty = BindableProperty.Create(
        nameof(IsTruncated),
        typeof(bool),
        typeof(MultiLineInputField),
        defaultBindingMode:BindingMode.OneWayToSource);
}