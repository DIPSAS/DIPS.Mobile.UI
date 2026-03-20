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

    public static readonly BindableProperty HidesOnScrollForProperty = BindableProperty.Create(
        nameof(HidesOnScrollFor),
        typeof(VisualElement),
        typeof(Toolbar),
        defaultValue: null);

    private bool m_isToolbarVisible = true;

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

    /// <summary>
    /// Set to a scrollable view (ScrollView, CollectionView) to make the toolbar automatically
    /// hide when the user scrolls down and show when scrolling up.
    /// Set to null to disable hide-on-scroll behavior.
    /// </summary>
    /// <example>
    /// <code>
    /// &lt;dui:ScrollView x:Name="scrollView" Loaded="OnScrollViewLoaded" /&gt;
    /// // In code-behind:
    /// void OnScrollViewLoaded(object? s, EventArgs e) =&gt; toolbar.HidesOnScrollFor = scrollView;
    /// </code>
    /// </example>
    public VisualElement? HidesOnScrollFor
    {
        get => (VisualElement?)GetValue(HidesOnScrollForProperty);
        set => SetValue(HidesOnScrollForProperty, value);
    }

    /// <summary>
    /// Whether the toolbar is currently shown. Use <see cref="Show"/> and <see cref="Hide"/> to animate.
    /// </summary>
    public bool IsToolbarVisible => m_isToolbarVisible;

    /// <summary>
    /// Animates the toolbar into view by sliding it up from below.
    /// </summary>
    public void Show()
    {
        if (m_isToolbarVisible)
            return;

        if (Handler is ToolbarHandler handler)
        {
            handler.AnimateShow();
        }
        
        m_isToolbarVisible = true;
    }

    /// <summary>
    /// Animates the toolbar out of view by sliding it down below the screen edge.
    /// Ideal for scroll-to-hide behavior.
    /// </summary>
    public void Hide()
    {
        if (!m_isToolbarVisible)
            return;

        if (Handler is ToolbarHandler handler)
        {
            handler.AnimateHide();
        }
        
        m_isToolbarVisible = false;
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
