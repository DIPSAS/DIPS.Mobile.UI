namespace DIPS.Mobile.UI.Components.VirtualListView.Adapters;

public abstract class VirtualListViewAdapterBase<TSection, TItem> : IVirtualListViewAdapter
{
    public virtual int GetNumberOfSections() => 1;

    public event EventHandler<InvalidateTypeEventArgs>? OnDataInvalidated;

    public virtual void InvalidateData()
    {
        OnDataInvalidated?.Invoke(this, new InvalidateTypeEventArgs());
    }

    public virtual void InvalidateItem(int sectionIndex, int itemIndex)
    {
        OnDataInvalidated?.Invoke(this, new InvalidateTypeEventArgs(sectionIndex, itemIndex));
    }

    public virtual void InvalidateSection(int sectionIndex)
    {
        OnDataInvalidated?.Invoke(this, new InvalidateTypeEventArgs(sectionIndex));
    }

    public abstract TItem GetItem(int sectionIndex, int itemIndex);

    public abstract int GetNumberOfItemsInSection(int sectionIndex);

    public virtual TSection GetSection(int sectionIndex)
        => default;

    object IVirtualListViewAdapter.GetItem(int sectionIndex, int itemIndex)
        => GetItem(sectionIndex, itemIndex);

    object IVirtualListViewAdapter.GetSection(int sectionIndex)
        => GetSection(sectionIndex);

    void IVirtualListViewAdapter.InvalidateData()
        => InvalidateData();
}