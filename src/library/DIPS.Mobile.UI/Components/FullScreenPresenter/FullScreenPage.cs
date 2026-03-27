using DIPS.Mobile.UI.Resources.LocalizedStrings.LocalizedStrings;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;

namespace DIPS.Mobile.UI.Components.FullScreenPresenter;

/// <summary>
/// A full screen page that displays a captured image of a view with a close button.
/// Used internally by <see cref="FullScreenPresenterService"/>.
/// </summary>
internal class FullScreenPage : ContentPage
{
    public FullScreenPage(ImageSource imageSource)
    {
        BackgroundColor = Colors.GetColor(ColorName.color_background_default);

        Microsoft.Maui.Controls.Shell.SetNavBarIsVisible(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

        var image = new Image
        {
            Source = imageSource,
            Aspect = Aspect.AspectFit,
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Fill
        };

        var closeButton = CreateCloseButton();

        var grid = new Grid
        {
            Children = { image, closeButton }
        };

        Content = grid;
    }

    private static View CreateCloseButton()
    {
        var button = new ImageButton
        {
            Source = Icons.GetIcon(IconName.close_line),
            BackgroundColor = Colors.GetColor(ColorName.color_surface_default),
            WidthRequest = Sizes.GetSize(SizeName.size_8),
            HeightRequest = Sizes.GetSize(SizeName.size_8),
            CornerRadius = (int)(Sizes.GetSize(SizeName.size_8) / 2),
            Padding = Sizes.GetSize(SizeName.size_1),
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Start,
            Margin = new Thickness(0, Sizes.GetSize(SizeName.size_2), Sizes.GetSize(SizeName.size_2), 0)
        };

        SemanticProperties.SetDescription(button, DUILocalizedStrings.Close);
        button.Clicked += async (_, _) => await FullScreenPresenterService.Close();

        return button;
    }
}
