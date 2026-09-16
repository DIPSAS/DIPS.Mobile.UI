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

    [Fact]
    public void GoBackTo_RaisesActivationAfterResetWithoutCompletionEvents()
    {
        var controller = CreateControllerWithCompletedSteps();
        var activated = new List<int>();
        var completed = new List<int>();
        var flowCompletedCount = 0;
        controller.StepActivated += (_, e) =>
        {
            activated.Add(e.Index);
            controller.CurrentIndex.Should().Be(1);
            controller.States.Should().Equal(
                StepFlowItemState.Completed,
                StepFlowItemState.Active,
                StepFlowItemState.Disabled,
                StepFlowItemState.Disabled);
        };
        controller.StepCompleted += (_, e) => completed.Add(e.Index);
        controller.FlowCompleted += (_, _) => flowCompletedCount++;

        controller.GoBackTo(1);

        activated.Should().Equal(1);
        completed.Should().BeEmpty();
        flowCompletedCount.Should().Be(0);
    }

    [Fact]
    public void GoBackTo_ResetsFollowingErrorState()
    {
        var controller = CreateControllerWithCompletedSteps();
        controller.SetState(3, StepFlowItemState.Error);

        controller.GoBackTo(1);

        controller.States.Should().Equal(
            StepFlowItemState.Completed,
            StepFlowItemState.Active,
            StepFlowItemState.Disabled,
            StepFlowItemState.Disabled);
    }

    [Fact]
    public void GoBackTo_WhileNoStepIsActive_ActivatesCompletedStep()
    {
        var controller = new StepFlowController { AutoAdvance = false };
        controller.Initialize(3);
        controller.CompleteCurrent();
        controller.CurrentIndex.Should().Be(-1);

        controller.GoBackTo(0);

        controller.CurrentIndex.Should().Be(0);
        controller.States.Should().Equal(
            StepFlowItemState.Active,
            StepFlowItemState.Disabled,
            StepFlowItemState.Disabled);
    }

    [Fact]
    public void GoBackTo_RequiresFollowingStepsToBeCompletedAgain()
    {
        var controller = CreateControllerWithCompletedSteps();
        var flowCompletedCount = 0;
        controller.FlowCompleted += (_, _) => flowCompletedCount++;

        controller.GoBackTo(1);
        controller.CompleteCurrent();
        controller.IsCompleted.Should().BeFalse();
        controller.GoTo(2);
        controller.CompleteCurrent();
        controller.IsCompleted.Should().BeFalse();
        controller.GoTo(3);
        controller.CompleteCurrent();

        controller.IsCompleted.Should().BeTrue();
        flowCompletedCount.Should().Be(1);
    }

    [Fact]
    public void Reset_AfterGoBackTo_RestoresInitialState()
    {
        var controller = CreateControllerWithCompletedSteps();
        controller.GoBackTo(1);

        controller.Reset();

        controller.CurrentIndex.Should().Be(0);
        controller.States.Should().Equal(
            StepFlowItemState.Active,
            StepFlowItemState.Disabled,
            StepFlowItemState.Disabled,
            StepFlowItemState.Disabled);
    }

    [Fact]
    public void GoTo_DisabledStep_PreservesLegacyActivation()
    {
        var controller = new StepFlowController { AutoAdvance = false };
        controller.Initialize(3);

        controller.GoTo(2);

        controller.CurrentIndex.Should().Be(2);
        controller.States.Should().Equal(
            StepFlowItemState.Disabled,
            StepFlowItemState.Disabled,
            StepFlowItemState.Active);
    }

    [Fact]
    public void GoBackTo_UninitializedFlow_DoesNothing()
    {
        var controller = new StepFlowController();

        controller.GoBackTo(0);

        controller.CurrentIndex.Should().Be(-1);
        controller.States.Should().BeEmpty();
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