using System.ComponentModel;
using System.Windows.Input;
using PropertyChangingEventArgs = Microsoft.Maui.Controls.PropertyChangingEventArgs;

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

    private void Button_OnClicked(object sender, EventArgs e)
    {
        HideText = !HideText;
        App.Current.MainPage.Navigation.PushAsync(new HåvardPage2());
    }

    public static readonly BindableProperty HideTextProperty = BindableProperty.Create(
        nameof(HideText),
        typeof(bool),
        typeof(HåvardPage));

    public bool HideText
    {
        get => (bool)GetValue(HideTextProperty);
        set => SetValue(HideTextProperty, value);
    }
}