using System.ComponentModel;
using System.Windows.Input;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
    }

    public ICommand NavigateCommand => new Command<string>(async s =>
    {
        App.Current.MainPage.Navigation.PushAsync(new HåvardPage());
    });

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        
    }

}