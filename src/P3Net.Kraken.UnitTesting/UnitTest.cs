/*
 * Copyright (c) 2005 by Michael Taylor
 * All rights reserved.
 */
using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace P3Net.Kraken.UnitTesting
{
    /// <summary>Base class for unit tests.</summary>
    public abstract class UnitTest
    {
#if NET_FRAMEWORK
        /// <summary>Determines if the user running the test is an administrator.</summary>
        public bool IsUserAdministrator
        {
            get
            {
                var user = User as WindowsPrincipal;
                if (User == null)
                    return false;

                //Check the SIDs
                var sidAdmin = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                if (user.IsInRole(sidAdmin))
                    return true;

                return false;
            }
        }
#endif

        /// <summary>Gets the test assembly being run.</summary>
        public Assembly TestAssembly
        {
            get
            {
                if (_assembly == null)
                    FindAssembly();

                return _assembly;
            }
        }

        /// <summary>Gets or sets the context for the tests.</summary>
        public TestContext TestContext { get; set; }

        /// <summary>Gets the user account under which the test is running.</summary>
        public IPrincipal User
        {
            get 
            {
                var user = System.Threading.Thread.CurrentPrincipal;
#if NET_FRAMEWORK

                if (user == null)
                    user = new WindowsPrincipal(WindowsIdentity.GetCurrent());
#endif

                return user;
            }
        }
                       
        /// <summary>Called prior to each test.</summary>
        [TestInitialize]
        public void InitializeTest ( ) => OnInitializeTest();
        
        /// <summary>Called after each test.</summary>
        [TestCleanup]
        public void UninitializeTest ( ) => OnUninitializeTest();

        #region Protected Members

#if NET_FRAMEWORK
        /// <summary>Asserts that the tester is an administrator.</summary>
        protected void AssertAdministrator ()
        {
            if (!IsUserAdministrator)
                Assert.Inconclusive("Administrative privileges are required.");
        }
#endif

        /// <summary>Called after each test.</summary>
        protected virtual void OnUninitializeTest ( )
        {
        }
        
        /// <summary>Called prior to each test.</summary>
        protected virtual void OnInitializeTest ( )
        {
        }
        #endregion

        #region Private Members

        private void FindAssembly ()
        {
            //Start walking the stack frame until we find the test class
            var trace = new StackTrace(1);
            foreach (var frame in trace.GetFrames())
            {
                var method = frame.GetMethod();
                if (method != null)
                {
                    if (method.DeclaringType != null)
                    {
                        var attrs = method.DeclaringType.GetCustomAttributes(typeof(TestClassAttribute), true);
                        if (attrs.Length > 0)
                        {
                            _assembly = method.DeclaringType.Assembly;
                            return;
                        };
                    };
                };
            };
        }

        private Assembly _assembly;

        #endregion
    }
}
