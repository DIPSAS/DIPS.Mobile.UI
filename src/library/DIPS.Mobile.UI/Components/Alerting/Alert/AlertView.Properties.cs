using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.API.Library;

namespace DIPS.Mobile.UI.Components.Alerting.Alert;

public partial class AlertView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(AlertView),
        propertyChanged: ((bindable, _, _) => ((AlertView)bindable).OnTitleOrDescriptionChanged()));

    /// <summary>
    /// The title of the alert.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty ShowCloseButtonProperty = BindableProperty.Create(
        nameof(ShowCloseButton),
        typeof(bool),
        typeof(AlertView), propertyChanged: (bindable, _, _) => ((AlertView)bindable).OnShowCloseButtonChanged());

    /// <summary>
    /// If true, a close button will be shown in the top right corner of the alert.
    /// </summary>
    public bool ShowCloseButton
    {
        get => (bool)GetValue(ShowCloseButtonProperty);
        set => SetValue(ShowCloseButtonProperty, value);
    }

    public static readonly BindableProperty BottomSheetTitleProperty = BindableProperty.Create(
        nameof(BottomSheetTitle),
        typeof(string),
        typeof(AlertView));

    /// <summary>
    /// Sets the title of the bottom sheet that appears when the alert is tapped.
    /// <remarks>Can only open a BottomSheet if the text inside the alert is truncated</remarks>
    /// </summary>
    public string? BottomSheetTitle
    {
        get => (string?)GetValue(BottomSheetTitleProperty);
        set => SetValue(BottomSheetTitleProperty, value);
    }

    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description),
        typeof(string),
        typeof(AlertView),
        propertyChanged: ((bindable, _, _) => ((AlertView)bindable).OnTitleOrDescriptionChanged()));

    /// <summary>
    /// The description of the alert.
    /// </summary>
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    internal static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(AlertView),
        propertyChanged: ((bindable, _, _) => ((AlertView)bindable).OnIconChanged()));

    /// <summary>
    /// The icon of the alert.
    /// </summary>
    /// <remarks>Use <see cref="Style"/> instead of settings this manually.</remarks>
    [TypeConverter(nameof(ImageSourceConverter))]
    internal ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    internal static readonly BindableProperty IconColorProperty = BindableProperty.Create(
        nameof(IconColor),
        typeof(Color),
        typeof(AlertView));
    
    /// <summary>
    /// The color of the icon.
    /// </summary>
    /// <remarks>Use <see cref="Style"/> instead of settings this manually.</remarks>
    internal Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
    }

    internal static readonly BindableProperty TextColorProperty = BindableProperty.Create(
        nameof(TextColor),
        typeof(Color),
        typeof(AlertView));

    internal Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public static readonly BindableProperty LeftButtonTextProperty = BindableProperty.Create(
        nameof(LeftButtonText),
        typeof(string),
        typeof(AlertView));

    /// <summary>
    /// The text of the left button.
    /// </summary>
    public string LeftButtonText
    {
        get => (string)GetValue(LeftButtonTextProperty);
        set => SetValue(LeftButtonTextProperty, value);
    }

    public static readonly BindableProperty LeftButtonCommandProperty = BindableProperty.Create(
        nameof(LeftButtonCommand),
        typeof(ICommand),
        typeof(AlertView), propertyChanged: ((bindable, _, _) => ((AlertView)bindable).OnButtonChanged()));

    public ICommand? LeftButtonCommand
    {
        get => (ICommand)GetValue(LeftButtonCommandProperty);
        set => SetValue(LeftButtonCommandProperty, value);
    }

    public static readonly BindableProperty LeftButtonCommandParameterProperty = BindableProperty.Create(
        nameof(LeftButtonCommandParameter),
        typeof(object),
        typeof(AlertView));

    public object LeftButtonCommandParameter
    {
        get => (object)GetValue(LeftButtonCommandParameterProperty);
        set => SetValue(LeftButtonCommandParameterProperty, value);
    }

    public static readonly BindableProperty RightButtonTextProperty = BindableProperty.Create(
        nameof(RightButtonText),
        typeof(string),
        typeof(AlertView));

    public string RightButtonText
    {
        get => (string)GetValue(RightButtonTextProperty);
        set => SetValue(RightButtonTextProperty, value);
    }

    public static readonly BindableProperty RightButtonCommandProperty = BindableProperty.Create(
        nameof(RightButtonCommand),
        typeof(ICommand),
        typeof(AlertView), propertyChanged: ((bindable, _, _) => ((AlertView)bindable).OnButtonChanged()));

    public ICommand? RightButtonCommand
    {
        get => (ICommand)GetValue(RightButtonCommandProperty);
        set => SetValue(RightButtonCommandProperty, value);
    }
    
    public static readonly BindableProperty RightButtonCommandParameterProperty = BindableProperty.Create(
        nameof(RightButtonCommandParameter),
        typeof(object),
        typeof(AlertView));

    public object RightButtonCommandParameter
    {
        get => (object)GetValue(RightButtonCommandParameterProperty);
        set => SetValue(RightButtonCommandParameterProperty, value);
    }

    public static readonly BindableProperty ShouldAnimateProperty = BindableProperty.Create(
        nameof(ShouldAnimate),
        typeof(bool),
        typeof(AlertView),
        true,
        propertyChanged: (bindable, _, _) => ((AlertView)bindable).OnShouldAnimateChanged());
    
    /// <summary>
    /// Determines whether the alert should animate when it appears.
    /// </summary>
    public bool ShouldAnimate
    {
        get => (bool)GetValue(ShouldAnimateProperty);
        set => SetValue(ShouldAnimateProperty, value);
    }

    public static readonly BindableProperty TitleTruncationModeProperty = BindableProperty.Create(
        nameof(TitleTruncationMode),
        typeof(AlertTitleTruncationMode),
        typeof(AlertView),
        propertyChanged: (bindable, _, _) => ((AlertView)bindable).OnTitleTruncationModeChanged());

    /// <summary>
    /// Determines how aggressively the title should be truncated.
    /// <remarks>Only works when <see cref="Description"/> is not set</remarks>
    /// </summary>
    public AlertTitleTruncationMode TitleTruncationMode
    {
        get => (AlertTitleTruncationMode)GetValue(TitleTruncationModeProperty);
        set => SetValue(TitleTruncationModeProperty, value);
    }
}

public enum AlertTitleTruncationMode
{
    /// <summary>
    /// Will aggressively truncate the title (MaxLines = 1)
    /// </summary>
    Aggressive,
    /// <summary>
    /// Will moderately truncate the title (MaxLines = 2)
    /// </summary>
    Moderate
}