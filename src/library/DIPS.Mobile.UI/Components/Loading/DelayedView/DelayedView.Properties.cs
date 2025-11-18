namespace DIPS.Mobile.UI.Components.Loading.DelayedView;

public partial class DelayedView
{
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
        nameof(ItemTemplate),
        typeof(DataTemplate),
        typeof(DelayedView));

    public static readonly BindableProperty SecondsUntilRenderProperty = BindableProperty.Create(
        nameof(SecondsUntilRender),
        typeof(float),
        typeof(DelayedView),
        defaultValue: .6f);
    
    /// <summary>
    /// The template used to render the view after the delay
    /// </summary>
    public DataTemplate? ItemTemplate
    {
        get => (DataTemplate?)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    /// <summary>
    /// How many seconds to wait until rendering the view
    /// </summary>
    public float SecondsUntilRender
    {
        get => (float)GetValue(SecondsUntilRenderProperty);
        set => SetValue(SecondsUntilRenderProperty, value);
    }

    /// <summary>
    /// Invoked when the view has been rendered
    /// </summary>
    public event EventHandler<EventArgs>? OnRendered;
}