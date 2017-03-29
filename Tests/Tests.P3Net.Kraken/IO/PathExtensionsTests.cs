using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.IO;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.IO
{
    [TestClass]
    public class PathExtensionsTests : UnitTest
    {        
        #region BuildPath

        [TestMethod]
        public void BuildPath_WithSimplePath ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows", "System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BuildPath_WithPrefixSlashPath ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows", @"\System32");

            //Assert
            actual.Should().Be(expected);
        }
        
        [TestMethod]
        public void BuildPath_WithPostfixSlashPath ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows\", "System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BuildPath_WithSeveralPaths ()
        {
            var expected = @"C:\Windows\System32\Drivers";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows", "System32", "Drivers");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BuildPath_WithManyPaths ()
        {
            var expected = @"C:\Windows\System32\Drivers\etc";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows", "System32", "Drivers", "etc");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BuildPath_WithNullPath1 ()
        {
            var expected = @"System32\Drivers";

            //Act
            var actual = PathExtensions.BuildPath(null, @"System32", "Drivers");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BuildPath_WithNullPath2 ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows", null, "System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BuildPath_WithNullPath3 ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows", "System32", null);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BuildPath_WithNullPathInMiddle ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows", null, "System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BuildPath_WithAllNull ()
        {
            //Act
            var actual = PathExtensions.BuildPath(null, null, null, null);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void BuildPath_WithPrefixAndPostfixSlashPath ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows\", @"\System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void BuildPath_EndingSlashIsStripped ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.BuildPath(@"C:\Windows\", @"\System32\");

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region CompactPath

        [TestMethod]
        public void CompactPath_PathFits ()
        {
            var expected = @"C:\Windows\SomeFile.txt";

            //Act
            var actual = PathExtensions.CompactPath(@"C:\Windows\SomeFile.txt", 100);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void CompactPath_PathIsTooSmall ()
        {
            var expected = @"C:\W...\SomeFile.txt";

            //Act
            var actual = PathExtensions.CompactPath(@"C:\Windows\SomeFile.txt", 20);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void CompactPath_PathIsTooSmallWithFile ()
        {
            var expected = @"...\Som...";

            //Act
            var actual = PathExtensions.CompactPath(@"C:\Windows\SomeFile.txt", 10);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void CompactPath_PathIsEmpty ()
        {
            //Act
            var actual = PathExtensions.CompactPath("", 100);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void CompactPath_PathIsNull ()
        {
            //Act
            var actual = PathExtensions.CompactPath(null, 100);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void CompactPath_LengthIsInvalid ()
        {
            //Act
            PathExtensions.CompactPath(@"C:\Windows", 0);
        }
        #endregion
        
        #region GetCommonPath

        [TestMethod]
        public void GetCommonPath_SameParent ()
        {
            var expected = @"C:\Windows";

            //Act
            var actual = PathExtensions.GetCommonPath(@"C:\Windows\Temp", @"C:\Windows\System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetCommonPath_OneIsParentOfOther ()
        {
            var expected = @"C:\Windows";

            //Act
            var actual = PathExtensions.GetCommonPath(@"C:\Windows\", @"C:\Windows\System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetCommonPath_DifferentParents ()
        {
            //Act
            var actual = PathExtensions.GetCommonPath(@"C:\Temp", @"D:\Temp");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetCommonPath_Path1IsEmpty ()
        {
            //Act
            var actual = PathExtensions.GetCommonPath("", @"C:\Windows\System32");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetCommonPath_Path1IsNull ()
        {
            //Act
            var actual = PathExtensions.GetCommonPath(null, @"C:\Windows\System32");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetCommonPath_Path2IsEmpty ()
        {
            //Act
            var actual = PathExtensions.GetCommonPath(@"C:\Windows", "");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetCommonPath_Path2IsNull ()
        {
            //Act
            var actual = PathExtensions.GetCommonPath(@"C:\Windows", null);

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion
               
        #region GetFullPath

        [TestMethod]
        public void GetFullPath_WithRelativePath ( )
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.GetFullPath(@"C:\Windows", @".\System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetFullPath_WithSimplePath ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.GetFullPath(@"C:\Windows", @"System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetFullPath_WithParentRelativePath ()
        {
            var expected = @"C:\Windows\System32";

            //Act
            var actual = PathExtensions.GetFullPath(@"C:\Windows", @".\System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetFullPath_WithComplexRelativePath ()
        {
            var expected = @"C:\Temp";

            //Act
            var actual = PathExtensions.GetFullPath(@"C:\Windows", @"..\..\Temp");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetFullPath_WithEmptyPath ()
        {
            var expected = @"C:\Windows";

            //Act
            var actual = PathExtensions.GetFullPath(@"C:\Windows", "");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetFullPath_WithNullPath ()
        {
            var expected = @"C:\Windows";

            //Act
            var actual = PathExtensions.GetFullPath(@"C:\Windows", null);

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetFullPath_WithNullBase ()
        {
            //Act
            PathExtensions.GetFullPath(null, "System32");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFullPath_WithEmptyBase ()
        {
            //Act
            PathExtensions.GetFullPath("", "System32");
        }
        #endregion
       
        #region GetRelativePath

        [TestMethod]
        public void GetRelativePath_WithSimplePath ()
        {
            var expected = @".\System32";

            //Act
            var actual = PathExtensions.GetRelativePath(@"C:\Windows", @"C:\Windows\System32");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetRelativePath_WithParentRelativePath ()
        {
            var expected = @".\System32\Drivers";

            //Act
            var actual = PathExtensions.GetRelativePath(@"C:\Windows", @"C:\Windows\System32\Drivers");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetRelativePath_NestedPath ()
        {
            var expected = @"..\..\FolderD\FolderE";

            //Act
            var actual = PathExtensions.GetRelativePath(@"C:\FolderA\FolderB\FolderC", @"C:\FolderA\FolderD\FolderE");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetRelativePath_WithUnrelatedPath ()
        {
            var expected = @"D:\Temp";

            //Act
            var actual = PathExtensions.GetRelativePath(@"C:\Windows", @"D:\Temp");

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetRelativePath_WithEmptyPath ()
        {
            //Act
            var actual = PathExtensions.GetRelativePath(@"C:\Windows", "");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void GetRelativePath_WithNullPath ()
        {
            //Act
            var actual = PathExtensions.GetRelativePath(@"C:\Windows", null);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetRelativePath_WithNullBase ()
        {
            //Act
            PathExtensions.GetRelativePath(null, "System32");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRelativePath_WithEmptyBase ()
        {
            //Act
            PathExtensions.GetRelativePath("", "System32");
        }

        [TestMethod]
        public void GetRelativePath_WithFiles()
        {
            var expected = @".\System32\SomeFile.dll";

            //Act
            var actual = PathExtensions.GetRelativePath(@"C:\Windows\Test.txt", @"C:\Windows\System32\SomeFile.dll");

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region IsRelative

        [TestMethod]
        public void IsRelative_PathIsRelative ()
        {
            //Act
            var actual = PathExtensions.IsRelative(@"..\Test");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsRelative_PathIsSimple ()
        {
            //Act
            var actual = PathExtensions.IsRelative(@"Test");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsRelative_PathIsAbsolute ()
        {
            //Act
            var actual = PathExtensions.IsRelative(@"C:\Temp");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsRelative_PathIsEmpty ()
        {
            //Act
            var actual = PathExtensions.IsRelative("");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsRelative_PathIsNull ()
        {
            //Act
            var actual = PathExtensions.IsRelative(null);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion

        #region IsUnc

        [TestMethod]
        public void IsUnc_PathIsUnc ()
        {
            //Act
            var actual = PathExtensions.IsUnc(@"\\server\path");

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsUnc_PathIsNotUnc ()
        {
            //Act
            var actual = PathExtensions.IsUnc(@"C:\Temp");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsUnc_PathIsEmpty ()
        {
            //Act
            var actual = PathExtensions.IsUnc("");

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsUnc_PathIsNull ()
        {
            //Act
            var actual = PathExtensions.IsUnc(null);

            //Assert
            actual.Should().BeFalse();
        }
        #endregion
    }
}
