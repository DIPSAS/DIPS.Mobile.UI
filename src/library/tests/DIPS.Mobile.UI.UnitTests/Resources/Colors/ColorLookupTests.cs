using System;
using System.Collections.Generic;
using System.Linq;
using DIPS.Mobile.UI.Resources.Colors;
using FluentAssertions;
using Xamarin.Forms.Internals;
using Xunit;
using Xunit.Abstractions;
using Enum = DIPS.Mobile.UI.Extensions.Enum;

namespace DIPS.Mobile.UI.UnitTests.Resources.Colors
{
    public class ColorLookupTests
    {
        private readonly ITestOutputHelper m_output;

        public ColorLookupTests(ITestOutputHelper output)
        {
            m_output = output;
        }
        
        [Fact]
        public void CheckAllColorsAndPrintAllColors()
        {
            var darkLightModeColors = new Dictionary<ColorName, ColorName>();
            var allColors = Enum.ToList<ColorName>().ToList();
            foreach (var colorName in allColors)
            {
                var (shouldHaveADarkModeColor, expectedDarkModeColorName) = ColorLookup.HasDarkModeColor(colorName);
                if (!shouldHaveADarkModeColor)
                {
                    expectedDarkModeColorName = colorName;
                }
                
                var oppositeColorName = ColorLookup.GetColorName(colorName, shouldHaveADarkModeColor);
                
                if(System.Enum.TryParse(oppositeColorName, out ColorName actualDarkModeColorName))
                {
                    actualDarkModeColorName.Should().Be(expectedDarkModeColorName);

                    if (expectedDarkModeColorName != colorName) //Print only colors that has a dark mode variant to the UI
                    {
                        darkLightModeColors.Add(colorName, actualDarkModeColorName);
                    }
                }
            }
            m_output.WriteLine("🎨 All Colors: \t");
            allColors.ForEach(c => m_output.WriteLine($"{c.ToString()}\t"));
            m_output.WriteLine("\n🎨 All Colors with dark mode variant: \t");
            darkLightModeColors.ForEach(pair => m_output.WriteLine($"{pair.Key}\n\tDark mode: {pair.Value}"));
        }
    }
}