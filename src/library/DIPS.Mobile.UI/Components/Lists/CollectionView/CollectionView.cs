using DIPS.Mobile.UI.Resources.Sizes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Lists;

public partial class CollectionView : Microsoft.Maui.Controls.CollectionView
{
    private readonly Border m_extraSpaceBorder;

    public CollectionView()
    {
        BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent;
        //Adds a extra space in the bottom to make sure the last item is not placed at the very bottom of the page, this makes the last item more accessible for people.
        m_extraSpaceBorder ??= new Border() {BackgroundColor = Microsoft.Maui.Graphics.Colors.Transparent};
        Footer = m_extraSpaceBorder;
        SelectionMode = SelectionMode.None;

        Scrolled += OnScrolled;
    }

    private double m_headerOffset;
    
    private void OnScrolled(object? sender, ItemsViewScrolledEventArgs e)
    {
        if (HeaderPositioning is HeaderPositioning.Normal || Header is not VisualElement view) return;

        view.TranslationY = Math.Max(TranslationY, e.VerticalOffset + m_headerOffset);

        if (HeaderPositioning is not HeaderPositioning.PartialSticky) return;
        
        if (this.AnimationIsRunning("PopBackIn")) return;
        
        if (e.VerticalDelta > 0)
        {
            m_headerOffset = Math.Max(-view.Height, m_headerOffset - e.VerticalDelta);
        }
        if (e.VerticalDelta < 0 && m_headerOffset < 0)
        {
            PopBackIn(view);
        }
    }
    
    private void PopBackIn(VisualElement view)
    {
        var animation = new Animation(d => m_headerOffset = d, -view.Height, 0);
        animation.Commit(this, "PopBackIn", 4, 250, Easing.Linear);
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