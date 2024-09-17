using DIPS.Mobile.UI.API.Vibration;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = Microsoft.Maui.Graphics.Colors;

namespace DIPS.Mobile.UI.API.Camera.ImageCapturing.Views.CameraZoom;

internal class ZoomSlider : Grid
{
    private double m_startingX;
    private readonly HorizontalStackLayout m_zoomRatiosLayout;
    private double m_minimumZoomRatiosLayoutTranslationX;
    private double m_maxZoomRatiosLayoutTranslationX;
    private int m_currentSnappedZoomRatio;
    private readonly float m_maxRatio;
    private readonly Action<float> m_onChangedZoomRatio;
    private readonly Action<bool> m_onPanningStateChanged;
    private Label m_zoomRatioLevelLabel;
    private double m_zoomRatioLevel;


    public ZoomSlider(float minRatio, float maxRatio, bool hasWideLens, Action<float> onChangedZoomRatio, Action<bool> onPanningStateChanged)
    {
        m_maxRatio = maxRatio;
        m_onChangedZoomRatio = onChangedZoomRatio;
        m_onPanningStateChanged = onPanningStateChanged;

        RowSpacing = Sizes.GetSize(SizeName.size_2);
        
        AddRowDefinition(new RowDefinition(GridLength.Star));
        AddRowDefinition(new RowDefinition(GridLength.Star));
        
        var panGestureRecognizer = new PanGestureRecognizer();
        panGestureRecognizer.PanUpdated += PanGestureRecognizerOnPanUpdated;
        GestureRecognizers.Add(panGestureRecognizer);
        
        m_zoomRatiosLayout = new HorizontalStackLayout
        {
            Spacing = Sizes.GetSize(SizeName.size_1),
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            VerticalOptions = LayoutOptions.Start,
            HorizontalOptions = LayoutOptions.Center
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
        
        this.Add(m_zoomRatiosLayout, 0, 1);
        this.Add(new BoxView
        {
            BackgroundColor = Colors.Gold,
            WidthRequest = 3,
            HeightRequest = Sizes.GetSize(SizeName.size_15)
        }, 0, 1);

        CreateZoomDisplayButton();
        
        m_zoomRatiosLayout.SizeChanged += ZoomRatiosLayoutOnSizeChanged;
    }

    private void CreateZoomDisplayButton()
    {
        m_zoomRatioLevelLabel = new Label
        {
            TextColor = Colors.Gold,
            Style = Styles.GetLabelStyle(LabelStyle.UI100),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalTextAlignment = TextAlignment.Center
        };
        
        var wrapper = new Grid
        {
            Padding = new Thickness(Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_2)),
            Children = { m_zoomRatioLevelLabel }
        };
        
        var border = new Border
        {
            BackgroundColor = Colors.Black.WithAlpha(0.5f),
            StrokeShape = new RoundRectangle
            {
                CornerRadius = Sizes.GetSize(SizeName.size_4)
            },
            StrokeThickness = 0,
            Content = wrapper,
            HorizontalOptions = LayoutOptions.Center
        };
        
        this.Add(border);
    }


    private List<float> GenerateZoomRatios(float minRatio, float maxRatio)
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
        m_zoomRatiosLayout.TranslationX += m_zoomRatiosLayout.Width / 2;
        m_maxZoomRatiosLayoutTranslationX = m_zoomRatiosLayout.TranslationX;
        m_minimumZoomRatiosLayoutTranslationX = m_maxZoomRatiosLayoutTranslationX - m_zoomRatiosLayout.Width;
    }

    private double CalculateZoomRatio(double desiredTranslationX)
    {
        // Calculate the current translation percentage
        var currentTranslation = desiredTranslationX - m_minimumZoomRatiosLayoutTranslationX;
        currentTranslation = m_zoomRatiosLayout.Width - currentTranslation;
        var translationPercentage = (currentTranslation / m_zoomRatiosLayout.Width);

        var zoomRatioIndex = ((m_maxRatio - 1) * 10) * translationPercentage;
        
        return zoomRatioIndex / 10 + 1;
    }

    private double CalculateTranslationXFromZoomRatio(int zoomRatio)
    {
        return m_maxZoomRatiosLayoutTranslationX - (m_zoomRatiosLayout.Width * (zoomRatio - 1) / m_maxRatio);
    }

    private void PanGestureRecognizerOnPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        TranslateZoomSlider(e);
    }
    
    public void TranslateZoomSlider(PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
                m_startingX = e.TotalX;
                m_onPanningStateChanged.Invoke(true);
                break;
            case GestureStatus.Running:
                {
                    var desiredTranslationX = m_zoomRatiosLayout.TranslationX + e.TotalX - m_startingX;
            
                    var actualZoomRatio = CalculateZoomRatio(m_zoomRatiosLayout.TranslationX);
                    
                    // Vibrate when near integers
                    var roundedZoomRatio = (int)MathF.Round((float)actualZoomRatio);
                    if (Math.Abs(roundedZoomRatio - actualZoomRatio) < 0.025f &&
                        roundedZoomRatio != m_currentSnappedZoomRatio)
                    {
                        m_currentSnappedZoomRatio = roundedZoomRatio;
                        VibrationService.Click();
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
            case GestureStatus.Completed:
                m_onPanningStateChanged.Invoke(false);
                break;
            case GestureStatus.Canceled:
                m_onPanningStateChanged.Invoke(false);
                break;
        }
    }
    
    public double ZoomRatioLevel
    {
        get => m_zoomRatioLevel;
        set
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
}