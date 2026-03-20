namespace DIPS.Mobile.UI.Components.Toolbar;

public partial class ToolbarHandler
{
    public ToolbarHandler() : base(PropertyMapper)
    {
    }

    public static IPropertyMapper<Toolbar, ToolbarHandler> PropertyMapper =
        new PropertyMapper<Toolbar, ToolbarHandler>
        {
            [nameof(Toolbar.Groups)] = MapGroups,
            [nameof(Toolbar.HorizontalAlignment)] = MapHorizontalAlignment,
        };

    private static partial void MapGroups(ToolbarHandler handler, Toolbar toolbar);
    private static partial void MapHorizontalAlignment(ToolbarHandler handler, Toolbar toolbar);

    /// <summary>
    /// Subscribe to IsVisible changes on all toolbar buttons for incremental add/remove.
    /// </summary>
    protected void SubscribeToItemPropertyChanges()
    {
        if (VirtualView is null)
            return;

        foreach (var group in VirtualView.Groups)
        {
            foreach (var button in group.Items)
            {
                button.PropertyChanged -= OnToolbarButtonPropertyChanged;
                button.PropertyChanged += OnToolbarButtonPropertyChanged;
            }
        }
    }

    /// <summary>
    /// Unsubscribe from all toolbar button property changes.
    /// </summary>
    protected void UnsubscribeFromItemPropertyChanges()
    {
        if (VirtualView is null)
            return;

        foreach (var group in VirtualView.Groups)
        {
            foreach (var button in group.Items)
            {
                button.PropertyChanged -= OnToolbarButtonPropertyChanged;
            }
        }
    }

    private void OnToolbarButtonPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ToolbarButton.IsVisible) && sender is ToolbarButton toolbarButton)
        {
            OnToolbarButtonVisibilityChanged(toolbarButton);
        }
    }

    /// <summary>
    /// Called when a single button's IsVisible changes. Platform handlers implement this
    /// to incrementally add/remove just that one item without rebuilding the entire toolbar.
    /// </summary>
    partial void OnToolbarButtonVisibilityChanged(ToolbarButton toolbarButton);

    /// <summary>
    /// Animates the toolbar into view by sliding it up from below.
    /// </summary>
    internal partial void AnimateShow();

    /// <summary>
    /// Animates the toolbar out of view by sliding it down off-screen.
    /// </summary>
    internal partial void AnimateHide();
}
