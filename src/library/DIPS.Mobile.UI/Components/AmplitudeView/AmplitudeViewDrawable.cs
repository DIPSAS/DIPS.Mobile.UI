using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.VoiceVisualizer;

public class AmplitudeViewDrawable : IDrawable
{
    private readonly GraphicsView m_graphicsView;
    
    private readonly List<float> m_amplitudes = [];
    private readonly List<float> m_cachedAmplitudes = [];

    private const float BarWidth = 10f;

    private double m_scrollOffset;
    private double m_sampleTimer;
    
    private bool m_isPaused;
    
    private DateTime? m_lastUpdateTime;
    
    private Components.AmplitudeView.AmplitudeView m_amplitudeView;

    public AmplitudeViewDrawable(GraphicsView graphicsView)
    {
        m_graphicsView = graphicsView;
    }

    public void Setup(Components.AmplitudeView.AmplitudeView amplitudeView)
    {
        m_amplitudeView = amplitudeView;
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (m_amplitudes.Count == 0)
        {
            // Calculates how many bars we can fit in the width of the drawing area
            var numBars = (int)(dirtyRect.Width / BarWidth);
            m_amplitudes.Clear();

            for (var i = 0; i < numBars; i++)
                m_amplitudes.Add(-1);
        }

        var centerY = dirtyRect.Height / 2;
        var cornerRadius = (float)Sizes.GetSize(SizeName.radius_small);

        var minHeight = dirtyRect.Height * .1f;
        
        for (var i = 0; i < m_amplitudes.Count; i++)
        {
            var value = m_amplitudes[i];
            var barHeight = Math.Clamp(value * dirtyRect.Height, minHeight, dirtyRect.Height);
            var x = (i * BarWidth) - m_scrollOffset + BarWidth;
            var y = centerY - (barHeight / 2);

            canvas.Alpha = m_isPaused ? 0.5f : 1;
            canvas.FillColor = value > 0 ? m_amplitudeView.AmplitudeColor : m_amplitudeView.PlaceholderAmplitudeColor;
            canvas.FillRoundedRectangle((float)x, y, BarWidth - 4, barHeight, cornerRadius);
        }

        DrawFadeOverlay(canvas, dirtyRect);
    }

    /// <summary>
    /// Fades out the sides
    /// </summary>
    private void DrawFadeOverlay(ICanvas canvas, RectF dirtyRect)
    {
        const float fadeWidth = 25f;

        var leftGradient = new LinearGradientPaint
        {
            StartColor = m_amplitudeView.FadeColor,
            EndColor = Microsoft.Maui.Graphics.Colors.White.WithAlpha(0),
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 0)
        };
        canvas.SetFillPaint(leftGradient, new RectF(0, 0, fadeWidth, dirtyRect.Height));
        canvas.FillRectangle(0, 0, fadeWidth, dirtyRect.Height);

        var rightGradient = new LinearGradientPaint
        {
            StartColor = Microsoft.Maui.Graphics.Colors.White.WithAlpha(0),
            EndColor = m_amplitudeView.FadeColor,
            StartPoint = new Point(0, 0),
            EndPoint = new Point(1, 0)
        };

        var rightX = dirtyRect.Width - fadeWidth;
        canvas.SetFillPaint(rightGradient, new RectF(rightX, 0, fadeWidth, dirtyRect.Height));
        canvas.FillRectangle(rightX, 0, fadeWidth, dirtyRect.Height);
    }

    public void Update()
    {
        if (m_lastUpdateTime is null)
        {
            m_lastUpdateTime = DateTime.UtcNow;
            return;
        }

        m_isPaused = false;

        var currentTime = DateTime.UtcNow;
        var deltaTime = (currentTime - m_lastUpdateTime.Value).TotalSeconds;
        m_lastUpdateTime = currentTime;

        m_scrollOffset += BarWidth * m_amplitudeView.SampleRate * deltaTime;

        m_sampleTimer += deltaTime;

        // This can be true multiple times if sample rate is higher than frame rate
        while (m_sampleTimer >= (1.0d / m_amplitudeView.SampleRate))
        {
            if (m_amplitudeView.Controller?.GetNextAmplitude() != null)
            {
                m_cachedAmplitudes.Add(m_amplitudeView.Controller.GetNextAmplitude());
            }

            m_sampleTimer -= (1.0d / m_amplitudeView.SampleRate);
        }

        while (m_scrollOffset >= BarWidth)
        {
            m_scrollOffset -= BarWidth;

            if (m_cachedAmplitudes.Count > 0)
            {
                if (m_amplitudes.Count >= m_cachedAmplitudes.Count)
                {
                    m_amplitudes.RemoveRange(0, m_cachedAmplitudes.Count);
                }

                m_amplitudes.AddRange(m_cachedAmplitudes);
                m_cachedAmplitudes.Clear();
            }
        }
        
        m_graphicsView.Invalidate();
    }

    public void Pause()
    {
        var firstPause = !m_isPaused;
        
        m_lastUpdateTime = DateTime.UtcNow;

        m_isPaused = true;
        
        if(firstPause)
            m_graphicsView.Invalidate();
    }
}