using System.Collections.ObjectModel;

namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// A cross-platform toolbar that displays grouped action buttons.
/// On iOS the toolbar uses a native UIToolbar. On Android a Material 3 toolbar.
/// Attach to a page via <see cref="Pages.ContentPage.BottomToolbar"/>.
/// </summary>
[ContentProperty(nameof(Groups))]
public class Toolbar : View
{
    public static readonly BindableProperty GroupsProperty = BindableProperty.Create(
        nameof(Groups),
        typeof(IList<ToolbarGroup>),
        typeof(Toolbar),
        defaultValueCreator: _ => new ObservableCollection<ToolbarGroup>());

    public static readonly BindableProperty HorizontalAlignmentProperty = BindableProperty.Create(
        nameof(HorizontalAlignment),
        typeof(ToolbarHorizontalAlignment),
        typeof(Toolbar),
        defaultValue: ToolbarHorizontalAlignment.Center);

    /// <summary>
    /// The groups of buttons displayed in the toolbar. Groups are separated visually.
    /// </summary>
    public IList<ToolbarGroup> Groups
    {
        get => (IList<ToolbarGroup>)GetValue(GroupsProperty);
        set => SetValue(GroupsProperty, value);
    }

    /// <summary>
    /// Controls how the toolbar is positioned horizontally. The toolbar is always compact (sized to content).
    /// </summary>
    public ToolbarHorizontalAlignment HorizontalAlignment
    {
        get => (ToolbarHorizontalAlignment)GetValue(HorizontalAlignmentProperty);
        set => SetValue(HorizontalAlignmentProperty, value);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (Groups is null)
            return;

        foreach (var group in Groups)
        {
            SetInheritedBindingContext(group, BindingContext);
        }
    }
}
