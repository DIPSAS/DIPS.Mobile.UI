using DIPS.Mobile.UI.Converters.ValueConverters;

namespace DIPS.Mobile.UI.UnitTests.Converters.ValueConverters
{
    public class ObjectToBoolConverterTests
    {
        private readonly ObjectToBoolConverter m_objectToBoolConverter;

        public ObjectToBoolConverterTests()
        {
            m_objectToBoolConverter = new ObjectToBoolConverter();
        }

        [Theory]
        [InlineData(1.0f, 1.0f)]
        [InlineData(1.0f, 0.8f, true)]
        [InlineData(23, 23)]
        [InlineData(156, 28, true)]
        [InlineData("true", "true")]
        [InlineData("fish", "moose", true)]
        private void Convert_TrueOutput(object inputValue, object trueValue, bool inverted = false)
        {
            m_objectToBoolConverter.TrueObject = trueValue;
            m_objectToBoolConverter.Inverted = inverted;
            var result = (bool)m_objectToBoolConverter.Convert(inputValue, null!, null!, null!);
            
            Assert.True(result);
        }
        
        [Theory]
        [InlineData(1.0f, 0.8f)]
        [InlineData(1.0f, 1.0f, true)]
        [InlineData(23, 156)]
        [InlineData(156, 156, true)]
        [InlineData("true", "false")]
        [InlineData("fish", "fish", true)]
        private void Convert_FalseOutput(object inputValue, object trueValue, bool inverted = false)
        {
            m_objectToBoolConverter.TrueObject = trueValue;
            m_objectToBoolConverter.Inverted = inverted;
            var result = (bool)m_objectToBoolConverter.Convert(inputValue, null!, null!, null!);
            
            Assert.False(result);
        }

        [Fact]
        private void Convert_CustomClass_ReturnTrue()
        {
            var customClass = new CustomClass(2, "Hello");
            var otherClass = new CustomClass(2, "Hello");

            m_objectToBoolConverter.TrueObject = customClass;

            var result1 = (bool)m_objectToBoolConverter.Convert(otherClass, null!, null!, null!);
            
            Assert.True(result1);

            m_objectToBoolConverter.Inverted = true;
            
            var result2 = (bool)m_objectToBoolConverter.Convert(otherClass, null!, null!, null!);
            
            Assert.False(result2);
        }
        
        [Fact]
        private void Convert_CustomClass_ReturnFalse()
        {
            var customClass = new CustomClass(2, "Hello");
            var otherClass = new CustomClass(2, "Hello...");

            m_objectToBoolConverter.TrueObject = customClass;

            var result = (bool)m_objectToBoolConverter.Convert(otherClass, null!, null!, null!);
            
            Assert.False(result);
        }

        private class CustomClass
        {
            public CustomClass(int someProperty, string anotherProperty)
            {
                SomeProperty = someProperty;
                AnotherProperty = anotherProperty;
            }

            public int SomeProperty { get; }
            public string AnotherProperty { get; }

            public override bool Equals(object? obj)
            {
                if (obj is CustomClass customClass)
                {
                    return Equals(customClass);
                }
                
                return false;
            }

            protected bool Equals(CustomClass other)
            {
                return SomeProperty == other.SomeProperty && AnotherProperty == other.AnotherProperty;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(SomeProperty, AnotherProperty);
            }
        }
    }
}