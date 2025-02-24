using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Components.Slidable;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;

internal class ZoomSlider : Grid
{
    private CancellationTokenSource m_cancellationTokenSource = new();
    private readonly HorizontalStackLayout m_zoomRatiosLayout;
    private readonly BoxView m_pin;
    private Border m_zoomRatioLevelBorder;
    private Label m_zoomRatioLevelLabel;
    
    private readonly float m_maxRatio;
    
    private readonly Action<float> m_onChangedZoomRatio;
    private readonly Action<PanUpdatedEventArgs> m_onPanned;
    
    private double m_startingX;
    private double m_minimumZoomRatiosLayoutTranslationX;
    private double m_maxZoomRatiosLayoutTranslationX;
    private double m_zoomRatioLevel;
    
    private int m_currentSnappedZoomRatio;

    public const int DelayUntilFadeOut = 500;

    public ZoomSlider(float minRatio, float maxRatio, Action<float> onChangedZoomRatio, Action<PanUpdatedEventArgs> onPanned)
    {
        InputTransparent = true;
        CascadeInputTransparent = false;
        m_maxRatio = maxRatio;
        m_onChangedZoomRatio = onChangedZoomRatio;
        m_onPanned = onPanned;
        
        VerticalOptions = LayoutOptions.Center;
        RowSpacing = Sizes.GetSize(SizeName.content_margin_medium);
        
        AddRowDefinition(new RowDefinition(GridLength.Star));
        AddRowDefinition(new RowDefinition(GridLength.Star));
        
        m_zoomRatiosLayout = new HorizontalStackLayout
        {
            Spacing = Sizes.GetSize(SizeName.content_margin_xsmall),
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center,
            Opacity = 0
        };

        var zoomRatios = GenerateZoomRatios(minRatio, maxRatio);
        
        foreach (var isInt in zoomRatios.Select(zoomRatio => zoomRatio % 1 == 0))
        {
            m_zoomRatiosLayout.Add(isInt
                ? new BoxView
                {
                    BackgroundColor = Colors.White, WidthRequest = 1, HeightRequest = Sizes.GetSize(SizeName.size_5)
                }
                : new BoxView
                {
                    BackgroundColor = Colors.White.WithAlpha(0.25f), WidthRequest = 1, HeightRequest = 10
                });
        }
        
        m_pin = new BoxView
        {
            // TODO: Lisa
            BackgroundColor = Colors.Gold, WidthRequest = 3, HeightRequest = Sizes.GetSize(SizeName.size_15), Opacity = 0
        };
        
        this.Add(m_zoomRatiosLayout, 0, 1);
        this.Add(m_pin, 0, 1);

        CreateZoomDisplayButton();
        
        m_zoomRatiosLayout.SizeChanged += ZoomRatiosLayoutOnSizeChanged;
        DUI.OrientationChanged += OnOrientationChanged;
    }

    private void OnOrientationChanged(OrientationDegree orientationDegree)
    {
        m_zoomRatioLevelLabel.RotateTo(orientationDegree.OrientationDegreeToMauiRotation());
    }

    public void FadeTo(float opacity)
    {
        m_pin.FadeTo(opacity);
        m_zoomRatiosLayout.FadeTo(opacity);
        m_zoomRatioLevelBorder.FadeTo(opacity);

        if (opacity > 0)
        {
            var panGestureRecognizer = new PanGestureRecognizer();
            panGestureRecognizer.PanUpdated += OnPanned;
            GestureRecognizers.Add(panGestureRecognizer);
            InputTransparent = false;
        }
        else
        {
            GestureRecognizers.Clear();
            InputTransparent = true;
        }
    }

    private void OnPanned(object? sender, PanUpdatedEventArgs e)
    {
        PanGestureRecognizerOnPanUpdated(e);
    }

    private void CreateZoomDisplayButton()
    {
        m_zoomRatioLevelLabel = new Label
        {
            // TODO: Lisa
            TextColor = Colors.Gold,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            Text = "1,0" 
        };
        
        var wrapper = new Grid
        {
            Padding = new Thickness(Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_2)),
            Children = { m_zoomRatioLevelLabel }
        };
        
