using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;

internal class ZoomButtons : HorizontalStackLayout
{
    private readonly Action<float> m_onChangedZoomRatio;
    private readonly Action<PanUpdatedEventArgs> m_onPanned;
    private readonly List<ZoomButton> m_zoomButtons;

    private ZoomButton m_currentlyActiveButton;

    public ZoomButtons(float minRatio, float maxRatio, Action<float> onChangedZoomRatio, Action<PanUpdatedEventArgs> onPanned)
    {
        m_onChangedZoomRatio = onChangedZoomRatio;
        m_onPanned = onPanned;

        // We do not support ultra-wide cameras yet, so we can safely assume that the minimum ratio is 1
        minRatio = 1;
        
        var panGestureRecognizer = new PanGestureRecognizer();
        panGestureRecognizer.PanUpdated += OnPanned;
        GestureRecognizers.Add(panGestureRecognizer);
        
        BackgroundColor = Colors.Black.WithAlpha(0.2f);
        Spacing = Sizes.GetSize(SizeName.size_1);
        VerticalOptions = LayoutOptions.Center;
        HorizontalOptions = LayoutOptions.Center;
        Padding = 2;
        
        UI.Effects.Layout.Layout.SetCornerRadius(this, Sizes.GetSize(SizeName.size_5));
        
        var firstButton = CreateCameraZoomButton(minRatio, true);
        var secondButton = CreateCameraZoomButton(minRatio + 1);
        var thirdButton = CreateCameraZoomButton(MathF.Min(secondButton.DefaultZoomRatio + 1, maxRatio));

        // If we have not yet hit the max ratio, lets add another button
        ZoomButton? fourthButton = null;
        if (Math.Abs(thirdButton.DefaultZoomRatio - maxRatio) > 0.01f)
        {
            fourthButton = CreateCameraZoomButton(MathF.Min(secondButton.DefaultZoomRatio + 2, maxRatio));
        }

        m_currentlyActiveButton = firstButton.IsDefaultActive ? firstButton : secondButton;
        
        Add(firstButton);
        Add(secondButton);
        Add(thirdButton);
        
        if(fourthButton is not null)
            Add(fourthButton);
        
        this.SizeChanged += OnSizeChanged;
        
        m_zoomButtons = [firstButton, secondButton, thirdButton];
        
        if(fourthButton is not null)
            m_zoomButtons.Add(fourthButton);
        
        DUI.OrientationChanged += OrientationChanged;
    }

    private void OrientationChanged(OrientationDegree orientationDegree)
    {
        foreach (var child in Children)
        {
            if (child is View view)
            {
                view.RotateTo(orientationDegree.OrientationDegreeToMauiRotation());
            }
        }
    }

    private void OnPanned(object? sender, PanUpdatedEventArgs e)
    {
        m_onPanned.Invoke(e);
    }

    private ZoomButton CreateCameraZoomButton(float zoomRatio, bool defaultActive = false)
    {
        var thirdButton = new ZoomButton(zoomRatio, OnTappedButton, m_onPanned, defaultActive);
        return thirdButton;
    }

    private void OnSizeChanged(object? sender, EventArgs e)
    {
        WidthRequest = Width;
    }

    private void OnTappedButton((float, ZoomButton) valueTuple)
    {
        m_onChangedZoomRatio.Invoke(valueTuple.Item1);

        if (m_currentlyActiveButton == valueTuple.Item2)
            return;
        
        m_currentlyActiveButton.AnimateToInactive();
        m_currentlyActiveButton = valueTuple.Item2;
    }

    public void SetZoomRatio(float zoomRatio)
    {
        var buttonToBeActive = m_zoomButtons.First();
        for (var i = 1; i < m_zoomButtons.Count; i++)
        {
            var nextButton = m_zoomButtons[i];
            if (nextButton.DefaultZoomRatio > zoomRatio)
                break;
            
            buttonToBeActive = nextButton;
        }

        if (buttonToBeActive != m_currentlyActiveButton)
        {
            m_currentlyActiveButton.ResetToDefault();
            m_currentlyActiveButton.AnimateToInactive();
            m_currentlyActiveButton = buttonToBeActive;
        }
        
        buttonToBeActive.SetCustomZoomRatio(zoomRatio);
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
            return;

        DUI.OrientationChanged -= OrientationChanged;
        this.SizeChanged -= OnSizeChanged;
    }
}

// We do not directly inherit from Border, to make the tappable area larger
internal class ZoomButton : ContentView
{
    private readonly Action<(float, ZoomButton)> m_onTapped;
    private readonly Action<PanUpdatedEventArgs> m_onPanned;
    private readonly Label m_label;
    private readonly Border m_border;

    private const float InActiveScale = .75f;

    public ZoomButton(float defaultZoomRatio, Action<(float, ZoomButton)> onTapped, Action<PanUpdatedEventArgs> onPanned, bool defaultDefaultActive = false)
    {
        m_onTapped = onTapped;
        m_onPanned = onPanned;
        
        m_border = new Border();

        IsDefaultActive = defaultDefaultActive;
        DefaultZoomRatio = defaultZoomRatio;

        m_border.StrokeShape = new Ellipse { Stroke = Colors.Transparent };
        m_border.StrokeThickness = 0;
        m_border.BackgroundColor = Colors.Black.WithAlpha(0.5f);
        m_border.HeightRequest = Sizes.GetSize(SizeName.size_9);
        m_border.WidthRequest = Sizes.GetSize(SizeName.size_9);

        HeightRequest = Sizes.GetSize(SizeName.size_9);
        WidthRequest = Sizes.GetSize(SizeName.size_9);
        
        m_border.Scale = IsDefaultActive ? 1 : InActiveScale;
        
        m_label = new Label
        {
            Text = IsDefaultActive ? $"{defaultZoomRatio} ×" : $"{defaultZoomRatio}",
            TextColor = IsDefaultActive ? Colors.Gold : Colors.White,
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            FontSize = 12
        };
        
        m_border.Content = m_label;
        
        var panGestureRecognizer = new PanGestureRecognizer();
        var tapGestureRecognizer = new TapGestureRecognizer();
        
        panGestureRecognizer.PanUpdated += OnPanned;
        tapGestureRecognizer.Tapped += OnTapped;
        
        GestureRecognizers.Add(panGestureRecognizer);
        GestureRecognizers.Add(tapGestureRecognizer);

        Content = m_border;
    }

    private void OnTapped(object? sender, TappedEventArgs e)
    {
        ResetToDefault();
        AnimateToActive();
        m_onTapped.Invoke((DefaultZoomRatio, this));
    }

    private void OnPanned(object? sender, PanUpdatedEventArgs e)
    {
        m_onPanned.Invoke(e);
    }

    private void AnimateToActive()
    {
        m_border.ScaleTo(1);
        
        m_label.Text += " ×";
        m_label.TextColor = Colors.Gold;
    }

    public void AnimateToInactive()
    {
        m_border.ScaleTo(InActiveScale);

        ResetToDefault();
    }
    
    public void SetCustomZoomRatio(float zoomRatio)
    {
        m_label.Text = $"{zoomRatio:F1}";
        AnimateToActive();
    }

    public void ResetToDefault()
    {
        m_label.Text = $"{DefaultZoomRatio}";
        m_label.TextColor = Colors.White;
    }
    
    public float DefaultZoomRatio { get; }
    public bool IsDefaultActive { get; }
}