using DIPS.Mobile.UI.Resources.Colors;
using DIPS.Mobile.UI.Resources.Icons;
using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using DIPS.Mobile.UI.Resources.Sizes;
using Microsoft.Maui.Controls.Shapes;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FullScreenPresenter;

internal class FullScreenPage : Microsoft.Maui.Controls.ContentPage
{
    private readonly View m_content;

    public FullScreenPage(View content)
    {
        m_content = content;

        Microsoft.Maui.Controls.Shell.SetNavBarIsVisible(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

        BackgroundColor = Colors.GetColor(ColorName.color_background_default);

        var closeButton = CreateCloseButton();

        Content = new Grid
        {
            Children =
            {
                content,
                closeButton
            }
        };
    }

    private View CreateCloseButton()
    {
        var buttonSize = Sizes.GetSize(SizeName.size_8);

        var closeIcon = new DIPS.Mobile.UI.Components.Images.Image
        {
            Source = Icons.GetIcon(IconName.close_line),
            TintColor = Colors.GetColor(ColorName.color_icon_default),
            WidthRequest = Sizes.GetSize(SizeName.size_4),
            HeightRequest = Sizes.GetSize(SizeName.size_4),
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        var closeButton = new Border
        {
            StrokeShape = new Ellipse(),
            StrokeThickness = 0,
            BackgroundColor = Colors.GetColor(ColorName.color_surface_default),
            WidthRequest = buttonSize,
            HeightRequest = buttonSize,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(Sizes.GetSize(SizeName.content_margin_medium)),
            Content = closeIcon
        };

        SemanticProperties.SetDescription(closeButton, DUILocalizedStrings.Close);

        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += async (_, _) => await FullScreenPresenterService.Close();
        closeButton.GestureRecognizers.Add(tapGesture);

        return closeButton;
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Detach the content so it can be returned to its original parent
        if (Content is Grid grid && grid.Children.Contains(m_content))
        {
            grid.Children.Remove(m_content);
        }
    }
}
