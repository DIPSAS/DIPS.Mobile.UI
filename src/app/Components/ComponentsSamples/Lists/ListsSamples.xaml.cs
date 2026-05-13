using System.Windows.Input;

namespace Components.ComponentsSamples.Lists;

public partial class ListsSamples
{
    public ListsSamples()
    {
        NavigateToCollectionViewCommand = new Command(async () =>
            await Shell.Current.Navigation.PushAsync(new CollectionViewSamples()));
        NavigateToScrollViewCommand = new Command(async () =>
            await Shell.Current.Navigation.PushAsync(new ScrollViewSamples()));
        
        BindingContext = this;
        InitializeComponent();
    }

    public ICommand NavigateToCollectionViewCommand { get; }
    public ICommand NavigateToScrollViewCommand { get; }
}
