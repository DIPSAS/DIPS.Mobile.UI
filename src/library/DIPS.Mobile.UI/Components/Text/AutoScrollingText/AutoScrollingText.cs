using System.ComponentModel;
using DIPS.Mobile.UI.Effects.Animation;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Alert;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Animation = DIPS.Mobile.UI.Effects.Animation.Animation;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Text.AutoScrollingText;

public partial class AutoScrollingText : Grid
{
    private readonly BoxView m_fadingBox;
    private readonly ScrollView m_scrollView;
    private Task? m_scrollingTask;
    private readonly Labels.Label m_label;
    private bool m_isUserScrolling;
    private readonly Button m_scrollToBottomHelper;

    public AutoScrollingText()
    {
        m_label = new Labels.Label
        {
            VerticalTextAlignment = TextAlignment.End
        };
        
        m_fadingBox = new BoxView
        {
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            VerticalOptions = LayoutOptions.Start,
            InputTransparent = true
        };

        m_scrollView = new ScrollView 
        { 
            VerticalScrollBarVisibility = ScrollBarVisibility.Never,
            Content = m_label 
        };

        m_scrollToBottomHelper = new Button
        {
            ImageSource = Icons.GetIcon(IconName.arrow_down_thick_line_fill),
            Style = Styles.GetButtonStyle(ButtonStyle.SecondaryIconButtonSmall),
            Command = new Command(() =>
            {
                _ = ScrollToBottom(true);
                m_isUserScrolling = false;
                m_scrollToBottomHelper!.IsVisible = false;
            }),
            IsVisible = false,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.content_margin_medium))
        };
        
        Animation.SetFadeIn(m_scrollToBottomHelper, new AnimationConfig());
        
        m_scrollView.PropertyChanged += ScrollViewOnPropertyChanged;
        m_scrollView.Scrolled += ScrollViewOnScrolled;
        
        m_fadingBox.SetBinding(IsVisibleProperty, static (AutoScrollingText autoScrollingText) => autoScrollingText.ShouldFadeOut, source: this);
        m_label.SetBinding(StyleProperty, static (AutoScrollingText autoScrollingText) => autoScrollingText.Style, source: this);
        m_label.SetBinding(TextColorProperty, static (AutoScrollingText autoScrollingText) => autoScrollingText.TextColor, source: this);
        
        SetFadingBoxFade();
        
        Add(m_scrollView);
        Add(m_fadingBox);
        Add(m_scrollToBottomHelper);
    }

    private async void ScrollViewOnScrolled(object? sender, ScrolledEventArgs e)
    { 
        if(!m_scrollingTask?.IsCompleted ?? false)
            return;

        m_isUserScrolling = !IsAtBottom;

        if (IsAtBottom)
        {
            await m_scrollToBottomHelper.FadeTo(0);
            m_scrollToBottomHelper.IsVisible = false;
        }
        else m_scrollToBottomHelper.IsVisible = true;
    }

    private void ScrollViewOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == ScrollView.ContentSizeProperty.PropertyName)
        {
            _ = ScrollToBottom();
        }
    }

    private bool IsAtBottom => m_scrollView.ScrollY + m_scrollView.Height >= m_scrollView.ContentSize.Height - 1;
    
    public async Task ScrollToBottom(bool force = false)
    {   
        if(m_isUserScrolling && !force)
            return;
        
        // Don't scroll if ScrollView's height is not initialized or if the content is smaller than the ScrollView
        if(m_scrollView.Height < 0 || m_scrollView.ContentSize.Height <= m_scrollView.Height && !force)
            return;

        await Task.Delay(10);
        
        m_scrollingTask = m_scrollView.ScrollToAsync(0, m_scrollView.ContentSize.Height, true);
    }

    private void SetFadingBoxFade()
    {
        var fadeColor = FadeColor ?? BackgroundColor ?? Colors.GetColor(ColorName.color_neutral_10);
        
        m_fadingBox.Background = new LinearGradientBrush
        {
            EndPoint = new Point(0, 1),
            GradientStops = 
            [
                new GradientStop(fadeColor, 0), 
                new GradientStop(fadeColor.WithAlpha(0), 1)
            ]
        };
    }
}