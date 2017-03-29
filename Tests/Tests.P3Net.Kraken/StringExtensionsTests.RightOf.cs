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
        public void RightOf_Char_InMiddle ()
        {
            //Act
            var actual = "ABC1DEF".RightOf('1');

            //Assert
            actual.Should().Be("DEF");
        }

        [TestMethod]
        public void RightOf_Char_AtStart ()
        {
            //Act
            var actual = "1ABC".RightOf('1');

            //Assert
            actual.Should().Be("ABC");
        }

        [TestMethod]
        public void RightOf_Char_AtEnd ()
        {
            //Act
            var actual = "ABCDEF1".RightOf('1');

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_Char_NotFound ()
        {
            //Act
            var actual = "ABCDEF".RightOf('1');

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_Char_StringIsEmpty ()
        {
            //Act
            var actual = "".RightOf('1');

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_Char_MultipleTokens ()
        {
            //Act
            var actual = "ABC1DEF2GHI3".RightOf('1', '2', '3');

            //Assert
            actual.Should().Be("DEF2GHI3");
        }
        #endregion

        #region IList<Char>
        
        [TestMethod]
        public void RightOf_ListOfChar_InMiddle()
        {
            //Act
            var actual = "ABC1DEF2".RightOf(new char[] { '1', '2' });

            //Assert
            actual.Should().Be("DEF2");
        }

        [TestMethod]
        public void RightOf_ListOfChar_AtStart ()
        {
            //Act
            var actual = "2ABC1DEF".RightOf(new char[] { '1', '2' });

            //Assert
            actual.Should().Be("ABC1DEF");
        }

        [TestMethod]
        public void RightOf_ListOfChar_AtEnd ()
        {
            //Act
            var actual = "ABCDEF2".RightOf(new char[] { '1', '2' });

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_ListOfChar_NotFound ()
        {
            //Act
            var actual = "ABCDEF".RightOf(new char[] { '1', '2' });

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_ListOfChar_StringIsEmpty ()
        {
            //Act
            var actual = "".RightOf(new char[] { '1', '2' });

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_ListOfChar_TokenIsNull ()
        {
            IList<char> token = null;
            var target = "ABC";

            //Act
            var actual = target.RightOf(token);

            //Assert
            actual.Should().Be(target);
        }

        [TestMethod]
        public void RightOf_ListOfChar_TokenIsEmpty ()
        {
            var target = "ABC";

            //Act
            var actual = target.RightOf(new List<char>());

            //Assert
            actual.Should().Be(target);
        }
        #endregion

        #region String

        [TestMethod]
        public void RightOf_String_InMiddle ()
        {
            //Act
            var actual = "ABC:_DEF".RightOf(":_");

            //Assert
            actual.Should().Be("DEF");
        }

        [TestMethod]
        public void RightOf_String_AtStart ()
        {
            //Act
            var actual = ":_ABC".RightOf(":_");

            //Assert
            actual.Should().Be("ABC");
        }

        [TestMethod]
        public void RightOf_String_AtEnd ()
        {
            //Act
            var actual = "ABCDEF:_".RightOf(":_");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_String_NotFound ()
        {
            //Act
            var actual = "ABCDEF".RightOf(":_");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_String_StringIsEmpty ()
        {
            //Act
            var actual = "".RightOf(":_");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_String_CaseInsensitive ()
        {
            //Act
            var actual = "ABCxxDEF".RightOf("XX", StringComparison.CurrentCultureIgnoreCase);

            //Assert
            actual.Should().Be("DEF");
        }

        [TestMethod]
        public void RightOf_String_WithDefaultComparison ()
        {
            //Act
            var actual = "ABCxxDEF".RightOf("XX");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void RightOf_String_TokenIsNull ()
        {
            var target = "ABC";
            string token = null;

            //Act
            var actual = target.RightOf(token);

            //Assert
            actual.Should().Be(target);
        }
        [TestMethod]
        public void RightOf_String_TokenIsEmpty ()
        {
            var target = "ABC";

            //Act
            var actual = target.RightOf("");

            //Assert
            actual.Should().Be(target);
        }
        #endregion
    }
}
