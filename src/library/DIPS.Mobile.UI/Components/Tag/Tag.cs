using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using DIPS.Mobile.UI.Resources.Styles.Tag;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Tag;

public partial class Tag : Grid
{
    public Tag()
    {
        DIPS.Mobile.UI.Effects.Layout.Layout.SetStrokeThickness(this, Sizes.GetSize(SizeName.stroke_medium));
        DIPS.Mobile.UI.Effects.Layout.Layout.SetCornerRadius(this, Sizes.GetSize(SizeName.size_half));

        ColumnDefinitions = [new ColumnDefinition(GridLength.Auto), new ColumnDefinition(GridLength.Star)];
        Style = Styles.GetTagStyle(TagStyle.Default);
        Padding = Sizes.GetSize(SizeName.size_1);
        
        HorizontalOptions = LayoutOptions.Start;

        CreateIcon();
        CreateTextLabel();
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

        this.Add(icon, 0);
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
        
        this.Add(label, 1);
    }
}