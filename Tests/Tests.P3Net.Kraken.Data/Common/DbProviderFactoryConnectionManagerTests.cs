#region Imports

using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

using P3Net.Kraken.Data;
using P3Net.Kraken.Data.Common;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Data.Common
{
    [TestClass]
    public class DbProviderFactoryConnectionManagerTests : UnitTest
    {
        [TestMethod]
        public void Connect ()
        {
            Assert.Inconclusive("Not implemented");
        }
    }
}
