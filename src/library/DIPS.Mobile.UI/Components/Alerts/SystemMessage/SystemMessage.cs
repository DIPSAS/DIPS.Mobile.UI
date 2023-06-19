using System.Timers;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Timer = System.Timers.Timer;

namespace DIPS.Mobile.UI.Components.Alerts.SystemMessage;

internal class SystemMessage : ContentView, IDisposable
{
    private readonly SystemMessageConfigurator m_configurator;
    private readonly Action m_onFinished;
    private readonly Timer m_timer;
    private readonly Grid m_contentGrid;

    public SystemMessage(SystemMessageConfigurator configurator, Action onFinished)
    {
        m_configurator = configurator;
        m_onFinished = onFinished;

        m_timer = new Timer(configurator.Duration);
        
        var label = new Label
        {
            Text = configurator.Text, 
            HorizontalTextAlignment = TextAlignment.Start, 
            VerticalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Center,
            TextColor = configurator.TextColor
        };

        m_contentGrid = new Grid
        {
            ColumnSpacing = Sizes.GetSize(SizeName.size_1),
            Padding = Sizes.GetSize(SizeName.size_3),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start,
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new(GridLength.Auto),
                new(GridLength.Star)
            }
        };
        
        // We have to fake that border contains the grid, because border does not resize its elements
        // correctly when animating its scale
        var border = new Border
        {
            BackgroundColor = configurator.BackgroundColor,
            Margin = Sizes.GetSize(SizeName.size_3_negative),
            StrokeShape = new RoundRectangle { CornerRadius = Sizes.GetSize(SizeName.size_3) }
        };

        m_contentGrid.Add(border);
        Grid.SetColumnSpan(border, 2);
        
        if(configurator.Icon is not null)
        {
            m_contentGrid.Add(new Image
            {
                Source = configurator.Icon,
                VerticalOptions = LayoutOptions.Center,
                TintColor = configurator.IconColor
            });
            
        }
        
        m_contentGrid.Add(label, 1);
        
        Content = m_contentGrid;

        InputTransparent = true;

        m_contentGrid.Scale = 0.75;

        Padding = new Thickness(Sizes.GetSize(SizeName.size_10), 
            Sizes.GetSize(SizeName.size_15));
    }
    
    public void Show()
    {
        SemanticScreenReader.Default.Announce($"{DUILocalizedStrings.SystemMessage}: {m_configurator.Text}");
        
        m_timer.Enabled = true;
        m_timer.Elapsed += OnTimerEnded;
        
        m_contentGrid.FadeTo(1, easing: Easing.CubicOut);
        m_contentGrid.ScaleTo(1, easing: Easing.CubicOut);
    }

    private void OnTimerEnded(object? sender, ElapsedEventArgs e)
    {
        m_onFinished.Invoke();
    }

    public async Task FadeOut()
    {
        var fade = m_contentGrid.FadeTo(0, easing: Easing.CubicIn);
        var scale = m_contentGrid.ScaleTo(.75, easing: Easing.CubicIn);
        await Task.WhenAll(fade, scale);
    }

    public void Dispose()
    {
        m_timer.Stop();
        m_timer.Elapsed -= OnTimerEnded;
    }
}