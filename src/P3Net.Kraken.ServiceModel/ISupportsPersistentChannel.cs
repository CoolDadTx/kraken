/*
 * Copyright © 2013 Michael Taylor
 * All Rights Reserved
 */
using System;
using System.Collections.Generic;

namespace P3Net.Kraken.ServiceModel
{
    /// <summary>Represents an object that can open and close persistent channels.</summary>
    public interface ISupportsPersistentChannel
    {
        /// <summary>Closes the channel.</summary>
        void Close ();

        /// <summary>Opens the channel.</summary>
        void Open ();
    }
}
