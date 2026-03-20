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
    /// Also subscribes to ToolbarTaskButton state changes and ToolbarTaskError.HasError changes.
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

                if (button is ToolbarTaskButton taskButton)
                {
                    SubscribeToErrorPropertyChanges(taskButton);
                }
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

                if (button is ToolbarTaskButton taskButton)
                {
                    UnsubscribeFromErrorPropertyChanges(taskButton);
                }
            }
        }
    }

    private void SubscribeToErrorPropertyChanges(ToolbarTaskButton taskButton)
    {
        if (taskButton.Error is not null)
        {
            taskButton.Error.PropertyChanged -= OnToolbarTaskErrorPropertyChanged;
            taskButton.Error.PropertyChanged += OnToolbarTaskErrorPropertyChanged;
        }
    }

    private void UnsubscribeFromErrorPropertyChanges(ToolbarTaskButton taskButton)
    {
        if (taskButton.Error is not null)
        {
            taskButton.Error.PropertyChanged -= OnToolbarTaskErrorPropertyChanged;
        }
    }

    private void OnToolbarTaskErrorPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(ToolbarTaskError.HasError))
            return;

        if (sender is not ToolbarTaskError error)
            return;

        // Find the ToolbarTaskButton that owns this error
        if (VirtualView is null)
            return;

        foreach (var group in VirtualView.Groups)
        {
            foreach (var button in group.Items)
            {
                if (button is ToolbarTaskButton taskButton && taskButton.Error == error)
                {
                    OnToolbarTaskButtonStateChanged(taskButton);
                    return;
                }
            }
        }
    }

    private void OnToolbarButtonPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (sender is not ToolbarButton toolbarButton)
            return;

        switch (e.PropertyName)
        {
            case nameof(ToolbarButton.IsVisible):
                OnToolbarButtonVisibilityChanged(toolbarButton);
                break;
            case nameof(ToolbarTaskButton.IsBusy):
            case nameof(ToolbarTaskButton.IsFinished):
                if (sender is ToolbarTaskButton taskButton)
                {
                    OnToolbarTaskButtonStateChanged(taskButton);
                }
                break;
            case nameof(ToolbarTaskButton.Error):
                if (sender is ToolbarTaskButton taskButtonForError)
                {
                    // Re-subscribe to the new Error object
                    UnsubscribeFromErrorPropertyChanges(taskButtonForError);
                    SubscribeToErrorPropertyChanges(taskButtonForError);
                    OnToolbarTaskButtonStateChanged(taskButtonForError);
                }
                break;
        }
    }

    /// <summary>
    /// Called when a single button's IsVisible changes. Platform handlers implement this
    /// to incrementally add/remove just that one item without rebuilding the entire toolbar.
    /// </summary>
    partial void OnToolbarButtonVisibilityChanged(ToolbarButton toolbarButton);

    /// <summary>
    /// Called when a ToolbarTaskButton's IsBusy, IsFinished, or Error.HasError changes.
    /// Platform handlers implement this to swap the button view for the appropriate state indicator.
    /// </summary>
    partial void OnToolbarTaskButtonStateChanged(ToolbarTaskButton toolbarTaskButton);

    /// <summary>
    /// Animates the toolbar into view by sliding it up from below.
    /// </summary>
    internal partial void AnimateShow();

    /// <summary>
    /// Animates the toolbar out of view by sliding it down off-screen.
    /// </summary>
    internal partial void AnimateHide();
}
