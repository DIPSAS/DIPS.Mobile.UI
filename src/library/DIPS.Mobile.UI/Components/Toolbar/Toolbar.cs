using DIPS.Mobile.UI.Components.Buttons;
using DIPS.Mobile.UI.Components.Dividers;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Button;

namespace DIPS.Mobile.UI.Components.Toolbar;

/// <summary>
/// A cross-platform toolbar component that displays a horizontal bar of icon buttons.
/// </summary>
/// <remarks>
/// iOS: https://developer.apple.com/design/human-interface-guidelines/toolbars
/// Android: https://m3.material.io/components/toolbars/overview
/// </remarks>
[ContentProperty(nameof(Buttons))]
public partial class Toolbar : ContentView
{
    private readonly Grid m_buttonsGrid = new();

    public Toolbar()
    {
        this.SetAppThemeColor(BackgroundColorProperty, ColorName.color_surface_default);

        var topBorder = new Divider
        {
            HeightRequest = Sizes.GetSize(SizeName.stroke_small)
        };

        Content = new VerticalStackLayout
        {
            Spacing = 0,
            Children = { topBorder, m_buttonsGrid }
        };
    }

    private void OnButtonsChanged()
    {
        m_buttonsGrid.ColumnDefinitions.Clear();
        m_buttonsGrid.Children.Clear();

        if (Buttons is null || Buttons.Count == 0)
            return;

        for (var i = 0; i < Buttons.Count; i++)
        {
            m_buttonsGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
            var toolbarButton = Buttons[i];
            toolbarButton.BindingContext = BindingContext;
            var buttonView = CreateButtonView(toolbarButton);
            Grid.SetColumn(buttonView, i);
            m_buttonsGrid.Add(buttonView);
        }
    }

    protected override void OnBindingContextChanged()
    {
        base.OnBindingContextChanged();

        if (Buttons is null)
            return;

        foreach (var toolbarButton in Buttons)
        {
            toolbarButton.BindingContext = BindingContext;
        }
    }

    private static View CreateButtonView(ToolbarButton toolbarButton)
    {
        var button = new Button
        {
            Style = Styles.GetButtonStyle(ButtonStyle.GhostIconSmall),
            HorizontalOptions = LayoutOptions.Center,
        };

        button.SetBinding(Button.ImageSourceProperty, new Binding(nameof(ToolbarButton.Icon), source: toolbarButton));
        button.SetBinding(IsEnabledProperty, new Binding(nameof(ToolbarButton.IsEnabled), source: toolbarButton));
        button.SetBinding(Button.CommandProperty, new Binding(nameof(ToolbarButton.Command), source: toolbarButton));
        button.SetBinding(Button.CommandParameterProperty, new Binding(nameof(ToolbarButton.CommandParameter), source: toolbarButton));

        if (!string.IsNullOrEmpty(toolbarButton.Title))
        {
            SemanticProperties.SetDescription(button, toolbarButton.Title);
        }

        return button;
    }
}
