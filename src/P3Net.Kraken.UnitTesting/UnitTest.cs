/*
 * Copyright (c) 2005 by Michael Taylor
 * All rights reserved.
 */
#region Imports

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Security;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
#endregion

namespace P3Net.Kraken.UnitTesting
{
    /// <summary>Base class for unit tests.</summary>
    public abstract class UnitTest
    {        
        #region Public Members

        #region Attributes

        /// <summary>Determines if the user running the test is an administrator.</summary>
        public bool IsUserAdministrator
        {
            get
            {
                WindowsPrincipal user = User;
                if (user == null)
                    return false;

                //Check the SIDs
                SecurityIdentifier sidAdmin = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
                if (user.IsInRole(sidAdmin))
                    return true;

                return false;
            }
        }

        /// <summary>Gets the test assembly being run.</summary>
        public Assembly TestAssembly
        {
            get
            {
                if (m_Assembly == null)
                    FindAssembly();

                return m_Assembly;
            }
        }

        /// <summary>Gets or sets the context for the tests.</summary>
        public TestContext TestContext { get; set; }

        /// <summary>Gets the user account under which the test is running.</summary>
        public WindowsPrincipal User
        {
            get 
            { 
                WindowsPrincipal user = System.Threading.Thread.CurrentPrincipal as WindowsPrincipal;
                if (user == null)
                    user = new WindowsPrincipal(WindowsIdentity.GetCurrent());

                return user;
            }
        }
        #endregion

        #region Methods
                
        /// <summary>Called prior to each test.</summary>
        [TestInitialize]
        public void InitializeTest ( )
        {
            OnInitializeTest();
        }
        
        /// <summary>Called after each test.</summary>
        [TestCleanup]
        public void UninitializeTest ( )
        {
            OnUninitializeTest();
        }
        #endregion
        
        #endregion
        
        #region Protected Members

        /// <summary>Asserts that the tester is an administrator.</summary>
        protected void AssertAdministrator ()
        {
            if (!IsUserAdministrator)
                Assert.Inconclusive("Administrative privileges are required.");
        }

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
            StackTrace trace = new StackTrace(1);
            foreach (StackFrame frame in trace.GetFrames())
            {
                MethodBase method = frame.GetMethod();
                if (method != null)
                {
                    if (method.DeclaringType != null)
                    {
                        object[] attrs = method.DeclaringType.GetCustomAttributes(typeof(TestClassAttribute), true);
                        if (attrs.Length > 0)
                        {
                            m_Assembly = method.DeclaringType.Assembly;
                            return;
                        };
                    };
                };
            };
        }

        private Assembly m_Assembly;

        #endregion 
    }
}
