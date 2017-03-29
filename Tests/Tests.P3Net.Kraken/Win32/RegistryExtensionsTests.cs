using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Win32;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.Win32;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken.Win32
{
    [TestClass]
    public class RegistryExtensionsTests : UnitTest
    {
        #region Tests

        #region ContainsSubkey

        [TestMethod]
        public void ContainsSubkey_Exists ()
        {
            bool actual = false;
            using (var target = CreateTestKey())
            {
                target.CreateSubKey("SomeKey");

                //Act
                actual = target.ContainsSubkey("SomeKey");
            };

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ContainsSubkey_DoesNotExists ()
        {
            bool actual = true;
            using (var target = CreateTestKey())
            {
                actual = target.ContainsSubkey("abc");
            };

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ContainsSubkey_NestedExists ()
        {
            bool actual = false;
            using (var target = CreateTestKey())
            {
                var keyName = @"SomeKey\SomeKey2";
                target.CreateSubKey(keyName);

                //Act
                actual = target.ContainsSubkey(keyName);
            };

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ContainsSubkey_NestedDoesNotExists ()
        {
            bool actual = true;

            using (var target = CreateTestKey())
            {
                actual = target.ContainsSubkey(@"abc\def");
            };

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ContainsSubkey_NameIsEmpty ()
        {
            var actual = Registry.CurrentUser.ContainsSubkey("");

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ContainsSubkey_NameIsNull ()
        {
            var actual = Registry.CurrentUser.ContainsSubkey(null);

            actual.Should().BeFalse();
        }
        #endregion

        #region ContainsValue

        [TestMethod]
        public void ContainsValue_Exists ()
        {
            bool actual = false;
            using (var target = CreateTestKey())
            {
                target.SetValue("IntValue", 10);

                //Act
                actual = target.ContainsValue("IntValue");
            };

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void ContainsValue_DoesNotExists ()
        {
            bool actual = true;
            using (var target = CreateTestKey())
            {                
                //Act
                actual = target.ContainsValue("SomeValue");
            };

            //Assert
            actual.Should().BeFalse();
        }
        
        [TestMethod]        
        public void ContainsValue_NameIsEmpty ()
        {
            var actual = Registry.CurrentUser.ContainsValue("");

            actual.Should().BeFalse();
        }

        [TestMethod]
        public void ContainsValue_NameIsNull ()
        {
            var actual = Registry.CurrentUser.ContainsValue(null);

            actual.Should().BeFalse();
        }
        #endregion

        #region GetValueAsDouble

        [TestMethod]
        public void GetValueAsDouble_ValueIsCorrectType ()
        {
            var expected = 4.567;
            double actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", expected);

                //Act
                actual = target.GetValueAsDouble("SomeValue");
            };

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetValueAsDouble_ValueIsMissing ()
        {
            double actual;

            using (var target = CreateTestKey())
            {
                //Act
                actual = target.GetValueAsDouble("SomeValue");
            };

            //Assert
            actual.Should().BeApproximately(0);
        }

        [TestMethod]
        public void GetValueAsDouble_ValueIsNotCorrectType ()
        {
            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", "Hello");

                Action action = () => target.GetValueAsDouble("SomeValue");

                action.ShouldThrow<FormatException>();
            };
        }

        [TestMethod]
        public void GetValueAsDouble_ValueIsEmpty ()
        {
            double expected = 12.34;
            double actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                actual = target.GetValueAsDouble("");
            };

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetValueAsDouble_ValueIsNull ()
        {
            double expected = 12.34;
            double actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                actual = target.GetValueAsDouble(null);
            };

            //Assert
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void GetValueAsDouble_CustomDefaultValue ()
        {
            double expected = 12.34;
            double actual;

            using (var target = CreateTestKey())
            {

                //Act
                actual = target.GetValueAsDouble("SomeValue", expected);
            };

            //Assert
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #region GetValueAsInt32

        [TestMethod]
        public void GetValueAsInt32_ValueIsCorrectType ()
        {
            var expected = 4567;
            int actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", expected);

                //Act
                actual = target.GetValueAsInt32("SomeValue");
            };

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsInt32_ValueIsMissing ()
        {
            int actual;

            using (var target = CreateTestKey())
            {
                //Act
                actual = target.GetValueAsInt32("SomeValue");
            };

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetValueAsInt32_ValueIsNotCorrectType ()
        {
            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", "Hello");

                Action action = () => target.GetValueAsInt32("SomeValue");

                action.ShouldThrow<FormatException>();
            };
        }

        [TestMethod]
        public void GetValueAsInt32_ValueIsEmpty ()
        {
            var expected = 1234;
            int actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                actual = target.GetValueAsInt32("");
            };

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsInt32_ValueIsNull ()
        {
            var expected = 1234;
            int actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                actual = target.GetValueAsInt32(null);
            };

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsInt32_CustomDefaultValue ()
        {
            var expected = 1234;
            int actual;

            using (var target = CreateTestKey())
            {

                //Act
                actual = target.GetValueAsInt32("SomeValue", expected);
            };

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region GetValueAsInt64

        [TestMethod]
        public void GetValueAsInt64_ValueIsCorrectType ()
        {
            var expected = 123456789;
            long actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", expected);

                //Act
                actual = target.GetValueAsInt64("SomeValue");
            };

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsInt64_ValueIsMissing ()
        {
            long actual;

            using (var target = CreateTestKey())
            {
                //Act
                actual = target.GetValueAsInt64("SomeValue");
            };

            //Assert
            actual.Should().Be(0);
        }

        [TestMethod]
        public void GetValueAsInt64_ValueIsNotCorrectType ()
        {
            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", "Hello");

                Action action = () => target.GetValueAsInt64("SomeValue");

                action.ShouldThrow<FormatException>();
            };
        }

        [TestMethod]
        public void GetValueAsInt64_ValueIsEmpty ()
        {
            var expected = 12345678;
            long actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                actual = target.GetValueAsInt64("");
            };

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsInt64_ValueIsNull ()
        {
            var expected = 12345678;
            long actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                actual = target.GetValueAsInt64(null);
            };

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsInt64_CustomDefaultValue ()
        {
            var expected = 12345678;
            long actual;

            using (var target = CreateTestKey())
            {

                //Act
                actual = target.GetValueAsInt64("SomeValue", expected);
            };

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region GetValueAsString

        [TestMethod]
        public void GetValueAsString_ValueIsCorrectType ()
        {
            var expected = "Hello";
            string actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", expected);

                //Act
                actual = target.GetValueAsString("SomeValue");
            };

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsString_ValueIsMissing ()
        {
            string actual;

            using (var target = CreateTestKey())
            {
                //Act
                actual = target.GetValueAsString("SomeValue");
            };

            //Assert
            actual.Should().BeEmpty();
        }
        
        [TestMethod]
        public void GetValueAsString_ValueIsEmpty ()
        {
            var expected = "Goodbye";
            string actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                actual = target.GetValueAsString("");
            };

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsString_ValueIsNull ()
        {
            var expected = "SeeYa";
            string actual;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                actual = target.GetValueAsString(null);
            };

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetValueAsString_CustomDefaultValue ()
        {
            var expected = "World";
            string actual;

            using (var target = CreateTestKey())
            {
                //Act
                actual = target.GetValueAsString("SomeValue", expected);
            };

            //Assert
            actual.Should().Be(expected);
        }
        #endregion

        #region TryGetValueAsDouble

        [TestMethod]
        public void TryGetValueAsDouble_ValueIsCorrectType ()
        {
            var expected = 4.567;
            double actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", expected);

                //Act
                result = target.TryGetValueAsDouble("SomeValue", out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void TryGetValueAsDouble_ValueIsMissing ()
        {
            double actual;
            bool result;

            using (var target = CreateTestKey())
            {
                //Act
                result = target.TryGetValueAsDouble("SomeValue", out actual);
            };

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsDouble_ValueIsNotCorrectType ()
        {
            double actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", "Hello");

                //Act
                result = target.TryGetValueAsDouble("SomeValue", out actual);
            };

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsDouble_ValueIsEmpty ()
        {
            double expected = 12.34;
            double actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                result = target.TryGetValueAsDouble("", out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().BeApproximately(expected);
        }

        [TestMethod]
        public void TryGetValueAsDouble_ValueIsNull ()
        {
            double expected = 12.34;
            double actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                result = target.TryGetValueAsDouble(null, out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().BeApproximately(expected);
        }
        #endregion

        #region TryGetValueAsInt32

        [TestMethod]
        public void TryGetValueAsInt32_ValueIsCorrectType ()
        {
            var expected = 1234567;
            int actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", expected);

                //Act
                result = target.TryGetValueAsInt32("SomeValue", out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsInt32_ValueIsMissing ()
        {
            int actual;
            bool result;

            using (var target = CreateTestKey())
            {
                //Act
                result = target.TryGetValueAsInt32("SomeValue", out actual);
            };

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsInt32_ValueIsNotCorrectType ()
        {
            int actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", "Hello");

                //Act
                result = target.TryGetValueAsInt32("SomeValue", out actual);
            };

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsInt32_ValueIsEmpty ()
        {
            var expected = 1234567;
            int actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                result = target.TryGetValueAsInt32("", out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsInt32_ValueIsNull ()
        {
            var expected = 1234567;
            int actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                result = target.TryGetValueAsInt32(null, out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }
        #endregion

        #region TryGetValueAsInt64

        [TestMethod]
        public void TryGetValueAsInt64_ValueIsCorrectType ()
        {
            var expected = 12345678;
            long actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", expected);

                //Act
                result = target.TryGetValueAsInt64("SomeValue", out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsInt64_ValueIsMissing ()
        {
            long actual;
            bool result;

            using (var target = CreateTestKey())
            {
                //Act
                result = target.TryGetValueAsInt64("SomeValue", out actual);
            };

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsInt64_ValueIsNotCorrectType ()
        {
            long actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", "Hello");

                //Act
                result = target.TryGetValueAsInt64("SomeValue", out actual);
            };

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsInt64_ValueIsEmpty ()
        {
            var expected = 12345678;
            long actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                result = target.TryGetValueAsInt64("", out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsInt64_ValueIsNull ()
        {
            var expected = 12345678;
            long actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                result = target.TryGetValueAsInt64(null, out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }
        #endregion

        #region TryGetValueAsString

        [TestMethod]
        public void TryGetValueAsString_ValueIsCorrectType ()
        {
            var expected = "Hello";
            string actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("SomeValue", expected);

                //Act
                result = target.TryGetValueAsString("SomeValue", out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsString_ValueIsMissing ()
        {
            string actual;
            bool result;

            using (var target = CreateTestKey())
            {
                //Act
                result = target.TryGetValueAsString("SomeValue", out actual);
            };

            //Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void TryGetValueAsString_ValueIsEmpty ()
        {
            var expected = "Goodbye";
            string actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                result = target.TryGetValueAsString("", out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void TryGetValueAsString_ValueIsNull ()
        {
            var expected = "Cool";
            string actual;
            bool result;

            using (var target = CreateTestKey())
            {
                target.SetValue("", expected);

                //Act
                result = target.TryGetValueAsString(null, out actual);
            };

            //Assert
            result.Should().BeTrue();
            actual.Should().Be(expected);
        }
        #endregion

        #endregion

        #region Protected Members

        protected override void OnInitializeTest ()
        {
            base.OnInitializeTest();

            try
            {
                Registry.CurrentUser.DeleteSubKeyTree(GetRootTestKey(), false);
            } catch 
            {
                Assert.Inconclusive("Unable to clean up test registry key to run tests.");
            };
        }

        protected override void OnUninitializeTest ()
        {
            base.OnUninitializeTest();

            //Delete the test key
            try
            {
                Registry.CurrentUser.DeleteSubKeyTree(GetRootTestKey(), false);
            } catch
            { /* Ignore */ };
        }
        #endregion

        #region Private Members
              
        private string GetRootTestKey ()
        {
            return @"Software\UnitTesting\" + this.GetType().Name;
        }

        private RegistryKey CreateTestKey ( )
        {
            var subkey = GetRootTestKey() + @"\" + TestContext.TestName;
            
            return Registry.CurrentUser.CreateSubKey(subkey, RegistryKeyPermissionCheck.ReadWriteSubTree, RegistryOptions.None);
        }
        #endregion
    }
}
