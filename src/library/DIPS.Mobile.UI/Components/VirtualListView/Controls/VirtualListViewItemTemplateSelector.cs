namespace DIPS.Mobile.UI.Components.VirtualListView.Controls;

public abstract class VirtualListViewItemTemplateSelector
{
    public abstract DataTemplate SelectTemplate(object item, int sectionIndex, int itemIndex, BindableObject container);
}

public abstract class VirtualListViewSectionTemplateSelector
{
    public abstract DataTemplate SelectTemplate(object section, int sectionIndex, BindableObject container);
}