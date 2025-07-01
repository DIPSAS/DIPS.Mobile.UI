using DIPS.Mobile.UI.API.Library;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Counters;

public partial class Counter : Grid
{
    private readonly Border m_primaryBorder = new()
    {
        StrokeShape = new RoundRectangle {StrokeThickness = 0},
        Padding = 0,
        Margin = 0
    };

    private readonly Border m_secondaryBorder = new()
    {
        StrokeShape = new RoundRectangle {StrokeThickness = 0},
        Padding = 0,
        Margin = 0
    };

    private readonly BoxView m_divider = new() { Color = Colors.GetColor(ColorName.color_border_default), Margin = 0};

    public Counter()
    {
        ColumnSpacing = 0;
        ColumnDefinitions =
        [
            new ColumnDefinition {Width = GridLength.Auto},
            new ColumnDefinition {Width = 1},
            new ColumnDefinition {Width = GridLength.Auto}
        ];
        var primaryLabel = new Label {Style = Styles.GetLabelStyle(LabelStyle.Body200), Margin = new Thickness(Sizes.GetSize(SizeName.size_2), 0)};
        var secondaryLabel = new Label {Style = Styles.GetLabelStyle(LabelStyle.Body200), Margin = new Thickness(Sizes.GetSize(SizeName.size_2), 0)};

        UI.Effects.Layout.Layout.SetCornerRadius(this, Sizes.GetSize(SizeName.size_2));
        
        m_primaryBorder.Content = primaryLabel;
        m_secondaryBorder.Content = secondaryLabel;
        
        primaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.Value, source: this);
        secondaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.SecondaryValue, source: this);
        
        this.Add(m_primaryBorder, 0);
        this.Add(m_divider, 1);
        this.Add(m_secondaryBorder, 2);
        
        OnModeChanged();
        OnIsUrgentChanged();
        OnIsSecondaryUrgentChanged();
    }

    private void OnIsUrgentChanged()
    {
        m_primaryBorder.BackgroundColor = Colors.GetColor(IsUrgent ? ColorName.color_fill_danger_subtle : ColorName.color_fill_subtle);
        UpdateDivider();
    }

    private void OnModeChanged()
    {
        switch (Mode)
        {
            case CounterDisplayMode.Single:
                {
                    m_primaryBorder.IsVisible = true;
                    m_secondaryBorder.IsVisible = false;
                    break;
                }
            case CounterDisplayMode.Double:
                {
                    m_primaryBorder.IsVisible = true;
                    m_secondaryBorder.IsVisible = true;
                    break;
                }
            case CounterDisplayMode.Auto:
                {
                    m_primaryBorder.IsVisible = true;
                    m_secondaryBorder.IsVisible = SecondaryValue != 0;
                    break;
                }
        }
        
        UpdateDivider();
    }

    private void UpdateDivider()
    {
        m_divider.IsVisible = m_primaryBorder.IsVisible && m_secondaryBorder.IsVisible;
        m_divider.Color = IsUrgent || IsSecondaryUrgent ? Colors.GetColor(ColorName.color_border_default_hover) : Colors.GetColor(ColorName.color_border_default);
    }

    private void OnIsSecondaryUrgentChanged()
    {
        m_secondaryBorder.BackgroundColor = Colors.GetColor(IsSecondaryUrgent ? ColorName.color_fill_danger_subtle : ColorName.color_fill_subtle);
        UpdateDivider();
    }

    private void OnSecondaryValueChanged()
    {
        OnModeChanged();
    }
}