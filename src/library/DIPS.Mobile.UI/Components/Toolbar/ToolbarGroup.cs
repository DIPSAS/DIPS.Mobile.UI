using System.Collections.ObjectModel;

namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// A group of toolbar buttons. Groups are separated visually in the toolbar.
/// </summary>
[ContentProperty(nameof(Items))]
public class ToolbarGroup : Element
{
    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(IList<ToolbarButton>),
        typeof(ToolbarGroup),
        defaultValueCreator: _ => new ObservableCollection<ToolbarButton>());

    /// <summary>
    /// The buttons in this group.
    /// </summary>
    public IList<ToolbarButton> Items
    {
        get => (IList<ToolbarButton>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (Items is null)
            return;

        foreach (var item in Items)
        {
            SetInheritedBindingContext(item, BindingContext);
        }
    }
}
