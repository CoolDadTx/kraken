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
    public class DirectoryExtensionsTests : UnitTest
    {
        #region Tests
                
        #region GetDirectorySize

        [TestMethod]
        public void GetDirectorySize_UseRecursion ( )
        {
            long expected = 0;
            var targetPath = GetTempTestDirectory();

            expected += CreateTestFile(targetPath, "abc.gds", 100);
            expected += CreateTestFile(targetPath, "def.gds", 45);
            expected += CreateTestFile(targetPath, "ghi.gds", 200);

            var subpath = targetPath + @"\Child";
            expected += CreateTestFile(subpath, "jkl.gds", 300);
            expected += CreateTestFile(subpath, "mno.gds", 50);
                        
            //Act
            var actual = DirectoryExtensions.GetDirectorySize(targetPath, "*.*", SearchOption.AllDirectories).Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDirectorySize_NoRecursion ()
        {
            long expected = 0;
            var targetPath = GetTempTestDirectory();

            expected += CreateTestFile(targetPath, "abc.gds", 100);
            expected += CreateTestFile(targetPath, "def.gds", 45);
            expected += CreateTestFile(targetPath, "ghi.gds", 200);

            var subpath = targetPath + @"\Child";
            CreateTestFile(subpath, "jkl.gds", 300);
            CreateTestFile(subpath, "mno.gds", 50);

            //Act
            var actual = DirectoryExtensions.GetDirectorySize(targetPath).Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDirectorySize_WithSearchPattern ()
        {
            long expected = 0;
            var targetPath = GetTempTestDirectory();

            expected += CreateTestFile(targetPath, "abc.gds", 100);
            CreateTestFile(targetPath, "def.def", 45);
            expected += CreateTestFile(targetPath, "ghi.gds", 200);
            CreateTestFile(targetPath, "jkl.def", 1000);
            
            //Act
            var actual = DirectoryExtensions.GetDirectorySize(targetPath, "*.gds").Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDirectorySize_WithSearchOptions ()
        {
            long expected = 0;
            var targetPath = GetTempTestDirectory();

            expected += CreateTestFile(targetPath, "abc.gds", 100);
            expected += CreateTestFile(targetPath, "ghi.gds", 200);

            var subpath = targetPath + @"\Child";
            expected += CreateTestFile(targetPath, "jkl.gds", 300);
            expected += CreateTestFile(targetPath, "mno.gds", 50);

            //Act
            var actual = DirectoryExtensions.GetDirectorySize(targetPath, SearchOption.AllDirectories).Bytes;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetDirectorySize_WithEmptyPath ()
        {
            var actual = DirectoryExtensions.GetDirectorySize("", SearchOption.AllDirectories).Bytes;

            //Assert
            actual.Should().Be(0);
        }
        #endregion

        #region SafeEnumerateDirectories

        [TestMethod]
        public void SafeEnumerateDirectories_NoRecursion ()
        {
            var basePath = GetTempTestDirectory();
            var expected = CreateTestDirectories(basePath, "Temp1", "Temp2", @"Temp3");
            CreateTestDirectories(basePath, @"Temp3\Temp3_1");

            //Act
            var actual = DirectoryExtensions.SafeEnumerateDirectories(basePath);

            //Assert
            actual.ToList().Should().Contain(expected);
        }

        [TestMethod]
        public void SafeEnumerateDirectories_WithRecursion ()
        {
            var basePath = GetTempTestDirectory();
            var expected = CreateTestDirectories(basePath, "Temp1", "Temp2", @"Temp3\Temp3_1", @"Temp3\Temp3_2");

            //Act
            var actual = DirectoryExtensions.SafeEnumerateDirectories(basePath, "*.*", SearchOption.AllDirectories);

            //Assert
            actual.ToList().Should().Contain(expected);
        }

        [TestMethod]
        public void SafeEnumerateDirectories_PathIsNull ()
        {
            //Act
            var actual = DirectoryExtensions.SafeEnumerateDirectories(null);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void SafeEnumerateDirectories_PathIsEmpty ()
        {
            //Act
            var actual = DirectoryExtensions.SafeEnumerateDirectories("");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void SafeEnumerateDirectories_WithSearchPattern ()
        {
            var basePath = GetTempTestDirectory();
            var expected = CreateTestDirectories(basePath, "Tmp2", "Tmp3");
            CreateTestDirectories(basePath, "Temp1", @"Tmp3\Temp3_1", @"Tmp3\Temp3_2", @"Tmp3\Tmp3_2");
            
            //Act
            var actual = DirectoryExtensions.SafeEnumerateDirectories(basePath, "Tmp*");

            //Assert
            actual.ToList().Should().Contain(expected);
        }

        [TestMethod]
        public void SafeEnumerateDirectories_WithSearchOptions ()
        {
            var basePath = GetTempTestDirectory();
            var expected = CreateTestDirectories(basePath, "Temp1", "Tmp2", @"Tmp3\Temp3_1", @"Tmp3\Temp3_2");

            //Act
            var actual = DirectoryExtensions.SafeEnumerateDirectories(basePath, SearchOption.AllDirectories).ToList();

            //Assert
            actual.Should().Contain(expected);
        }
        #endregion

        #region SafeEnumerateFiles

        [TestMethod]
        public void SafeEnumerateFiles_NoRecursion ()
        {
            var basePath = GetTempTestDirectory();
            var expected = CreateTestFiles(basePath, new TestFile("abc.sef"), new TestFile("def.sef"));
            CreateTestFiles(basePath, new TestFile(@"Temp1\ghi.sef"), new TestFile(@"Temp1\jkl.sef"));
            
            //Act
            var actual = DirectoryExtensions.SafeEnumerateFiles(basePath);

            //Assert
            actual.ToList().Should().Contain(expected);
        }

        [TestMethod]
        public void SafeEnumerateFiles_WithRecursion ()
        {
            var basePath = GetTempTestDirectory();
            var expected = CreateTestFiles(basePath, new TestFile("abc.sef"), new TestFile("def.sef"),
                                        new TestFile(@"Temp1\ghi.sef"), new TestFile(@"Temp2\jkl.sef"));

            //Act
            var actual = DirectoryExtensions.SafeEnumerateFiles(basePath, "*.*", SearchOption.AllDirectories);

            //Assert
            actual.ToList().Should().Contain(expected);
        }

        [TestMethod]
        public void SafeEnumerateFiles_PathIsNull ()
        {
            //Act
            var actual = DirectoryExtensions.SafeEnumerateFiles(null);

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void SafeEnumerateFiles_PathIsEmpty ()
        {
            //Act
            var actual = DirectoryExtensions.SafeEnumerateFiles("");

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void SafeEnumerateFiles_WithSearchPattern ()
        {
            var basePath = GetTempTestDirectory();
            var expected = CreateTestFiles(basePath, new TestFile("def.def"));
            CreateTestFiles(basePath, 
                                new TestFile("abc.sef"),
                                new TestFile(@"Temp1\ghi.def"),
                                new TestFile(@"Temp1\jkl.sef"));
            
            //Act
            var actual = DirectoryExtensions.SafeEnumerateFiles(basePath, "*.def");

            //Assert
            actual.ToList().Should().Contain(expected);
        }
        
        [TestMethod]
        public void SafeEnumerateFiles_WithSearchOptions ()
        {
            var basePath = GetTempTestDirectory();
            var expected = CreateTestFiles(basePath, new TestFile("abc.sef"), new TestFile("def.def"),
                                            new TestFile(@"Temp1\ghi.def"), new TestFile(@"Temp2\jkl.sef"));
            
            //Act
            var actual = DirectoryExtensions.SafeEnumerateFiles(basePath, SearchOption.AllDirectories);

            //Assert
            actual.ToList().Should().Contain(expected);
        }

        [TestMethod]
        public void SafeEnumerateFiles_WithSearchPatternAndOptions ()
        {
            var basePath = GetTempTestDirectory();
            var expected = CreateTestFiles(basePath, new TestFile("def.def"), new TestFile(@"Temp1\ghi.def"));
            CreateTestFiles(basePath, new TestFile("abc.sef"), new TestFile(@"Temp2\jkl.sef"));

            //Act
            var actual = DirectoryExtensions.SafeEnumerateFiles(basePath, "*.def", SearchOption.AllDirectories);

            //Assert
            actual.ToList().Should().Contain(expected);
        }
        #endregion

        #endregion

        #region Protected Members

        protected override void OnUninitializeTest()
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
#if NET_FRAMEWORK
            var basePath = TestContext.DeploymentDirectory;              
#else
            var basePath = Directory.GetCurrentDirectory();
#endif

            return Path.Combine(basePath, GetType().Name);
        }

        private string GetTempTestDirectory () => Path.Combine(GetBaseTempDirectory(), TestContext.TestName);

        private IEnumerable<string> CreateTestDirectories ( string basePath, params string[] paths )
        {
            var dirs = new List<string>();

            foreach (var path in paths)
            {
                var fullPath = basePath + @"\" + path;                
                Directory.CreateDirectory(fullPath);
                dirs.Add(fullPath);
            };

            return dirs;
        }

        private static long CreateTestFile ( string path, string filename, long size )
        {
            var fullPath = Path.Combine(path, filename);
            if (!Directory.Exists(Path.GetDirectoryName(fullPath)))
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            using (var stream = File.OpenWrite(fullPath))
            {
                var data = new byte[size];
                stream.WriteBytes(data);
            };

            var info = new FileInfo(fullPath);
            return info.Length;
        }

        private struct TestFile
        {
            public TestFile ( string filename ) : this(filename, 1)                
            { }
            public TestFile ( string filename, long size ) : this()
            {
                FileName = filename ?? "";
                Length = size;
            }

            public string FileName { get; private set; }
            public long Length { get; private set; }
        }

        private static IEnumerable<string> CreateTestFiles ( string path, params TestFile[] files )
        {
            var names = new List<string>();

            foreach (var file in files)
            {
                CreateTestFile(path, file.FileName, file.Length);
                names.Add(path + @"\" + file.FileName);
            };

            return names;
        }
#endregion
    }
}
