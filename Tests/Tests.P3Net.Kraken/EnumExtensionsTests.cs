#region Imports

using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public partial class EnumExtensionsTests : UnitTest
    {        
        #region Format

        [TestMethod]
        public void Format_StandardFormat ()
        {
            //Act
            var expected = Enum.Format(typeof(SimpleEnumeration), SimpleEnumeration.Second, "g");
            var actual = EnumExtensions.Format(SimpleEnumeration.Second, "g");

            //Assert
            actual.Should().Be(actual); 
        }

        [TestMethod]
        public void Format_NumericFormat ()
        {
            //Act
            var expected = Enum.Format(typeof(SimpleEnumeration), SimpleEnumeration.Second, "d");
            var actual = EnumExtensions.Format(SimpleEnumeration.Second, "d");

            //Assert
            actual.Should().Be(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void Format_FormatIsEmpty ()
        {
            //Act
            var expected = Enum.Format(typeof(SimpleEnumeration), SimpleEnumeration.Second, "");
            var actual = EnumExtensions.Format(SimpleEnumeration.Second, "");

            //Assert
            actual.Should().Be(actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Format_FormatIsNull ()
        {
            EnumExtensions.Format(SimpleEnumeration.First, null);
        }
        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Format_TypeIsNotEnum ()
        {
            //Act
            EnumExtensions.Format(4.5, "g");
        }

        [TestMethod]
        public void Format_ValueIsOutOfRange ()
        {
            //Act
            var actual = EnumExtensions.Format((SimpleEnumeration)100, "g");

            //Assert
            actual.Should().Be("100");
        }

        [TestMethod]
        public void Format_FlagEnumWithMultipleValues ()
        {
            //Arrange
            var target = FlagEnumeration.Abstract | FlagEnumeration.Sealed;

            //Act
            var actual = EnumExtensions.Format(target, "g");

            //Assert
            actual.Should().Be("Abstract, Sealed");
        }
        #endregion

        #region GetFormattedNames

        [TestMethod]
        public void GetFormattedNames_ValidEnum ()
        {
            //Arrange
            var expected = new Tuple<SimpleEnumeration, string>[] { 
                                Tuple.Create(SimpleEnumeration.First, "First"),
                                Tuple.Create(SimpleEnumeration.Second, "Second"),
                                Tuple.Create(SimpleEnumeration.Third, "Third")
                         };

            //Act                
            var actual = EnumExtensions.GetFormattedNames<SimpleEnumeration>();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetFormattedNames_EnumHasComplexValues ()
        {
            //Arrange
            var expected = new Tuple<ComplexEnumeration, string>[] { 
                                Tuple.Create(ComplexEnumeration.Item1, "Item 1"),
                                Tuple.Create(ComplexEnumeration.Item2, "Item 2"),
                                Tuple.Create(ComplexEnumeration.Item3, "Item 3")
                         };

            //Act                
            var actual = EnumExtensions.GetFormattedNames<ComplexEnumeration>();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetFormattedNames_TypeHasCustomConverter ()
        {
            //Arrange
            var expected = new Tuple<EnumerationWithTypeConverter, string>[] {
                                Tuple.Create(EnumerationWithTypeConverter.First, "One"),
                                Tuple.Create(EnumerationWithTypeConverter.Second, "Two"),
                                Tuple.Create(EnumerationWithTypeConverter.Third, "Three")
                };

            //Act
            var actual = EnumExtensions.GetFormattedNames<EnumerationWithTypeConverter>();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFormattedNames_TypeIsNotEnum ()
        {
            EnumExtensions.GetFormattedNames<int>();
        }
        #endregion

        #region GetName

        [TestMethod]
        public void GetName_ValidEnum ()
        {
            //Act
            var actual = EnumExtensions.GetName(SimpleEnumeration.First);

            //Assert
            actual.Should().Be("First");
        }

        [TestMethod]
        public void GetName_ValueIsOutOfRange ()
        {
            //Act
            var actual = EnumExtensions.GetName((SimpleEnumeration)100);            

            //Assert
            actual.Should().BeNull();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetName_TypeIsNotEnum ()
        {
            //Act
            EnumExtensions.GetName(4.5);
        }

        [TestMethod]        
        public void GetName_FlagEnumWithMultipleValues ()
        {
            //Act
            var actual = EnumExtensions.GetName(FlagEnumeration.Abstract | FlagEnumeration.Sealed);

            //Assert
            actual.Should().BeNull();
        }
        #endregion

        #region GetNames

        [TestMethod]
        public void GetNames_ValidEnum ()
        {
            //Arrange
            var expected = new string[] { "First", "Second", "Third" };
        
            //Act                
            var actual = EnumExtensions.GetNames<SimpleEnumeration>();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetNames_EnumHasComplexValues ()
        {
            //Arrange
            var expected = new string[] { "Item1", "Item2", "Item3" };

            //Act                
            var actual = EnumExtensions.GetNames<ComplexEnumeration>();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void GetNames_TypeHasCustomConverter ()
        {
            //Arrange
            var expected = new string[] { "First", "Second", "Third" };

            //Act
            var actual = EnumExtensions.GetNames<EnumerationWithTypeConverter>();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetNames_TypeIsNotEnum ()
        {
            EnumExtensions.GetNames<int>();
        }
        #endregion

        #region GetUnderlyingType

        [TestMethod]
        public void GetUnderlyingType_EnumIsInt32 ()
        {
            //Act
            var actual = EnumExtensions.GetUnderlyingType<SimpleEnumeration>();

            //Assert
            actual.Should().Be(typeof(int));
        }

        [TestMethod]
        public void GetUnderlyingType_EnumIsInt16 ()
        {
            //Act
            var actual = EnumExtensions.GetUnderlyingType<SmallEnumeration>();

            //Assert
            actual.Should().Be(typeof(short));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetUnderlyingType_TypeIsNotEnum ()
        {
            //Act
            EnumExtensions.GetUnderlyingType<int>();
        }
        #endregion

        #region GetValues

        [TestMethod]
        public void GetValues_SimpleEnumeration ()
        {
            //Arrange
            var expected = new SimpleEnumeration[] { 
                                SimpleEnumeration.First, 
                                SimpleEnumeration.Second,
                                SimpleEnumeration.Third
                };

            //Act
            var actual = EnumExtensions.GetValues<SimpleEnumeration>();

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetValues_TypeIsNotEnum ()        
        {
            EnumExtensions.GetValues<int>();
        }
        #endregion

        #region IsAll

        [TestMethod]
        public void IsAll_ValueInList ()
        {
            var target = FlagEnumeration.Abstract | FlagEnumeration.Static;

            //Act
            var actual = target.IsAll(FlagEnumeration.Abstract, FlagEnumeration.Static);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsAll_ValueIsNotInList ()
        {
            var target = FlagEnumeration.Sealed;

            //Act
            var actual = target.IsAll(FlagEnumeration.Abstract, FlagEnumeration.Sealed);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAll_ListIsNull ()
        {
            var target = FlagEnumeration.Sealed;

            //Act
            var actual = target.IsAll();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAll_ListIsEmpty ()
        {
            var target = FlagEnumeration.Sealed;

            //Act
            var actual = target.IsAll(new Enum[0]);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAll_ArrayIsNull ()
        {
            var target = FlagEnumeration.Sealed;

            //Act
            var actual = target.IsAll(null);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsAny

        [TestMethod]
        public void IsAny_ValueInList ()
        {
            var target = SimpleEnumeration.Second;

            //Act
            var actual = target.IsAny(SimpleEnumeration.First, SimpleEnumeration.Second);

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsAny_ValueIsNotInList ()
        {
            var target = SimpleEnumeration.Second;

            //Act
            var actual = target.IsAny(SimpleEnumeration.First, SimpleEnumeration.Third);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAny_ListIsNull ()
        {
            var target = SimpleEnumeration.Second;

            //Act
            var actual = target.IsAny();

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAny_ListIsEmpty ()
        {
            var target = SimpleEnumeration.Second;

            //Act
            var actual = target.IsAny(new Enum[0]);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAny_ArrayIsNull ()
        {
            var target = SimpleEnumeration.Second;

            //Act
            var actual = target.IsAny(null);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAny_WithFlag_HasNone ()
        {
            var target = FlagEnumeration.Abstract | FlagEnumeration.Sealed;

            var actual = target.IsAny(FlagEnumeration.Static);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsAny_WithFlag_HasSome ()
        {
            var target = FlagEnumeration.Abstract | FlagEnumeration.Sealed;

            var actual = target.IsAny(FlagEnumeration.Static, FlagEnumeration.Abstract);

            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsAny_WithFlag_HasAll ()
        {
            var target = FlagEnumeration.Abstract | FlagEnumeration.Sealed;

            var actual = target.IsAny(FlagEnumeration.Abstract, FlagEnumeration.Sealed);

            actual.Should().BeTrue();
        }
        #endregion

        #region Parse

        [TestMethod]
        public void Parse_IsValidWithText ()
        {
            //Act
            var actual = EnumExtensions.Parse<SimpleEnumeration>("First");

            //Assert
            actual.Should().Be(SimpleEnumeration.First);
        }

        [TestMethod]
        public void Parse_IsValidWithFlags ()
        {
            //Act
            var actual = EnumExtensions.Parse<FlagEnumeration>("Abstract, Sealed");

            //Assert
            actual.Should().Be(FlagEnumeration.Abstract | FlagEnumeration.Sealed);
        }

        [TestMethod]
        public void Parse_IsValidWithNumber ()
        {
            //Act
            var actual = EnumExtensions.Parse<SimpleEnumeration>("2");

            //Assert
            actual.Should().Be(SimpleEnumeration.Second);
        }

        [TestMethod]
        public void Parse_IsNotValid ()
        {
            //Act
            Action action = () => EnumExtensions.Parse<SimpleEnumeration>("Twenty");

            action.Should().Throw<FormatException>();
        }

        [TestMethod]
        public void Parse_ValueIsEmpty ()
        {
            Action action = () => EnumExtensions.Parse<SimpleEnumeration>("");

            action.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void Parse_ValueIsNull ()
        {
            Action action = () => EnumExtensions.Parse<SimpleEnumeration>(null);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void Parse_IsValidWithText_IgnoringCase ()
        {
            //Act
            var actual = EnumExtensions.Parse<SimpleEnumeration>("first", true);

            //Assert
            actual.Should().Be(SimpleEnumeration.First);
        }

        [TestMethod]
        public void Parse_TypeIsNotEnum ()
        {
            Action action = () => EnumExtensions.Parse<double>("4.5");

            action.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void Parse_WithCustomConverterWorks ()
        {
            var expected = EnumerationWithTypeConverter.First;
            var actual = EnumExtensions.Parse<EnumerationWithTypeConverter>("One");

            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Parse_WithCustomConverterDefaultsToBase ()
        {
            var actual = EnumExtensions.Parse<EnumerationWithTypeConverter>("First");

            actual.Should().Be(EnumerationWithTypeConverter.First);
        }
        #endregion

        #region ToString

        [TestMethod]
        public void ToString_WithNoConverter ()
        {
            var actual = EnumExtensions.ToString(SimpleEnumeration.First);

            actual.Should().Be(SimpleEnumeration.First.ToString());
        }

        [TestMethod]
        public void ToString_WithConverter ()
        {
            var actual = EnumExtensions.ToString(EnumerationWithTypeConverter.Second);

            actual.Should().Be("Two");
        }

        [TestMethod]
        public void ToString_NotEnum ()
        {
            Action action = () => EnumExtensions.ToString(DateTime.Now);

            action.ShouldThrowArgumentException();
        }
        #endregion

        #region TryParse

        [TestMethod]
        public void TryParse_IsValidWithText ()
        {
            SimpleEnumeration result;
            var actual = EnumExtensions.TryParse("First", out result);

            actual.Should().BeTrue();
            result.Should().Be(SimpleEnumeration.First);
        }

        [TestMethod]
        public void TryParse_IsValidWithFlags ()
        {
            FlagEnumeration result;
            var actual = EnumExtensions.TryParse("Abstract, Sealed", out result);

            actual.Should().BeTrue();
            result.Should().Be(FlagEnumeration.Abstract | FlagEnumeration.Sealed);
        }

        [TestMethod]
        public void TryParse_IsValidWithNumber ()
        {
            SimpleEnumeration result;
            var actual = EnumExtensions.TryParse("2", out result);

            actual.Should().BeTrue();
            result.Should().Be(SimpleEnumeration.Second);
        }

        [TestMethod]
        public void TryParse_IsNotValid ()
        {
            SimpleEnumeration result;
            var actual = EnumExtensions.TryParse("Twenty", out result);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void TryParse_ValueIsEmpty ()
        {
            SimpleEnumeration result;
            var actual = EnumExtensions.TryParse("", out result);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void TryParse_ValueIsNull ()
        {
            SimpleEnumeration result;
            var actual = EnumExtensions.TryParse(null, out result);

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void TryParse_IsValidWithText_IgnoringCase ()
        {
            SimpleEnumeration result;
            var actual = EnumExtensions.TryParse("first", true, out result);

            //Assert
            actual.Should().BeTrue();
            result.Should().Be(SimpleEnumeration.First);
        }

        [TestMethod]
        public void TryParse_TypeIsNotEnum ()
        {
            double result;
            Action action = () => EnumExtensions.TryParse("4.5", out result);

            action.ShouldThrowArgumentException();
        }

        [TestMethod]
        public void TryParse_WithCustomConverterWorks ()
        {
            EnumerationWithTypeConverter result;
            var actual = EnumExtensions.TryParse("One", out result);

            actual.Should().BeTrue();
            result.Should().Be(EnumerationWithTypeConverter.First);
        }

        [TestMethod]
        public void TryParse_WithCustomConverterDefaultsToBase ()
        {
            EnumerationWithTypeConverter result;
            var actual = EnumExtensions.TryParse("First", out result);

            actual.Should().BeTrue();
            result.Should().Be(EnumerationWithTypeConverter.First);
        }

        [TestMethod]
        public void TryParse_BadFormat ()
        {
            var expected = FailEnumeration.Format;

            FailEnumeration result;
            var actual = EnumExtensions.TryParse(expected.ToString(), out result);

            result.Should().Be(expected);
        }

        [TestMethod]
        public void TryParse_BadArgument ()
        {
            var expected = FailEnumeration.Argument;

            FailEnumeration result;
            var actual = EnumExtensions.TryParse(expected.ToString(), out result);

            result.Should().Be(expected);
        }
        #endregion
        
        #region Private Members

        private enum SimpleEnumeration
        {
            First = 1,
            Second = 2,
            Third = 3,
        }

        private enum ComplexEnumeration
        {
            Item1 = 1,
            Item2 = 2,
            Item3 = 3
        }

        private enum SmallEnumeration : short
        {
            A = 1,
            B = 2,
            C = 3
        }

        private enum LargeEnumeration : long
        {
            Small = 1,
            Medium = 2,
            Large = 3,
        }

        [Flags]
        private enum FlagEnumeration
        {
            Abstract = 1,
            Sealed = 2,
            Static = 4,
        }

        [TypeConverter(typeof(EnumerationWithTypeConverterConverter))]
        private enum EnumerationWithTypeConverter
        {
            First = 1,
            Second = 2,
            Third = 3,
        }

        [TypeConverter(typeof(FailEnumerationTypeConverter))]
        private enum FailEnumeration { Format, Argument, NotSupported }

        //Returns the numeric equivalent
        private sealed class EnumerationWithTypeConverterConverter : TypeConverter
        {
            public override object ConvertTo ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
            {
                if (destinationType == typeof(string))
                {
                    switch ((EnumerationWithTypeConverter)value)
                    {
                        case EnumerationWithTypeConverter.First: return "One";
                        case EnumerationWithTypeConverter.Second: return "Two";
                        case EnumerationWithTypeConverter.Third: return "Three";
                    };
                };

                return null;
            }

            public override object ConvertFrom ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
            {
                var str = value as string;
                if (str != null)
                {
                    if (String.Compare(str, "One", true) == 0)
                        return EnumerationWithTypeConverter.First;
                    else if (String.Compare(str, "Two", true) == 0)
                        return EnumerationWithTypeConverter.Second;
                    else if (String.Compare(str, "Three", true) == 0)
                        return EnumerationWithTypeConverter.Third;
                };

                return null;
            }
        }

        private sealed class FailEnumerationTypeConverter : TypeConverter
        {
            public override object ConvertFrom ( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value )
            {
                var str = value as string;
                if (String.Compare(str, "Format", true) == 0)
                    throw new FormatException();
                else if (String.Compare(str, "Argument", true) == 0)
                    throw new ArgumentException();
                else if (String.Compare(str, "NotSupported", true) == 0)
                    throw new NotSupportedException();

                return null;
            }
        }
        #endregion
    }
}
