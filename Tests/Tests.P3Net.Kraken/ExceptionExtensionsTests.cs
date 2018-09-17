#region Imports

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken;
using P3Net.Kraken.UnitTesting;

#endregion

namespace Tests.P3Net.Kraken
{
#pragma warning disable 618
    [TestClass]
    public class ExceptionExtensionsTests : UnitTest
    {
        #region Tests
        
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
            var actual = ExceptionExtensions.IsFrameworkExceptionRegistered(typeof(TargetInvocationException));

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void IsFrameworkExceptionRegistered_IsRegularException ()
        {
            //Act
            var actual = ExceptionExtensions.IsFrameworkExceptionRegistered(typeof(ArgumentException));

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsFrameworkExceptionRegistered_IsNull ()
        {
            //Act
            var actual = ExceptionExtensions.IsFrameworkExceptionRegistered(null);

            //Assert
            actual.Should().BeFalse();
        }

        [TestMethod]
        public void IsFrameworkExceptionRegistered_IsCustomException ()
        {
            //Act
            ExceptionExtensions.RegisterFrameworkException(typeof(CustomException));
            var actual = ExceptionExtensions.IsFrameworkExceptionRegistered(typeof(CustomException));

            //Assert
            actual.Should().BeTrue();
        }
        #endregion

        #region RegisterException

        [TestMethod]
        public void RegisterException_WithCustomException ()
        {
            ExceptionExtensions.RegisterFrameworkException(typeof(CustomException));

            //Act
            var actual = ExceptionExtensions.IsFrameworkExceptionRegistered(typeof(CustomException));

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void RegisterException_ExceptionAlreadyExists ()
        {
            ExceptionExtensions.RegisterFrameworkException(typeof(CustomException));
            ExceptionExtensions.RegisterFrameworkException(typeof(CustomException));

            //Act
            var actual = ExceptionExtensions.IsFrameworkExceptionRegistered(typeof(CustomException));

            //Assert
            actual.Should().BeTrue();
        }

        [TestMethod]
        public void RegisterException_ExceptionIsNull ()
        {
            Action action = () => ExceptionExtensions.RegisterFrameworkException(null);

            action.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #endregion

        #region Private Members

        private sealed class CustomException : Exception
        {
            public CustomException () : base("CustomException") { }
        }
        #endregion
    }
#pragma warning restore 618
}
