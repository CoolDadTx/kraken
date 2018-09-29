/*
 * Copyright © 2011 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Reflection;
using P3Net.Kraken.UnitTesting;

#endregion

namespace Tests.P3Net.Kraken.Reflection
{
    [TestClass] 
    public class AssemblyDetailsTests : UnitTest
    {
        #region Tests
        
        [TestMethod]
        public void CompanyName_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = GetAssemblyAttribute<AssemblyCompanyAttribute>(target).Company;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.CompanyName;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Configuration_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = "";

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.Configuration;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Copyright_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = GetAssemblyAttribute<AssemblyCopyrightAttribute>(target).Copyright;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.Copyright;

            //Assert
            actual.Should().Be(expected);
        }
        
        [TestMethod]
        public void Description_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = GetAssemblyAttribute<AssemblyDescriptionAttribute>(target).Description;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.Description;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FileName_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = "system.dll";

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.FileName;

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void FileNameWithoutExtension_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = "system";

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.FileNameWithoutExtension;

            //Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void FileVersion_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = new Version(GetAssemblyAttribute<AssemblyFileVersionAttribute>(target).Version);

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.FileVersion;

            //Assert 
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FileVersionString_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = GetAssemblyAttribute<AssemblyFileVersionAttribute>(target).Version;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.FileVersionString;

            //Assert 
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void FullPath_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = target.Location;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.FullPath;

            //Assert 
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void InformationalVersion_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = GetAssemblyAttribute<AssemblyInformationalVersionAttribute>(target).InformationalVersion;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.InformationalVersion;

            //Assert 
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void IsClsCompliant_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = GetAssemblyAttribute<CLSCompliantAttribute>(target).IsCompliant;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.IsClsCompliant;

            //Assert 
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void IsComVisible_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = GetAssemblyAttribute<ComVisibleAttribute>(target).Value;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.IsComVisible;

            //Assert 
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ProductName_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = GetAssemblyAttribute<AssemblyProductAttribute>(target).Product;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.ProductName;

            //Assert 
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Title_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = GetAssemblyAttribute<AssemblyTitleAttribute>(target).Title;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.Title;

            //Assert 
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Trademark_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = "";

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.Trademark;

            //Assert 
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Version_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var name = target.GetName();
            var expected = name.Version;

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.Version;

            //Assert 
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void VersionString_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var name = target.GetName();
            var expected = name.Version.ToString();

            //Act            
            var details = target.GetAssemblyDetails();
            var actual = details.VersionString;

            //Assert 
            actual.Should().Be(expected);
        }
        #endregion

        #region Private Members

        private T GetAssemblyAttribute<T> ( Assembly assembly ) where T : Attribute
        {
            return (from a in assembly.GetCustomAttributes(typeof(T), false)
                    select a).Cast<T>().FirstOrDefault();
        }
        #endregion
    }
}
