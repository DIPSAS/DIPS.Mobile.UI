﻿using DIPS.Mobile.UI.Converters.MultiValueConverters;

namespace DIPS.Mobile.UI.UnitTests.Converters.MultiValueConverters
{
    public class PositionInListConverterTests
    {
        private readonly PositionInListConverter m_cut = new PositionInListConverter();

        [Theory]
        [InlineData(new object[] { "item 1", new object[] { "item 1", "item 2", "item 3" }, "first", true })]
        [InlineData(new object[] { "item 1", new object[] { "item 1", "item 2", "item 3" }, "0", true })]
        [InlineData(new object[] { "item 3", new object[] { "item 1", "item 2", "item 3" }, "last", true })]
        [InlineData(new object[] { "item 3", new object[] { "item 1", "item 2", "item 3" }, "2", true })]
        [InlineData(new object[] { "item 2", new object[] { "item 1", "item 2", "item 3" }, "1", true })]
        [InlineData(new object[] { "item 3", new object[] { "item 1", "item 2", "item 3" }, "1", false })]
        [InlineData(new object[] { "item 3", new object[] { "item 1", "item 2", "item 3" }, "5", false })]
        [InlineData(new object[] { "item 1", new object[] { "item 1", "item 2", "item 3" }, "first", false, true })]
        [InlineData(new object[] { "item 1", new object[] { "item 1", "item 2", "item 3" }, "0", false, true })]
        [InlineData(new object[] { "item 3", new object[] { "item 1", "item 2", "item 3" }, "last", false, true })]
        [InlineData(new object[] { "item 3", new object[] { "item 1", "item 2", "item 3" }, "2", false, true })]
        [InlineData(new object[] { "item 2", new object[] { "item 1", "item 2", "item 3" }, "1", false ,true})]
        [InlineData(new object[] { "item 3", new object[] { "item 1", "item 2", "item 3" }, "1", true, true })]
        public void Convert_Cases_CorrectOutput(string item, object[] items, string position, bool expected, bool inverted = false)
        {
            m_cut.Position = position;
            m_cut.Inverted = inverted;

            var output = m_cut.Convert<bool>(new object[] { item, items });

            output.Should().Be(expected);
        }
    }
}
