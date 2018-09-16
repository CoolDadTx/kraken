/*
 * Copyright © 2005 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
using System.Diagnostics.CodeAnalysis;
#endregion

namespace P3Net.Kraken.WinForms.Native
{
    /// <summary>Defines the different messages for an animation control.</summary>
    [SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
    public enum AnimationMessage
    {
        /// <summary>
        /// ACM_OPENW.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Instance handle for the module containing the resource.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>The path to the filename or the name of the resource to open.</description>
        ///		</item>
        /// </list>
        /// </summary>
        Open = 0x0503,
        
        /// <summary>
        /// ACM_PLAY.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Number of times to replay the file.  Use -1 for indefinitely.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>MAKELONG(wFrom, wTo) where <i>wFrom</i> is the zero-based starting frame and
        ///			<i>wTo</i> is the zero-based ending frame.  If <i>wFrom</i> is 0 then it starts at the first frame.
        ///			If <i>wTo</i> is -1 then it runs to the last frame.</description>
        ///		</item>
        /// </list>
        /// </summary>
        Play = 0x0501,

        /// <summary>
        /// ACM_STOP.
        /// <para/>
        /// <list type="table">
        ///		<item>
        ///			<term>WPARAM</term>
        ///			<descrption>Unused.</descrption>
        ///		</item>
        ///		<item>
        ///			<term>LPARAM</term>
        ///			<description>Unused.</description>
        ///		</item>
        /// </list>
        /// </summary>
        Stop = 0x0502,
    }
}
