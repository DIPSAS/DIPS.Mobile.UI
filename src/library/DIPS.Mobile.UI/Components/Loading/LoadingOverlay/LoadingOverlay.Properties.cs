using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Loading.LoadingOverlay;

public partial class LoadingOverlay
{
    public View? Content
    {
        get => (View?)GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }

    public bool IsBusy
    {
        get => (bool)GetValue(IsBusyProperty);
        set => SetValue(IsBusyProperty, value);
    }

    /// <summary>
    /// Sets the text to be displayed in the label beside the ActivityIndicator.
    /// </summary>
    /// <remarks>If empty, the ActivityIndicator will be centered</remarks>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    /// <summary>
    /// Sets the color of the ActivityIndicator and the text color of the label.
    /// </summary>
    public Color ContentColor
    {
        get => (Color)GetValue(ContentColorProperty);
        set => SetValue(ContentColorProperty, value);
    }

    /// <summary>
    /// Sets the color of the overlay background.
    /// </summary>
    public Color OverlayColor
    {
        get => (Color)GetValue(OverlayColorProperty);
        set => SetValue(OverlayColorProperty, value);
    }

    /// <summary>
    /// The value used to fade out the content when the overlay is active. Default value is 0.5
    /// </summary>
    /// <remarks>A value of 0, means that the original content will be completely faded out</remarks>
    public double ContentFadeOutValue
    {
        get => (double)GetValue(ContentFadeOutValueProperty);
        set => SetValue(ContentFadeOutValueProperty, value);
    }
    
    public static readonly BindableProperty ContentFadeOutValueProperty = BindableProperty.Create(
        nameof(ContentFadeOutValue),
        typeof(double),
        typeof(LoadingOverlay),
        0.5);
    
    public static readonly BindableProperty OverlayColorProperty = BindableProperty.Create(
        nameof(OverlayColor),
        typeof(Color),
        typeof(LoadingOverlay),
        defaultValue: Colors.GetColor(ColorName.color_fill_default));
    
    public static readonly BindableProperty ContentColorProperty = BindableProperty.Create(
        nameof(ContentColor),
        typeof(Color),
        typeof(LoadingOverlay),
        defaultValue: Colors.GetColor(ColorName.color_text_subtle));
    
    public static readonly BindableProperty TextProperty = BindableProperty.Create(
        nameof(Text),
        typeof(string),
        typeof(LoadingOverlay));
    
    public static readonly BindableProperty IsBusyProperty = BindableProperty.Create(
        nameof(IsBusy),
        typeof(bool),
        typeof(LoadingOverlay),
        propertyChanged: (bindable, _, _) => _ = ((LoadingOverlay)bindable).OnIsBusyChanged());
    
    public static readonly BindableProperty ContentProperty = BindableProperty.Create(
        nameof(Content),
        typeof(View),
        typeof(LoadingOverlay));
}