using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

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
            var container = new VerticalStackLayout();
            var titleContainer = new HorizontalStackLayout()
            {
                Spacing = Sizes.GetSize(SizeName.size_1),
                HorizontalOptions = LayoutOptions.Center,
                Padding = new Thickness(Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_4), Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_1))            
            };
            m_titleLabel = CreateTitleLabel();
            m_counterLabel = CreateCounterLabel();
    
            titleContainer.Add(m_titleLabel);
            titleContainer.Add(m_counterLabel);
            container.Add(titleContainer);
            
            var boxView = new BoxView
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = Sizes.GetSize(SizeName.size_half),
                BackgroundColor = Colors.GetColor(ColorName.color_border_button_active)
            };
            boxView.SetBinding(IsVisibleProperty, static (Tab tab) => tab.IsSelected, source: this);
            container.Add(boxView);
            
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