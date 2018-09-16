/*
 * Copyright © 2010 Michael Taylor
 * All Rights Reserved
 */
#region Imports

using System;
#endregion

namespace P3Net.Kraken
{
    /// <summary>Represents intervals for duration.</summary>
    public enum DurationInterval
    {
        /// <summary>Clock ticks</summary>
        Ticks = 0,

        /// <summary>Days</summary>
        Days,

        /// <summary>Hours</summary>
        Hours,

        /// <summary>Minutes</summary>
        Minutes,

        /// <summary>Seconds</summary>
        Seconds,

        /// <summary>Milliseconds</summary>
        Milliseconds,
    }
}
