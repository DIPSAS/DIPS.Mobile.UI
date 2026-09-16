using DIPS.Mobile.UI.Components.StepFlow;
using Microsoft.Maui.Controls;

namespace DIPS.Mobile.UI.UnitTests.Components.StepFlow;

public class StepFlowItemCompatibilityTests
{
    [Fact]
    public void LockWhenCompleted_BindableProperty_PreservesLegacyContract()
    {
        var property = StepFlowItem.LockWhenCompletedProperty;

        property.PropertyName.Should().Be(nameof(StepFlowItem.LockWhenCompleted));
        property.DeclaringType.Should().Be(typeof(StepFlowItem));
        property.ReturnType.Should().Be(typeof(bool));
        property.DefaultValue.Should().Be(true);
        property.DefaultBindingMode.Should().Be(BindingMode.OneWay);
    }

    [Fact]
    public void CanGoBack_BindableProperty_IsSeparateAndOptIn()
    {
        var property = StepFlowItem.CanGoBackProperty;

        property.Should().NotBeSameAs(StepFlowItem.LockWhenCompletedProperty);
        property.PropertyName.Should().Be(nameof(StepFlowItem.CanGoBack));
        property.DeclaringType.Should().Be(typeof(StepFlowItem));
        property.ReturnType.Should().Be(typeof(bool));
        property.DefaultValue.Should().Be(false);
        property.DefaultBindingMode.Should().Be(BindingMode.OneWay);
    }
}