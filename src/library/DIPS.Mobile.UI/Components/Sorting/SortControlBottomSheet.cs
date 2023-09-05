using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Effects.Touch;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;

namespace DIPS.Mobile.UI.Components.Sorting;

internal class SortControlBottomSheet : BottomSheet
{
    private readonly SortControl m_sortControl;
    private readonly SortOrderButton m_ascendingButton;
    private readonly SortOrderButton m_descendingButton;

    private readonly CollectionView m_collectionView = new();

    public SortControlBottomSheet(SortControl sortControl)
    {
        m_sortControl = sortControl;
        
        Padding = Sizes.GetSize(SizeName.size_4);
        
        var contentGrid = new Grid
        {
            RowDefinitions = new RowDefinitionCollection
            {
                new(GridLength.Auto), 
                new(GridLength.Star),
                new(GridLength.Auto)
            }
        };

        var sortOrderButtonsGrid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection 
            { 
                new(GridLength.Star), 
                new(GridLength.Star) 
            },
            ColumnSpacing = Sizes.GetSize(SizeName.size_3)
        };

        m_ascendingButton = new SortOrderButton(SortOrder.Ascending, sortControl)
        {
            HorizontalOptions = LayoutOptions.End
        };
        
        m_descendingButton = new SortOrderButton(SortOrder.Descending, sortControl)
        {
            HorizontalOptions = LayoutOptions.Start
        };
        
        Touch.SetCommand(m_ascendingButton, new Command(() => SetCurrentSortOrder(SortOrder.Ascending)));
        Touch.SetCommand(m_descendingButton, new Command(() => SetCurrentSortOrder(SortOrder.Descending)));
        
        sortOrderButtonsGrid.Add(m_ascendingButton);
        sortOrderButtonsGrid.Add(m_descendingButton, 1);
        
        
        m_collectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(SortControl.ItemsSource), source: m_sortControl));

        m_collectionView.ItemTemplate = new DataTemplate(LoadTemplate);
        
        contentGrid.Add(sortOrderButtonsGrid);
        contentGrid.Add(m_collectionView, 0, 1);
        
        Content = contentGrid;
    }

    private object LoadTemplate()
    {
        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new(GridLength.Auto), new(GridLength.Star)
            },
            ColumnSpacing = Sizes.GetSize(SizeName.size_1)
        };

        var radioButton = new RadioButton
        {
            VerticalOptions = LayoutOptions.Center,
            
        };

        var label = new Label { VerticalTextAlignment = TextAlignment.Center };
        label.SetBinding(Label.TextProperty, new Binding { Path = m_sortControl.ItemDisplayProperty });
        
        grid.Add(radioButton);
        grid.Add(label, 1);
        
        return grid;
    }

    private void SetCurrentSortOrder(SortOrder sortOrder)
    {
        m_sortControl.CurrentSortOrder = sortOrder;
        
        m_ascendingButton.UpdateLayout();
        m_descendingButton.UpdateLayout();
    }
   
}