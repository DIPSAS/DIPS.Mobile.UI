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

    private void DeviceDisplayOnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        
    }

    public override string Name => "Nested CollectionView";
}

public abstract class UITest
{
    public abstract void BeforeTest(ContentPage contentPage);

    public abstract string Name { get; }

    public ContentPage CreatePage()
    {
        var uiTestContentPage = new UITestContentPage();
        uiTestContentPage.AutomationId = Name;
        uiTestContentPage.Title = Name;
        BeforeTest(uiTestContentPage);
        return uiTestContentPage;
    }
}

public class UITestContentPage : ContentPage
{
    public UITestContentPage()
    {
        // DeviceDisplay.MainDisplayInfoChanged += DeviceDisplayOnMainDisplayInfoChanged;
    }

    private void DeviceDisplayOnMainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        
    }
}