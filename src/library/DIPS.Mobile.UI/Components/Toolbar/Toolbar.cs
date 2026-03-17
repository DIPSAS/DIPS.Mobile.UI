using System.Collections.ObjectModel;

namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// A cross-platform toolbar that displays a floating glass capsule with icon buttons.
/// </summary>
/// <remarks>
/// On iOS the toolbar renders a glass capsule using UIGlassEffect (Liquid Glass on iOS 26+).
/// On Android a Material 3 styled toolbar is displayed.
/// Place this view at the bottom of your page layout (e.g. inside a Grid with VerticalOptions="End").
/// </remarks>
[ContentProperty(nameof(Buttons))]
public class Toolbar : View
{
    public static readonly BindableProperty ButtonsProperty = BindableProperty.Create(
        nameof(Buttons),
        typeof(IList<ToolbarButton>),
        typeof(Toolbar),
        defaultValueCreator: _ => new ObservableCollection<ToolbarButton>());

    public static readonly BindableProperty HorizontalAlignmentProperty = BindableProperty.Create(
        nameof(HorizontalAlignment),
        typeof(ToolbarHorizontalAlignment),
        typeof(Toolbar),
        defaultValue: ToolbarHorizontalAlignment.Center);

    /// <summary>
    /// The buttons displayed in the toolbar.
    /// </summary>
    public IList<ToolbarButton> Buttons
    {
        get => (IList<ToolbarButton>)GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
    }

    /// <summary>
    /// Controls how the toolbar capsule is positioned horizontally.
    /// <see cref="ToolbarHorizontalAlignment.Center"/> stretches to fill the available width.
    /// <see cref="ToolbarHorizontalAlignment.Start"/> and <see cref="ToolbarHorizontalAlignment.End"/> make the capsule compact (sized to content).
    /// </summary>
    public ToolbarHorizontalAlignment HorizontalAlignment
    {
        get => (ToolbarHorizontalAlignment)GetValue(HorizontalAlignmentProperty);
        set => SetValue(HorizontalAlignmentProperty, value);
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (Buttons is null)
            return;

        foreach (var toolbarButton in Buttons)
        {
            toolbarButton.BindingContext = BindingContext;
        }
    }
}
