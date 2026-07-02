using DIPS.Mobile.UI.Components.BottomSheets;
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
}
