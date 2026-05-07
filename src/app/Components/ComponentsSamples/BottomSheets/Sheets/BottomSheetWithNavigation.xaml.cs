using DIPS.Mobile.UI.Components.ListItems.Extensions;
using DIPS.Mobile.UI.Resources.Sizes;
using DIPS.Mobile.UI.Resources.Styles.Label;
using Label = DIPS.Mobile.UI.Components.Labels.Label;

namespace Components.ComponentsSamples.BottomSheets.Sheets;

public partial class BottomSheetWithNavigation
{
    public BottomSheetWithNavigation()
    {
        InitializeComponent();
    }

    private async void OnPage1Tapped(object? sender, EventArgs e)
    {
        var content = new VerticalStackLayout
        {
            Spacing = 0,
            Margin = new Thickness(
                Sizes.GetSize(SizeName.content_margin_medium),
                Sizes.GetSize(SizeName.content_margin_large),
                Sizes.GetSize(SizeName.content_margin_medium),
                0)
        };
        
        content.Add(new Label
        {
            Text = "This is page 1. You navigated here from the root.",
            Style = DIPS.Mobile.UI.Resources.Styles.Styles.GetLabelStyle(LabelStyle.UI200),
            Padding = Sizes.GetSize(SizeName.size_4)
        });

        await Sheet.PushAsync(content, "Page 1");
    }

    private async void OnPage2Tapped(object? sender, EventArgs e)
    {
        var content = new VerticalStackLayout
        {
            Spacing = 0,
            Margin = new Thickness(
                Sizes.GetSize(SizeName.content_margin_medium),
                Sizes.GetSize(SizeName.content_margin_large),
                Sizes.GetSize(SizeName.content_margin_medium),
                0)
        };
        
        content.Add(new Label
        {
            Text = "This is page 2. Tap the item below to push yet another page.",
            Style = DIPS.Mobile.UI.Resources.Styles.Styles.GetLabelStyle(LabelStyle.UI200),
            Padding = Sizes.GetSize(SizeName.size_4)
        });

        var deeperNavItem = new NavigationListItem
        {
            Title = "Go deeper",
            Subtitle = "Push a third page from here"
        };
        deeperNavItem.Tapped += async (_, _) =>
        {
            var deepContent = new VerticalStackLayout
            {
                Margin = new Thickness(
                    Sizes.GetSize(SizeName.content_margin_medium),
                    Sizes.GetSize(SizeName.content_margin_large),
                    Sizes.GetSize(SizeName.content_margin_medium),
                    0)
            };
            deepContent.Add(new Label
            {
                Text = "This is the deepest page. Use the back button to go back.",
                Style = DIPS.Mobile.UI.Resources.Styles.Styles.GetLabelStyle(LabelStyle.UI200),
                Padding = Sizes.GetSize(SizeName.size_4)
            });

            await Sheet.PushAsync(deepContent, "Page 3");
        };
        
        content.Add(deeperNavItem);

        await Sheet.PushAsync(content, "Page 2");
    }
}
