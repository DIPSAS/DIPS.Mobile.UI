using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.BottomSheets.Header;

public class BottomSheetHeaderBehavior : BindableObject
{
    /// <summary>
    /// The command to be executed when the container that title and back button is in is tapped
    /// </summary>
    public ICommand? TitleAndBackButtonContainerCommand
    {
        get => (ICommand?)GetValue(TitleAndBackButtonContainerCommandProperty);
        set => SetValue(TitleAndBackButtonContainerCommandProperty, value);
    }
    
    /// <summary>
    /// Whether the container should be enabled or not. If it is not enabled, the command will not be able to be executed
    /// </summary>
    public bool IsTitleAndBackButtonContainerEnabled
    {
        get => (bool)GetValue(IsTitleAndBackButtonContainerEnabledProperty);
        set => SetValue(IsTitleAndBackButtonContainerEnabledProperty, value);
    }

    /// <summary>
    /// Executed when the close button is tapped, if not bound, the <see cref="BottomSheet"/> will be closed as default when the close button is tapped.
    /// <remarks>
    /// An action is sent as a CommandParameter, if the Action is executed, the <see cref="BottomSheet"/> will be closed.
    /// </remarks>
    /// <code>
    /// public ICommand CloseButtonCommand => new Command(action =>
    /// {
    ///     // Here you can display a confirmation dialog
    ///     if(shouldClose)
    ///         Close();
    /// });
    /// </code>
    /// </summary>
    public ICommand CloseButtonCommand
    {
        get => (ICommand)GetValue(CloseButtonCommandProperty);
        set => SetValue(CloseButtonCommandProperty, value);
    }

    public bool IsBackButtonVisible
    {
        get => (bool)GetValue(IsBackButtonVisibleProperty);
        set => SetValue(IsBackButtonVisibleProperty, value);
    }

    /// <summary>
    /// Whether the Header should be visible or not
    /// </summary>
    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set => SetValue(IsVisibleProperty, value);
    }
    
    public static readonly BindableProperty IsVisibleProperty = BindableProperty.Create(
        nameof(IsVisible),
        typeof(bool),
        typeof(BottomSheetHeaderBehavior),
        true);

    public static readonly BindableProperty CloseButtonCommandProperty = BindableProperty.Create(
        nameof(CloseButtonCommand),
        typeof(ICommand),
        typeof(BottomSheetHeaderBehavior));
    
    public static readonly BindableProperty TitleAndBackButtonContainerCommandProperty = BindableProperty.Create(
        nameof(TitleAndBackButtonContainerCommand),
        typeof(ICommand),
        typeof(BottomSheetHeaderBehavior));
    
    public static readonly BindableProperty IsBackButtonVisibleProperty = BindableProperty.Create(
        nameof(IsBackButtonVisible),
        typeof(bool),
        typeof(BottomSheetHeaderBehavior));
    
    public static readonly BindableProperty IsTitleAndBackButtonContainerEnabledProperty = BindableProperty.Create(
        nameof(IsTitleAndBackButtonContainerEnabled),
        typeof(bool),
        typeof(BottomSheetHeaderBehavior),
        true);
}