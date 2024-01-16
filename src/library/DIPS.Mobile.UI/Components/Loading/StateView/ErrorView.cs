using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

internal class ErrorView : RefreshView
{
    public ErrorView()
    {
        SetBinding(CommandProperty, new Binding(nameof(ErrorViewModel.RefreshCommand)));
        SetBinding(IsRefreshingProperty, new Binding(nameof(ErrorViewModel.IsRefreshing)));

        var verticalStackLayout = new VerticalStackLayout { Spacing = 0, VerticalOptions = LayoutOptions.Center };

        var scrollView = new ScrollView();

        scrollView.AddLogicalChild(verticalStackLayout);

        var icon = new Images.Image.Image
        {
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            HeightRequest = Sizes.GetSize(SizeName.size_15),
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_4))
        };
        icon.SetBinding(Image.SourceProperty, new Binding(nameof(ErrorViewModel.Icon)));
        icon.SetBinding(IsVisibleProperty, new Binding(nameof(ErrorViewModel.Icon), converter: new IsEmptyConverter{ Inverted = true }));
        
        var titleLabel = new Labels.Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_3)),
            TextColor = Colors.GetColor(ColorName.color_neutral_80),
            Style = Styles.GetLabelStyle(LabelStyle.UI300)
        };

        titleLabel.SetBinding(Label.TextProperty, new Binding(nameof(ErrorViewModel.Title)));

        var descriptionLabel = new Labels.Label
        {
            TextColor = Colors.GetColor(ColorName.color_neutral_70),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };
        descriptionLabel.SetBinding(Label.TextProperty, new Binding(nameof(ErrorViewModel.Description)));

        verticalStackLayout.Add(icon);
        verticalStackLayout.Add(titleLabel);
        verticalStackLayout.Add(descriptionLabel);

        scrollView.Content = verticalStackLayout;
        Content = scrollView;
    }
}