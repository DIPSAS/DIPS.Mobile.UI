using DIPS.Mobile.UI.Components.Loading.StateView;
using DIPS.Mobile.UI.Effects.Touch;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace MemoryLeakTests.Tests;

public class CollectionViewTests : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        var grid = new Grid();
        Touch.SetCommand(grid, new Command(() => {}));
        Touch.SetIsEnabled(grid, true);
        var collectionView = new CollectionView(){AutomationId = "MyCollectionView"};
        collectionView.EmptyView = new EmptyView(){Content = new Label(){Text = "test"}};
        var footerGrid = new Grid();
        footerGrid.Add(new Label(){Text = "Testiiing"});
        collectionView.FooterTemplate = new DataTemplate(() => footerGrid);
        collectionView.ItemsSource = new List<string>() {"Item 1", "Item 2", "Item 3", "Item 4"};
        grid.Add(collectionView);
        
        contentPage.Content = grid;
    }

    public override string Name => "Nested CollectionView";
}