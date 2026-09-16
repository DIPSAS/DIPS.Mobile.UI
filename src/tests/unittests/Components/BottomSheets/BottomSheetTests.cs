using System.Linq;
using System.Threading.Tasks;
using DIPS.Mobile.UI.Components.BottomSheets;
using DIPS.Mobile.UI.Exceptions;
using Microsoft.Maui.Controls;
using Button = Microsoft.Maui.Controls.Button;

namespace DIPS.Mobile.UI.UnitTests.Components.BottomSheets;

public class BottomSheetTests
{
    [Fact]
    public void CreateBottomBar_PreservesConsumerAutomationIdsAndGeneratesMissingIds()
    {
        var buttonWithConsumerId = new Button { AutomationId = "Shared_ViewOptions_ResetButton" };
        var buttonWithoutConsumerId = new Button();
        var bottomSheet = new BottomSheet { ShowBottombarButtonsOnSubViews = true };

        bottomSheet.BottombarButtons.Add(buttonWithConsumerId);
        bottomSheet.BottombarButtons.Add(buttonWithoutConsumerId);

        var createBottomBar = () => bottomSheet.CreateBottomBar();

        createBottomBar.Should().NotThrow();

        buttonWithConsumerId.AutomationId.Should().Be("Shared_ViewOptions_ResetButton");
        buttonWithoutConsumerId.AutomationId.Should().Be("DUI.BottomSheet.BottomBarButton1");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void CreateBottomBar_KeepsRootBindingContext(bool showOnSubViews)
    {
        var context = new object();
        var bottomSheet = new BottomSheet
        {
            BindingContext = context,
            ShowBottombarButtonsOnSubViews = showOnSubViews
        };
        var button = new Button();
        bottomSheet.BottombarButtons.Add(button);

        var bottomBar = bottomSheet.CreateBottomBar();

        bottomBar.BindingContext.Should().BeSameAs(context);
        button.BindingContext.Should().BeSameAs(context);
    }

    [Fact]
    public void Navigation_DefaultsToRootWithoutPersistentBottomBar()
    {
        var bottomSheet = new BottomSheet();

        bottomSheet.CanPopNavigation.Should().BeFalse();
        bottomSheet.ShowBottombarButtonsOnSubViews.Should().BeFalse();
        BottomSheet.ShowBottombarButtonsOnSubViewsProperty.DefaultValue.Should().Be(false);
    }

    [Fact]
    public async Task PopAtRoot_IsANoOpAndKeepsRootContent()
    {
        var root = new Grid();
        var bottomSheet = new BottomSheet { Content = root, Title = "Root" };

        await bottomSheet.PopAsync();
        await bottomSheet.PopToRootAsync();

        bottomSheet.CanPopNavigation.Should().BeFalse();
        bottomSheet.Content.Should().BeSameAs(root);
        bottomSheet.Title.Should().Be("Root");
    }

    [Fact]
    public async Task FailedPlatformPush_RestoresRootStack()
    {
        var root = new Grid();
        var bottomSheet = new BottomSheet { Content = root };

        // The net10.0 partial deliberately throws; do not replace production navigation for testing.
        await Assert.ThrowsAsync<Only_Here_For_UnitTests>(() => bottomSheet.PushAsync(new ContentPage()));

        bottomSheet.CanPopNavigation.Should().BeFalse();
        bottomSheet.Content.Should().BeSameAs(root);
    }

    [Fact]
    public async Task FailedPlatformPush_PreservesExistingEntry()
    {
        var bottomSheet = new BottomSheet();
        var existingEntry = new BottomSheetNavigationEntry(new ContentPage());
        bottomSheet.NavigationStack.Push(existingEntry);

        await Assert.ThrowsAsync<Only_Here_For_UnitTests>(() => bottomSheet.PushAsync(new ContentPage()));

        bottomSheet.NavigationStack.Should().ContainSingle().Which.Should().BeSameAs(existingEntry);
        bottomSheet.CanPopNavigation.Should().BeTrue();
    }

    [Fact]
    public async Task FailedPlatformPop_RestoresEntry()
    {
        var bottomSheet = new BottomSheet();
        var entry = new BottomSheetNavigationEntry(new ContentPage());
        bottomSheet.NavigationStack.Push(entry);

        await Assert.ThrowsAsync<Only_Here_For_UnitTests>(() => bottomSheet.PopAsync());

        bottomSheet.NavigationStack.Should().ContainSingle().Which.Should().BeSameAs(entry);
        bottomSheet.CanPopNavigation.Should().BeTrue();
    }

    [Fact]
    public async Task FailedPlatformPopToRoot_RestoresOriginalOrder()
    {
        var bottomSheet = new BottomSheet();
        var first = new BottomSheetNavigationEntry(new ContentPage());
        var second = new BottomSheetNavigationEntry(new ContentPage());
        var third = new BottomSheetNavigationEntry(new ContentPage());
        bottomSheet.NavigationStack.Push(first);
        bottomSheet.NavigationStack.Push(second);
        bottomSheet.NavigationStack.Push(third);

        await Assert.ThrowsAsync<Only_Here_For_UnitTests>(() => bottomSheet.PopToRootAsync());

        bottomSheet.NavigationStack.ToArray().Should().Equal(third, second, first);
        bottomSheet.CanPopNavigation.Should().BeTrue();
    }

    [Fact]
    public void InteractivePop_OnlyRemovesMatchingTopPageAndReturnsToRoot()
    {
        var root = new Grid();
        var bottomSheet = new BottomSheet { Content = root };
        var first = new ContentPage();
        var second = new ContentPage();
        bottomSheet.NavigationStack.Push(new BottomSheetNavigationEntry(first));
        bottomSheet.NavigationStack.Push(new BottomSheetNavigationEntry(second));

        bottomSheet.HandleInteractivePop(first);
        bottomSheet.NavigationStack.Should().HaveCount(2);

        bottomSheet.HandleInteractivePop(second);
        bottomSheet.NavigationStack.Peek().Page.Should().BeSameAs(first);

        // A duplicate notification must not remove another page.
        bottomSheet.HandleInteractivePop(second);
        bottomSheet.NavigationStack.Should().ContainSingle();

        bottomSheet.HandleInteractivePop(first);
        bottomSheet.HandleInteractivePop(first);
        bottomSheet.CanPopNavigation.Should().BeFalse();
        bottomSheet.Content.Should().BeSameAs(root);
    }

    [Fact]
    public void NavigationStack_IsOwnedByEachSheet()
    {
        var first = new BottomSheet();
        var second = new BottomSheet();
        first.NavigationStack.Push(new BottomSheetNavigationEntry(new ContentPage()));

        first.CanPopNavigation.Should().BeTrue();
        second.CanPopNavigation.Should().BeFalse();
    }
}