        m_zoomRatioLevelBorder = new Border
        {
            BackgroundColor = Colors.Black.WithAlpha(0.5f),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = Sizes.GetSize(SizeName.size_4)
            },
            StrokeThickness = 0,
            Content = wrapper,
            WidthRequest = Sizes.GetSize(SizeName.size_13),
            HeightRequest = Sizes.GetSize(SizeName.size_8),
            HorizontalOptions = LayoutOptions.Center,
            Opacity = 0
        };
        
        this.Add(m_zoomRatioLevelBorder);
    }

    private static List<float> GenerateZoomRatios(float minRatio, float maxRatio)
    {
        var zoomRatios = new List<float>();
        for (var ratio = minRatio; ratio <= maxRatio; ratio += 0.1f)
        {
            zoomRatios.Add((float)Math.Round(ratio, 1));
        }
        return zoomRatios;
    }
    
    private void ZoomRatiosLayoutOnSizeChanged(object? sender, EventArgs e)
    {
        if(m_zoomRatiosLayout.TranslationX != 0)
            return;
        
        m_zoomRatiosLayout.TranslationX += m_zoomRatiosLayout.Width / 2;
        m_maxZoomRatiosLayoutTranslationX = m_zoomRatiosLayout.TranslationX;
        m_minimumZoomRatiosLayoutTranslationX = m_maxZoomRatiosLayoutTranslationX - m_zoomRatiosLayout.Width;
    }

    private double CalculateZoomRatio(double desiredTranslationX)
    {
        var currentTranslation = desiredTranslationX - m_minimumZoomRatiosLayoutTranslationX;
        currentTranslation = m_zoomRatiosLayout.Width - currentTranslation;
        var translationPercentage = (currentTranslation / m_zoomRatiosLayout.Width);

        var zoomRatioIndex = ((m_maxRatio - 1) * 10) * translationPercentage;
        
        return zoomRatioIndex / 10 + 1;
    }

    private double CalculateTranslationXFromZoomRatio(float zoomRatio)
    {
        return m_maxZoomRatiosLayoutTranslationX - (m_zoomRatiosLayout.Width * ((zoomRatio - 1) / (m_maxRatio - 1)));
    }

    private void PanGestureRecognizerOnPanUpdated(PanUpdatedEventArgs e)
    {
        m_onPanned.Invoke(e);
    }
    
    public void TranslateZoomSlider(PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                m_startingX = e.TotalX;
                
                m_cancellationTokenSource.Cancel();
                m_cancellationTokenSource = new CancellationTokenSource();
                break;
            case GestureStatus.Running:
                {
                    var desiredTranslationX = m_zoomRatiosLayout.TranslationX + e.TotalX - m_startingX;
            
                    var actualZoomRatio = CalculateZoomRatio(m_zoomRatiosLayout.TranslationX);
                    
                    // Vibrate when near integers
                    var roundedZoomRatio = (int)MathF.Round((float)actualZoomRatio);
                    if (Math.Abs(roundedZoomRatio - actualZoomRatio) < 0.025f)
                    {
                        if (roundedZoomRatio != m_currentSnappedZoomRatio)
                        {
                            VibrationService.SelectionChanged();
                        }
                        m_currentSnappedZoomRatio = roundedZoomRatio;
                    }
                    else m_currentSnappedZoomRatio = -1;
                    
                    // Stop the user from panning too far or too little
                    if(desiredTranslationX > m_maxZoomRatiosLayoutTranslationX)
                    {
                        desiredTranslationX = m_maxZoomRatiosLayoutTranslationX;
                    }
                    else if (desiredTranslationX < m_minimumZoomRatiosLayoutTranslationX)
                    {
                        desiredTranslationX = m_minimumZoomRatiosLayoutTranslationX;
                    }
                    
                    m_zoomRatiosLayout.TranslationX = desiredTranslationX;
                    m_startingX = e.TotalX;

                    ZoomRatioLevel = actualZoomRatio;

                    break;
                }
        }
    }

    public double ZoomRatioLevel
    {
        get => m_zoomRatioLevel;
        private set
        {
            m_zoomRatioLevel = value;
            m_zoomRatioLevelLabel.Text = value.ToString("F1");
            m_onChangedZoomRatio.Invoke((float)value);
        }
    }

    public void SetZoomRatio(float zoomRatio)
    {
        m_zoomRatiosLayout.TranslationX = CalculateTranslationXFromZoomRatio((int)zoomRatio);
    }

    public async Task<bool> OnPinchToZoom(float zoomRatio)
    {
        m_zoomRatiosLayout.TranslationX = CalculateTranslationXFromZoomRatio(zoomRatio);
        m_zoomRatioLevelLabel.Text = zoomRatio.ToString("F1");

        m_cancellationTokenSource.Cancel();
        m_cancellationTokenSource = new CancellationTokenSource();

        try
        {
            await Task.Delay(DelayUntilFadeOut, m_cancellationTokenSource.Token);
            m_cancellationTokenSource.Token.ThrowIfCancellationRequested();
            return false;
        }
        catch
        {
            return true;
        }
    }

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is not null)
            return;

        m_zoomRatiosLayout.SizeChanged -= ZoomRatiosLayoutOnSizeChanged;
        DUI.OrientationChanged -= OnOrientationChanged;
        m_cancellationTokenSource.Cancel();
    }
}