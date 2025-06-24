using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Tabs
{
    public partial class Tab : ContentView
    {
        private Label m_titleLabel;
        private Label m_counterLabel;
        private bool m_isSelected;

        internal void ConstructView()
        {
            var container = new HorizontalStackLayout()
                {
                    Spacing = Sizes.GetSize(SizeName.size_1),
                    Margin = new Thickness(Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_3), Sizes.GetSize(SizeName.size_1))
                };
            
            m_titleLabel = CreateTitleLabel();
            m_counterLabel = CreateCounterLabel();
    
            container.Add(m_titleLabel);
            container.Add(m_counterLabel);
            
            Content = container;
        }
        
        private Labels.Label CreateTitleLabel()
        {
            var label = new Labels.Label
            {
                TextColor = Colors.GetColor(ColorName.color_neutral_70),
                Style = Styles.GetLabelStyle(LabelStyle.Body200),
                VerticalTextAlignment = TextAlignment.Center
            };
            //label.SetBinding<Tab, string>(Label.TextProperty, static (Tab tab) => tab.Title + "(" + tab.Counter + ")", source: this);

            return label;
        }
        
        private Labels.Label CreateCounterLabel()
        {
            var label = new Labels.Label
            {
                VerticalTextAlignment = TextAlignment.Center
            };
            label.SetBinding<Tab, string>(Label.TextProperty, static (Tab tab) => tab.Title, source: this);

            return label;
        }
    }
}