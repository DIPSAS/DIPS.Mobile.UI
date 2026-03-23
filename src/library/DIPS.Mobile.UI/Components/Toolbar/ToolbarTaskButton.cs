namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// A toolbar button that represents an asynchronous task with busy, finished, and error states.
/// Extends <see cref="ToolbarButton"/> with task lifecycle state management.
/// </summary>
public partial class ToolbarTaskButton : ToolbarButton
{
    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (HandleError is not null)
        {
            SetInheritedBindingContext(HandleError, BindingContext);
        }
    }
}
