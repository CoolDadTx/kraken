/*
 * Copyright © 2007 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class SafeComMemoryHandleTests : UnitTest
    {
        #region Tests

        #region Ctor

        [TestMethod]
        public void Ctor_EmptyPointerIsZero ()
        {
            //Act
            using (var target = new SafeComMemoryHandle())
            {
                //Assert
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
            };
        }

        [TestMethod]
        public void Ctor_NullPointerIsZero ()
        {
            //Act
            using (var target = new SafeComMemoryHandle(IntPtr.Zero))
            {
                //Assert
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
            };
        }

        [TestMethod]
        public void Ctor_ValidPointerIsSet ()
        {
            var expected = AllocateMemory(200);

            //Act
            using (var target = new SafeComMemoryHandle(expected))
            {
                //Assert
                target.Pointer.Should().Be(expected);
                target.IsInvalid.Should().BeFalse();
            };
        }
        #endregion

        #region Allocate

        [TestMethod]
        public void Allocate_ValidSizeWorks ()
        {
            //Act
            using (var target = SafeComMemoryHandle.Allocate(256))
            {
                var actual = target.Pointer;

                var written = WriteMemory(target.Pointer, 256);

                //Assert
                target.IsInvalid.Should().BeFalse();
                AssertMemory(target.Pointer, written);
            };
        }

        [TestMethod]
        public void Allocate_ZeroSizeFails ()
        {
            Action action = () => SafeComMemoryHandle.Allocate(0);

            action.ShouldThrowArgumentOutOfRangeException();
        }

        [TestMethod]        
        public void Allocate_InvalidSizeFails ()
        {            
            Action action = () => SafeComMemoryHandle.Allocate(-5);

            action.ShouldThrowArgumentOutOfRangeException();
        }
        #endregion

        #region Attach

        [TestMethod]
        public void Attach_ValidPointerWorks ()
        {
            var expected = AllocateMemory(100);

            //Act
            using (var target = new SafeComMemoryHandle())
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
            //Act
            using (var target = new SafeComMemoryHandle())
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
            var original = AllocateMemory(40);
            var expected = AllocateMemory(100);

            //Act
            using (var target = new SafeComMemoryHandle(original))
            {
                target.Attach(expected);

                //Assert
                target.Pointer.Should().Be(expected);
                target.IsInvalid.Should().BeFalse();
            };
        }
        #endregion

        #region Detach

        [TestMethod]
        public void Detach_ValidPointerWorks ()
        {
            var ptr = AllocateMemory(50);
            try
            {
                //Act
                using (var target = new SafeComMemoryHandle(ptr))
                {
                    var actual = target.Detach();

                    //Assert
                    actual.Should().Be(ptr);
                    target.Pointer.Should().BeZero();
                    target.IsInvalid.Should().BeTrue();
                };
            } finally
            {
                FreeMemory(ptr);
            };
        }

        [TestMethod]
        public void Detach_NullPointerWorks ()
        {
            //Act
            using (var target = new SafeComMemoryHandle())
            {
                var actual = target.Detach();

                //Assert
                actual.Should().BeZero();
                target.Pointer.Should().BeZero();
                target.IsInvalid.Should().BeTrue();
            };
        }
        #endregion

        #region Dispose

        [TestMethod]
        public void Dispose_ValidPointerWorks ()
        {
            var ptr = AllocateMemory(10);
           
            //Act
            var target = new SafeComMemoryHandle(ptr);
            target.Dispose();

            //Assert - No real way to confirm the memory was released
            target.Pointer.Should().BeZero();
            target.IsInvalid.Should().BeTrue();            
        }

        [TestMethod]
        public void Dispose_NullPointerWorks ()
        {
            //Act
            var target = new SafeComMemoryHandle();
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
            var target = new SafeComMemoryHandle();
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
                var target = new SafeComMemoryHandle(ptr);
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

        #region SecureStringToAnsi

        [TestMethod]
        public void SecureStringToAnsi_ValidStringWorks ()
        {
            var str = CreateSecureString("Hello");
            
            //Act 
            using (var target = SafeComMemoryHandle.SecureStringToAnsi(str))
            {
                //Assert
                target.IsInvalid.Should().BeFalse();
                AssertMemory(target.Pointer, Encoding.ASCII.GetBytes("Hello"));
            };
        }

        [TestMethod]
        public void SecureStringToAnsi_NullStringWorks ()
        {
            //Act
            using (var target = SafeComMemoryHandle.SecureStringToAnsi(null))
            {
                //Assert
                target.IsInvalid.Should().BeTrue();
                target.Pointer.Should().BeZero();
            };
        }

        [TestMethod]
        public void SecureStringToAnsi_ZeroesMemoryWhenClosed ()
        {
            var str = CreateSecureString("Hello");
            var target = SafeComMemoryHandle.SecureStringToAnsi(str);

            //Act
            target.Close();

            //Assert - All we can do is verify it doesn't blow up
            target.IsClosed.Should().BeTrue();
        }
        #endregion

        #region SecureStringToUnicode

        [TestMethod]
        public void SecureStringToUnicode_ValidStringWorks ()
        {
            var str = CreateSecureString("Hello");

            //Act 
            using (var target = SafeComMemoryHandle.SecureStringToUnicode(str))
            {
                //Assert
                target.IsInvalid.Should().BeFalse();
                AssertMemory(target.Pointer, Encoding.Unicode.GetBytes("Hello"));
            };
        }

        [TestMethod]
        public void SecureStringToUnicode_NullStringWorks ()
        {
            //Act
            using (var target = SafeComMemoryHandle.SecureStringToUnicode(null))
            {
                //Assert
                target.IsInvalid.Should().BeTrue();
                target.Pointer.Should().BeZero();
            };
        }

        [TestMethod]
        public void SecureStringToUnicode_ZeroesMemoryWhenClosed ()
        {
            var str = CreateSecureString("Hello");
            var target = SafeComMemoryHandle.SecureStringToUnicode(str);

            //Act
            target.Close();

            //Assert - All we can do is verify it doesn't blow up
            target.IsClosed.Should().BeTrue();
        }
        #endregion

        #region StringToAnsi

        [TestMethod]
        public void StringToAnsi_ValidStringWorks ()
        {
            string str = "Hello";

            //Act 
            using (var target = SafeComMemoryHandle.StringToAnsi(str))
            {
                //Assert
                target.IsInvalid.Should().BeFalse();
                AssertMemory(target.Pointer, Encoding.ASCII.GetBytes("Hello"));
            };
        }

        [TestMethod]
        public void StringToAnsi_NullStringWorks ()
        {
            //Act
            using (var target = SafeComMemoryHandle.StringToAnsi(null))
            {
                //Assert
                target.IsInvalid.Should().BeTrue();
                target.Pointer.Should().BeZero();
            };
        }
        #endregion

        #region StringToAuto

        [TestMethod]
        public void StringToAuto_ValidStringWorks ()
        {
            string str = "Hello";

            //Act 
            using (var target = SafeComMemoryHandle.StringToAuto(str))
            {
                //Assert
                target.IsInvalid.Should().BeFalse();
                AssertMemory(target.Pointer, Encoding.Unicode.GetBytes("Hello"));
            };
        }

        [TestMethod]
        public void StringToAuto_NullStringWorks ()
        {
            //Act
            using (var target = SafeComMemoryHandle.StringToAuto(null))
            {
                //Assert
                target.IsInvalid.Should().BeTrue();
                target.Pointer.Should().BeZero();
            };
        }
        #endregion

        #region StringToUnicode

        [TestMethod]
        public void StringToUnicode_ValidStringWorks ()
        {
            string str = "Hello";

            //Act 
            using (var target = SafeComMemoryHandle.StringToUnicode(str))
            {
                //Assert
                target.IsInvalid.Should().BeFalse();
                AssertMemory(target.Pointer, Encoding.Unicode.GetBytes("Hello"));
            };
        }

        [TestMethod]
        public void StringToUnicode_NullStringWorks ()
        {
            //Act
            using (var target = SafeComMemoryHandle.StringToUnicode(null))
            {
                //Assert
                target.IsInvalid.Should().BeTrue();
                target.Pointer.Should().BeZero();
            };
        }
        #endregion

        #endregion

        #region Private Members

        private IntPtr AllocateMemory ( int size )
        {
            IntPtr ptr = Marshal.AllocCoTaskMem(size);

            return ptr;
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
            try
            {
                Marshal.FreeCoTaskMem(ptr);
            } catch
            { /* Ignore */ };
        }

        private byte[] ReadMemory ( IntPtr ptr, int size )
        {
            byte[] buffer = new byte[size];
            for (int index = 0; index < size; ++index)
                buffer[index] = Marshal.ReadByte(ptr, index);

            return buffer;
        }

        private void AssertMemory ( IntPtr ptr, byte[] expected )
        {
            var actual = ReadMemory(ptr, expected.Length);

            actual.Should().BeEquivalentTo(expected);
        }

        private byte[] WriteMemory ( IntPtr ptr, int size )
        {
            byte[] values = new byte[size];           
            new Random().NextBytes(values);

            for (int index = 0; index < size; ++index)
                Marshal.WriteByte(ptr, index, values[index]);

            return values;
        }
        #endregion
    }
}
