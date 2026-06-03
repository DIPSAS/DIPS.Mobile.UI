namespace Components.ComponentsSamples.CollectionView;

public partial class FlatCollectionViewSample
{
    public FlatCollectionViewSample()
    {
        InitializeComponent();
    }

    private void OnCollectionViewFeatureToggled(object? sender, ToggledEventArgs e)
    {
        Dispatcher.Dispatch(TheCollectionView.ReloadData);
    }
}
