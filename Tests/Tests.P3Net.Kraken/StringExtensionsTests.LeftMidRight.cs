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
        #region Left
        
        [TestMethod]
        public void Left_CountLessThanLength()
        {
            //Act
            var actual = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Left(10);

            //Assert
            actual.Should().Be("ABCDEFGHIJ");
        }

        [TestMethod]
        public void Left_CountIsZero ()
        {
            //Act
            var actual = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Left(0);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Left_CountIsTooLarge ()
        {
            var target = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            //Act
            var actual = target.Left(50);

            //Assert
            actual.Should().Be(target);
        }

        [TestMethod]
        public void Left_CountIsNegative ()
        {
            Action action = () => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Left(-1);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region Mid

        [TestMethod]
        public void Mid_InMiddle ()
        {
            //Act
            var actual = "ABC123DEF".Mid(3, 5);

            //Assert
            actual.Should().Be("123");
        }

        [TestMethod]
        public void Mid_StartToMiddle ()
        {
            //Act
            var actual = "ABC123DEF".Mid(0, 2);

            //Assert
            actual.Should().Be("ABC");
        }

        [TestMethod]
        public void Mid_MiddleToEnd ()
        {
            //Act
            var actual = "ABC123DEF".Mid(6, 8);

            //Assert
            actual.Should().Be("DEF");
        }

        [TestMethod]
        public void Mid_EntireString ()
        {
            var target = "ABC123DEF";

            //Act
            var actual = target.Mid(0, 8);

            //Assert
            actual.Should().Be(target);
        }

        [TestMethod]
        public void Mid_StartIsNegative ()
        {
            Action action = () => "ABC".Mid(-1, 8);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void Mid_EndLessThanStart ()
        {
            Action action = () => "ABC".Mid(0, -1);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]
        public void Mid_StartLargerThanLength ()
        {
            //Act
            var actual = "ABC".Mid(3, 3);
                       
            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]        
        public void Mid_EndLargerThanLength ()
        {
            //Act
            var actual = "ABC123".Mid(3, 10);
            
            //Assert
            actual.Should().Be("123");
        }
        #endregion

        #region Right

        [TestMethod]
        public void Right_CountLessThanLength ()
        {
            //Act
            var actual = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Right(10);

            //Assert
            actual.Should().Be("QRSTUVWXYZ");
        }

        [TestMethod]
        public void Right_CountIsZero ()
        {
            //Act
            var actual = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Right(0);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void Right_CountIsTooLarge ()
        {
            var target = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            //Act
            var actual = target.Right(50);

            //Assert
            actual.Should().Be(target);
        }

        [TestMethod]
        public void Right_CountIsNegative ()
        {
            Action action = () => "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Right(-1);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion
    }
}
