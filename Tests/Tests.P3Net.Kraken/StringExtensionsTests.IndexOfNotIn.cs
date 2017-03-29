#region Imports

using System;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken
{    
    public partial class StringExtensionsTests
    {
        [TestMethod]
        public void IndexOfNotIn_NoTokenFound ()
        {
            //Act
            var actual = "ABC".IndexOfNotIn('1', '2', '3');

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void IndexOfNotIn_AllTokensInString ()
        {
            //Act
            var actual = "123".IndexOfNotIn('1', '2', '3');

            //Assert
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void IndexOfNotIn_TokenFoundInMiddle ()
        {
            //Act
            var actual = "AB2C".IndexOfNotIn('1', '2', '3');

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void IndexOfNotIn_TokenFoundAtStart ()
        {
            //Act
            var actual = "12ABC".IndexOfNotIn('1', '2', '3');

            //Assert
            actual.Should().Be(2);
        }

        [TestMethod]
        public void IndexOfNotIn_StartIndexWithNoTokens ()
        {
            var tokens = new char[] { '1', '2', '3' };

            //Act            
            var actual = "ABCDEFG".IndexOfNotIn(tokens, 3);

            //Assert
            actual.Should().Be(3);
        }

        [TestMethod]
        public void IndexOfNotIn_StartIndexWithTokensAfterIndex ()
        {
            var tokens = new char[] { '1', '2', '3' };

            //Act            
            var actual = "ABCD1FG".IndexOfNotIn(tokens, 3);

            //Assert
            actual.Should().Be(3);
        }

        [TestMethod]
        public void IndexOfNotIn_StartIndexWithTokensBeforeAndAfterIndex ()
        {
            var tokens = new char[] { '1', '2', '3' };

            //Act            
            var actual = "2ABCD1FG".IndexOfNotIn(tokens, 3);

            //Assert
            actual.Should().Be(3);
        }

        [TestMethod]
        public void IndexOfNotIn_TokenIsNull ()
        {
            Action action = () => "ABCD1FG".IndexOfNotIn(null, 3);

            action.ShouldThrowArgumentNullException();
        }

        [TestMethod]
        public void IndexOfNotIn_TokenIsEmpty ()
        {
            var tokens = new char[] { };
    
            var actual = "ABCD1FG".IndexOfNotIn(tokens, 0);

            //Assert
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void IndexOfNotIn_StartIndexIsNegative ()
        {   
            Action action = () => "ABCD1FG".IndexOfNotIn(new char[] { '1', '2', '3'}, -1);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void IndexOfNotIn_StartIndexTooLarge ()
        {
            //Act
            var actual = "ABCD1FG".IndexOfNotIn(new char[] { '1', '2', '3' }, 100);

            //Assert
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void IndexOfNotIn_SourceIsEmpty ()
        {
            //Act
            var actual = "".IndexOfNotIn(new char[] { '1', '2', '3' }, 0);

            //Assert
            actual.Should().Be(-1);
        }
    }
}
