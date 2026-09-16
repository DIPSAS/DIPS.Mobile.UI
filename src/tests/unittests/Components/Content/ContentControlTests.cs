using DIPS.Mobile.UI.Components.Content;
using DIPS.Mobile.UI.Components.Content.DataTemplateSelectors;
using Microsoft.Maui.Controls;

namespace DIPS.Mobile.UI.UnitTests.Components.Content;

public class ContentControlTests
{
    [Fact]
    public void BindingContextChanged_NullBoundSelectorItem_DoesNotSelectTemplate()
    {
        var contentControl = CreateContentControl();
        contentControl.SetBinding(ContentControl.SelectorItemProperty, new Binding(nameof(TestViewModel.SelectorItem)));

        var setBindingContext = () => contentControl.BindingContext = new TestViewModel(null);

        setBindingContext.Should().NotThrow();
        contentControl.Content.Should().BeNull();
    }

    [Theory]
    [InlineData(true, "True")]
    [InlineData(false, "False")]
    public void BindingContextChanged_UnsetSelectorItem_UsesBindingContext(bool value, string expectedText)
    {
        var contentControl = CreateContentControl();

        contentControl.BindingContext = value;

        contentControl.Content.Should().BeOfType<Label>().Which.Text.Should().Be(expectedText);
    }

    [Fact]
    public void SelectorItem_ChangedFromNull_SelectsTemplate()
    {
        var contentControl = CreateContentControl();
        contentControl.SetBinding(ContentControl.SelectorItemProperty, new Binding(nameof(TestViewModel.SelectorItem)));
        contentControl.BindingContext = new TestViewModel(null);

        contentControl.BindingContext = new TestViewModel(true);

        contentControl.Content.Should().BeOfType<Label>().Which.Text.Should().Be("True");
    }

    [Fact]
    public void SelectorItem_ChangedToNull_KeepsExistingContentUntilNextValue()
    {
        var contentControl = CreateContentControl();
        contentControl.SelectorItem = true;
        contentControl.BindingContext = new object();
        var originalContent = contentControl.Content;

        var clearSelectorItem = () => contentControl.SelectorItem = null;

        clearSelectorItem.Should().NotThrow();
        contentControl.Content.Should().BeSameAs(originalContent);

        contentControl.SelectorItem = false;

        contentControl.Content.Should().BeOfType<Label>().Which.Text.Should().Be("False");
    }

    private static ContentControl CreateContentControl() => new()
    {
        TemplateSelector = new BooleanDataTemplateSelector
        {
            TrueTemplate = new DataTemplate(() => new Label { Text = "True" }),
            FalseTemplate = new DataTemplate(() => new Label { Text = "False" })
        }
    };

    private sealed record TestViewModel(object? SelectorItem);
}