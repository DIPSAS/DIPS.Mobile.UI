using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;

namespace DIPS.Mobile.UI.Components.Counters;

public partial class Counter : Border
{
    private readonly Grid m_primaryGrid = new()
    {
        Padding = new Thickness(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_half)),
        BackgroundColor = Colors.GetColor(ColorName.color_fill_neutral)
    };
    private readonly Grid m_secondaryGrid = new()
    {
        Padding = new Thickness(Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_half)),
        BackgroundColor = Colors.GetColor(ColorName.color_fill_neutral)
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
    private readonly Grid m_grid;

    public Counter()
    {
        m_grid = new Grid
        {
            ColumnSpacing = 0,
            ColumnDefinitions =
            [
                new ColumnDefinition { Width = GridLength.Auto },
                new ColumnDefinition { Width = 1 },
                new ColumnDefinition { Width = GridLength.Auto }
            ],
        };

        StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_2)) };
        StrokeThickness = 1;
        Stroke = Colors.GetColor(ColorName.color_border_default);
        
        m_primaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.Value, source: this);
        m_secondaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.SecondaryValue, source: this);
        
        m_primaryGrid.Add(m_primaryLabel);
        m_primaryGrid.Add(m_primaryErrorIcon);
        
        m_secondaryGrid.Add(m_secondaryLabel);
        m_secondaryGrid.Add(m_secondaryErrorIcon);
        
        m_grid.Add(m_primaryGrid, IsFlipped ? 2 : 0);
        m_grid.Add(m_divider, 1);
        m_grid.Add(m_secondaryGrid, IsFlipped ? 0 : 2);
        
        OnModeChanged();
        OnIsUrgentChanged();
        OnIsSecondaryUrgentChanged();
        OnIsErrorChanged();
        OnIsSecondaryErrorChanged();
        
        Content = m_grid;
    }

    private void OnIsUrgentChanged()
    {
        m_primaryGrid.BackgroundColor = Colors.GetColor(IsUrgent ? ColorName.color_fill_danger : ColorName.color_fill_neutral);
        m_primaryLabel.TextColor = Colors.GetColor(IsUrgent ? ColorName.color_text_on_fill_danger : ColorName.color_text_default);
        UpdateDivider();
        UpdateStroke();
    }

    private void UpdateStroke()
    {
        if (IsUrgent && IsSecondaryUrgent)
        {
            Stroke = Colors.GetColor(ColorName.color_border_danger);
        }
        else
        {
            Stroke = Colors.GetColor(ColorName.color_border_default);
        }
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
        m_secondaryGrid.BackgroundColor = Colors.GetColor(IsSecondaryUrgent ? ColorName.color_fill_danger : ColorName.color_fill_neutral);
        m_secondaryLabel.TextColor = Colors.GetColor(IsSecondaryUrgent ? ColorName.color_text_on_fill_danger : ColorName.color_text_default);
        UpdateDivider();
        UpdateStroke();
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

    private void OnIsFlippedChanged()
    {
        m_grid.SetColumn(m_primaryGrid, IsFlipped ? 2 : 0);
        m_grid.SetColumn(m_secondaryGrid, IsFlipped ? 0 : 2);
    }
}