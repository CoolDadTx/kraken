/*
 * Global code analysis suppressions.
 * 
 * Copyright © 2006 Michael L Taylor ($COMPANY$)
 * All Rights Reserved
 * 
 * $Header: $
 */
#region Imports

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
#endregion


[module: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "P3Net.Kraken.Diagnostics")]