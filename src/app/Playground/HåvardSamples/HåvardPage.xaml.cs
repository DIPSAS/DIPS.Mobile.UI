using System.ComponentModel;
using System.Windows.Input;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
    }

    public ICommand SearchCommand => new Command<string>(async s =>
    {
    });

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        
    }

}