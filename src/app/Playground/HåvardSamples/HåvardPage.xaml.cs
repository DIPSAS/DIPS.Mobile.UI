using System.ComponentModel;
using System.Windows.Input;
using DIPS.Mobile.UI.API.Camera.BarcodeScanning;
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

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    }

    private void BindableObject_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
    }

    private void BindableObject_OnPropertyChanging(object sender, PropertyChangingEventArgs e)
    {
    }

    private void VisualElement_OnMeasureInvalidated(object sender, EventArgs e)
    {
    }

    private void Element_OnHandlerChanged(object sender, EventArgs e)
    {
    }

    private void VisualElement_OnSizeChanged(object sender, EventArgs e)
    {
    }

    private void TapGestureRecognizer_OnTapped(object sender, TappedEventArgs e)
    {
        if (sender is not View view) return;
        if (view.HeightRequest == -1)
        {
            view.HeightRequest = 60;
            return;
        }

        view.HeightRequest = -1;
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

    private void Switch_OnToggled(object sender, ToggledEventArgs e)
    {
        if (ContentView.Content is null)
        {
            ContentView.Content = m_image;
        }
        else
        {
            ContentView.Content = null;
        }
    }
}