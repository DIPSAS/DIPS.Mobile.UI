using Colors = Microsoft.Maui.Graphics.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Loading.LoadingOverlay;

[ContentProperty(nameof(Content))]
public partial class LoadingOverlay : Grid
{
    private Grid? m_overlay;

    private Grid CreateOverlay()
    {
        var activityIndicator = new ActivityIndicator
        {
            IsRunning = true,
            Color = ContentColor
        };

        var label = new Label
        {
            Text = Text,
            TextColor = ContentColor,
            HorizontalTextAlignment = TextAlignment.Start,
            VerticalTextAlignment = TextAlignment.Center,
            MaxLines = 1,
            LineBreakMode = LineBreakMode.TailTruncation,
        };

        var stack = new Grid
        {
            ColumnSpacing = Sizes.GetSize(SizeName.size_2),
            ColumnDefinitions =
            [
                new ColumnDefinition(GridLength.Auto), // ActivityIndicator
                new ColumnDefinition(GridLength.Star)
            ],
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Children = { activityIndicator, label }
        };
        
        var overlayBackground = new BoxView
        {
            Color = OverlayColor,
            Opacity = .5f
        };
        
        Grid.SetColumn(label, 1);

        var grid = new Grid { overlayBackground, stack };

        grid.SetBinding(HeightRequestProperty, static (IView view) => view.Height, source: Content);
        grid.SetBinding(WidthRequestProperty, static (IView view) => view.Width, source: Content);

        return grid;
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

        if (Handler is null)
            return;
        
        Insert(0, Content);
        _ = OnIsBusyChanged();
    }

    private async Task OnIsBusyChanged()
    {
        if (Handler is null) return;
        if (IsBusy && m_overlay is null)
        {
            m_overlay = CreateOverlay();
            m_overlay.Opacity = 0;
            Add(m_overlay);
            _ = m_overlay.FadeTo(1);
            _ = Content?.FadeTo(ContentFadeOutValue);
        }
        else if(m_overlay is not null)
        {
            _ = Content?.FadeTo(1);
            await m_overlay.FadeTo(0);
            m_overlay.DisconnectHandlers();
            Remove(m_overlay);
            m_overlay = null;
        }
    }
}