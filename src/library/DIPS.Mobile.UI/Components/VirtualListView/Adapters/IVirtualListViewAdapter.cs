namespace DIPS.Mobile.UI.Components.VirtualListView.Adapters;

public interface IVirtualListViewAdapter
{
    int GetNumberOfSections();

    object GetSection(int sectionIndex);

    int GetNumberOfItemsInSection(int sectionIndex);

    object GetItem(int sectionIndex, int itemIndex);

    event EventHandler OnDataInvalidated;

    void InvalidateData();
}