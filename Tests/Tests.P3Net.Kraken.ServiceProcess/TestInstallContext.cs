/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Collections;
using System.Configuration.Install;
using System.Diagnostics;

#endregion

namespace Tests.P3Net.Kraken
{
    /// <summary>Provides uninstall support for installers.</summary>
    /// <typeparam name="T">The type of service to install.</typeparam>
    public class TestInstallContext<T> : IDisposable where T : Installer
    {
        #region Construction

        /// <summary>Initializes an instance of the <see cref="InstallContext`1"/> class.</summary>
        /// <param name="rootInstaller">The root installer.</param>
        /// <param name="installer">The installer to install.</param>
        /// <param name="assemblyPath">The assembly path to use.</param>
        public TestInstallContext ( Installer rootInstaller, T installer, string assemblyPath )
        {
            if (rootInstaller == null)
                throw new ArgumentNullException("rootInstaller");
            if (installer == null)
                throw new ArgumentNullException("installer");

            AssemblyPath = assemblyPath;			
            Installer = installer;
            RootInstaller = rootInstaller;
        }
        #endregion

        #region Public Members

        #region Attributes

        /// <summary>Gets the path to the assembly.</summary>
        public string AssemblyPath { get; private set; }

        /// <summary>Determines if it is installed or not.</summary>
        public bool IsInstalled { get; private set; }

        /// <summary>Gets the installer under test.</summary>
        public T Installer { get; private set; }

        /// <summary>Gets the root installer.</summary>
        public Installer RootInstaller { get; private set; }
        #endregion

        #region Methods

        /// <summary>Installs the root installer.</summary>
        public void Install ()
        {			
            //InstallUtil is great but we can't programmatically access its information
            //so we'll do the installation manually using the underlying class helper
            TransactedInstaller mainInstaller = PrepareInstaller();

            mainInstaller.Install(new Hashtable());
            IsInstalled = true;			
        }

        /// <summary>Uninstalls the installer.</summary>
        public void Uninstall ()
        {
            Dispose(true);
        }
        #endregion

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose ()
        {
            Dispose(true);
        }
        #endregion

        #region Private Members
                
        private void Dispose ( bool disposing )
        {
            if (disposing && IsInstalled)
            {
                TransactedInstaller installer = PrepareInstaller();

                try
                {
                    installer.Uninstall(null);
                } finally
                {
                    IsInstalled = false;
                };
            };
        }

        private TransactedInstaller PrepareInstaller ()
        {
            TransactedInstaller installer = new TransactedInstaller();

            installer.Context = new InstallContext(null, null);
            installer.Context.Parameters["assemblypath"] = AssemblyPath;
                            
            installer.Installers.Add(RootInstaller);
            
            return installer;
        }
        #endregion
    }
}
