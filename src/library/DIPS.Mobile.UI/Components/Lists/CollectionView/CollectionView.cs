using SearchBar = DIPS.Mobile.UI.Components.Searching.SearchBar;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView : Microsoft.Maui.Controls.CollectionView
{
    private readonly Border m_extraSpaceBorder;
    
    private readonly List<WeakReference<VisualElement>> m_inputFields = [];

    public CollectionView()
    {
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        //Adds a extra space in the bottom to make sure the last item is not placed at the very bottom of the page, this makes the last item more accessible for people.
        m_extraSpaceBorder ??= new Border() {BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent};
        Footer = m_extraSpaceBorder;
        SelectionMode = SelectionMode.None;
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
            return;

        if (!RemoveFocusOnScroll)
            return;

        var page = this.FindParentOfType<ContentPage>();
        RetrieveInputFields(page);
    }

    private void RetrieveInputFields(IVisualTreeElement? visualTreeElement)
    {
        foreach (var child in visualTreeElement?.GetVisualTreeDescendants() ?? [])
        {
            if(Equals(child, visualTreeElement))
                continue;
            
            switch (child)
            {
                case InputView editor:
                    m_inputFields.Add(new WeakReference<VisualElement>(editor));
                    break;
                case SearchBar searchBar:
                    m_inputFields.Add(new WeakReference<VisualElement>(searchBar));
                    break;
            }
            
            RetrieveInputFields(child);
        }
    }
    
    protected override void OnScrolled(ItemsViewScrolledEventArgs e)
    {
        base.OnScrolled(e);

#if __ANDROID__ 
        // OnScrolled gets kicked off when you change the collections item source for some reason, so we have to detect if its a scroll or not
        if (Handler is CollectionViewHandler {PlatformView.ScrollState: 0}) 
            return; //0 is idle
#endif
        
        if (!RemoveFocusOnScroll)
            return;

        foreach (var inputFieldReference in m_inputFields)
        {
            if (inputFieldReference.TryGetTarget(out var inputField))
            {
                if(inputField is SearchBar searchBar)
                    searchBar.Unfocus();
                else
                    inputField.Unfocus();
            }
        }
    }

    private void TrySetItemSpacing()
    {
        var oldItemsLayout = ItemsLayout;
        ItemsLayout = oldItemsLayout switch
        {
            LinearItemsLayout linearItemsLayout => new LinearItemsLayout(linearItemsLayout.Orientation)
            {
                ItemSpacing = ItemSpacing
            },
            GridItemsLayout gridItemsLayout => new GridItemsLayout(gridItemsLayout.Span, gridItemsLayout.Orientation)
            {
                HorizontalItemSpacing = ItemSpacing, VerticalItemSpacing = ItemSpacing,
            },
            _ => null
        };
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (HasAdditionalSpaceAtTheEnd && Footer == m_extraSpaceBorder)
        {
            AddExtraSpaceAtTheEnd();
        }
    }

    private void AddExtraSpaceAtTheEnd()
    {
        m_extraSpaceBorder.HeightRequest = Height/2; //The border has to be half the visible size
    }
}