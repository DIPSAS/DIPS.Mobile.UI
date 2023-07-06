using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.ListItems;

public partial class LoadableListItem
{
    /// <summary>
    /// Sets the <see cref="LoadableListItem"/> as busy, will show an <see cref="ActivityIndicator"/>, replacing the <see cref="LoadableListItem"/>'s content
    /// </summary>
    /// <remarks>If <see cref="IsBusy"/> is also set to true, the <see cref="LoadableListItem"/> will prioritize to instead show the error content</remarks>
    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    /// <summary>
    /// Determines whether an error has occured, will replace the ListItem's content with error content
    /// </summary>
    public bool IsError
    {
        get => (bool)GetValue(IsErrorProperty);
        set => SetValue(IsErrorProperty, value);
    }

    /// <summary>
    /// The command to be executed if a user taps the <see cref="LoadableListItem"/>
    /// </summary>
    /// <remarks>Will only be executed if <see cref="IsError"/> is true</remarks>
    public ICommand OnErrorTappedCommand
    {
        get => (ICommand)GetValue(OnErrorTappedCommandProperty);
        set => SetValue(OnErrorTappedCommandProperty, value);
    }

    /// <summary>
    /// The parameter to <see cref="OnErrorTappedCommand"/>
    /// </summary>
    public object OnErrorTappedCommandParameter
    {
        get => GetValue(OnErrorTappedCommandParameterProperty);
        set => SetValue(OnErrorTappedCommandParameterProperty, value);
    }
    
    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
        nameof(IsBusy),
        typeof(bool),
        typeof(LoadableListItem),
        propertyChanged:OnIsBusyChanged);
    
    public static readonly BindableProperty IsErrorProperty = BindableProperty.Create(
        nameof(IsError),
        typeof(bool),
        typeof(LoadableListItem),
        propertyChanged:OnIsErrorChanged);
    
    public static readonly BindableProperty OnErrorTappedCommandProperty = BindableProperty.Create(
        nameof(OnErrorTappedCommand),
        typeof(ICommand),
        typeof(LoadableListItem));
    
    public static readonly BindableProperty OnErrorTappedCommandParameterProperty = BindableProperty.Create(
        nameof(OnErrorTappedCommandParameter),
        typeof(object),
        typeof(LoadableListItem));
}