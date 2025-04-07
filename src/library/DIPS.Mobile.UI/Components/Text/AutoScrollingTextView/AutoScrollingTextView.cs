using System.ComponentModel;
using DIPS.Mobile.UI.Effects.Animation;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;
using Animation = DIPS.Mobile.UI.Effects.Animation.Animation;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using PropertyChangingEventArgs = Microsoft.Maui.Controls.PropertyChangingEventArgs;

namespace DIPS.Mobile.UI.Components.Text.AutoScrollingTextView;

public partial class AutoScrollingTextView : Grid
{
    private readonly BoxView m_fadingBox;
    private readonly ScrollView m_scrollView;
    private readonly Labels.Label m_label;
    private readonly Button m_scrollToBottomHelper;
    
    public AutoScrollingTextView()
    {
        m_label = new Labels.Label
        {
            VerticalTextAlignment = TextAlignment.End
        };
        
        m_fadingBox = new BoxView
        {
            HeightRequest = Sizes.GetSize(SizeName.size_10),
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
            ImageSource = Icons.GetIcon(IconName.arrow_down_line),
            Style = Styles.GetButtonStyle(ButtonStyle.SecondaryIconButtonSmall),
            Command = new Command(() => _ = ScrollToBottom(true)),
            IsVisible = false,
            VerticalOptions = LayoutOptions.End,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.content_margin_medium))
        };
        
        Animation.SetFadeIn(m_scrollToBottomHelper, new AnimationConfig());
        
        m_scrollView.PropertyChanging += ScrollViewOnPropertyChanged;
        m_scrollView.Scrolled += ScrollViewOnScrolled;
        
        m_fadingBox.SetBinding(IsVisibleProperty, static (AutoScrollingTextView autoScrollingText) => autoScrollingText.ShouldFadeOut, source: this);
        m_label.SetBinding(StyleProperty, static (AutoScrollingTextView autoScrollingText) => autoScrollingText.Style, source: this);
        m_label.SetBinding(TextColorProperty, static (AutoScrollingTextView autoScrollingText) => autoScrollingText.TextColor, source: this);
        
        SetFadingBoxFade();
        
        Add(m_scrollView);
        Add(m_fadingBox);
        Add(m_scrollToBottomHelper);
    }

    private void ScrollViewOnScrolled(object? sender, ScrolledEventArgs e)
    {
        if (IsAtBottom)
        {
            _ = FadeHelperOut();
        }
        else
        {
            m_scrollToBottomHelper.IsVisible = true;
        }
    }

    private async Task FadeHelperOut()
    {
        if(!m_scrollToBottomHelper.IsVisible)
            return;
        
        await m_scrollToBottomHelper.FadeTo(0);
        m_scrollToBottomHelper.IsVisible = false;
    }

    private void ScrollViewOnPropertyChanged(object sender, PropertyChangingEventArgs e)
    {
        if (e.PropertyName == ScrollView.ContentSizeProperty.PropertyName)
        {
            _ = ScrollToBottom();
        }
    }

    private bool IsAtBottom => Math.Abs((m_scrollView.ScrollY + m_scrollView.Height) - m_scrollView.ContentSize.Height) < 1 || m_scrollView.ScrollY + m_scrollView.Height > m_scrollView.ContentSize.Height;
    
    public async Task ScrollToBottom(bool force = false)
    {   
        // Don't scroll if ScrollView's height is not initialized or if the content is smaller than the ScrollView
        if(m_scrollView.Height < 0 || m_scrollView.ContentSize.Height <= m_scrollView.Height && !force)
            return;

        if (!IsAtBottom && !force)
            return;
        
        await Task.Delay(10);
        
        await m_scrollView.ScrollToAsync(0, m_scrollView.ContentSize.Height, false);
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

    protected override void OnHandlerChanging(HandlerChangingEventArgs args)
    {
        base.OnHandlerChanging(args);

        if (args.NewHandler is null)
        {
            m_scrollView.PropertyChanging -= ScrollViewOnPropertyChanged;
            m_scrollView.Scrolled -= ScrollViewOnScrolled;   
        }
    }
}