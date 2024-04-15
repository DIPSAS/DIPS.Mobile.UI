using DIPS.Mobile.UI.Components.VirtualListView.Adapters;

namespace Playground.VetleSamples;

public class TestAdapter : VirtualListViewAdapterBase<object, DummyClass>
{
    private DummyClass? m_dummyClass;

    public TestAdapter(DummyClass dummyClass)
    {
        m_dummyClass = dummyClass;
    }
    
    public void SetData(DummyClass dummyClass)
    {
        m_dummyClass = dummyClass;
        
        InvalidateData();
    }
    
    public override DummyClass GetItem(int sectionIndex, int itemIndex)
    {
        return m_dummyClass ?? new DummyClass([]);
    }

    public override int GetNumberOfItemsInSection(int sectionIndex)
    {
        return 1;
    }
}