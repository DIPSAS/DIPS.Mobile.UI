using DIPS.Mobile.UI.Components.Labels;
using CollectionView = DIPS.Mobile.UI.Components.Lists.CollectionView;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace MemoryLeakTests.Tests;

public class CollectionViewTest : UITest
{
    public override void BeforeTest(ContentPage contentPage)
    {
        var collectionView = new CollectionView
        {
            ItemsSource = new List<string> { "Item 1", "Item 2", "Item 3" },
            ItemTemplate = new DataTemplate(() => new Label { Text = "Item" })
        };

        contentPage.Content = collectionView;
    }

    public override string Name => "CollectionView";
}
