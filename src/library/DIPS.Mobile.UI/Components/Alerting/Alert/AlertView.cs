using System.Windows.Input;
using DIPS.Mobile.UI.Internal;
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
    private readonly Grid? m_grid;
    private readonly HorizontalStackLayout m_horizontalStackLayout;
    private Label? m_titleLabel;
    private Image? m_image;

    public AlertView()
    {
        Style = Styles.GetAlertStyle(AlertStyle.Information);
        StrokeShape = new RoundRectangle() {CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.size_2))};
        Content = m_grid = new Grid()
        {
            AutomationId = "AlertGrid".ToDUIAutomationId<AlertView>(),
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
        
        m_horizontalStackLayout = new HorizontalStackLayout() {AutomationId="HorizontalStackLayout".ToDUIAutomationId<AlertView>(), HorizontalOptions = LayoutOptions.Start, Spacing = Sizes.GetSize(SizeName.size_2), IsVisible = false, Margin = new Thickness(0, Sizes.GetSize(SizeName.size_2), 0, 0)};
        m_grid.Add(m_horizontalStackLayout,1,2);
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
            
            m_horizontalStackLayout.Add(CreateButton(LeftButtonText, LeftButtonCommand, LeftButtonCommandParameter, "LeftButton".ToDUIAutomationId<AlertView>()));
        }

        if (RightButtonCommand != null)
        {
            m_horizontalStackLayout.Add(CreateButton(RightButtonText, RightButtonCommand, RightButtonCommandParameter, "RightButton".ToDUIAutomationId<AlertView>()));
        }
    }

    private static Button CreateButton(string buttonText, ICommand buttonCommand, object buttonCommandParameter, string automationId)
    {
        return new Button
        {
            AutomationId = automationId,
            HorizontalOptions = LayoutOptions.Start,
            Style = Styles.GetButtonStyle(ButtonStyle.SecondarySmall),
            Command = buttonCommand,
            CommandParameter = buttonCommandParameter,
            Text = buttonText
        };
    }

    private void OnTitleChanged()
    {
        m_titleLabel = new Label()
        {
            AutomationId = "TitleLabel".ToDUIAutomationId<AlertView>(),
            Style = Styles.GetLabelStyle(LabelStyle.UI200), TextColor = Colors.GetColor(ColorName.color_neutral_90)
        };
        m_titleLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (AlertView alertView) => alertView.Title, source: this);

        m_grid?.Add(m_titleLabel, 1);
        OnIconChanged();
    }

    private void OnDescriptionChanged()
    {
        var descriptionLabel = new Label()
        {
            AutomationId = "DescriptionLabel".ToDUIAutomationId<AlertView>(),
            Style = Styles.GetLabelStyle(LabelStyle.Body100),
            TextColor = Colors.GetColor(ColorName.color_neutral_90)
        };
        
        descriptionLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (AlertView alertView) => alertView.Description, source: this);

        m_grid?.Add(descriptionLabel, 1, 1);
    }

    private void OnIconChanged()
    {
        if (m_grid?.Contains(m_image) == true)
        {
            m_grid.Remove(m_image);
        }
        
        m_image = new Image()
        {
            AutomationId = "Image".ToDUIAutomationId<AlertView>(),
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            WidthRequest = Sizes.GetSize(SizeName.size_5),
            VerticalOptions = LayoutOptions.End
        };
        
        m_image.SetBinding(Image.TintColorProperty, static (AlertView alertView) => alertView.IconColor, source: this, mode: BindingMode.OneTime);
        m_image.SetBinding(Microsoft.Maui.Controls.Image.SourceProperty, static (AlertView alertView) => alertView.Icon, source: this, mode: BindingMode.OneTime);

        if (m_titleLabel is null)
        {
            m_grid?.Add(m_image, 0, 1);
            return;
        }
        m_grid?.Add(m_image, 0, string.IsNullOrEmpty(m_titleLabel?.Text) ? 0 : 1);
    }
}