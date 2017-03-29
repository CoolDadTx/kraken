#region Imports

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
#endregion

namespace Tests.P3Net.Kraken
{    
    public partial class StringExtensionsTests
    {
        #region Char

        [TestMethod]
        public void LeftOf_Char_InMiddle ()
        {
            //Act
            var actual = "ABC1DEF".LeftOf('1');

            //Assert
            actual.Should().Be("ABC");
        }

        [TestMethod]
        public void LeftOf_Char_AtStart ()
        {
            //Act
            var actual = "1ABC".LeftOf('1');

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void LeftOf_Char_AtEnd ()
        {
            //Act
            var actual = "ABCDEF1".LeftOf('1');

            //Assert
            actual.Should().Be("ABCDEF");
        }

        [TestMethod]
        public void LeftOf_Char_NotFound ()
        {
            //Act
            var actual = "ABCDEF".LeftOf('1');

            //Assert
            actual.Should().Be("ABCDEF");
        }

        [TestMethod]
        public void LeftOf_Char_StringIsEmpty ()
        {
            //Act
            var actual = "".LeftOf('1');

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void LeftOf_Char_MultipleTokens () 
        {
            //Act
            var actual = "ABC1DEF2GHI3".LeftOf('1', '2', '3');

            //Assert
            actual.Should().Be("ABC");
        }
        #endregion

        #region IList<char>

        [TestMethod]
        public void LeftOf_ListOfChar_InMiddle ()
        {
            //Act
            var actual = "ABC1DEF2GHI3".LeftOf(new char[] { '1', '2' });

            //Assert
            actual.Should().Be("ABC");
        }

        [TestMethod]
        public void LeftOf_ListOfChar_AtStart ()
        {
            //Act
            var actual = "2ABC1DEF".LeftOf(new char[] { '1', '2' });

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void LeftOf_ListOfChar_AtEnd ()
        {
            //Act
            var actual = "ABCDEF2".LeftOf(new char[] { '1', '2' });

            //Assert
            actual.Should().Be("ABCDEF");
        }

        [TestMethod]
        public void LeftOf_ListOfChar_NotFound ()
        {
            var target = "ABCDEF";

            //Act
            var actual = target.LeftOf(new char[] { '1', '2' });

            //Assert
            actual.Should().Be(target);
        }

        [TestMethod]
        public void LeftOf_ListOfChar_StringIsEmpty ()
        {
            //Act
            var actual = "".LeftOf(new char[] { '1', '2' });

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void LeftOf_ListOfChar_TokenIsNull ()
        {
            //Arrange
            IList<char> tokens = null;
            var target = "ABC";

            //Act
            var actual = target.LeftOf(tokens);

            //Assert
            actual.Should().Be(target);
        }

        [TestMethod]
        public void LeftOf_ListOfChar_TokenIsEmpty ()
        {
            //Arrange            
            var target = "ABC";

            //Act
            var actual = target.LeftOf(new List<char>());

            //Assert
            actual.Should().Be(target);
        }
        #endregion

        #region String

        [TestMethod]
        public void LeftOf_String_InMiddle ()
        {
            //Act
            var actual = "ABC:_DEF".LeftOf(":_");

            //Assert
            actual.Should().Be("ABC");
        }

        [TestMethod]
        public void LeftOf_String_AtStart ()
        {
            //Act
            var actual = ":_ABC".LeftOf(":_");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void LeftOf_String_AtEnd ()
        {
            //Act
            var actual = "ABCDEF:_".LeftOf(":_");

            //Assert
            actual.Should().Be("ABCDEF");
        }

        [TestMethod]
        public void LeftOf_String_NotFound ()
        {
            //Act
            var actual = "ABCDEF".LeftOf(":_");

            //Assert
            actual.Should().Be("ABCDEF");
        }

        [TestMethod]
        public void LeftOf_String_StringIsEmpty ()
        {
            //Act
            var actual = "".LeftOf(":_");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void LeftOf_String_CaseInsensitive ()
        {
            //Act
            var actual = "ABCxxDEF".LeftOf("XX", StringComparison.CurrentCultureIgnoreCase);

            //Assert
            actual.Should().Be("ABC");
        }

        [TestMethod]
        public void LeftOf_String_WithDefaultComparison ()
        {
            //Act
            var actual = "ABCxxDEF".LeftOf("XX");

            //Assert
            actual.Should().Be("ABCxxDEF");
        }

        [TestMethod]
        public void LeftOf_String_TokenIsNull ()
        {
            var target = "ABC";
            string token = null;

            //Act
            var actual = target.LeftOf(token);

            //Assert
            actual.Should().Be(actual);
        }

        [TestMethod]
        public void LeftOf_String_TokenIsEmpty ()
        {
            var target = "ABC";
            
            //Act
            var actual = target.LeftOf("");

            //Assert
            actual.Should().Be(actual);
        }
        #endregion
    }
}
