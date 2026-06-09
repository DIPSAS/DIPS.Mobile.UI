using System.Linq;
using DIPS.Mobile.UI.Resources.Styles;
using DIPS.Mobile.UI.Resources.Styles.Label;
using DIPS.Mobile.UI.Resources.Styles.Span;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using DuiStyles = DIPS.Mobile.UI.Resources.Styles.Styles;

namespace DIPS.Mobile.UI.UnitTests.Resources.Styles;

public class SectionHeaderStyleTests
{
    [Fact]
    public void LabelSectionHeaderStyle_ShouldSetHeadingLevel2()
    {
        var style = DuiStyles.GetLabelStyle(LabelStyle.SectionHeader);

        var headingLevelSetter = GetSetters(style).Single(setter => setter.Property == SemanticProperties.HeadingLevelProperty);

        headingLevelSetter.Value.Should().Be(SemanticHeadingLevel.Level2);
    }

    [Fact]
    public void SpanSectionHeaderStyle_ShouldNotSetHeadingLevel()
    {
        var style = DuiStyles.GetSpanStyle(SpanStyle.SectionHeader);

        GetSetters(style).Should().NotContain(setter => setter.Property == SemanticProperties.HeadingLevelProperty);
    }

    private static IEnumerable<Setter> GetSetters(Style? style)
    {
        while (style is not null)
        {
            foreach (var setter in style.Setters.OfType<Setter>())
            {
                yield return setter;
            }

            style = style.BasedOn;
        }
    }
}