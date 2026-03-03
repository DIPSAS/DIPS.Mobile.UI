using System.Collections.ObjectModel;

namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// A cross-platform toolbar that displays a horizontal bar of icon buttons.
/// Set this on <see cref="Pages.ContentPage.BottomToolbar"/> to display a bottom toolbar.
/// </summary>
/// <remarks>
/// On iOS the toolbar is rendered by the UINavigationController's built-in bottom toolbar,
/// which provides native Liquid Glass on iOS 26+.
/// On Android a Material 3 Bottom App Bar is injected at the bottom of the page.
/// </remarks>
[ContentProperty(nameof(Buttons))]
public class Toolbar : Element
{
    public static readonly BindableProperty ButtonsProperty = BindableProperty.Create(
        nameof(Buttons),
        typeof(IList<ToolbarButton>),
        typeof(Toolbar),
        defaultValueCreator: _ => new ObservableCollection<ToolbarButton>());

    /// <summary>
    /// The buttons displayed in the toolbar.
    /// </summary>
    public IList<ToolbarButton> Buttons
    {
        get => (IList<ToolbarButton>)GetValue(ButtonsProperty);
        set => SetValue(ButtonsProperty, value);
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
