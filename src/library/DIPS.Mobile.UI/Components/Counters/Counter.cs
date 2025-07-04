using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.Counters;

public partial class Counter : Grid
{
    private readonly Grid m_primaryGrid = new()
    {
        Padding = new Thickness(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_half)),
        BackgroundColor = Colors.GetColor(ColorName.color_fill_subtle)
    };
    private readonly Grid m_secondaryGrid = new()
    {
        Padding = new Thickness(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_half)),
        BackgroundColor = Colors.GetColor(ColorName.color_fill_subtle)
    };
    
    private readonly Label m_primaryLabel = new() {Style = Styles.GetLabelStyle(LabelStyle.Body200), HeightRequest = Sizes.GetSize(SizeName.size_5), TextColor = Colors.GetColor(ColorName.color_text_default)};
    private readonly Label m_secondaryLabel = new() {Style = Styles.GetLabelStyle(LabelStyle.Body200), HeightRequest = Sizes.GetSize(SizeName.size_5), TextColor = Colors.GetColor(ColorName.color_text_default) };

    private readonly Image m_primaryErrorIcon = new()
    {
        Source = Icons.GetIcon(IconName.alert_line),
        WidthRequest = Sizes.GetSize(SizeName.size_5),
        HeightRequest = Sizes.GetSize(SizeName.size_5)
    };
    private readonly Image m_secondaryErrorIcon = new()
    {
        Source = Icons.GetIcon(IconName.alert_line),
        WidthRequest = Sizes.GetSize(SizeName.size_5),
        HeightRequest = Sizes.GetSize(SizeName.size_5)
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

        UI.Effects.Layout.Layout.SetCornerRadius(this, Sizes.GetSize(SizeName.size_2));
        
        m_primaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.Value, source: this);
        m_secondaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.SecondaryValue, source: this);
        
        m_primaryGrid.Add(m_primaryLabel);
        m_primaryGrid.Add(m_primaryErrorIcon);
        
        m_secondaryGrid.Add(m_secondaryLabel);
        m_secondaryGrid.Add(m_secondaryErrorIcon);
        
        this.Add(m_primaryGrid, 0);
        this.Add(m_divider, 1);
        this.Add(m_secondaryGrid, 2);
        
        OnModeChanged();
        OnIsUrgentChanged();
        OnIsSecondaryUrgentChanged();
        OnIsErrorChanged();
        OnIsSecondaryErrorChanged();
    }

    private void OnIsUrgentChanged()
    {
        m_primaryGrid.BackgroundColor = Colors.GetColor(IsUrgent ? ColorName.color_fill_danger_subtle : ColorName.color_fill_subtle);
        m_primaryLabel.TextColor = Colors.GetColor(IsUrgent ? ColorName.color_text_warning : ColorName.color_text_default);
        UpdateDivider();
    }

    private void OnModeChanged()
    {
        switch (Mode)
        {
            case CounterDisplayMode.Single:
                {
                    m_primaryGrid.IsVisible = true;
                    m_secondaryGrid.IsVisible = false;
                    break;
                }
            case CounterDisplayMode.Double:
                {
                    m_primaryGrid.IsVisible = true;
                    m_secondaryGrid.IsVisible = true;
                    break;
                }
            case CounterDisplayMode.Auto:
                {
                    m_primaryGrid.IsVisible = true;
                    m_secondaryGrid.IsVisible = SecondaryValue != 0;
                    break;
                }
        }
        
        UpdateDivider();
    }

    private bool ShouldDisplaySecondaryCounter => Mode is CounterDisplayMode.Double || (Mode is CounterDisplayMode.Auto && SecondaryValue != 0);
    
    private void UpdateDivider()
    {
        m_divider.IsVisible = ShouldDisplaySecondaryCounter;
        m_divider.Color = IsUrgent || IsSecondaryUrgent ? Colors.GetColor(ColorName.color_border_default_hover) : Colors.GetColor(ColorName.color_border_default);
    }

    private void OnIsSecondaryUrgentChanged()
    {
        m_secondaryGrid.BackgroundColor = Colors.GetColor(IsSecondaryUrgent ? ColorName.color_fill_danger_subtle : ColorName.color_fill_subtle);
        m_secondaryLabel.TextColor = Colors.GetColor(IsSecondaryUrgent ? ColorName.color_text_warning : ColorName.color_text_default);
        UpdateDivider();
    }

    private void OnSecondaryValueChanged()
    {
        OnModeChanged();
    }

    private void OnIsErrorChanged()
    {
        m_primaryErrorIcon.IsVisible = IsError;
        m_primaryLabel.IsVisible = !IsError;
    }

    private void OnIsSecondaryErrorChanged()
    {
        m_secondaryErrorIcon.IsVisible = IsSecondaryError;
        m_secondaryLabel.IsVisible = !IsSecondaryError;
    }
}