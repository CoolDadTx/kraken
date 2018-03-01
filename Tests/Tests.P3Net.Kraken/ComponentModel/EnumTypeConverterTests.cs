using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using P3Net.Kraken.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.ComponentModel;

namespace Tests.P3Net.Kraken.ComponentModel
{
    [TestClass]
    public class EnumTypeConverterTests : UnitTest
    {
        #region CanConvertFrom
        
        [TestMethod]
        public void CanConvertFrom_IsString ()
        {
            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.CanConvertFrom(typeof(string));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanConvertFrom_IsType ()
        {
            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.CanConvertFrom(typeof(DateTimeKind));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanConvertFrom_IsInvalid ()
        {
            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.CanConvertFrom(typeof(UnitTest));

            actual.Should().BeFalse();
        }
        #endregion

        #region CanConvertTo

        [TestMethod]
        public void CanConvertTo_IsString ()
        {
            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.CanConvertTo(typeof(string));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanConvertTo_IsType ()
        {
            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.CanConvertTo(typeof(DateTimeKind));

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanConvertTo_IsInvalid ()
        {
            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.CanConvertTo(typeof(UnitTest));

            actual.Should().BeFalse();
        }
        #endregion

        #region ConvertFrom

        [TestMethod]
        public void ConvertFrom_IsValidString ()
        {
            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.ConvertFrom("Utc");

            actual.Should().Be(DateTimeKind.Utc);
        }

        [TestMethod]
        public void ConvertFrom_IsInvalidString ()
        {
            var target = new EnumTypeConverter<DateTimeKind>();
            Action action = () => target.ConvertFrom("abc");

            action.Should().Throw<FormatException>();
        }

        [TestMethod]
        public void ConvertFrom_IsOfType ()
        {
            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.ConvertFrom(DateTimeKind.Local);

            actual.Should().Be(DateTimeKind.Local);
        }

        [TestMethod]
        public void ConvertFrom_CallsBase ()
        {
            var target = new DoNothingEnumTypeConverter();
            var actual = target.ConvertFrom("Second");

            actual.Should().Be(SimpleEnum.Second);
        }

        [TestMethod]
        public void ConvertFrom_HasCustomConversion ()
        {
            var target = new TestEnumTypeConverter();
            var actual = target.ConvertFrom("Two");

            actual.Should().Be(SimpleEnum.Second);
        }
        #endregion

        #region ConvertTo

        [TestMethod]
        public void ConvertTo_ToType ()
        {
            var expected = DateTimeKind.Local;

            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.ConvertTo(expected, typeof(DateTimeKind));

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ConvertTo_ToString ()
        {
            var expected = DateTimeKind.Local;

            var target = new EnumTypeConverter<DateTimeKind>();
            var actual = target.ConvertTo(expected, typeof(string));

            actual.Should().Be(expected.ToString());
        }

        [TestMethod]
        public void ConvertTo_CallsBase ()
        {
            var target = new DoNothingEnumTypeConverter();
            var actual = target.ConvertTo(SimpleEnum.Second, typeof(string));

            actual.Should().Be("Second");
        }

        [TestMethod]
        public void ConvertTo_HasCustomConverter ()
        {
            var target = new TestEnumTypeConverter();
            var actual = target.ConvertTo(SimpleEnum.Third, typeof(string));

            actual.Should().Be("Three");
        }
        #endregion

        #region Private Members

        private enum SimpleEnum { First, Second, Third }

        private sealed class TestEnumTypeConverter : EnumTypeConverter<SimpleEnum>
        {
            protected override bool FromString ( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string str, out SimpleEnum result )
            {
                if (String.Compare(str, "One", true) == 0)
                {
                    result = SimpleEnum.First;
                    return true;
                } else if (String.Compare(str, "Two", true) == 0)
                {
                    result = SimpleEnum.Second;
                    return true;
                } else if (String.Compare(str, "Three", true) == 0)
                {
                    result = SimpleEnum.Third;
                    return true;
                };

                return base.FromString(context, culture, str, out result);
            }

            protected override bool ToString ( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, SimpleEnum value, out string result )
            {
                switch (value)
                {
                    case SimpleEnum.First: result = "One"; return true;
                    case SimpleEnum.Second: result = "Two"; return true;
                    case SimpleEnum.Third: result = "Three"; return true;
                };

                return base.ToString(context, culture, value, out result);
            }
        }

        private sealed class DoNothingEnumTypeConverter : EnumTypeConverter<SimpleEnum>
        {
            protected override bool FromString ( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, string str, out SimpleEnum result )
            {
                result = SimpleEnum.First;
                return false;
            }

            protected override bool ToString ( System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, SimpleEnum value, out string result )
            {
                result = null;

                return false;
            }
        }
        #endregion
    }
}
