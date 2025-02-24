using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class ErrorView : ScrollView
{
    public ErrorView()
    {
        var verticalStackLayout = new VerticalStackLayout {Spacing = 0, VerticalOptions = LayoutOptions.Center};

        var icon = new Images.Image.Image
        {
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            HeightRequest = Sizes.GetSize(SizeName.size_15),
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.content_margin_large))
        };
        icon.SetBinding(Image.SourceProperty, static (ErrorViewModel errorViewModel) => errorViewModel.Icon);
        icon.SetBinding(IsVisibleProperty, static (ErrorViewModel errorViewModel) => errorViewModel.Icon, converter: new IsEmptyConverter {Inverted = true});

        var titleLabel = new Labels.Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.content_margin_medium)),
            TextColor = Colors.GetColor(ColorName.color_neutral_80),
            Style = Styles.GetLabelStyle(LabelStyle.UI300)
        };

        titleLabel.SetBinding(Label.TextProperty, static (ErrorViewModel errorViewModel) => errorViewModel.Title);

        var descriptionLabel = new Labels.Label
        {
            // TODO: Lisa
            TextColor = Colors.GetColor(ColorName.color_neutral_70),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center
        };
        descriptionLabel.SetBinding(Label.TextProperty, static (ErrorViewModel errorViewModel) => errorViewModel.Description);

        verticalStackLayout.Add(icon);
        verticalStackLayout.Add(titleLabel);
        verticalStackLayout.Add(descriptionLabel);

        Content = verticalStackLayout;
    }

    public static readonly BindableProperty ErrorViewModelProperty = BindableProperty.Create(
        nameof(ErrorViewModel),
        typeof(ErrorViewModel),
        typeof(ErrorView),
        propertyChanged: (bindable, _, _) =>
            ((ErrorView)bindable).BindingContext = ((ErrorView)bindable).ErrorViewModel);

    public ErrorViewModel ErrorViewModel
    {
        get => (ErrorViewModel)GetValue(ErrorViewModelProperty);
        set => SetValue(ErrorViewModelProperty, value);
    }
}