namespace DIPS.Mobile.UI.Components.Slidable;

public partial class SlidableContentLayout
{
    /// <summary>
    /// <see cref="BindingContextFactory"/>
    /// </summary>
    public static readonly BindableProperty BindingContextFactoryProperty = BindableProperty.Create(
        nameof(BindingContextFactory),
        typeof(Func<int, object>),
        typeof(SlidableLayout),
        propertyChanged: (s, e, n) => ((SlidableContentLayout)s).ResetAll());

    /// <summary>
    /// Factory used to create instaces of the viewmodels scrolled between. Takes an int and returns an object.
    /// </summary>
    public Func<int, object> BindingContextFactory
    {
        get { return (Func<int, object>)GetValue(BindingContextFactoryProperty); }
        set { SetValue(BindingContextFactoryProperty, value); }
    }

    /// <summary>
    /// <see cref="ItemTemplate"/>
    /// </summary>
    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(
        nameof(ItemTemplate),
        typeof(DataTemplate),
        typeof(SlidableLayout),
        propertyChanged: (s, e, n) => ((SlidableContentLayout)s).ResetAll());


    /// <summary>
    /// Template used in creating each item
    /// </summary>
    public DataTemplate ItemTemplate
    {
        get => (DataTemplate)GetValue(ItemTemplateProperty);
        set => SetValue(ItemTemplateProperty, value);
    }

    /// <summary>
    /// Invoked when people tap content in the layout
    /// </summary>
    public event EventHandler<ContentTappedEventArgs>? ContentTapped;

    /// <summary>
    /// Indicates if items should be scaled down when getting further away from the center.
    /// </summary>
    public bool ScaleDown { get; set; } = true;
}