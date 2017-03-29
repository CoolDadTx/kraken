/*
 * Copyright © 2007 Michael L Taylor
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;
using System.IO;
using System.Management;

using P3Net.Kraken;
#endregion

#if share_ready
namespace P3Net.Kraken.Net
{
	/// <summary>Provides access to network shares.</summary>
	/// <preliminary/>
	[CodeNotAnalyzed]
	[CodeNotDocumented]
	[CodeNotTested]
	public static class NetworkShare
	{
		#region Public Members

		#region Constants

		/// <summary>Defines the number of simultaneous users allowed on the share.</summary>
		public const long UnlimitedUsers = 0xFFFFFFFF;
		#endregion

		#region Methods

		#region CreateShare

		/// <summary>Creates a new share.</summary>
		/// <param name="shareName">The name of the share to create.</param>		
		/// <param name="path">The local path of the share.</param>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="shareName"/> or <paramref name="path"/> is empty or invalid.</exception>
		/// <exception cref="DirectoryNotFoundException">The path is invalid or does not exist for the specified type.</exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareAlreadyExistsException">A share already exists with the given name.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		/// <remarks>
		/// This overload creates a share on the local machine to a drive with an unlimited number of users.
		/// </remarks>
		public static NetworkShareInfo CreateShare ( string shareName, string path )
		{ return CreateShare(null, shareName, path, UnlimitedUsers, null, NetworkShareTypes.DiskDrive); }

		/// <summary>Creates a new share.</summary>
		/// <param name="shareName">The name of the share to create.</param>		
		/// <param name="path">The local path of the share.</param>
		/// <param name="maximumUsers">The maximum number of simultaneous users allowed on the share.  Set to 0xFFFFFFFF to set no limit.</param>
		/// <param name="description">The description of the share.</param>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumUsers"/> is less than -1 or greater than Int32.MaxValue.</exception>
		/// <exception cref="ArgumentException"><paramref name="shareName"/> or <paramref name="path"/> is empty or invalid.</exception>
		/// <exception cref="DirectoryNotFoundException">The path is invalid or does not exist for the specified type.</exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareAlreadyExistsException">A share already exists with the given name.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		/// <remarks>
		/// This overload creates a share on the local machine to a drive.
		/// </remarks>
		public static NetworkShareInfo CreateShare ( string shareName, string path, long maximumUsers, string description )
		{ return CreateShare(null, shareName, path, maximumUsers, description, NetworkShareTypes.DiskDrive); }

		/// <summary>Creates a new share.</summary>
		/// <param name="shareName">The name of the share to create.</param>		
		/// <param name="path">The local path of the share.</param>
		/// <param name="maximumUsers">The maximum number of simultaneous users allowed on the share.  Set to 0xFFFFFFFF to set no limit.</param>
		/// <param name="description">The description of the share.</param>
		/// <param name="type">The type of the share.</param>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="shareName"/> or <paramref name="path"/> is empty or invalid.		
		/// <para>-or-</para>
		/// <paramref name="type"/> specifies an administrative share.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumUsers"/> is less than -1 or greater than Int32.MaxValue.</exception>
		/// <exception cref="DirectoryNotFoundException">The path is invalid or does not exist for the specified type.</exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareAlreadyExistsException">A share already exists with the given name.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		public static NetworkShareInfo CreateShare ( string shareName, string path, long maximumUsers, string description, NetworkShareTypes type )
		{ return CreateShare(null, shareName, path, maximumUsers, description, type); }

		/// <summary>Creates a new share.</summary>
		/// <param name="machineName">The machine on which to create the share.  If empty or <see langword="null"/> then
		/// the current machine is used.</param>
		/// <param name="shareName">The name of the share to create.</param>		
		/// <param name="path">The local path of the share.</param>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="shareName"/> or <paramref name="path"/> is empty or invalid.		
		/// <para>-or-</para>
		/// <paramref name="machineName"/> is invalid.
		/// </exception>
		/// <exception cref="DirectoryNotFoundException">The path is invalid or does not exist for the specified type.</exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareAlreadyExistsException">A share already exists with the given name.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		/// <remarks>
		/// This overload creates a share to a drive with an unlimited number of users.
		/// </remarks>
		public static NetworkShareInfo CreateShare ( string machineName, string shareName, string path )
		{ return CreateShare(machineName, shareName, path, UnlimitedUsers, null, NetworkShareTypes.DiskDrive); }

		/// <summary>Creates a new share.</summary>
		/// <param name="machineName">The machine on which to create the share.  If empty or <see langword="null"/> then
		/// the current machine is used.</param>
		/// <param name="shareName">The name of the share to create.</param>		
		/// <param name="path">The local path of the share.</param>
		/// <param name="maximumUsers">The maximum number of simultaneous users allowed on the share.  Set to 0xFFFFFFFF to set no limit.</param>
		/// <param name="description">The description of the share.</param>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="shareName"/> or <paramref name="path"/> is empty or invalid.		
		/// <para>-or-</para>
		/// <paramref name="machineName"/> is invalid.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumUsers"/> is less than -1 or greater than Int32.MaxValue.</exception>
		/// <exception cref="DirectoryNotFoundException">The path is invalid or does not exist for the specified type.</exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareAlreadyExistsException">A share already exists with the given name.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		public static NetworkShareInfo CreateShare ( string machineName, string shareName, string path, long maximumUsers, string description )
		{ return CreateShare(machineName, shareName, path, maximumUsers, description, NetworkShareTypes.DiskDrive); }

		/// <summary>Creates a new share.</summary>
		/// <param name="machineName">The machine on which to create the share.  If empty or <see langword="null"/> then
		/// the current machine is used.</param>
		/// <param name="shareName">The name of the share to create.</param>		
		/// <param name="path">The local path of the share.</param>
		/// <param name="maximumUsers">The maximum number of simultaneous users allowed on the share.  Set to 0xFFFFFFFF to set no limit.</param>
		/// <param name="description">The description of the share.</param>
		/// <param name="type">The type of the share.</param>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> or <paramref name="path"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="shareName"/> or <paramref name="path"/> is empty or invalid.		
		/// <para>-or-</para>
		/// <paramref name="machineName"/> is invalid.
		/// <para>-or-</para>
		/// <paramref name="type"/> specifies an administrative share.
		/// </exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="maximumUsers"/> is less than -1 or greater than Int32.MaxValue.</exception>
		/// <exception cref="DirectoryNotFoundException">The path is invalid or does not exist for the specified type.</exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareAlreadyExistsException">A share already exists with the given name.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		public static NetworkShareInfo CreateShare ( string machineName, string shareName, string path, long maximumUsers, string description, NetworkShareTypes type )
		{
			//Validation
			shareName = ValidateShareName(shareName);
			path = ValidationHelper.ThrowIfArgumentStringEmpty(path, "path");
			description = (description != null) ? description.Trim() : "";
			if ((type & NetworkShareTypes.Administrative) == NetworkShareTypes.Administrative)
				throw new ArgumentException("Administrative shares can not be created.");
			ValidationHelper.ThrowIfArgumentOutOfRange((int)maximumUsers, "maximumUsers", -1, Int32.MaxValue);

			//Open a connection
			ManagementScope scope = OpenWMI(machineName);
				
			//Get the class
			using (ManagementClass cls = new ManagementClass())
			{
				cls.Scope = scope;
				cls.ClassPath.ClassName = "Win32_Share";

				//Create the object
				using (ManagementObject inst = cls.CreateInstance())
				{
					//Create the instance
					int returnCode = Convert.ToInt32(inst.InvokeMethod("Create", new object[] { path, shareName, type, maximumUsers, description, null, null }));
					if (returnCode != 0)
						throw CreateException(shareName, returnCode);

					//Return the new instance
					NetworkShareInfo share = new NetworkShareInfo(machineName);
					share.Description = description;
					share.MaximumUsers = maximumUsers;
					share.Name = shareName;
					share.Path = path;
					share.ShareType = type;

					return share;
				};
			};			
		}
		#endregion

		#region DeleteShare

		/// <summary>Deletes an existing share.</summary>
		/// <param name="shareName">The name of the share to delete.</param>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="shareName"/> is empty or invalid.</exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		public static void DeleteShare ( string shareName )
		{ DeleteShare(shareName, null); }

		/// <summary>Deletes an existing share.</summary>
		/// <param name="shareName">The name of the share to delete.</param>
		/// <param name="machineName">The machine on which to delete the share.  If empty or <see langword="null"/> then
		/// the current machine is used.</param>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="shareName"/> is empty or invalid.
		/// <para>-or-</para>
		/// <paramref name="machineName"/> is invalid.
		/// </exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		public static void DeleteShare ( string machineName, string shareName )
		{
			//Validate
			shareName = ValidateShareName(shareName);

			ManagementScope scope = OpenWMI(machineName);
			
			//Query for the share 
			ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Share WHERE [Name] = '" + shareName + "'");
			using (ManagementObjectSearcher search = new ManagementObjectSearcher(scope, query))
			{
				using (ManagementObjectCollection results = search.Get())
				{
					if (results != null)
					{
						foreach (ManagementObject result in results)
						{
							//Only happens once
							int returnCode = Convert.ToInt32(result.InvokeMethod("Delete", null));
							switch (returnCode)
							{
								case 0: break;
								case 25: break;  //Doesn't exist

								default: throw CreateException(shareName, returnCode);
							};

							return;
						};
					};
				};
			};
		}
		#endregion

		#region GetShare

		/// <summary>Gets a specific share on a machine.</summary>		
		/// <param name="shareName">The name of the share to retrieve.</param>
		/// <returns>The share information, if available, or <see langword="null"/> otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException"><paramref name="shareName"/> is empty or invalid.</exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		public static NetworkShareInfo GetShare ( string shareName )
		{ return GetShare(null, shareName); }

		/// <summary>Gets a specific share on a machine.</summary>		
		/// <param name="shareName">The name of the share to retrieve.</param>
		/// <param name="machineName">The name of the machine containing the share.  If <see langword="null"/> or
		/// empty then the current machine is used.</param>
		/// <returns>The share information, if available, or <see langword="null"/> otherwise.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="shareName"/> is <see langword="null"/>.</exception>
		/// <exception cref="ArgumentException">
		/// <paramref name="shareName"/> is empty.
		/// <para>-or-</para>
		/// <paramref name="shareName"/> or <paramref name="machineName"/> are invalid.
		/// </exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		public static NetworkShareInfo GetShare ( string machineName, string shareName )
		{
			//Validate
			shareName = ValidateShareName(shareName);

			//Retrieve the share again
			ManagementScope scope = OpenWMI(machineName);
			
			//Query for the share 
			ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Share WHERE [Name] = '" + shareName + "'");
			using (ManagementObjectSearcher search = new ManagementObjectSearcher(scope, query))
			{
				using (ManagementObjectCollection results = search.Get())
				{
					if (results != null)
					{
						foreach (ManagementObject result in results)
						{
							return MakeShare(machineName, result);
						};
					};
				};
			};

			return null;
		}
		#endregion

		#region GetShares

		/// <summary>Gets all the shares defined on the machine.</summary>		
		/// <returns>An array of shares.</returns>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		public static NetworkShareInfo[] GetShares ()
		{ return GetShares(null); }

		/// <summary>Gets all the shares defined on the machine.</summary>				
		/// <param name="machineName">The name of the machine to enumerate.  If <see langword="null"/> or
		/// empty then the current machine is used.</param>
		/// <returns>An array of shares.</returns>
		/// <exception cref="ArgumentException"><paramref name="machineName"/> is invalid.</exception>
		/// <exception cref="UnauthorizedAccessException">The user does not have permission to perform the operation.</exception>
		/// <exception cref="ShareException">An error occurred performing the operation.</exception>
		public static NetworkShareInfo[] GetShares ( string machineName )
		{					
			NetworkShareInfo[] shares = null;

			//Open the WMI server
			ManagementScope scope = OpenWMI(machineName);

			//Retrieve all the shares
			ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Share");
			using (ManagementObjectSearcher search = new ManagementObjectSearcher(scope, query))
			{
				//Enumerate the shares
				using (ManagementObjectCollection results = search.Get())
				{
					if (results != null)
					{
						shares = new NetworkShareInfo[results.Count];
						int nIdx = 0;
						foreach (ManagementObject result in results)
						{
							shares[nIdx++] = MakeShare(machineName, result);								
						};
					};
				};				
			};
		
			return shares ?? new NetworkShareInfo[0];
		}
		#endregion

		#endregion

		#endregion //Public Members

		#region Internal Members

		#region Methods

		internal static NetworkShareStatus GetShareStatus ( string machineName, string shareName )
		{
			Debug.Assert(!String.IsNullOrEmpty(shareName), "Share name is invalid.");
			Debug.Assert(shareName.IndexOf('\'') < 0, "Share name contains invalid character.");

			//Retrieve the share again
			ManagementScope scope = OpenWMI(machineName);
			
			//Query for the share 
			ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_Share WHERE [Name] = '" + shareName + "'");
			using (ManagementObjectSearcher search = new ManagementObjectSearcher(scope, query))
			{
				using (ManagementObjectCollection results = search.Get())
				{
					if (results != null) 
					{
						foreach (ManagementObject result in results)
						{
							string str = result["Status"] as string;
							return (str != null) ? NetworkShareStatusHelper.Parse(str) : NetworkShareStatus.Unknown;
						};
					};
				};				
			};
									
			return NetworkShareStatus.Unknown;
		}
		#endregion

		#endregion //Internal Members

		#region Private Members

		#region Methods

		private static Exception CreateException ( string shareName, int errorCode )
		{
			switch (errorCode)
			{
				case 0: return null;
				case 2: return new UnauthorizedAccessException("Access denied.");
				case 9: return new ArgumentException("Share name is invalid.", "shareName");
				case 21: return new ArgumentException("Parameter is invalid.");					
				case 22: return new ShareAlreadyExistsException(shareName);
				case 23: return new ShareException("The path is a redirected path.", 23);
				case 24: return new DirectoryNotFoundException("The path could not be found.");
			};

			return new ShareException("Unknown error.", errorCode);
		}

		private static NetworkShareInfo MakeShare ( string machineName, ManagementObject result )
		{
			NetworkShareInfo share = new NetworkShareInfo(machineName);

			//Copy the data
			share.Description = result["Description"] as string;

			if (!Convert.ToBoolean(result["AllowMaximum"]))
				share.MaximumUsers = Convert.ToUInt32(result["MaximumAllowed"]);

			share.Name = result["Name"] as string;
			share.Path = result["Path"] as string;
			share.ShareType = (NetworkShareTypes)Convert.ToUInt32(result["Type"]);

			return share;
		}

		private static ManagementScope OpenWMI ( string machineName )
		{
			ManagementPath path = new ManagementPath();
			machineName = (machineName != null) ? machineName.Trim() : "";
			if (machineName.Length > 0)
				path.Server = machineName;

			return new ManagementScope(path, null);
		}

		private static string ValidateShareName ( string shareName )
		{
			shareName = ValidationHelper.ThrowIfArgumentStringEmpty(shareName, "shareName");
			if (shareName.IndexOf('\'') >= 0)
				throw new ArgumentException("Share name contains one or more invalid characters.", "shareName");

			return shareName;
		}
		#endregion

		#endregion //Private Members
	}
}
#endif