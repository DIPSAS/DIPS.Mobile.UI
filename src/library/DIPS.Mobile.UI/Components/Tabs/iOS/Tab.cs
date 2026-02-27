using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Tabs
{
    internal partial class Tab : ContentView
    {
        private Label m_titleLabel;
        private Label m_counterLabel;
        private Border m_border;
        
        public Tab()
        {
            ConstructView();
        }

        internal void ConstructView()
        {
            // Single Grid replaces nested VerticalStackLayout + HorizontalStackLayout
            var container = new Grid
            {
                ColumnDefinitions =
                [
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto),
                    new ColumnDefinition(GridLength.Auto),
                    new ColumnDefinition(GridLength.Star)
                ],
                RowDefinitions =
                [
                    new RowDefinition(GridLength.Star),
                    new RowDefinition(GridLength.Auto)
                ]
            };
            
            m_titleLabel = CreateTitleLabel();
            m_counterLabel = CreateCounterLabel();

            // Bottom margin combines the original HorizontalStackLayout's bottom padding (size_1)
            // with VerticalStackLayout's inter-child spacing (content_margin_xsmall)
            var bottomMargin = Sizes.GetSize(SizeName.size_1) + Sizes.GetSize(SizeName.content_margin_xsmall);
            // Top margin replicates the original HorizontalStackLayout's top padding (size_4)
            // Counter left margin replicates the original HorizontalStackLayout's Spacing (size_1)
            m_titleLabel.Margin = new Thickness(0, Sizes.GetSize(SizeName.size_4), 0, bottomMargin);
            m_counterLabel.Margin = new Thickness(Sizes.GetSize(SizeName.size_1), Sizes.GetSize(SizeName.size_4), 0, bottomMargin);

            container.Add(m_titleLabel, 1, 0);
            container.Add(m_counterLabel, 2, 0);
            
            var boxView = new BoxView
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = Sizes.GetSize(SizeName.size_half),
                BackgroundColor = Colors.GetColor(ColorName.color_border_button_active)
            };
            boxView.SetBinding(IsVisibleProperty, static (Tab tab) => tab.IsSelected, source: this);
            container.Add(boxView, 0, 1);
            Grid.SetColumnSpan(boxView, 4);
            
            m_border = new Border
            {
                Content = container
            };

            Touch.SetCommand(m_border, new Command(() => SendTapped()));
            
            Content = m_border;
        }
        
        private Labels.Label CreateTitleLabel()
        {
            var label = new Labels.Label
            {
                VerticalTextAlignment = TextAlignment.Center,
            };
            
            label.SetBinding<Tab, string>(Label.TextProperty, static (Tab tab) => tab.Title, source: this);

            return label;
        }
        
        private Labels.Label CreateCounterLabel()
        {
            var label = new Labels.Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };

            var spans = new List<Span>();
            spans.Add(new Span { Text = "(" });
            
            var counterString = new Span();
            counterString.SetBinding<Tab, int?>(Span.TextProperty, static (Tab tab) => tab.Counter, source: this);
            spans.Add(counterString);
            
            spans.Add(new Span { Text = ")" });

            label.FormattedText = new FormattedString();
            foreach (var span in spans)
            {
                span.SetBinding(Span.FontFamilyProperty, static (Label label) => label.FontFamily, source: label);
                span.SetBinding(Span.FontSizeProperty, static (Label label) => label.FontSize, source: label);  
                label.FormattedText.Spans.Add(span);
            }
            
            label.SetBinding(Label.IsVisibleProperty, static (Tab tab) => tab.Counter, converter: new IsEmptyConverter(){Inverted = true}, source: this);

            return label;
        }
        
        public void SetTextStyle(Style textStyle, Color textColor)
        {
            m_titleLabel.TextColor = textColor;
            m_titleLabel.Style = textStyle;
            m_counterLabel.TextColor = textColor;
            m_counterLabel.Style = textStyle;
        }
        
        public void SendTapped()
        {
            Command?.Execute(CommandParameter);
            Tapped?.Invoke(this, EventArgs.Empty);
        }
    }
}