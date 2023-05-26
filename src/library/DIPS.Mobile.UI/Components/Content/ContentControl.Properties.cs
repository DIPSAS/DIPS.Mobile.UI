namespace DIPS.Mobile.UI.Components.Content;

public partial class ContentControl
{
    /// <summary>
    /// The template selector used to select a template. The item passed to the <see cref="TemplateSelector.SelectTemplate"/> is <see cref="SelectorItem"/> or BindingContext if <see cref="SelectorItem"/> is not set. 
    /// </summary>
    public DataTemplateSelector? TemplateSelector { get; set; }


    /// <summary>
    ///  <see cref="SelectorItem" />
    /// </summary>
    public static readonly BindableProperty SelectorItemProperty = BindableProperty.Create(
        nameof(SelectorItem),
        typeof(object),
        typeof(ContentControl),
        null,
        BindingMode.OneWay,
        propertyChanged: (s, o, n) => ((ContentControl)s).UpdateContent());

    /// <summary>
    /// Used as the item for the <see cref="TemplateSelector"/> when selecting an item
    /// </summary>
    public object? SelectorItem
    {
        get => (object)GetValue(SelectorItemProperty);
        set => SetValue(SelectorItemProperty, value);
    }
}