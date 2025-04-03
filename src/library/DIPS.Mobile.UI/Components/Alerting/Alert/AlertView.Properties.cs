using System.ComponentModel;
using System.Windows.Input;

namespace DIPS.Mobile.UI.Components.Alerting.Alert;

public partial class AlertView
{
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(
        nameof(Title),
        typeof(string),
        typeof(AlertView),
        propertyChanged: ((bindable, _, _) => ((AlertView)bindable).OnTitleChanged()));

    /// <summary>
    /// The title of the alert.
    /// </summary>
    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(
        nameof(Description),
        typeof(string),
        typeof(AlertView),
        propertyChanged: ((bindable, _, _) => ((AlertView)bindable).OnDescriptionChanged()));

    /// <summary>
    /// The description of the alert.
    /// </summary>
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public static readonly BindableProperty IconProperty = BindableProperty.Create(
        nameof(Icon),
        typeof(ImageSource),
        typeof(AlertView),
        propertyChanged: ((bindable, _, _) => ((AlertView)bindable).OnIconChanged()));

    /// <summary>
    /// The icon of the alert.
    /// </summary>
    /// <remarks>Use <see cref="Style"/> instead of settings this manually.</remarks>
    [TypeConverter(nameof(ImageSourceConverter))]
    public ImageSource Icon
    {
        get => (ImageSource)GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
        nameof(IconColor),
        typeof(Color),
        typeof(AlertView));

    /// <summary>
    /// The color of the icon.
    /// </summary>
    /// <remarks>Use <see cref="Style"/> instead of settings this manually.</remarks>
    public Color IconColor
    {
        get => (Color)GetValue(IconColorProperty);
        set => SetValue(IconColorProperty, value);
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

    public static readonly BindableProperty ButtonAlignmentProperty = BindableProperty.Create(
        nameof(ButtonAlignment),
        typeof(ButtonAlignmentType),
        typeof(AlertView),
        defaultValue: ButtonAlignmentType.Auto,
        propertyChanged: ((bindable, _, _) => ((AlertView)bindable).OnButtonAlignmentChanged()));

    public ButtonAlignmentType ButtonAlignment
    {
        get => (ButtonAlignmentType)GetValue(ButtonAlignmentProperty);
        set => SetValue(ButtonAlignmentProperty, value);
    }
}