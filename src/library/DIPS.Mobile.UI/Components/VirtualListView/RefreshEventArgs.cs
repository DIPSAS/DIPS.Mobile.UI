namespace DIPS.Mobile.UI.Components.VirtualListView;

public class RefreshEventArgs : EventArgs
{
    public RefreshEventArgs(Action completion)
        : base()
    {
        Complete = completion;
    }

    public readonly Action Complete;
}