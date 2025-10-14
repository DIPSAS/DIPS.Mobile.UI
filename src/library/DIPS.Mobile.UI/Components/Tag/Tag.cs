using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using DIPS.Mobile.UI.Resources.Styles.Tag;
using Microsoft.Maui.Controls.Shapes;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Tag;

public partial class Tag : Border
{
    private readonly Grid m_grid;

    public Tag()
    {
        StrokeThickness = Sizes.GetSize(SizeName.stroke_medium);
        StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_half)) };
        this.SetBinding(StrokeProperty, static (Tag tag) => tag.BorderColor, source: this);
        
        m_grid = new Grid
        {
            ColumnDefinitions = [new ColumnDefinition(GridLength.Auto), new ColumnDefinition(GridLength.Star)]
        };
        
        Style = Styles.GetTagStyle(TagStyle.Default);
        Padding = Sizes.GetSize(SizeName.size_half);
        
        HorizontalOptions = LayoutOptions.Start;

        CreateIcon();
        CreateTextLabel();

        Content = m_grid;
    }

    private void CreateIcon()
    {
        var icon = new Images.Image.Image
        {
            HeightRequest = Sizes.GetSize(SizeName.size_4), WidthRequest = Sizes.GetSize(SizeName.size_4),
            Margin = new Thickness(){Right = Sizes.GetSize(SizeName.size_1)}
        };
        
        icon.SetBinding(Image.SourceProperty, static (Tag tag) => tag.Icon, source: this);
        icon.SetBinding(Images.Image.Image.TintColorProperty, static (Tag tag) => tag.IconColor, source: this);
        icon.SetBinding(IsVisibleProperty, static (Tag tag) => tag.Icon, converter: new IsEmptyConverter { Inverted = true }, source: this);

        m_grid.Add(icon, 0);
    }

    private void CreateTextLabel()
    {
        var label = new Label
        {
            Style = Styles.GetLabelStyle(LabelStyle.Body100),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalOptions = LayoutOptions.Start,
            MaxLines = 1
        };
        
        label.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (Tag tag) => tag.Text, source: this);
        label.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (Tag tag) => tag.TextColor, source: this);
        label.SetBinding(Microsoft.Maui.Controls.Label.LineBreakModeProperty, static (Tag tag) => tag.LineBreakMode, source: this);
        
        m_grid.Add(label, 1);
    }
}