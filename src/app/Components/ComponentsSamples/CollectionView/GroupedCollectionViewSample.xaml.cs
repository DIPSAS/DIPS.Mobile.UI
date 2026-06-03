namespace Components.ComponentsSamples.CollectionView;

public partial class GroupedCollectionViewSample
{
    public GroupedCollectionViewSample()
    {
        InitializeComponent();
    }

    private void OnCollectionViewFeatureToggled(object? sender, ToggledEventArgs e)
    {
        Dispatcher.Dispatch(TheCollectionView.ReloadData);
    }
}
