namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarTaskButton
{
    /// <summary>
    /// <see cref="IsBusy"/>
    /// </summary>
    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
        nameof(IsBusy),
        typeof(bool),
        typeof(ToolbarTaskButton),
        defaultValue: false);

    /// <summary>
    /// <see cref="IsFinished"/>
    /// </summary>
    public static readonly BindableProperty IsFinishedProperty = BindableProperty.Create(
        nameof(IsFinished),
        typeof(bool),
        typeof(ToolbarTaskButton),
        defaultValue: false);

    /// <summary>
    /// <see cref="Error"/>
    /// </summary>
    public static readonly BindableProperty ErrorProperty = BindableProperty.Create(
        nameof(Error),
        typeof(ToolbarTaskError),
        typeof(ToolbarTaskButton));

    /// <summary>
    /// When true, the button is replaced with a spinner to indicate a loading/busy state.
    /// </summary>
    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    /// <summary>
    /// When true, the button is replaced with a checkmark icon to indicate the task completed successfully.
    /// </summary>
    public bool IsFinished
    {
        get => (bool)GetValue(IsFinishedProperty);
        set => SetValue(IsFinishedProperty, value);
    }

    /// <summary>
    /// The error state configuration for this task button.
    /// Set <see cref="ToolbarTaskError.HasError"/> to true to replace the button with an error icon.
    /// </summary>
    public ToolbarTaskError? Error
    {
        get => (ToolbarTaskError?)GetValue(ErrorProperty);
        set => SetValue(ErrorProperty, value);
    }
}
