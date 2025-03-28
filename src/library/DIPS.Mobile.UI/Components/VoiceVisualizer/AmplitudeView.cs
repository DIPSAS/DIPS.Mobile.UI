using System.Diagnostics;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;

namespace DIPS.Mobile.UI.Components.VoiceVisualizer;

public partial class AmplitudeView : Grid
{
    private readonly AmplitudeViewDrawable m_drawable;
    private readonly Stopwatch m_stopwatch = new();

    private readonly Label m_elapsedTimeLabel = new()
    {
        Style = Styles.GetLabelStyle(LabelStyle.Header500),
        Text = "0:00",
        HorizontalTextAlignment = TextAlignment.Center,
        VerticalTextAlignment = TextAlignment.Center
    };

    public AmplitudeView()
    {
        RowDefinitions = new RowDefinitionCollection
        (
            new RowDefinition(GridLength.Star),
            new RowDefinition(GridLength.Auto)
        );

        RowSpacing = Sizes.GetSize(SizeName.content_margin_small);
        MinimumHeightRequest = 50;

        var graphicsView = new GraphicsView();
        
        m_drawable = new AmplitudeViewDrawable(graphicsView);

        graphicsView.Drawable = m_drawable;

        this.Add(graphicsView);
        this.Add(m_elapsedTimeLabel, 0, 1);
    }

    private void Initialize()
    {
        if (Controller is null)
        {
            throw new Exception("Controller is not set, make sure to bind it to the Controller property");
        }
        
        m_drawable.Setup(this);

        Controller!.OnIsPlayingChanged -= OnIsPlayingChanged;
        Controller.OnIsPlayingChanged += OnIsPlayingChanged;
        
        SetupTimer();
        SetupUpdate();
    }

    private void SetupUpdate()
    {
        Dispatcher.StartTimer(TimeSpan.FromMilliseconds(1000 / FramesPerSecond), () =>
        {
            if (Controller is not null && Controller.IsRunning)
            {
                m_drawable.Update();  
            }
            else
            {
                m_drawable.Pause();
            }
            
            return IsActive;
        });
    }

    private void SetupTimer()
    {
        if (!HasTimer)
        {
            m_elapsedTimeLabel.IsVisible = false;
            return;
        }
        
        if (Controller!.IsRunning)
        {
            m_stopwatch.Start();            
        }
            
        Dispatcher.StartTimer(TimeSpan.FromSeconds(0.25), () =>
        {
            if (Controller is null || !Controller.IsRunning)
                return IsActive;

            var minutes = (m_stopwatch.ElapsedMilliseconds / 1000) / 60;
            var seconds = (m_stopwatch.ElapsedMilliseconds / 1000) % 60;

            m_elapsedTimeLabel.Text = $"{minutes:D1}:{seconds:D2}";

            return IsActive;
        });
    }

    private void OnIsPlayingChanged(bool state)
    {
        if (state)
        {
            m_elapsedTimeLabel.CancelAnimations();
            m_elapsedTimeLabel.FadeTo(1);
            m_stopwatch.Start();
        }
        else
        {
            m_drawable.Pause();
            
            m_stopwatch.Stop();
            _ = StartLabelFadeAnimation();
        }
    }

    private async Task StartLabelFadeAnimation()
    {
        while (Controller is not null && !Controller.IsRunning)
        {
            await m_elapsedTimeLabel.FadeTo(0);
            await Task.Delay(250);
            if(!Controller.IsRunning)
                await m_elapsedTimeLabel.FadeTo(1);

            await Task.Delay(500);
        }
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is not null)
        {
            Initialize();
            return;
        }
        
        if(Controller is not null)
            Controller.OnIsPlayingChanged -= OnIsPlayingChanged;
    }

    private bool IsActive => Handler is not null && Controller is not null;
}