using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.UnitTests.Converters.ValueConverters
{
    public class TypeToObjectConverterTests
    {
        private const string ExpectedTrueObject = "true string";
        private const string ExpectedFalseObject = "false string";
        private const string InputObject = "input string";
        private readonly TypeToObjectConverter m_typeToObjectConverter = new();


        [Fact]
        public void Convert_ValueIsNull_ThrowsXamlParseException()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(string);

            Action act = () => m_typeToObjectConverter.Convert<object>(null);

            act.Should().Throw<XamlParseException>().Where(ex => ex.Message.Contains("value")).And.Message.Contains("null");
        }

        [Fact]
        public void Convert_TrueObjectIsNull_ThrowsXamlParseException()
        {
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(string);

            Action act = () => m_typeToObjectConverter.Convert<object>(InputObject);

            act.Should().Throw<XamlParseException>().Where(ex => ex.Message.Contains("TrueObject")).And.Message.Contains("null");
        }
        [Fact]
        public void Convert_FalseObjectIsNull_ThrowsXamlParseException()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.Type = typeof(string);

            Action act = () => m_typeToObjectConverter.Convert<object>(InputObject);

            act.Should().Throw<XamlParseException>().Where(ex => ex.Message.Contains("FalseObject")).And.Message.Contains("null");
        }

        [Fact]
        public void Convert_ValueIsSameType_TrueObjectAsOutput()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(string);

            var actualOutput = m_typeToObjectConverter.Convert<object>(InputObject);

            actualOutput.Should().Be(ExpectedTrueObject);
        }
        [Fact]
        public void Convert_ValueIsNotSameType_FalseObjectAsOutput()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(string);

            var actualOutput = m_typeToObjectConverter.Convert<object>(1);

            actualOutput.Should().Be(ExpectedFalseObject);
        }
        
        [Fact]
        public void Convert_ValueIsOfSubTypeWhenDisallowSubTypes_FalseObjectAsOutput()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(MyTestTypeA);

            var actualOutput = m_typeToObjectConverter.Convert<string>(new MyTestTypeB());

            actualOutput.Should().Be(ExpectedFalseObject);
        }
        
        [Fact]
        public void Convert_ValueIsOfSubTypeWhenAllowSubTypes_TrueObjectAsOutput()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(MyTestTypeA);
            m_typeToObjectConverter.IncludeInheritance = true;

            var actualOutput = m_typeToObjectConverter.Convert<string>(new MyTestTypeB());

            actualOutput.Should().Be(ExpectedTrueObject);
        }
        
        [Fact]
        public void Convert_ValueIsNotOfSubTypeWhenAllowSubTypes_FalseObjectAsOutput()
        {
            m_typeToObjectConverter.TrueObject = ExpectedTrueObject;
            m_typeToObjectConverter.FalseObject = ExpectedFalseObject;
            m_typeToObjectConverter.Type = typeof(MyTestTypeA);
            m_typeToObjectConverter.IncludeInheritance = true;

            var actualOutput = m_typeToObjectConverter.Convert<string>("This is a string");

            actualOutput.Should().Be(ExpectedFalseObject);
        }

        private class MyTestTypeA;

        private class MyTestTypeB : MyTestTypeA;
    }
}
