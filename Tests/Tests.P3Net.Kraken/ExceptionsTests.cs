using System;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

namespace Tests.P3Net.Kraken
{
    [TestClass]
    public class ExceptionsTests : UnitTest
    {
        #region GetRootException

        [TestMethod]
        public void GetRootException_WithSimpleException ()
        {
            var expected = new Exception("Some exception");

            //Act
            var actual = expected.GetRootException();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetRootException_WithNestedException ()
        {
            var expected = new Exception("Some exception",
                                new Exception("Inner exception"));

            //Act
            var actual = expected.GetRootException();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetRootException_WithFrameworkException ()
        {
            var expected = new Exception("Inner exception");
            var target = new TargetInvocationException(expected);

            //Act
            var actual = target.GetRootException();

            //Assert
            actual.Should().Be(expected);
        }

        [TestMethod]
        public void GetRootException_WithFrameworkExceptionAndNoChild ()
        {
            var target = new TargetInvocationException(null);

            //Act
            var actual = target.GetRootException();

            //Assert
            actual.Should().Be(target);
        }
        #endregion

        #region IsFrameworkExceptionRegistered

        [TestMethod]
        public void IsFrameworkExceptionRegistered_IsCoreException ()
        {
            //Act
            var actual = Exceptions.IsFrameworkExceptionRegistered(typeof(TargetInvocationException));

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsFrameworkExceptionRegistered_IsRegularException ()
        {
            //Act
            var actual = Exceptions.IsFrameworkExceptionRegistered(typeof(ArgumentException));

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsFrameworkExceptionRegistered_IsNull ()
        {
            //Act
            var actual = Exceptions.IsFrameworkExceptionRegistered(null);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsFrameworkExceptionRegistered_IsCustomException ()
        {
            //Act
            Exceptions.RegisterFrameworkException(typeof(CustomException));
            var actual = Exceptions.IsFrameworkExceptionRegistered(typeof(CustomException));

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region RegisterException

        [TestMethod]
        public void RegisterException_WithCustomException ()
        {
            Exceptions.RegisterFrameworkException(typeof(CustomException));

            //Act
            var actual = Exceptions.IsFrameworkExceptionRegistered(typeof(CustomException));

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void RegisterException_ExceptionAlreadyExists ()
        {
            Exceptions.RegisterFrameworkException(typeof(CustomException));
            Exceptions.RegisterFrameworkException(typeof(CustomException));

            //Act
            var actual = Exceptions.IsFrameworkExceptionRegistered(typeof(CustomException));

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void RegisterException_ExceptionIsNull ()
        {
            Action action = () => Exceptions.RegisterFrameworkException(null);

            action.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #region Private Members

        private sealed class CustomException : Exception
        {
            public CustomException () : base("CustomException") { }
        }
        #endregion
    }
}
