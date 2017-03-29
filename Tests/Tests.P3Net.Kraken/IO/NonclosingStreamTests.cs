#region Imports

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;
using Moq;

using P3Net.Kraken.IO;
using P3Net.Kraken.UnitTesting;

#endregion

namespace Tests.P3Net.Kraken.IO
{
    [TestClass]
    public class NonclosingStreamTests : UnitTest
    {
        #region Construction

        [TestMethod]
        public void Ctor_WithValidStream ()
        {
            var mock = new Mock<Stream>();
            mock.Setup(x => x.Close());

            //Act
            var target = new NonclosingStream(mock.Object);
            target.Close();

            //Assert
            mock.Verify(x => x.Close(), Times.Never());
        }

        [TestMethod]
        public void Ctor_NullStreamFails ()
        {
            Action action = () => new NonclosingStream(null);

            action.ShouldThrowArgumentNullException();
        }
        #endregion

        #region Closing

        [TestMethod]
        public void Closing_InnerStreamNotClosed ()
        {
            //Arrange
            var mock = new Mock<MemoryStream>();
            
            //Act
            var target = new NonclosingStream(mock.Object);
            target.Close();

            //Assert
            mock.Verify(x => x.Close(), Times.Never());
        }

        [TestMethod]
        public void Closing_InnerStreamNotClosedWithUsing ()
        {
            //Arrange
            var mock = new Mock<MemoryStream>();

            //Act
            using (var target = new NonclosingStream(mock.Object))
            { };

            //Assert
            mock.Verify(x => x.Close(), Times.Never());
        }
        #endregion

        #region Wrapping

        #region Attributes
        
        [TestMethod]
        public void CanRead_IsWrapped ()
        {
            var mock = new Mock<Stream>();
            mock.SetupGet(x => x.CanRead).Returns(true);

            //Act
            var target = new NonclosingStream(mock.Object);
            var actual = target.CanRead;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanSeek_IsWrapped ()
        {
            var mock = new Mock<Stream>();
            mock.SetupGet(x => x.CanSeek).Returns(true);

            //Act
            var target = new NonclosingStream(mock.Object);
            var actual = target.CanSeek;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void CanWrite_IsWrapped ()
        {
            var mock = new Mock<Stream>();
            mock.SetupGet(x => x.CanWrite).Returns(true);

            //Act
            var target = new NonclosingStream(mock.Object);
            var actual = target.CanWrite;

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void Length_IsWrapped ()
        {
            var expected = 100;
            var mock = new Mock<Stream>();
            mock.SetupGet(x => x.Length).Returns(expected);

            //Act
            var target = new NonclosingStream(mock.Object);
            var actual = target.Length;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Position_GetIsWrapped ()
        {
            var expected = 50;

            var mock = new Mock<Stream>();
            mock.SetupGet(x => x.Position).Returns(expected);
            
            //Act
            var target = new NonclosingStream(mock.Object);
            var actual = target.Position;

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void Position_SetIsWrapped ()
        {
            var expected = 50L;

            var mock = new Mock<Stream>();
            mock.SetupProperty(x => x.Position);

            //Act
            var target = new NonclosingStream(mock.Object);
            target.Position = expected;

            //Assert
            mock.VerifySet(x => x.Position = It.Is<long>(a => a == expected));
        }
        #endregion

        #region Methods

        [TestMethod]
        public void Flush_IsWrapped ()
        {
            var mock = new Mock<Stream>();
            mock.Setup(x => x.Flush());

            //Act
            var target = new NonclosingStream(mock.Object);
            target.Flush();

            //Assert
            mock.Verify(x => x.Flush(), Times.Once());
        }

        [TestMethod]
        public void Read_IsWrapped ()
        {
            var data = new byte[3] { 1, 2, 3 };
            var offset = 1;
            var count = 2;

            var expectedData = new byte[] { 0, 1, 2 };
            var actualData = new byte[expectedData.Length];

            var mock = new Mock<Stream>();
            mock.Setup(x => x.Read(It.IsAny<byte[]>(), It.Is<int>(o => o == offset), It.Is<int>(c => c == count)))
                .Returns(count)
                .Callback<byte[], int, int>((d, o, c) => Array.Copy(data, 0, d, o, c));

            //Act
            var target = new NonclosingStream(mock.Object);
            var expectedRead = target.Read(actualData, offset, count);

            //Assert
            expectedRead.Should().Be(count);
            actualData.Should().ContainInOrder(expectedData);
        }

        [TestMethod]
        public void Seek_IsWrapped ()
        {
            var expectedOffset = 100L;
            var expectedPosition = expectedOffset;

            var mock = new Mock<Stream>();
            mock.Setup(x => x.Seek(It.IsAny<long>(), It.IsAny<SeekOrigin>()))
                .Returns(expectedPosition);

            //Act
            var target = new NonclosingStream(mock.Object);
            var actualPosition = target.Seek(expectedOffset, SeekOrigin.Begin);

            //Assert
            actualPosition.Should().Be(expectedPosition);
            mock.Verify(x => x.Seek(It.Is<long>(o => o == expectedOffset), It.Is<SeekOrigin>(o => o == SeekOrigin.Begin)));
        }

        [TestMethod]
        public void SetLength_IsWrapped ()
        {
            var expectedLength = 100L;

            var mock = new Mock<Stream>();
            mock.Setup(x => x.SetLength(It.IsAny<long>()));

            //Act
            var target = new NonclosingStream(mock.Object);
            target.SetLength(expectedLength);

            //Assert
            mock.Verify(x => x.SetLength(It.Is<long>(l => l == expectedLength)));
        }

        [TestMethod]
        public void Write_IsWrapped ()
        {            
            var data = new byte[] { 0, 1, 2 };
            var offset = 1;
            var count = 2;

            var expectedData = new byte[] { 1, 2 };            
            var actualData = new byte[2];

            var mock = new Mock<Stream>();
            mock.Setup(x => x.Write(It.IsAny<byte[]>(), It.Is<int>(o => o == offset), It.Is<int>(c => c == count)))
                .Callback<byte[], int, int>(( d, o, c ) => Array.Copy(d, o, actualData, 0, c));

            //Act
            var target = new NonclosingStream(mock.Object);
            target.Write(data, offset, count);

            //Assert
            actualData.Should().ContainInOrder(expectedData);
        }
        #endregion

        #endregion
    }
}
