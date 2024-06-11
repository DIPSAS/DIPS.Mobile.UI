using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Alert;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Alerting.Alert;

public partial class AlertView : Border
{
    public AlertView()
    {
        Style = Styles.GetAlertStyle(AlertStyle.Information);
        Grid grid;
        StrokeShape = new RoundRectangle() {CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_2))};
        Content = grid = new Grid()
        {
            ColumnDefinitions =
            [
                new ColumnDefinition(GridLength.Auto),
                new ColumnDefinition(GridLength.Star)
            ],
            RowDefinitions =
            [
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Star)
            ],
            ColumnSpacing = Sizes.GetSize(SizeName.size_2),
            Padding = Sizes.GetSize(SizeName.size_2)
        };

        var image = new Image()
        {
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            WidthRequest = Sizes.GetSize(SizeName.size_5),
            TintColor = Colors.GetColor(ColorName.color_neutral_90),
            VerticalOptions = LayoutOptions.End
        };
        image.SetBinding(Microsoft.Maui.Controls.Image.SourceProperty, new Binding(nameof(Icon), source: this));

        grid.Add(image, 0, 0);

        var titleLabel = new Label()
        {
            Style = Styles.GetLabelStyle(LabelStyle.UI200), TextColor = Colors.GetColor(ColorName.color_neutral_90)
        };
        titleLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, new Binding(nameof(Title), source: this));

        grid.Add(titleLabel, 1, 0);

        var descriptionLabel = new Label()
        {
            Style = Styles.GetLabelStyle(LabelStyle.Body100),
            TextColor = Colors.GetColor(ColorName.color_neutral_90)
        };
        descriptionLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty,
            new Binding(nameof(Description), source: this));

        grid.Add(descriptionLabel, 1, 1);

        //TODO: Add buttons when they are needed
    }
}