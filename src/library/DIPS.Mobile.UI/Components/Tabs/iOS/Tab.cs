using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Effects.Touch;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.Components.Tabs
{
    public partial class Tab : ContentView
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
                Padding = new Thickness(Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_4), Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_2))            
            };
            m_titleLabel = CreateTitleLabel();
            m_counterLabel = CreateCounterLabel();
    
            titleContainer.Add(m_titleLabel);
            titleContainer.Add(m_counterLabel);
            container.Add(titleContainer);
            
            var boxView = new BoxView
            {
                HorizontalOptions = LayoutOptions.Fill,
                HeightRequest = Sizes.GetSize(SizeName.size_1),
                BackgroundColor = Colors.GetColor(ColorName.color_border_action_secondary_active)
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
                TextColor = Colors.GetColor(ColorName.color_neutral_70),
                Style = Styles.GetLabelStyle(LabelStyle.Body200),
            };
            
            label.SetBinding(Label.TextColorProperty, static (Tab tab) => tab.TextColor, source: this);
            label.SetBinding<Tab, string>(Label.TextProperty, static (Tab tab) => tab.Title, source: this);
            label.SetBinding(Label.StyleProperty, static (Tab tab) => tab.TextStyle, source: this);

            return label;
        }
        
        private Labels.Label CreateCounterLabel()
        {
            var label = new Labels.Label
            {
                TextColor = Colors.GetColor(ColorName.color_neutral_70),
                Style = Styles.GetLabelStyle(LabelStyle.Body200),
            };
            
            var counterString = new Span();
            
            counterString.SetBinding<Tab, string>(Span.TextProperty, static (Tab tab) => tab.Counter, source: this);
            
            label.FormattedText = new FormattedString()
            {
                Spans =
                {
                    new Span { Text = "(", TextColor = Colors.GetColor(ColorName.color_text_default) },
                    counterString,
                    new Span { Text = ")", TextColor = Colors.GetColor(ColorName.color_text_default) }
                }
            };
            
            label.SetBinding<Tab, string>(Label.IsVisibleProperty, static (Tab tab) => tab.Counter, converter: new IsEmptyConverter(){Inverted = true}, source: this);

            return label;
        }
    }
}