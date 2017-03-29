/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;

using P3Net.Kraken.Collections;
using P3Net.Kraken.Diagnostics;

namespace P3Net.Kraken.ServiceProcess
{
	/// <summary>Determines the action to take when a service fails.</summary>
	public sealed class ServiceFailureActions
	{
		#region Construction

		/// <summary>Initializes an instance of the <see cref="ServiceFailureActions"/> class.</summary>
		public ServiceFailureActions ()
		{ /* Do nothing */ }

        internal ServiceFailureActions ( NativeMethods.SERVICE_FAILURE_ACTIONS actions )
        {
            FillMembers(actions);
        }

		internal ServiceFailureActions ( IntPtr pBuffer )
		{
			object obj = Marshal.PtrToStructure(pBuffer, typeof(NativeMethods.SERVICE_FAILURE_ACTIONS));
			FillMembers((NativeMethods.SERVICE_FAILURE_ACTIONS)obj);
		}
		#endregion

		#region Public Members

		/// <summary>Specifies the maximum number of actions available.</summary>
		public const int MaximumActions = 3;

		/// <summary>Gets the action(s) to perform if the service fails.</summary>
		/// <remarks>
		/// There can be up to 3 actions specified for a service.  All actions share
		/// the same core set of values.  For each action the default is to do
		/// nothing.  In this case the delay is ignored.
		/// <para/>
		/// The array is of <see cref="MaximumActions"/> size.
		/// </remarks>
		[Category("Behavior")]
		[Description("The action(s) to take if the service fails.")]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		public ServiceAction[] Actions
		{
			get 
            {
                if (m_actions == null)
                    m_actions = new ServiceAction[MaximumActions];

                return m_actions;
            }
		}

		/// <summary>Gets or sets the command to run if an action specifies a command.</summary>
		/// <value>The default value is an empty string.</value>
		/// <remarks>
		/// The command runs in the context of the service's user account.
		/// </remarks>
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("The command to run if an action specifies to run a command.")]
		public string Command
		{
			get { return m_command ?? ""; }
			set { m_command = (value != null) ? value.Trim() : ""; }
		}

		/// <summary>Determines if there is at least one custom action defined.</summary>
		public bool HasActionsDefined
		{
			get
			{			
				if (m_actions.IsNullOrEmpty())
					return false;

                return m_actions.Any(a => a.ActionType != ServiceActionMode.None);
			}
		}

		/// <summary>Gets or sets the message to display if the machine is rebooted.</summary>
		/// <value>The default value is an empty string representing no message.</value>
		/// <remarks>
		/// In Vista or higher the string can be of the form: <i>@[path\]dllname,-strId</i> to
		/// use a localized string.  Refer to <b>RegLoadMUIString</b> for more information.
		/// </remarks>
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("The message to display if an action specifies to reboot the machine.")]
		public string RebootMessage
		{
			get { return m_rebootMessage ?? ""; }
			set { m_rebootMessage = (value != null) ? value.Trim() : ""; }
		}

		/// <summary>Specifies the interval, in seconds, that must elapse before the failure count is reset to zero.</summary>
		/// <value>The default is 0.</value>		
		/// <remarks>
		/// This property is only valid when at least one failure action is defined.
		/// </remarks>
		/// <exception cref="ArgumentOutOfRangeException">When setting the property and value is less than 0.</exception>
		[Category("Behavior")]
		[DefaultValue(typeof(TimeSpan), "0")]
		[Description("The interval, in seconds, before the failure count will reset.")]
		public TimeSpan ResetPeriod
		{
			get { return m_resetTime; }
			set 
			{
                Verify.Argument("value", value).Is(x => x.TotalSeconds >= 0.0);
					
				m_resetTime = value; 
			}
		}
		#endregion
		
		#region Internal Members

        //Determines if the class has non-default values
        internal bool HasNondefaultValues ()
        {
            return ((m_resetTime.TotalSeconds > -1) ||
                    (RebootMessage.Length > 0) ||
                    (Command.Length > 0) || HasActionsDefined);
        }
		#endregion

		#region Private Members

		#region Methods

		private void FillMembers ( NativeMethods.SERVICE_FAILURE_ACTIONS actions )
		{
			m_resetTime = new TimeSpan(0, 0, (int)actions.dwResetPeriod);

			if (actions.rebootMsg != IntPtr.Zero)
				m_rebootMessage = Marshal.PtrToStringUni(actions.rebootMsg);
			if (actions.command != IntPtr.Zero)
				m_command = Marshal.PtrToStringUni(actions.command);

			if (actions.actions != IntPtr.Zero)
			{
				NativeMethods.SC_ACTION nativeAction = new NativeMethods.SC_ACTION();
				int sizeSCACTION = Marshal.SizeOf(nativeAction);

				//The unmanaged code is a pointer to memory so we have to enumerate the elements one by one				
				int maxActions = Math.Max((int)actions.numActions, MaximumActions);
				for (int index = 0; index < maxActions; ++index)
				{
					//Calculate the address of the next element
					IntPtr pElement = (IntPtr)(actions.actions.ToInt64() + (sizeSCACTION * index));

					//Marshal the data back
					nativeAction = (NativeMethods.SC_ACTION)Marshal.PtrToStructure(pElement, typeof(NativeMethods.SC_ACTION));

					//Build the actual element
					Actions[index] = new ServiceAction(nativeAction);
				};
			};
		}
		#endregion
        					
		private TimeSpan m_resetTime;
		private string m_rebootMessage;
		private string m_command;
        private ServiceAction[] m_actions;

		#endregion 
	}
}
