namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class Toolbar
{
    /// <summary>
    /// <see cref="Buttons"/>
    /// </summary>
    public static readonly BindableProperty ButtonsProperty = BindableProperty.Create(
        nameof(Buttons),
        typeof(IList<ToolbarButton>),
        typeof(Toolbar),
        defaultValueCreator: _ => new List<ToolbarButton>(),
        propertyChanged: (bindable, _, _) => ((Toolbar)bindable).OnButtonsChanged());

    /// <summary>
    /// The buttons to display in the toolbar.
    /// </summary>
    public IList<ToolbarButton> Buttons
    {
        get => (IList<ToolbarButton>)GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }
}
