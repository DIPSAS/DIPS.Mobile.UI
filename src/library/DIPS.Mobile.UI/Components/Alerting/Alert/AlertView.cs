using DIPS.Mobile.UI.Components.Buttons;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Alert;
using DIPS.Mobile.UI.Resources.Styles.Button;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Microsoft.Maui.Controls.Shapes;
using Button = DIPS.Mobile.UI.Components.Buttons.Button;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace DIPS.Mobile.UI.Components.Alerting.Alert;

public partial class AlertView : Border
{
    private readonly HorizontalStackLayout m_horizontalStackLayout;

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
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Auto),
            ],
            ColumnSpacing = Sizes.GetSize(SizeName.size_2),
            Padding = Sizes.GetSize(SizeName.size_2)
        };

        var image = new Image()
        {
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            WidthRequest = Sizes.GetSize(SizeName.size_5),
            VerticalOptions = LayoutOptions.End
        };
        image.SetBinding(Image.TintColorProperty, new Binding(nameof(IconColor), source:this));
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
        
        m_horizontalStackLayout = new HorizontalStackLayout() {HorizontalOptions = LayoutOptions.Start, Spacing = Sizes.GetSize(SizeName.size_2), IsVisible = false, Margin = new Thickness(0, Sizes.GetSize(SizeName.size_2), 0, 0)};
        grid.Add(m_horizontalStackLayout,1,2);
    }

    private void OnButtonChanged()
    {
        m_horizontalStackLayout.IsVisible = LeftButtonCommand != null || RightButtonCommand != null;
        if (!m_horizontalStackLayout.IsVisible)
        {
            return;
        }

        m_horizontalStackLayout.Clear();
        if (LeftButtonCommand != null)
        {
            if (RightButtonCommand != null)
            {
                m_horizontalStackLayout.Clear();
            }
            
            m_horizontalStackLayout.Add(CreateButton(nameof(LeftButtonText), nameof(LeftButtonCommand),nameof(LeftButtonCommandParameter)));
        }

        if (RightButtonCommand != null)
        {
            m_horizontalStackLayout.Add(CreateButton(nameof(RightButtonText), nameof(RightButtonCommand),nameof(RightButtonCommandParameter)));
        }
    }

    private Button CreateButton(string leftButtonTextName, string leftButtonCommandName, string leftButtonCommandParameterName)
    {
        var button = new Button
        {
            HorizontalOptions = LayoutOptions.Start,
            Style = Styles.GetButtonStyle(ButtonStyle.SecondarySmall),
        };
            
        button.SetBinding(Microsoft.Maui.Controls.Button.TextProperty, new Binding(path:leftButtonTextName, source:this));
        button.SetBinding(Microsoft.Maui.Controls.Button.CommandProperty, new Binding(path:leftButtonCommandName, source:this));
        button.SetBinding(Microsoft.Maui.Controls.Button.CommandParameterProperty, new Binding(path:leftButtonCommandParameterName, source:this));
        return button;
    }
}