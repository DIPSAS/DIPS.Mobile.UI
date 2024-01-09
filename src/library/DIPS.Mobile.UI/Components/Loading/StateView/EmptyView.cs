using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Colors = DIPS.Mobile.UI.Resources.Colors.Colors;
using Label = DIPS.Mobile.UI.Components.Labels.Label;
using VerticalStackLayout = DIPS.Mobile.UI.Components.Lists.VerticalStackLayout;

namespace DIPS.Mobile.UI.Components.Loading.StateView;

internal class EmptyView : VerticalStackLayout
{
    public EmptyView()
    {
        Spacing = 0;
        VerticalOptions = LayoutOptions.Center;

        var titleLabel = new Label
        {
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center,
            Margin = new Thickness(0, 0, 0, Sizes.GetSize(SizeName.size_3)),
            TextColor = Colors.GetColor(ColorName.color_neutral_80),
            Style = Styles.GetLabelStyle(LabelStyle.UI300)
        };
       
        titleLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, new Binding(nameof(EmptyViewModel.EmptyTitle)));

        var descriptionLabel = new Label
        {
            TextColor = Colors.GetColor(ColorName.color_neutral_70),
            VerticalOptions = LayoutOptions.Center,
            HorizontalOptions = LayoutOptions.Center
        };
        descriptionLabel.SetBinding(Microsoft.Maui.Controls.Label.TextProperty, new Binding(nameof(EmptyViewModel.EmptyDescription)));
        
        Add(titleLabel);
        Add(descriptionLabel);
    }

}