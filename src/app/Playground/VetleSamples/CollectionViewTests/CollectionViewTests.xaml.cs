namespace Playground.VetleSamples.CollectionViewTests;

public partial class CollectionViewTests
{
    public CollectionViewTests()
    {
        InitializeComponent();
    }

    private void RegularCollectionView(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new RegularCollectionView());
    }

    private void GroupedCollectionView(object sender, EventArgs e)
    {
        Shell.Current.Navigation.PushAsync(new GroupedCollectionView());
    }
}