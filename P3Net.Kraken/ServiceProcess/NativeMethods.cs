/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * Primarily copied from System.ServiceProcess assembly code.
 */
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace P3Net.Kraken.ServiceProcess
{
	//Helper class for unmanaged calls	
	[ExcludeFromCodeCoverage]
	internal static class NativeMethods
	{
		#region Types

        [StructLayout(LayoutKind.Sequential)]
        public struct SERVICE_DELAYED_AUTO_START_INFO
        {
            [MarshalAs(UnmanagedType.Bool)]
            public bool fDelayedAutoStart;
        }

		[StructLayout(LayoutKind.Sequential)]
		public struct DESCRIPTION_STRUCT
		{
			public IntPtr strValue;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct DWORD_STRUCT
		{
			public uint dwValue;

			public DWORD_STRUCT ( uint value )
			{
				dwValue = value;
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct QUERY_SERVICE_CONFIG : IDisposable
		{
			public uint dwServiceType;
			public uint dwStartType;
			public uint dwErrorControl;

			public IntPtr lpBinaryPathName;
			public IntPtr lpLoadOrderGroup;

			public uint dwTagId;

			public IntPtr lpDependencies;
			public IntPtr lpServiceStartName;
			public IntPtr lpDisplayName;

			void IDisposable.Dispose ()
			{
				Clear();
			}

			public void Clear ( )
			{
				ClearHGlobal(ref lpBinaryPathName);
				ClearHGlobal(ref lpLoadOrderGroup);
				ClearHGlobal(ref lpDependencies);
				ClearHGlobal(ref lpServiceStartName);
				ClearHGlobal(ref lpDisplayName);
			}

			private static void ClearHGlobal ( ref IntPtr pMemory )
			{
				if (pMemory != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(pMemory);
					pMemory = IntPtr.Zero;
				};
			}
		}

		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct SC_ACTION
		{
			public SC_ACTION ( ServiceAction action )
			{
				type = (int)action.ActionType;
				delay = Convert.ToUInt32(action.Delay.TotalMilliseconds);
			}

			public int type;
			public uint delay;
		}

		public enum SERVICE_CONFIG_INFOLEVEL
		{
			DESCRIPTION = 1,
			FAILURE_ACTIONS = 2,
			DELAYED_AUTO_START_INFO = 3,
			FAILURE_ACTIONS_FLAG = 4,
			SERVICE_SID_INFO = 5,
			REQUIRED_PRIVILEGES_INFO = 6,
			PRESHUTDOWN_INFO = 7,
		}

		
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct SERVICE_FAILURE_ACTIONS : IDisposable
		{
			public SERVICE_FAILURE_ACTIONS ( ServiceFailureActions sfa )
			{
				if (sfa.ResetPeriod.TotalSeconds > 0)
					dwResetPeriod = Convert.ToUInt32(sfa.ResetPeriod.TotalSeconds);
				else
					dwResetPeriod = 0;

				if (sfa.RebootMessage.Length > 0)
					rebootMsg = Marshal.StringToHGlobalUni(sfa.RebootMessage);
				else
					rebootMsg = IntPtr.Zero;

				if (sfa.Command.Length > 0)
					command = Marshal.StringToHGlobalUni(sfa.Command);
				else
					command = IntPtr.Zero;
								
				if (sfa.HasActionsDefined)
				{
					numActions = (uint)sfa.Actions.Length;

					//Allocate space for the unmanaged array
					SC_ACTION nativeAction;
					int sizeSCACTION = Marshal.SizeOf(typeof(SC_ACTION));
					actions = Marshal.AllocHGlobal(sizeSCACTION * sfa.Actions.Length);

					//Nowe we need to copy the elements to the new array (but we have to track where we place it)					
					IntPtr pElement = actions;
					for (int nIdx = 0; nIdx < numActions; ++nIdx)
					{
						//Copy the action data
						nativeAction = new SC_ACTION(sfa.Actions[nIdx]);

						//The destination is actual calculated from the base of our pointer
						pElement = (IntPtr)(actions.ToInt64() + (sizeSCACTION * nIdx));
						Marshal.StructureToPtr(nativeAction, pElement, false);
					};
				} else
				{
					numActions = 0;
					actions = IntPtr.Zero;
				};
			}

			public uint dwResetPeriod;

            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
			public IntPtr rebootMsg;

            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
			public IntPtr command;

			public uint numActions;

            [SuppressMessage("Microsoft.Reliability", "CA2006:UseSafeHandleToEncapsulateNativeResources")]
			public IntPtr actions;

			void IDisposable.Dispose ()
			{
				Clear();
			}

			public void Clear ( )
			{
				ClearHGlobal(ref rebootMsg);
				ClearHGlobal(ref command);
				ClearHGlobal(ref actions);
			}

			private static void ClearHGlobal ( ref IntPtr pMemory )
			{
				if (pMemory != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(pMemory);
					pMemory = IntPtr.Zero;
				};
			}		
		}
		#endregion

		#region Constants

		public const uint SERVICE_NO_CHANGE = 0xFFFFFFFF;

		public const uint ERROR_INSUFFICIENT_BUFFER = 122;
		#endregion

		#region Methods
				
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]		
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage")]
		internal static extern bool ChangeServiceConfig ( SafeHandle serviceHandle, uint dwServiceType, uint dwStartType, uint dwErrorControl,
										string lpBinaryPathName, string lpLoadOrderGroup, IntPtr dwTagId, string lpDependencies,
										string lpServiceStartName, string lpPassword, string lpDisplayName );

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage")]
		internal static extern bool ChangeServiceConfig2 ( SafeHandle serviceHandle, SERVICE_CONFIG_INFOLEVEL infoLevel, ref SERVICE_FAILURE_ACTIONS serviceDesc );

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage")]
		internal static extern bool QueryServiceConfig ( SafeHandle hService, IntPtr lpServiceConfig, uint cbBufSize, out uint pcbBytesNeeded );

		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		[SuppressMessage("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage")]
		internal static extern bool QueryServiceConfig2 ( SafeHandle hService, SERVICE_CONFIG_INFOLEVEL dwInfoLevel, 
				IntPtr lpBuffer, uint cbBufSize, out uint pcbBytesNeeded );		
		#endregion
	}
}