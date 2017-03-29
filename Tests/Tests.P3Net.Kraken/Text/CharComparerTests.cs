using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Text;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Text
{
    [TestClass]
    public class CharComparerTests : UnitTest
    {
        #region CurrentCulture

        [TestMethod]
        public void Compare_CurrentCulture_SameCase ( )
        {
            //Act
            var target = CharComparer.GetComparer(StringComparison.CurrentCulture);
            var actual = target.Compare(CapitalLetterI, CapitalLetterI);

            //Assert 
            actual.Should().Be(0); 
        }

        [TestMethod]
        public void Compare_CurrentCulture_MixedCase ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.CurrentCulture);
            var actual = target.Compare(CapitalLetterI, SmallLetterI);

            //Assert 
            actual.Should().NotBe(0);
        }

        [TestMethod]
        public void Compare_CurrentCulture_FirstLessThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.CurrentCulture);
            var actual = target.Compare(SmallLetterI, SmallDotlessLetterI);
            
            //Assert 
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void Compare_CurrentCulture_FirstGreaterThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.CurrentCulture);
            var actual = target.Compare(SmallDotlessLetterI, SmallLetterI);

            //Assert 
            actual.Should().Be(1);
        }
        #endregion

        #region CurrentCultureIgnoreCase

        [TestMethod]
        public void Compare_CurrentCultureIgnoreCase_SameCase ()
        {
            //Act
            var target = CharComparer.GetComparer(StringComparison.CurrentCultureIgnoreCase);
            var actual = target.Compare(CapitalLetterI, CapitalLetterI);

            //Assert 
            actual.Should().Be(0);
        }

        [TestMethod]
        public void Compare_CurrentCultureIgnoreCase_MixedCase ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.CurrentCultureIgnoreCase);
            var actual = target.Compare(CapitalLetterI, SmallLetterI);

            //Assert 
            actual.Should().Be(0);
        }

        [TestMethod]
        public void Compare_CurrentCultureIgnoreCase_FirstLessThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.CurrentCultureIgnoreCase);
            var actual = target.Compare(SmallLetterI, SmallDotlessLetterI);

            //Assert 
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void Compare_CurrentCultureIgnoreCase_FirstGreaterThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.CurrentCultureIgnoreCase);
            var actual = target.Compare(SmallDotlessLetterI, SmallLetterI);

            //Assert 
            actual.Should().Be(1);
        }
        #endregion

        #region InvariantCulture

        [TestMethod]
        public void Compare_InvariantCulture_SameCase ()
        {
            //Act
            var target = CharComparer.GetComparer(StringComparison.InvariantCulture);
            var actual = target.Compare(CapitalLetterI, CapitalLetterI);

            //Assert 
            actual.Should().Be(0);
        }

        [TestMethod]
        public void Compare_InvariantCulture_MixedCase ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.InvariantCulture);
            var actual = target.Compare(CapitalLetterI, SmallDotlessLetterI);

            //Assert 
            actual.Should().NotBe(0);
        }

        [TestMethod]
        public void Compare_InvariantCulture_FirstLessThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.InvariantCulture);
            var actual = target.Compare(SmallLetterI, SmallDotlessLetterI);

            //Assert 
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void Compare_InvariantCulture_FirstGreaterThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.InvariantCulture);
            var actual = target.Compare(SmallDotlessLetterI, CapitalLetterI);

            //Assert 
            actual.Should().Be(1);
        }
        #endregion

        #region InvariantCultureIgnoreCase

        [TestMethod]
        public void Compare_InvariantCultureIgnoreCase_SameCase ()
        {
            //Act
            var target = CharComparer.GetComparer(StringComparison.InvariantCultureIgnoreCase);
            var actual = target.Compare(SmallDotlessLetterI, SmallDotlessLetterI);

            //Assert 
            actual.Should().Be(0);
        }

        [TestMethod]
        public void Compare_InvariantCultureIgnoreCase_MixedCase ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.InvariantCultureIgnoreCase);
            var actual = target.Compare(SmallLetterI, CapitalLetterI);

            //Assert 
            actual.Should().Be(0);
        }

        [TestMethod]
        public void Compare_InvariantCultureIgnoreCase_FirstLessThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.InvariantCultureIgnoreCase);
            var actual = target.Compare(SmallLetterI, SmallDotlessLetterI);

            //Assert 
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void Compare_InvariantCultureIgnoreCase_FirstGreaterThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.InvariantCultureIgnoreCase);
            var actual = target.Compare(SmallDotlessLetterI, CapitalLetterI);

            //Assert 
            actual.Should().Be(1);
        }
        #endregion

        #region Ordinal

        [TestMethod]
        public void Compare_Ordinal_SameCase ()
        {
            //Act
            var target = CharComparer.GetComparer(StringComparison.Ordinal);
            var actual = target.Compare(SmallLetterI, SmallLetterI);

            //Assert 
            actual.Should().Be(0);
        }

        [TestMethod]
        public void Compare_Ordinal_MixedCase ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.Ordinal);
            var actual = target.Compare(CapitalLetterI, SmallLetterI);

            //Assert 
            actual.Should().NotBe(0);
        }

        [TestMethod]
        public void Compare_Ordinal_FirstLessThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.Ordinal);
            var actual = target.Compare(SmallLetterI, SmallDotlessLetterI);

            //Assert 
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void Compare_Ordinal_FirstGreaterThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.Ordinal);
            var actual = target.Compare(SmallDotlessLetterI, CapitalLetterI);

            //Assert 
            actual.Should().Be(1);
        }
        #endregion

        #region OrdinalIgnoreCase

        [TestMethod]
        public void Compare_OrdinalIgnoreCase_SameCase ()
        {
            //Act
            var target = CharComparer.GetComparer(StringComparison.OrdinalIgnoreCase);
            var actual = target.Compare(CapitalLetterI, CapitalLetterI);

            //Assert 
            actual.Should().Be(0);
        }

        [TestMethod]
        public void Compare_OrdinalIgnoreCase_MixedCase ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.OrdinalIgnoreCase);
            var actual = target.Compare(CapitalLetterI, SmallLetterI);

            //Assert 
            actual.Should().Be(0);
        }

        [TestMethod]
        public void Compare_OrdinalIgnoreCase_FirstLessThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.OrdinalIgnoreCase);
            var actual = target.Compare(SmallLetterI, SmallDotlessLetterI);

            //Assert 
            actual.Should().Be(-1);
        }

        [TestMethod]
        public void Compare_OrdinalIgnoreCase_FirstGreaterThanSecond ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.OrdinalIgnoreCase);
            var actual = target.Compare(SmallDotlessLetterI, CapitalLetterI);

            //Assert 
            actual.Should().Be(1);
        }
        #endregion

        #region GetComparer
        
        [TestMethod]
        public void GetComparer_CurrentCulture ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.CurrentCulture);
            var actual = target.Compare('A', 'a');

            //Assert 
            actual.Should().NotBe(0);
        }

        [TestMethod]
        public void GetComparer_CurrentCultureIgnoreCase ()
        {
            //Act - Cannot actually detect comparer type so use heuristics
            var target = CharComparer.GetComparer(StringComparison.CurrentCultureIgnoreCase);
            var actual = target.Compare('A', 'a');

            //Assert 
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetComparer_InvariantCulture ()
        {
            //Act
            var actual = CharComparer.GetComparer(StringComparison.InvariantCulture);

            //Assert 
            actual.Should().Be(CharComparer.InvariantCulture);
        }

        [TestMethod]
        public void GetComparer_InvariantCultureIgnoreCase ()
        {
            //Act
            var actual = CharComparer.GetComparer(StringComparison.InvariantCultureIgnoreCase);

            //Assert 
            actual.Should().Be(CharComparer.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void GetComparer_Ordinal ()
        {
            //Act
            var actual = CharComparer.GetComparer(StringComparison.Ordinal);

            //Assert 
            actual.Should().Be(CharComparer.Ordinal);
        }

        [TestMethod]
        public void GetComparer_OrdinalIgnoreCase ()
        {
            //Act
            var actual = CharComparer.GetComparer(StringComparison.OrdinalIgnoreCase);

            //Assert 
            actual.Should().Be(CharComparer.OrdinalIgnoreCase);
        }

        [TestMethod]
        public void GetComparer_BadComparison ()
        {
            Action action = () => CharComparer.GetComparer((StringComparison)1000);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region ToStringComparer

        [TestMethod]
        public void ToStringComparer_CurrentCulture ()
        {
            var target = CharComparer.CurrentCulture;

            var actual = target.ToStringComparer();

            actual.Should().Be(StringComparer.CurrentCulture);
        }

        [TestMethod]
        public void ToStringComparer_CurrentCultureIgnoreCase ()
        {
            var target = CharComparer.CurrentCultureIgnoreCase;

            var actual = target.ToStringComparer();

            actual.Should().Be(StringComparer.CurrentCultureIgnoreCase);
        }

        [TestMethod]
        public void ToStringComparer_InvariantCulture ()
        {
            var target = CharComparer.InvariantCulture;

            var actual = target.ToStringComparer();

            actual.Should().Be(StringComparer.InvariantCulture);
        }

        [TestMethod]
        public void ToStringComparer_InvariantCultureIgnoreCase ()
        {
            var target = CharComparer.InvariantCultureIgnoreCase;

            var actual = target.ToStringComparer();

            actual.Should().Be(StringComparer.InvariantCultureIgnoreCase);
        }

        [TestMethod]
        public void ToStringComparer_Ordinal ()
        {
            var target = CharComparer.Ordinal;

            var actual = target.ToStringComparer();

            actual.Should().Be(StringComparer.Ordinal);
        }

        [TestMethod]
        public void ToStringComparer_OrdinalIgnoreCase ()
        {
            var target = CharComparer.OrdinalIgnoreCase;

            var actual = target.ToStringComparer();

            actual.Should().Be(StringComparer.OrdinalIgnoreCase);
        }
        #endregion

        #region Private Members

        private struct CompareData
        {
        }

        //From String.Compare in MSDN
        private const char SmallDotlessLetterI = 'ı';   // 0x131
        private const char SmallLetterI = 'i';          // 0x069
        private const char CapitalLetterI = 'I';        // 0x049
        #endregion
    }
}
