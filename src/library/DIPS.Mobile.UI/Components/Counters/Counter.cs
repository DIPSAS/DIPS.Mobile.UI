using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.Counters;

public partial class Counter : Grid
{
    private readonly Label m_primaryLabel = new() {Style = Styles.GetLabelStyle(LabelStyle.Body200), Padding = new Thickness(Sizes.GetSize(SizeName.size_2), 0), BackgroundColor = Colors.GetColor(ColorName.color_fill_subtle)};
    private readonly Label m_secondaryLabel = new() {Style = Styles.GetLabelStyle(LabelStyle.Body200), Padding = new Thickness(Sizes.GetSize(SizeName.size_2), 0), BackgroundColor = Colors.GetColor(ColorName.color_fill_subtle)};

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

        UI.Effects.Layout.Layout.SetCornerRadius(this, Sizes.GetSize(SizeName.size_2));
        
        m_primaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.Value, source: this);
        m_secondaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.SecondaryValue, source: this);
        
        this.Add(m_primaryLabel, 0);
        this.Add(m_divider, 1);
        this.Add(m_secondaryLabel, 2);
        
        OnModeChanged();
        OnIsUrgentChanged();
        OnIsSecondaryUrgentChanged();
    }

    private void OnIsUrgentChanged()
    {
        m_primaryLabel.BackgroundColor = Colors.GetColor(IsUrgent ? ColorName.color_fill_danger_subtle : ColorName.color_fill_subtle);
        UpdateDivider();
    }

    private void OnModeChanged()
    {
        switch (Mode)
        {
            case CounterDisplayMode.Single:
                {
                    m_primaryLabel.IsVisible = true;
                    m_secondaryLabel.IsVisible = false;
                    break;
                }
            case CounterDisplayMode.Double:
                {
                    m_primaryLabel.IsVisible = true;
                    m_secondaryLabel.IsVisible = true;
                    break;
                }
            case CounterDisplayMode.Auto:
                {
                    m_primaryLabel.IsVisible = true;
                    m_secondaryLabel.IsVisible = SecondaryValue != 0;
                    break;
                }
        }
        
        UpdateDivider();
    }

    private void UpdateDivider()
    {
        m_divider.IsVisible = m_primaryLabel.IsVisible && m_secondaryLabel.IsVisible;
        m_divider.Color = IsUrgent || IsSecondaryUrgent ? Colors.GetColor(ColorName.color_border_default_hover) : Colors.GetColor(ColorName.color_border_default);
    }

    private void OnIsSecondaryUrgentChanged()
    {
        m_secondaryLabel.BackgroundColor = Colors.GetColor(IsSecondaryUrgent ? ColorName.color_fill_danger_subtle : ColorName.color_fill_subtle);
        UpdateDivider();
    }

    private void OnSecondaryValueChanged()
    {
        OnModeChanged();
    }
}