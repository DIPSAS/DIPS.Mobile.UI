using System.Windows.Input;
using Microsoft.Maui.Controls.Internals;

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
    /// Sets the max number of characters that is allowed before the send button disables. 
    /// </summary>
    public int MaxTextLength
    {
        get => (int)GetValue(MaxTextLengthProperty);
        set => SetValue(MaxTextLengthProperty, value);
    }

    /// <summary>
    ///  Executed when people tap the save button.
    /// </summary>
    /// <remarks>The text gets sent as a string to the <see cref="Command"/> as a CommandParameter</remarks>
    public ICommand? SaveCommand
    {
        get => (ICommand?)GetValue(SaveCommandProperty);
        set => SetValue(SaveCommandProperty, value);
    }

    /// <summary>
    /// Event that will be invoked when people tap the save button.
    /// </summary>
    public event EventHandler? SaveTapped; 
    
    /// <summary>
    /// Executed when people tap the cancel button.
    /// </summary>
    /// <remarks>Use <see cref="CancelCommandParameter"/></remarks> to send objects to the command.
    public ICommand CancelCommand
    {
        get => (ICommand)GetValue(CancelCommandProperty);
        set => SetValue(CancelCommandProperty, value);
    }

    public object CancelCommandParameter
    {
        get => (object)GetValue(CancelCommandParameterProperty);
        set => SetValue(CancelCommandParameterProperty, value);
    }
    
    /// Event that will be invoked when people tap the cancel button.
    public event EventHandler? CancelTapped;

    /// <summary>
    /// The parameter to be sent with <see cref="SaveCommand"/>
    /// </summary>
    public object SaveCommandParameter
    {
        get => (object)GetValue(SaveCommandParameterProperty);
        set => SetValue(SaveCommandParameterProperty, value);
    }

    public bool IsSavingSuccess
    {
        get => (bool)GetValue(IsSavingSuccessProperty);
        set => SetValue(IsSavingSuccessProperty, value);
    }
    
    /// <summary>
    /// Whether the component is saving or not, will fade out text and display a spinner
    /// </summary>
    public bool IsSaving
    {
        get => (bool)GetValue(IsSavingProperty);
        set => SetValue(IsSavingProperty, value);
    }
    
    /// <summary>
    /// Determines whether the component is in an error state or not
    /// </summary>
    public bool IsError
    {
        get => (bool)GetValue(IsErrorProperty);
        set => SetValue(IsErrorProperty, value);
    }

    /// <summary>
    /// Sets the error text that is displayed as HelpText
    /// </summary>
    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }
    
    /// <summary>
    /// Determines whether the 'Save' and 'Cancel' buttons should be visible.
    /// /// </summary>
    public bool ShowButtons
    {
        get => (bool)GetValue(ShowButtonsProperty);
        set => SetValue(ShowButtonsProperty, value);
    }
    
    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        nameof(ErrorText),
        typeof(string),
        typeof(MultiLineInputField));
    
    public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(
            nameof(IsError),
            typeof(bool),
            typeof(MultiLineInputField),
            propertyChanged: (bindable, _, _) => ((MultiLineInputField)bindable).OnIsErrorChanged());
    
    public static readonly BindableProperty IsSavingProperty = BindableProperty.Create(
        nameof(IsSaving),
        typeof(bool),
        typeof(MultiLineInputField),
        propertyChanged: (bindable, _, _) => ((MultiLineInputField)bindable).OnIsSavingChanged(),
        defaultBindingMode: BindingMode.TwoWay);
    
    public static readonly BindableProperty SaveCommandProperty = BindableProperty.Create(
        nameof(SaveCommand),
        typeof(ICommand),
        typeof(MultiLineInputField));

    public static readonly BindableProperty MaxLinesProperty = BindableProperty.Create(
        nameof(MaxLines),
        typeof(int),
        typeof(MultiLineInputField),
        defaultValue:int.MaxValue);

    public static readonly BindableProperty MaxTextLengthProperty = BindableProperty.Create(
        nameof(MaxTextLength),
        typeof(int),
        typeof(MultiLineInputField),
        propertyChanged: (bindable, _, _) => ((MultiLineInputField)bindable).OnMaxTextLengthChanged(),
        defaultValue: 0);
    
    public static readonly BindableProperty IsTruncatedProperty = BindableProperty.Create(
        nameof(IsTruncated),
        typeof(bool),
        typeof(MultiLineInputField),
        defaultBindingMode:BindingMode.OneWayToSource);
    
    public static readonly BindableProperty IsSavingSuccessProperty = BindableProperty.Create(
        nameof(IsSavingSuccess),
        typeof(bool),
        typeof(MultiLineInputField),
        propertyChanged: (bindable, _, _) => ((MultiLineInputField)bindable).OnIsSavingSuccessChanged());
    
    public static readonly BindableProperty SaveCommandParameterProperty = BindableProperty.Create(
        nameof(SaveCommandParameter),
        typeof(object),
        typeof(MultiLineInputField));
    
    public static readonly BindableProperty CancelCommandProperty = BindableProperty.Create(
        nameof(CancelCommand),
        typeof(ICommand),
        typeof(MultiLineInputField));
    
    public static readonly BindableProperty CancelCommandParameterProperty = BindableProperty.Create(
        nameof(CancelCommandParameter),
        typeof(object),
        typeof(MultiLineInputField));
    
    public static readonly BindableProperty ShowButtonsProperty = BindableProperty.Create(
        nameof(ShowButtons),
        typeof(bool),
        typeof(MultiLineInputField),
        defaultValue: true,
        propertyChanged: (bindable, _, _) => ((MultiLineInputField)bindable).OnShowButtonsChanged());
}