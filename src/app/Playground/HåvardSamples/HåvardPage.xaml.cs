using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
using DIPS.Mobile.UI.API.Tip;
using DIPS.Mobile.UI.Resources.Icons;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using PropertyChangingEventArgs = Microsoft.Maui.Controls.PropertyChangingEventArgs;

namespace Playground.HåvardSamples;

public partial class HåvardPage
{
    public HåvardPage()
    {
        InitializeComponent();
        m_barcodeScanner = new BarcodeScanner();
        m_image = new Image() {Source = Icons.GetIcon(IconName.document_fill)};
    }

    public ICommand NavigateCommand => new Command<string>(async s =>
    {
        App.Current.MainPage.Navigation.PushAsync(new HåvardPage());
    });

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(3000);
        TipService.Show("My awesome tip", Label);
    }

    public static readonly BindableProperty HideTextProperty = BindableProperty.Create(
        nameof(HideText),
        typeof(bool),
        typeof(HåvardPage));

    private readonly BarcodeScanner m_barcodeScanner;
    private readonly Image m_image;

    public bool HideText
    {
        get => (bool)GetValue(HideTextProperty);
        set => SetValue(HideTextProperty, value);
    }

    // private void ShowTip(object sender, EventArgs e)
    // {
    //     TipService.Show("Testing tip", Label);
    // }
}