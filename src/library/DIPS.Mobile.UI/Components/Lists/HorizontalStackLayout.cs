namespace DIPS.Mobile.UI.Components.Lists;

public partial class HorizontalStackLayout : Microsoft.Maui.Controls.HorizontalStackLayout
{
    public HorizontalStackLayout()
    {
        Unloaded += Dispose;
    }

    private void Dispose(object? sender, EventArgs e)
    {
        Unloaded -= Dispose;
        foreach (var child in Children)
        {
            if (child is View view)
            {
                view.SizeChanged -= OnChildSizeChanged;
            }
        }
    }
    
    protected override void OnChildAdded(Element child)
    {
        base.OnChildAdded(child);
        if (child is View view)
        {
            view.SizeChanged += OnChildSizeChanged;
        }
    }

    private void OnChildSizeChanged(object? sender, EventArgs e)
    {
        if (sender is not View view) return;
        if (view.Parent is not View parentView) return;
        var trailingXPosition = view.X + view.Width;
        if (trailingXPosition > parentView.Width)
        {
            ChildOutOfBounds?.Invoke(sender, EventArgs.Empty);
        }
    }

    protected override void OnChildRemoved(Element child, int oldLogicalIndex)
    {
        base.OnChildRemoved(child, oldLogicalIndex);
        
        if (child is View view)
        {
            view.SizeChanged -= OnChildSizeChanged;
        }
        
    }
}