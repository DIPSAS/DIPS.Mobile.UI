namespace DIPS.Mobile.UI.Components.Lists;

public partial class ScrollView : Microsoft.Maui.Controls.ScrollView
{
    private bool m_hasAddedSpaceToBottom;
    
    private readonly List<WeakReference<VisualElement>> m_inputFields = [];

    public ScrollView()
    {
#if __ANDROID__ //Not possible to set padding on scroll view after its rendered
        AdjustPadding(AndroidAdditionalSpaceAtEnd);
#endif
        
        this.Scrolled += OnScrolled;
    }

#if __IOS__
    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        AdjustPadding(height);
    }
#endif

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
        {
            this.Scrolled -= OnScrolled;
            return;
        }
        
        if(!RemoveFocusOnScroll)
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
                case Searching.SearchBar searchBar:
                    m_inputFields.Add(new WeakReference<VisualElement>(searchBar));
                    break;
            }
            
            RetrieveInputFields(child);
        }
    }
    
    private void OnScrolled(object? sender, ScrolledEventArgs e)
    {
        if (!RemoveFocusOnScroll)
            return;

        foreach (var inputFieldReference in m_inputFields)
        {
            if (inputFieldReference.TryGetTarget(out var inputField))
            {
                if(inputField is Searching.SearchBar searchBar)
                    searchBar.Unfocus();
                else
                    inputField.Unfocus();
            }
        }
    }

    private void AdjustPadding(double height)
    {
        var oldPadding = Padding;
        if (height > 0 && !m_hasAddedSpaceToBottom && HasAdditionalSpaceAtTheEnd)
        {
            m_hasAddedSpaceToBottom = true;
            var newPadding = new Thickness(oldPadding.Left, oldPadding.Top, oldPadding.Right,
                (int)(oldPadding.Bottom + (height / 2)));
            Padding = newPadding;
        } 
    }
}