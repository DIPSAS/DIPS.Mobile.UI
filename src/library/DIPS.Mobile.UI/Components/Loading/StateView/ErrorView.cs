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

        var titleLabel = new Labels.Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_3)),
            TextColor = Colors.GetColor(ColorName.color_neutral_80),
            Style = Styles.GetLabelStyle(LabelStyle.UI300)
        };

        titleLabel.SetBinding(Label.TextProperty, new Binding(nameof(ErrorViewModel.ErrorViewTitle)));

        var descriptionLabel = new Labels.Label
        {
            TextColor = Colors.GetColor(ColorName.color_neutral_70),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
        };
        descriptionLabel.SetBinding(Label.TextProperty, new Binding(nameof(ErrorViewModel.ErrorViewDescription)));

        verticalStackLayout.Add(titleLabel);
        verticalStackLayout.Add(descriptionLabel);

        scrollView.Content = verticalStackLayout;
        Content = scrollView;
    }
}