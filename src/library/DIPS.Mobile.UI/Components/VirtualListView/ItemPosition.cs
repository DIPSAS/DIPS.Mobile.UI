namespace DIPS.Mobile.UI.Components.VirtualListView;

public struct ItemPosition : IEquatable<ItemPosition>
{
    public ItemPosition(int sectionIndex = 0, int itemIndex = 0)
    {
        SectionIndex = sectionIndex;
        ItemIndex = itemIndex;
    }

    public int SectionIndex { get; }
    public int ItemIndex { get; }

    public bool Equals(ItemPosition other)
        => SectionIndex == other.SectionIndex && ItemIndex == other.ItemIndex;
}