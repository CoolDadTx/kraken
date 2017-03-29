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
    public class DirectoryInfoExtensionsTests : UnitTest
    {
        #region Tests
                
        #region GetSize

        [TestMethod]
        public void GetSize_UseRecursion ()
        {
            long expected = 0;
            var targetPath = GetTempTestDirectory();

            expected += CreateTestFileWithSize(targetPath, "abc.gds", 100);
            expected += CreateTestFileWithSize(targetPath, "def.gds", 45);
            expected += CreateTestFileWithSize(targetPath, "ghi.gds", 200);

            var subpath = targetPath + @"\Child";
            expected += CreateTestFileWithSize(subpath, "jkl.gds", 300);
            expected += CreateTestFileWithSize(subpath, "mno.gds", 50);
                        
            //Act
            var actual = new DirectoryInfo(targetPath).GetSize("*.*", SearchOption.AllDirectories).Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetSize_NoRecursion ()
        {
            long expected = 0;
            var targetPath = GetTempTestDirectory();

            expected += CreateTestFileWithSize(targetPath, "abc.gds", 100);
            expected += CreateTestFileWithSize(targetPath, "def.gds", 45);
            expected += CreateTestFileWithSize(targetPath, "ghi.gds", 200);

            var subpath = targetPath + @"\Child";
            CreateTestFileWithSize(subpath, "jkl.gds", 300);
            CreateTestFileWithSize(subpath, "mno.gds", 50);

            //Act
            var actual = new DirectoryInfo(targetPath).GetSize().Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetSize_WithSearchPattern ()
        {
            long expected = 0;
            var targetPath = GetTempTestDirectory();

            expected += CreateTestFileWithSize(targetPath, "abc.gds", 100);
            CreateTestFileWithSize(targetPath, "def.def", 45);
            expected += CreateTestFileWithSize(targetPath, "ghi.gds", 200);
            CreateTestFileWithSize(targetPath, "jkl.def", 1000);
            
            //Act
            var actual = new DirectoryInfo(targetPath).GetSize("*.gds").Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetSize_WithSearchOptions ()
        {
            long expected = 0;
            var targetPath = GetTempTestDirectory();

            expected += CreateTestFileWithSize(targetPath, "abc.gds", 100);
            expected += CreateTestFileWithSize(targetPath, "ghi.gds", 200);

            var subpath = targetPath + @"\Child";
            expected += CreateTestFileWithSize(targetPath, "jkl.gds", 300);
            expected += CreateTestFileWithSize(targetPath, "mno.gds", 50);

            //Act
            var actual = new DirectoryInfo(targetPath).GetSize(SearchOption.AllDirectories).Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetSize_DoesNotExist ()
        {
            //Act
            var actual = new DirectoryInfo(GetTempTestDirectory() + @"\abc").GetSize().Bytes;

            //Assert
            actual.Should().Be(0);
        }
        #endregion

        #region SafeEnumerateDirectories

        [TestMethod]
        public void SafeEnumerateDirectories_NoRecursion ()
        {
            var basePath = GetTempTestDirectory();
            Directory.CreateDirectory(basePath + @"\Temp1");
            Directory.CreateDirectory(basePath + @"\Temp2");
            Directory.CreateDirectory(basePath + @"\Temp3\Temp3_1");

            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateDirectories();
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "Temp1", "Temp2", "Temp3" });
        }

        [TestMethod]
        public void SafeEnumerateDirectories_WithRecursion ()
        {
            var basePath = GetTempTestDirectory();
            Directory.CreateDirectory(basePath + @"\Temp1");
            Directory.CreateDirectory(basePath + @"\Temp2");
            Directory.CreateDirectory(basePath + @"\Temp3\Temp3_1");
            Directory.CreateDirectory(basePath + @"\Temp3\Temp3_2");

            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateDirectories("*.*", SearchOption.AllDirectories);
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "Temp1", "Temp2", "Temp3", "Temp3_1", "Temp3_2" });
        }

        [TestMethod]
        public void SafeEnumerateDirectories_WithSearchPattern ()
        {
            var basePath = GetTempTestDirectory();
            Directory.CreateDirectory(basePath + @"\Temp1");
            Directory.CreateDirectory(basePath + @"\Tmp2");
            Directory.CreateDirectory(basePath + @"\Tmp3\Temp3_1");
            Directory.CreateDirectory(basePath + @"\Tmp3\Temp3_2");
            Directory.CreateDirectory(basePath + @"\Tmp3\Tmp3_2");

            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateDirectories("Tmp*");
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "Tmp2", "Tmp3"} );
        }

        [TestMethod]
        public void SafeEnumerateDirectories_WithSearchOptions ()
        {
            var basePath = GetTempTestDirectory();
            Directory.CreateDirectory(basePath + @"\Temp1");
            Directory.CreateDirectory(basePath + @"\Tmp2");
            Directory.CreateDirectory(basePath + @"\Tmp3\Temp3_1");
            Directory.CreateDirectory(basePath + @"\Tmp3\Temp3_2");

            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateDirectories(SearchOption.AllDirectories);
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "Temp1", "Tmp2", "Tmp3", "Temp3_1", "Temp3_2" });
        }

        [TestMethod]
        public void SafeEnumerateDirectories_WithSearchPatternAndRecursive ()
        {
            var basePath = GetTempTestDirectory();
            Directory.CreateDirectory(basePath + @"\Temp1");
            Directory.CreateDirectory(basePath + @"\Tmp2");
            Directory.CreateDirectory(basePath + @"\Tmp3\Temp3_1");
            Directory.CreateDirectory(basePath + @"\Tmp3\Temp3_2");
            Directory.CreateDirectory(basePath + @"\Tmp3\Tmp3_2");

            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateDirectories("Tmp*", SearchOption.AllDirectories);
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "Tmp2", "Tmp3", "Tmp3_2" });
        }

        [TestMethod]
        public void SafeEnumerateDirectories_DoesNotExist ()
        {
            //Act
            var actual = new DirectoryInfo(GetTempTestDirectory() + @"\abc").SafeEnumerateDirectories();

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #region SafeEnumerateFiles

        [TestMethod]
        public void SafeEnumerateFiles_NoRecursion ()
        {
            var basePath = GetTempTestDirectory();
            CreateTestFileWithSize(basePath, "abc.sef", 100);
            CreateTestFileWithSize(basePath, "def.sef", 1000);
            CreateTestFileWithSize(basePath + @"\Temp1", "ghi.sef", 100);
            CreateTestFileWithSize(basePath + @"\Temp2", "jkl.sef", 100);
            
            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateFiles();
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "abc.sef", "def.sef" });
        }

        [TestMethod]
        public void SafeEnumerateFiles_WithRecursion ()
        {
            var basePath = GetTempTestDirectory();
            CreateTestFileWithSize(basePath, "abc.sef", 100);
            CreateTestFileWithSize(basePath, "def.sef", 1000);
            CreateTestFileWithSize(basePath + @"\Temp1", "ghi.sef", 100);
            CreateTestFileWithSize(basePath + @"\Temp2", "jkl.sef", 100);

            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateFiles("*.*", SearchOption.AllDirectories);
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "abc.sef", "def.sef", "ghi.sef", "jkl.sef" });
        }

        [TestMethod]
        public void SafeEnumerateFiles_WithSearchPattern ()
        {
            var basePath = GetTempTestDirectory();
            CreateTestFileWithSize(basePath, "abc.sef", 100);
            CreateTestFileWithSize(basePath, "def.def", 1000);
            CreateTestFileWithSize(basePath, "jkl.def", 100);
            CreateTestFileWithSize(basePath + @"\Temp1", "ghi.def", 100);
            

            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateFiles("*.def");
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "def.def", "jkl.def" });
        }


        [TestMethod]
        public void SafeEnumerateFiles_WithSearchPatternAndOptions ()
        {
            var basePath = GetTempTestDirectory();
            CreateTestFileWithSize(basePath, "abc.sef", 100);
            CreateTestFileWithSize(basePath, "def.def", 1000);
            CreateTestFileWithSize(basePath + @"\Temp1", "ghi.def", 100);
            CreateTestFileWithSize(basePath + @"\Temp2", "jkl.sef", 100);

            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateFiles("*.def", SearchOption.AllDirectories);
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "def.def", "ghi.def" });
        }

        [TestMethod]
        public void SafeEnumerateFiles_WithSearchOptions ()
        {
            var basePath = GetTempTestDirectory();
            CreateTestFileWithSize(basePath, "abc.sef", 100);
            CreateTestFileWithSize(basePath, "def.def", 1000);
            CreateTestFileWithSize(basePath + @"\Temp1", "ghi.def", 100);
            CreateTestFileWithSize(basePath + @"\Temp2", "jkl.sef", 100);

            //Act
            var actual = new DirectoryInfo(basePath).SafeEnumerateFiles(SearchOption.AllDirectories);
            var actualStrings = (from a in actual select a.Name).ToList();

            //Assert
            actualStrings.Should().Contain(new string[] { "abc.sef", "def.def", "ghi.def", "jkl.sef" });
        }

        [TestMethod]
        public void SafeEnumerateFiles_DoesNotExist ()
        {
            //Act
            var actual = new DirectoryInfo(GetTempTestDirectory() + @"\abc").SafeEnumerateFiles();

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #endregion

        #region Protected Members

        protected override void OnUninitializeTest ()
        {
            base.OnUninitializeTest();

            //Clean up  - if it worked
            try
            {
                Directory.Delete(GetBaseTempDirectory(), true);
            } catch
            { };
        }
        #endregion

        #region Private Members

        private string GetBaseTempDirectory ()
        {
            //Would prefer to use TestRunResultsDirectory but that isn't initialized for some reason
            return TestContext.TestDeploymentDir + @"\" + this.GetType().Name;
        }

        private string GetTempTestDirectory ( )
        {
            return GetBaseTempDirectory() + @"\" + TestContext.TestName;
        }

        private static long CreateTestFileWithSize ( string path, string filename, long size )
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var fullPath = path + "/" + filename;
            using (var stream = File.OpenWrite(fullPath))
            {
                var data = new byte[size];
                stream.WriteBytes(data);
            };

            var info = new FileInfo(fullPath);
            return info.Length;
        }        
        #endregion
    }
}
