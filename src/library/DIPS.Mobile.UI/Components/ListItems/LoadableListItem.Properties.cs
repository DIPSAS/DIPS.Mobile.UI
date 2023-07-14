using System.Windows.Input;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.ListItems;

[ContentProperty(nameof(LoadedContentItem))]
public partial class LoadableListItem
{
    public View LoadedContentItem
    {
        get => (View)GetValue(LoadedContentItemProperty);
        set => SetValue(LoadedContentItemProperty, value);
    }
    
    public View? StaticContentItem
    {
        get => (View)GetValue(StaticContentProperty);
        set => SetValue(StaticContentProperty, value);
    }
    
    public bool FadeContentIn
    {
        get => (bool)GetValue(FadeContentInProperty);
        set => SetValue(FadeContentInProperty, value);
    }
    
    public string ErrorText
    {
        get => (string)GetValue(ErrorTextProperty);
        set => SetValue(ErrorTextProperty, value);
    }

    public Color ErrorTextColor
    {
        get => (Color)GetValue(ErrorTextColorProperty);
        set => SetValue(ErrorTextColorProperty, value);
    }
    
    public string BusyText
    {
        get => (string)GetValue(BusyTextProperty);
        set => SetValue(BusyTextProperty, value);
    }
    
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
    public ICommand? OnErrorTappedCommand
    {
        get => (ICommand)GetValue(OnErrorTappedCommandProperty);
        set => SetValue(OnErrorTappedCommandProperty, value);
    }

    /// <summary>
    /// The parameter to <see cref="OnErrorTappedCommand"/>
    /// </summary>
    public object? OnErrorTappedCommandParameter
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
    
    public static readonly BindableProperty BusyTextProperty = BindableProperty.Create(
        nameof(BusyText),
        typeof(string),
        typeof(LoadableListItem));
    
    public static readonly BindableProperty ErrorTextProperty = BindableProperty.Create(
        nameof(ErrorText),
        typeof(string),
        typeof(LoadableListItem));
    
    public static readonly BindableProperty FadeContentInProperty = BindableProperty.Create(
        nameof(FadeContentIn),
        typeof(bool),
        typeof(LoadableListItem));
    
    public static readonly BindableProperty StaticContentProperty = BindableProperty.Create(
        nameof(StaticContentItem),
        typeof(View),
        typeof(LoadableListItem));
    
    public static readonly BindableProperty LoadedContentItemProperty = BindableProperty.Create(
        nameof(LoadedContentItem),
        typeof(View),
        typeof(LoadableListItem));
    
    public static readonly BindableProperty ErrorTextColorProperty = BindableProperty.Create(
        nameof(ErrorTextColor),
        typeof(Color),
        typeof(LoadableListItem),
        defaultValue: Colors.GetColor(ColorName.color_neutral_90));
}