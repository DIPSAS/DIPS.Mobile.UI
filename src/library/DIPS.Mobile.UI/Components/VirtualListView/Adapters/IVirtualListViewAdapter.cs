namespace DIPS.Mobile.UI.Components.VirtualListView.Adapters;

public interface IVirtualListViewAdapter
{
    int GetNumberOfSections();

    object GetSection(int sectionIndex);

    int GetNumberOfItemsInSection(int sectionIndex);

    object GetItem(int sectionIndex, int itemIndex);

    event EventHandler<InvalidateTypeEventArgs> OnDataInvalidated;

    void InvalidateData();
    void InvalidateItem(int sectionIndex, int itemIndex);
    void InvalidateSection(int sectionIndex);
}

public class InvalidateTypeEventArgs : EventArgs
{
    public InvalidateTypeEventArgs()
    {
        InvalidateType = InvalidateType.Data;
    }

    public InvalidateTypeEventArgs(int sectionIndex)
    {
        SectionIndex = sectionIndex;
        InvalidateType = InvalidateType.Section;
    }

    public InvalidateTypeEventArgs(int sectionIndex, int itemIndex)
    {
        SectionIndex = sectionIndex;
        ItemIndex = itemIndex;
        InvalidateType = InvalidateType.Item;
    }

    public InvalidateType InvalidateType { get; }
    
    public int SectionIndex { get; }
    public int ItemIndex { get; }
   

}

public enum InvalidateType
{
    Data,
    Item,
    Section
}