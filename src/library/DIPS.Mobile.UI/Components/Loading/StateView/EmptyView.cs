using DIPS.Mobile.UI.Converters.ValueConverters;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Image = DIPS.Mobile.UI.Components.Images.Image.Image;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

public class EmptyView : ScrollView
{
    public EmptyView()
    {
        var verticalStackLayout = new VerticalStackLayout { Spacing = 0, VerticalOptions = LayoutOptions.Center };
        
        var icon = new Image
        {
            WidthRequest = Sizes.GetSize(SizeName.size_15),
            HeightRequest = Sizes.GetSize(SizeName.size_15),
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_4))
        };
        // icon.SetBinding(Microsoft.Maui.Controls.Image.SourceProperty, new Binding(nameof(EmptyViewModel.Icon)));
        // icon.SetBinding(IsVisibleProperty, new Binding(nameof(EmptyViewModel.Icon), converter: new IsEmptyConverter{ Inverted = true }));
        
        var titleLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_3)),
            TextColor = Colors.GetColor(ColorName.color_neutral_80),
            Style = Styles.GetLabelStyle(LabelStyle.UI300)
        };
        // titleLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, new Binding(nameof(EmptyViewModel.Title)));

        var descriptionLabel = new Label
        {
            TextColor = Colors.GetColor(ColorName.color_neutral_70),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center
        };
        // descriptionLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, new Binding(nameof(EmptyViewModel.Description)));
        
        verticalStackLayout.Add(icon);
        verticalStackLayout.Add(titleLabel);
        verticalStackLayout.Add(descriptionLabel);

        Content = verticalStackLayout;
    }

    public static readonly BindableProperty EmptyViewModelProperty = BindableProperty.Create(
        nameof(EmptyViewModel),
        typeof(EmptyViewModel),
        typeof(EmptyView), propertyChanged:(bindable, _, _) =>
            ((EmptyView)bindable).BindingContext = ((EmptyView)bindable).EmptyViewModel);

    public EmptyViewModel EmptyViewModel
    {
        get => (EmptyViewModel)GetValue(EmptyViewModelProperty);
        set => SetValue(EmptyViewModelProperty, value);
    }
}