using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Layout = DIPS.Mobile.UI.Effects.Layout.Layout;

namespace DIPS.Mobile.UI.Components.Counters;

public partial class Counter : Grid
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
    
    private readonly Label m_primaryLabel = new()
    {
        Style = Styles.GetLabelStyle(LabelStyle.Body200), 
        VerticalTextAlignment = TextAlignment.Center,
        HorizontalTextAlignment = TextAlignment.Center,
        TextColor = Colors.GetColor(ColorName.color_text_default)
    };
    private readonly Label m_secondaryLabel = new()
    {
        Style = Styles.GetLabelStyle(LabelStyle.Body200),
        VerticalTextAlignment = TextAlignment.Center,
        HorizontalTextAlignment = TextAlignment.Center,
        TextColor = Colors.GetColor(ColorName.color_text_default)
    };

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
            new ColumnDefinition { Width = GridLength.Auto },
            new ColumnDefinition { Width = 1 },
            new ColumnDefinition { Width = GridLength.Auto }
        ];

        DIPS.Mobile.UI.Effects.Layout.Layout.SetCornerRadius(this, Sizes.GetSize(SizeName.size_2));
        DIPS.Mobile.UI.Effects.Layout.Layout.SetStrokeThickness(this, 1);
        DIPS.Mobile.UI.Effects.Layout.Layout.SetStroke(this, Colors.GetColor(ColorName.color_border_default));
        
        m_primaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.Value, source: this);
        m_secondaryLabel.SetBinding(Label.TextProperty, static (Counter counter) => counter.SecondaryValue, source: this);
        
        m_primaryGrid.Add(m_primaryLabel);
        m_primaryGrid.Add(m_primaryErrorIcon);
        
        m_secondaryGrid.Add(m_secondaryLabel);
        m_secondaryGrid.Add(m_secondaryErrorIcon);
        
        this.Add(m_primaryGrid, IsFlipped ? 2 : 0);
        this.Add(m_divider, 1);
        this.Add(m_secondaryGrid, IsFlipped ? 0 : 2);
        
        OnModeChanged();
        OnIsUrgentChanged();
        OnIsSecondaryUrgentChanged();
        OnIsErrorChanged();
        OnIsSecondaryErrorChanged();
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
        if (IsUrgent && SecondaryValue == 0 || IsUrgent && IsSecondaryUrgent)
        {
            DIPS.Mobile.UI.Effects.Layout.Layout.SetStroke(this, Colors.GetColor(ColorName.color_border_danger));
        }
        else
        {
            DIPS.Mobile.UI.Effects.Layout.Layout.SetStroke(this, Colors.GetColor(ColorName.color_border_default));
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
        this.SetColumn(m_primaryGrid, IsFlipped ? 2 : 0);
        this.SetColumn(m_secondaryGrid, IsFlipped ? 0 : 2);
    }
}