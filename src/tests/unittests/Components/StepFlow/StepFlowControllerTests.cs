using DIPS.Mobile.UI.Components.StepFlow;

namespace DIPS.Mobile.UI.UnitTests.Components.StepFlow;

public class StepFlowControllerTests
{
    [Fact]
    public void GoBackTo_CompletedStep_ActivatesStepAndDisablesFollowingSteps()
    {
        var controller = CreateControllerWithCompletedSteps();

        controller.GoBackTo(1);

        controller.CurrentIndex.Should().Be(1);
        controller.States.Should().Equal(
            StepFlowItemState.Completed,
            StepFlowItemState.Active,
            StepFlowItemState.Disabled,
            StepFlowItemState.Disabled);
    }

    [Fact]
    public void GoBackTo_FirstCompletedStep_ActivatesFirstStepAndDisablesFollowingSteps()
    {
        var controller = CreateControllerWithCompletedSteps();

        controller.GoBackTo(0);

        controller.CurrentIndex.Should().Be(0);
        controller.States.Should().Equal(
            StepFlowItemState.Active,
            StepFlowItemState.Disabled,
            StepFlowItemState.Disabled,
            StepFlowItemState.Disabled);
    }

    [Fact]
    public void GoBackTo_NonCompletedStep_DoesNothing()
    {
        var controller = new StepFlowController { AutoAdvance = false };
        controller.Initialize(3);

        controller.GoBackTo(1);

        controller.GoBackTo(0);

        controller.GoBackTo(-1);

        controller.GoBackTo(3);

        controller.GoBackTo(99);

        controller.CurrentIndex.Should().Be(0);
        controller.States.Should().Equal(
            StepFlowItemState.Active,
            StepFlowItemState.Disabled,
            StepFlowItemState.Disabled);
    }

    [Fact]
    public void GoBackTo_CompletedFlow_DoesNothing()
    {
        var controller = CreateControllerWithCompletedSteps();
        controller.Complete(3);

        controller.GoBackTo(1);

        controller.CurrentIndex.Should().Be(-1);
        controller.States.Should().Equal(
            StepFlowItemState.Completed,
            StepFlowItemState.Completed,
            StepFlowItemState.Completed,
            StepFlowItemState.Completed);
    }

    [Fact]
    public void GoTo_CompletedStep_DoesNotActivateCompletedStep()
    {
        var controller = CreateControllerWithCompletedSteps();

        controller.GoTo(1);

        controller.CurrentIndex.Should().Be(3);
        controller.States.Should().Equal(
            StepFlowItemState.Completed,
            StepFlowItemState.Completed,
            StepFlowItemState.Completed,
            StepFlowItemState.Active);
    }

    private static StepFlowController CreateControllerWithCompletedSteps()
    {
        var controller = new StepFlowController { AutoAdvance = false };
        controller.Initialize(4);
        controller.Complete(0);
        controller.GoTo(1);
        controller.Complete(1);
        controller.GoTo(2);
        controller.Complete(2);
        controller.GoTo(3);
        return controller;
    }
}
