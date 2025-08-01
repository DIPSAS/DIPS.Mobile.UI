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

namespace DIPS.Mobile.UI.Components.Alerting. Alert;

public partial class AlertView : Border
{
    /// <summary>
    /// Determines how the <see cref="AlertView"/>'s buttons should align. 
    /// </summary>
    public enum ButtonAlignmentType
    {
        /// <summary>
        ///     Will automatically align the buttons <see cref="Inline"/> if there's enough space. Otherwise, it will use <see cref="Underlying"/>.
        /// </summary>
        Auto,
        /// <summary>
        ///     Aligns the buttons directly underneath the title and description.
        /// </summary>
        Underlying,
        /// <summary>
        ///     Aligns the buttons to the same line as the title, to the top right of the view.
        /// </summary>
        Inline
    }

    private readonly Grid m_grid;
    private readonly Grid m_innerGrid;
    private readonly HorizontalStackLayout m_buttonsContainer;
    private Label? m_titleLabel;
    private Image? m_image;
    private Label? m_descriptionLabel;

    public AlertView()
    {
        Style = Styles.GetAlertStyle(AlertStyle.Information);
        StrokeShape = new RoundRectangle() {CornerRadius = new CornerRadius(Sizes.GetSize(SizeName.radius_small))};
        Content = m_grid = new Grid()
        {
            AutomationId = "AlertGrid".ToDUIAutomationId<AlertView>(),
            ColumnDefinitions =
            [
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Auto)
            ]
        };
        
        m_innerGrid = new Grid
        {
            AutomationId = "InnerGrid".ToDUIAutomationId<AlertView>(),
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
            ColumnSpacing = Sizes.GetSize(SizeName.content_margin_medium),
            Padding = Sizes.GetSize(SizeName.content_margin_medium)
        };
        m_grid.Add(m_innerGrid, 0);
        
        m_buttonsContainer = new HorizontalStackLayout() {AutomationId="HorizontalStackLayout".ToDUIAutomationId<AlertView>(), HorizontalOptions = LayoutOptions.Start, VerticalOptions = LayoutOptions.Start, Spacing = Sizes.GetSize(SizeName.size_2), IsVisible = false, Margin = new Thickness(0, Sizes.GetSize(SizeName.size_2), 0, 0)};
    }

    private void OnButtonAlignmentChanged()
    {
        UpdateButtonAlignment();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        UpdateButtonAlignment();
    }

    private void UpdateButtonAlignment()
    {
        if (LeftButtonCommand is null && RightButtonCommand is null) return;
        
        var maxWidth = m_innerGrid.Measure(int.MaxValue, int.MaxValue).Width;
        var buttonsWidth = m_buttonsContainer.Measure(int.MaxValue, int.MaxValue).Width;
        var remainingWidth = Width - maxWidth - buttonsWidth;
        var buttonsWillFit = remainingWidth >= Sizes.GetSize(SizeName.content_margin_small);
        
        if (ButtonAlignment is ButtonAlignmentType.Auto && buttonsWillFit
            || ButtonAlignment is ButtonAlignmentType.Inline)
        {
            m_buttonsContainer.Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_small));
            m_grid!.Remove(m_buttonsContainer);
            m_innerGrid!.Remove(m_buttonsContainer);
            m_grid.Add(m_buttonsContainer,1);
        }
        else
        {
            m_buttonsContainer.Margin = new Thickness(0, Sizes.GetSize(SizeName.content_margin_small), 0, 0);
            m_grid!.Remove(m_buttonsContainer);
            m_innerGrid!.Remove(m_buttonsContainer);
            m_innerGrid.Add(m_buttonsContainer,1,2);
        }
    }

    private void OnButtonChanged()
    {
        m_buttonsContainer.IsVisible = LeftButtonCommand != null || RightButtonCommand != null;
        if (!m_buttonsContainer.IsVisible)
        {
            return;
        }

        m_buttonsContainer.Clear();
        if (LeftButtonCommand != null)
        {
            if (RightButtonCommand != null)
            {
                m_buttonsContainer.Clear();
            }
            
            m_buttonsContainer.Add(CreateButton(LeftButtonText, LeftButtonCommand, LeftButtonCommandParameter, "LeftButton".ToDUIAutomationId<AlertView>()));
        }

        if (RightButtonCommand != null)
        {
            m_buttonsContainer.Add(CreateButton(RightButtonText, RightButtonCommand, RightButtonCommandParameter, "RightButton".ToDUIAutomationId<AlertView>()));
        }
        
        UpdateButtonAlignment();
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
            Style = Styles.GetLabelStyle(LabelStyle.UI200),
            LineBreakMode = LineBreakMode.TailTruncation
        };
        m_titleLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (AlertView alertView) => alertView.Title, source: this);
        m_titleLabel.SetBinding(Microsoft.Maui.Controls.Label.MaxLinesProperty, static (AlertView alertView) => alertView.TitleMaxLines, source: this);

        m_innerGrid?.Add(m_titleLabel, 1);
        OnIconChanged();
        UpdateButtonAlignment();
    }

    private void OnDescriptionChanged()
    {
        m_descriptionLabel = new Label()
        {
            AutomationId = "DescriptionLabel".ToDUIAutomationId<AlertView>(),
            Style = Styles.GetLabelStyle(LabelStyle.Body100),
            LineBreakMode = LineBreakMode.TailTruncation
        };
        
        m_descriptionLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, static (AlertView alertView) => alertView.Description, source: this);
        m_descriptionLabel.SetBinding(Microsoft.Maui.Controls.Label.MaxLinesProperty, static (AlertView alertView) => alertView.DescriptionMaxLines, source: this);

        m_innerGrid?.Add(m_descriptionLabel, 1, 1);
        
        UpdateButtonAlignment();
    }

    private void OnIconChanged()
    {
        if (m_innerGrid?.Contains(m_image) == true)
        {
            m_innerGrid.Remove(m_image);
        }
        
        m_image = new Image()
        {
            AutomationId = "Image".ToDUIAutomationId<AlertView>(),
            HeightRequest = Sizes.GetSize(SizeName.size_5),
            WidthRequest = Sizes.GetSize(SizeName.size_5),
            VerticalOptions = LayoutOptions.Start
        };
        
        m_image.SetBinding(Image.TintColorProperty, static (AlertView alertView) => alertView.IconColor, source: this, mode: BindingMode.OneTime);
        m_image.SetBinding(Microsoft.Maui.Controls.Image.SourceProperty, static (AlertView alertView) => alertView.Icon, source: this, mode: BindingMode.OneTime);

        if (m_titleLabel is null)
        {
            m_innerGrid?.Add(m_image, 0, 1);
            return;
        }
        m_innerGrid?.Add(m_image, 0, string.IsNullOrEmpty(m_titleLabel?.Text) ? 0 : 1);
    }
}