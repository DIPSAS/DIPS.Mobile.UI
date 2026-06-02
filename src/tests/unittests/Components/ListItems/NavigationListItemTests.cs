using DIPS.Mobile.UI.Components.ListItems;
using DIPS.Mobile.UI.Components.ListItems.Extensions;
using Microsoft.Maui.Controls;

namespace DIPS.Mobile.UI.UnitTests.Components.ListItems;

public class NavigationListItemTests
{
    [Fact]
    public void NavigationListItem_Should_Use_Title_As_Default_Accessibility_Description()
    {
        var listItem = new NavigationListItem { Title = "Egne pasienter (0)" };

        SemanticProperties.GetDescription(listItem).Should().Be("Egne pasienter (0)");
    }

    [Fact]
    public void NavigationListItem_Should_Not_Overwrite_Custom_Accessibility_Description_Set_Before_Title()
    {
        var listItem = new NavigationListItem();
        SemanticProperties.SetDescription(listItem, "Custom description");

        listItem.Title = "Visible title";

        SemanticProperties.GetDescription(listItem).Should().Be("Custom description");
    }

    [Fact]
    public void NavigationListItem_Should_Not_Overwrite_Custom_Accessibility_Description_Set_After_Title()
    {
        var listItem = new NavigationListItem { Title = "Visible title" };

        SemanticProperties.SetDescription(listItem, "Custom description");
        listItem.Title = "Updated visible title";

        SemanticProperties.GetDescription(listItem).Should().Be("Custom description");
    }

    [Fact]
    public void ListItem_Should_Not_Use_Title_As_Default_Accessibility_Description()
    {
        var listItem = new ListItem { Title = "Plain list item" };

        SemanticProperties.GetDescription(listItem).Should().BeNull();
    }
}