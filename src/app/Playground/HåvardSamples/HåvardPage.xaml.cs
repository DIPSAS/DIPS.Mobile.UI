using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Components.Chips;
using DIPS.Mobile.UI.Components.Navigation.FloatingNavigationButton;
using SkiaSharp.Extended.UI.Controls;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
    }

    public ICommand SearchCommand => new Command<string>(async s =>
    {
        MenuLottieView.PropertyChanged += OnPropertyChanged;
        MenuLottieView.IsAnimationEnabled = true;
        await Task.Delay((int)MenuLottieView.Duration.TotalMilliseconds);
        MenuLottieView.IsAnimationEnabled = false;
    });

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        
    }
}