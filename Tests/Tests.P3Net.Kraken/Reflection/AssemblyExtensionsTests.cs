/*
 * Copyright © 2011 Michael L Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Reflection;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Reflection
{
    [TestClass] 
    public class AssemblyExtensionsTests : UnitTest
    {
        [TestMethod]
        public void GetAssemblyDetails_IsValid ()
        {
            var target = typeof(Uri).Assembly;
            var expected = "system.dll";

            //Act            
            var actual = target.GetAssemblyDetails();

            //Assert
            actual.Should().NotBeNull();
            actual.FileName.Should().BeEquivalentTo(expected);
        }
    }
}
