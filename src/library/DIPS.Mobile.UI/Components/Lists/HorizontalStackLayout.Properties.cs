namespace DIPS.Mobile.UI.Components.Lists;

public partial class HorizontalStackLayout
{
    /// <summary>
    /// Event that is invoked when a child is added to the stack but the child is out of bounds of the stacks size in the page.
    /// </summary>
    public event EventHandler<object>? ChildOutOfBounds;
}