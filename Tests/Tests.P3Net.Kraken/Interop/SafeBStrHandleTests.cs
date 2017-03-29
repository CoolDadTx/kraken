/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using P3Net.Kraken.Interop;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.Interop
{
    [TestClass]
    public class SafeBStrHandleTests : UnitTest
    {
        #region Tests

        #region Ctor

        [TestMethod]
        public void Ctor_EmptyWorks ()
        {
            //Act
            using (var target = new SafeBStrHandle())
            {
                //Assert
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
            };
        }

        [TestMethod]
        public void Ctor_NullPointerWorks ()
        {
            //Act
            using (var target = new SafeBStrHandle(IntPtr.Zero))
            {
                //Assert
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
            };
        }

        [TestMethod]
        public void Ctor_ValidPointerWorks ()
        {
            var expected = AllocateMemory(10);

            //Act
            using (var target = new SafeBStrHandle(expected))
            {
                //Assert
                target.Pointer.Should().Be(expected);
                target.IsInvalid.Should().BeFalse();
            };
        }

        [TestMethod]
        public void Ctor_ZeroFreeMemory ()
        {
            var ptr = Marshal.StringToBSTR("Hello");
            var expected = new byte[10];
            var target = new SafeBStrHandle(ptr, true);

            //Act
            target.Close();

            var actual = new byte[10];
            Marshal.Copy(ptr, actual, 0, actual.Length);

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #region Attach

        [TestMethod]
        public void Attach_ValidPointerWorks ()
        {
            var expected = AllocateMemory(10);

            //Act
            using (var target = new SafeBStrHandle())
            {
                target.Attach(expected);

                //Assert
                target.Pointer.Should().Be(expected);
                target.IsInvalid.Should().BeFalse();
            };
        }

        [TestMethod]
        public void Attach_NullPointerWorks ()
        {
            var original = AllocateMemory(10);

            //Act
            using (var target = new SafeBStrHandle(original))
            {
                target.Attach(IntPtr.Zero);

                //Assert
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
            };
        }

        [TestMethod]
        public void Attach_ReplaceValidPointerWorks ()
        {
            var original = AllocateMemory(10);
            var expected = AllocateMemory(20);

            //Act
            using (var target = new SafeBStrHandle(original))
            {
                target.Attach(expected);

                //Assert
                target.Pointer.Should().Be(expected);
                target.IsInvalid.Should().BeFalse();
            };
        }

        [TestMethod]
        public void Attach_ZeroFreeMemory ()
        {
            var ptr = Marshal.StringToBSTR("Hello");
            var expected = new byte[10];
            var target = new SafeBStrHandle();

            //Act
            target.Attach(ptr, true);
            target.Close();

            var actual = new byte[10];
            Marshal.Copy(ptr, actual, 0, actual.Length);

            //Assert
            actual.Should().ContainInOrder(expected);
        }
        #endregion

        #region Detach

        [TestMethod]
        public void Detach_ValidPointerWorks ()
        {
            var expected = AllocateMemory(10);

            //Act
            using (var target = new SafeBStrHandle(expected))
            {
                var actual = target.Detach();

                //Assert
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
                actual.Should().Be(expected);
            };
        }

        [TestMethod]
        public void Detach_NullPointerWorks ()
        {
            //Act
            using (var target = new SafeBStrHandle())
            {
                var actual = target.Detach();

                //Assert
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
                actual.Should().BeZero();
            };
        }
        #endregion

        #region Dispose

        [TestMethod]
        public void Dispose_ValidPointerWorks ()
        {
            var ptr = AllocateMemory(10);

            //Act
            var target = new SafeBStrHandle(ptr);
            target.Dispose();

            //Assert - No real way to confirm the memory was released
            target.Pointer.Should().BeZero();
            target.IsInvalid.Should().BeTrue();
        }

        [TestMethod]
        public void Dispose_NullPointerWorks ()
        {
            //Act
            var target = new SafeBStrHandle();
            target.Dispose();

            //Assert - No real way to confirm the memory was released
            target.Pointer.Should().BeZero();
            target.IsInvalid.Should().BeTrue();
        }

        [TestMethod]
        public void Dispose_AttachedPointerWorks ()
        {
            var ptr = AllocateMemory(10);

            //Act
            var target = new SafeBStrHandle();
            target.Attach(ptr);
            target.Dispose();

            //Assert - doesn't really confirm the memory was released
            target.Pointer.Should().BeZero();
            target.IsInvalid.Should().BeTrue();
        }

        [TestMethod]
        public void Dispose_DetachedPointerWorks ()
        {
            var ptr = AllocateMemory(10);

            try
            {
                //Act
                var target = new SafeBStrHandle(ptr);
                var actual = target.Detach();
                target.Dispose();

                //Assert - doesn't really confirm the memory was released
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
            } finally
            {
                FreeMemory(ptr);
            };
        }
        #endregion

        #region SecureStringToBStr

        [TestMethod]
        public void SecureStringToBStr_ValidStringWorks ()
        {
            var str = CreateSecureString("Hello");

            //Act
            using (var target = SafeBStrHandle.SecureStringToBStr(str))
            {
                //Assert
                target.Pointer.Should().NotBeZero();
                target.IsInvalid.Should().BeFalse();

                AssertMemory(target.Pointer, Encoding.Unicode.GetBytes("Hello"));
            };
        }

        [TestMethod]
        public void SecureStringToBStr_NullStringWorks ()
        {
            //Act
            using (var target = SafeBStrHandle.SecureStringToBStr(null))
            {
                //Assert
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
            };
        }
        #endregion

        #region StringToBStr

        [TestMethod]
        public void StringToBStr_ValidStringWorks ()
        {
            var str = "Hello";

            //Act
            using (var target = SafeBStrHandle.StringToBStr(str))
            {
                //Assert
                target.Pointer.Should().NotBeZero();
                target.IsInvalid.Should().BeFalse();

                AssertMemory(target.Pointer, Encoding.Unicode.GetBytes("Hello"));
            };
        }

        [TestMethod]
        public void StringToBStr_NullStringWorks ()
        {
            //Act
            using (var target = SafeBStrHandle.StringToBStr(null))
            {
                //Assert
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
            };
        }
        #endregion

        #region ToString

        [TestMethod]
        public void ToString_ReturnsString ()
        {
            var expected = "Hello";
            var target = SafeBStrHandle.StringToBStr(expected);

            //Act
            var actual = target.ToString();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void ToString_ReturnedStringIsEmptyWhenZeroFree ()
        {
            var ptr = Marshal.StringToBSTR("Hello");
            var target = new SafeBStrHandle(ptr, true);            

            //Act
            var actual = target.ToString();

            //Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void ToString_ReturnedStringIsEmptyWhenPointerIsZero ()
        {
            var target = new SafeBStrHandle();

            //Act
            var actual = target.ToString();

            //Assert
            actual.Should().BeEmpty();
        }
        #endregion

        #endregion

        #region Private Members

        private IntPtr AllocateMemory ( int size )
        {
            string str = new string(' ', size);
            return Marshal.StringToBSTR(str);
        }

        private void AssertMemory ( IntPtr ptr, byte[] expected )
        {
            byte[] actual = ReadMemory(ptr, expected.Length);

            actual.Should().BeEquivalentTo(expected);
        }

        private SecureString CreateSecureString ( string value )
        {
            SecureString str = new SecureString();
            foreach (Char ch in value)
                str.AppendChar(ch);

            return str;
        }

        private void FreeMemory ( IntPtr ptr )
        {
            Marshal.FreeBSTR(ptr);
        }
        
        private byte[] ReadMemory ( IntPtr ptr, int size )
        {
            var values = new byte[size];

            Marshal.Copy(ptr, values, 0, size);
            return values;
        }
        
        private byte[] WriteMemory ( IntPtr ptr, int size )
        {
            var values = new byte[size];
            new Random().NextBytes(values);

            Marshal.Copy(values, 0, ptr, size);
            return values;
        }
        #endregion
    }
}
