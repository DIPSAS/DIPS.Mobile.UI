using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using DIPS.Mobile.UI.Resources.Styles.Tag;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Tag;

public partial class Tag : HorizontalStackLayout
{
    public Tag()
    {
        Style = Styles.GetTagStyle(TagStyle.Default);
        Padding = Sizes.GetSize(SizeName.size_half);
        UI.Effects.Layout.Layout.SetCornerRadius(this, Sizes.GetSize(SizeName.size_half));
        
        Spacing = Sizes.GetSize(SizeName.size_1);
        
        HorizontalOptions = LayoutOptions.Start;

        CreateIcon();
        CreateTextLabel();
    }

    private void CreateIcon()
    {
        var icon = new Images.Image.Image
        {
            HeightRequest = Sizes.GetSize(SizeName.size_4), WidthRequest = Sizes.GetSize(SizeName.size_4),
        };
        
        icon.SetBinding(Image.SourceProperty, static (Tag tag) => tag.Icon, source: this);
        icon.SetBinding(Images.Image.Image.TintColorProperty, static (Tag tag) => tag.IconColor, source: this);
        icon.SetBinding(IsVisibleProperty, static (Tag tag) => tag.Icon, converter: new IsEmptyConverter { Inverted = true }, source: this);

        Add(icon);
    }

    private void CreateTextLabel()
    {
        var label = new Label
        {
            Style = Styles.GetLabelStyle(LabelStyle.Body100),
            VerticalTextAlignment = TextAlignment.Center,
            HorizontalOptions = LayoutOptions.Fill
        };
        
        label.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (Tag tag) => tag.Text, source: this);
        label.SetBinding(Microsoft.Maui.Controls.Label.TextColorProperty, static (Tag tag) => tag.TextColor, source: this);
        label.SetBinding(Microsoft.Maui.Controls.Label.LineBreakModeProperty, static (Tag tag) => tag.LineBreakMode, source: this);
        
        Add(label);
    }
}