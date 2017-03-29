/*
 * Extended Interlocked class.
 *
 * Copyright © 2005 Michael L Taylor
 * All Rights Reserved
 */
using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace P3Net.Kraken.Threading
{
	/// <summary>Provides additional interlocked operations support.</summary>
	/// <remarks>
	/// This class is partially based on the following articles:
	/// <list type="bullet">
	///		<item>Morrison, Vance. <i>Understand the Impact of Low-Lock Techniques in Multithreaded Apps.</i>, <u>MSDN Magazine.</u> October, 2005.</item>
	///		<item>Richter, Jeffrey. <i>Concurrent Affairs: Performance-Conscious Thread Synchronization.</i>, <u>MSDN Magazine.</u> October, 2005.</item>
	/// </list>
	/// </remarks>
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	public static class Interlock
	{			
		#region And

		/// <summary>Performs an atomic AND operation.</summary>
		/// <param name="target">The value to update.</param>
		/// <param name="andValue">The value to AND with <paramref name="target"/>.</param>
		/// <returns>The new value of <paramref name="target"/>.</returns>
		/// <remarks>
		/// The method will attempt the AND operation until it succeeds.
		/// </remarks>
        /// <example>This example, although it does not accurately representing the threading issues involved, simulates a context switch between 
        /// each atomic operation in SafeDoWork.  If SafeDoWork did not use an interlock method then a context switch could have occurred 
        /// during the section of code listed in the comments and the resulting output could have varied.
        /// <code lang="C#">
        ///		using System.Threading;
        /// 
        ///		public class App
        ///		{
        ///			//Used to simulate a thread context switch
        ///			private static AutoResetEvent switch1 = new AutoResetEvent(false);
        ///			private static AutoResetEvent switch2 = new AutoResetEvent(false);
        /// 
        ///			private static int SafeCounter = 0x00000000;
        /// 
        ///			private static void SafeDoWork ( )
        ///			{
        ///				//Simulate a stall waiting for the first thread
        ///				switch1.WaitOne();
        /// 
        ///				for (int nIdx = 0; nIdx &lt; 3; ++nIdx)
        ///				{
        ///					//By using the interlock method we ensure that SafeCounter does not change
        ///					//between the time we retrieve the value and the time we set it which can
        ///					//happen with even the bitwise operators
        ///					Interlock.And(ref SafeCounter, 0x10101010);
        ///
        ///					//Equivalent, non-thread safe code:
        ///					//int newValue = SafeCounter &amp; 0x10101010;
        ///					//SafeCounter = newValue;
        /// 
        ///					//Simulate context-switch
        ///					WaitHandle.SignalAndWait(switch1, switch2);	
        ///				};
        /// 
        ///				//A final time and release the lock
        ///				Interlock.And(ref SafeCounter, 0x10101010);
        ///				switch2.Set();	//Simulate context-switch
        ///			}
        /// 
        ///			private static void UnsafeDoWork ( )
        ///			{
        ///				SafeCounter += 0x10000000;
        ///				WaitHandle.SignalAndWait(switch1, switch2);	//Simulate context-switch
        ///				Console.WriteLine("SafeCounter = 0x{0:X}", SafeCounter);
        /// 
        ///				SafeCounter += 0x00100000;
        ///				WaitHandle.SignalAndWait(switch1, switch2);	//Simulate context-switch
        ///				Console.WriteLine("SafeCounter = 0x{0:X}", SafeCounter);
        /// 
        ///				SafeCounter += 0x00001000;
        ///				WaitHandle.SignalAndWait(switch1, switch2);	//Simulate context-switch
        ///				Console.WriteLine("SafeCounter = 0x{0:X}", SafeCounter);
        /// 
        ///				SafeCounter += 0x00000010;
        ///				WaitHandle.SignalAndWait(switch1, switch2);	//Simulate context-switch
        ///				Console.WriteLine("SafeCounter = 0x{0:X}", SafeCounter);
        ///			}
        /// 
        ///			static void Main ( )
        ///			{
        ///				Thread thread1 = new Thread(new ThreadStart(SafeDoWork));
        ///				Thread thread2 = new Thread(new ThreadStart(UnsafeDoWork));
        /// 
        ///				thread1.Start();
        ///				thread2.Start();
        /// 
        ///				thread1.Join();
        ///				thread2.Join();
        ///			}
        ///		}
        /// 
        ///		//Output is:
        ///		//	SafeCounter = 0x10000000
        ///		//	SafeCounter = 0x10100000
        ///		//  SafeCounter = 0x10101000
        ///		//	SafeCounter = 0x10101010
        /// </code>
        /// </example>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
		public static int And ( ref int target, int andValue )
		{
			int initValue, newValue, prevValue;

			do
			{
				//Get the current value
				initValue = target;

				//And
				newValue = initValue & andValue;

				//Try and swap, if it changed then this fails so we'll loop again
				prevValue = Interlocked.CompareExchange(ref target, newValue, initValue);
			} while (initValue != prevValue);

			return newValue;
		}

		/// <summary>Performs an atomic AND operation.</summary>
		/// <param name="target">The value to update.</param>
		/// <param name="andValue">The value to AND with <paramref name="target"/>.</param>
		/// <returns>The new value of <paramref name="target"/>.</returns>
		/// <remarks>
		/// The method will attempt the AND operation until it succeeds.
		/// </remarks>
		/// <example>
		/// Refer to <see cref="M:InterlockedEx.And(System.Int64, System.Int64)">Interlocked.And</see>
		/// for an example of using this method.
		/// </example>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
		public static long And ( ref long target, long andValue )
		{
			long initValue, newValue, prevValue;

			do
			{
				//Get the current value
				initValue = target;

				//And
				newValue = initValue & andValue;

				//Try and swap, if it changed then this fails so we'll loop again
				prevValue = Interlocked.CompareExchange(ref target, newValue, initValue);
			} while (initValue != prevValue);

			return newValue;
		}
		#endregion	

		#region Or

		/// <summary>Performs an atomic OR operation.</summary>
		/// <param name="target">The value to update.</param>
		/// <param name="andValue">The value to OR with <paramref name="target"/>.</param>
		/// <returns>The new value of <paramref name="target"/>.</returns>
        /// <remarks>
        /// The method will attempt the OR operation until it succeeds.
        /// </remarks>
        /// <example>
        /// Refer to <see cref="M:Interlock.And(System.Int64, System.Int64)">Interlock.And</see> for an example of using And which is identical to using this method.
        /// </example>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
		public static int Or ( ref int target, int andValue )
		{
			int initValue, newValue, prevValue;

			do
			{
				//Get the current value
				initValue = target;

				//Or
				newValue = initValue | andValue;

				//Try and swap, if it changed then this fails so we'll loop again
				prevValue = Interlocked.CompareExchange(ref target, newValue, initValue);
			} while (initValue != prevValue);

			return newValue;
		}

		/// <summary>Performs an atomic OR operation.</summary>
		/// <param name="target">The value to update.</param>
		/// <param name="andValue">The value to OR with <paramref name="target"/>.</param>
		/// <returns>The new value of <paramref name="target"/>.</returns>
		/// <remarks>
		/// The method will attempt the OR operation until it succeeds.
		/// </remarks>
		/// <example>
		/// Refer to <see cref="M:InterlockedEx.And(System.Int64, System.Int64)">Interlocked.And</see>
		/// for an example of using And which is identical to using this method.
		/// </example>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
		public static long Or ( ref long target, long andValue )
		{
			long initValue, newValue, prevValue;

			do
			{
				//Get the current value
				initValue = target;

				//Or
				newValue = initValue | andValue;

				//Try and swap, if it changed then this fails so we'll loop again
				prevValue = Interlocked.CompareExchange(ref target, newValue, initValue);
			} while (initValue != prevValue);

			return newValue;
		}
		#endregion

		#region Xor

		/// <summary>Performs an atomic XOR operation.</summary>
		/// <param name="target">The value to update.</param>
		/// <param name="andValue">The value to XOR with <paramref name="target"/>.</param>
		/// <returns>The new value of <paramref name="target"/>.</returns>
		/// <remarks>
		/// The method will attempt the XOR operation until it succeeds.
		/// </remarks>
		/// <example>
		/// Refer to <see cref="M:InterlockedEx.And(System.Int64, System.Int64)">Interlocked.And</see>
		/// for an example of using And which is identical to using this method.
		/// </example>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
		public static int Xor ( ref int target, int andValue )
		{
			int initValue, newValue, prevValue;

			do
			{
				//Get the current value
				initValue = target;

				//Xor
				newValue = initValue ^ andValue;

				//Try and swap, if it changed then this fails so we'll loop again
				prevValue = Interlocked.CompareExchange(ref target, newValue, initValue);
			} while (initValue != prevValue);

			return newValue;
		}

		/// <summary>Performs an atomic XOR operation.</summary>
		/// <param name="target">The value to update.</param>
		/// <param name="andValue">The value to XOR with <paramref name="target"/>.</param>
		/// <returns>The new value of <paramref name="target"/>.</returns>
		/// <remarks>
		/// The method will attempt the XOR operation until it succeeds.
		/// </remarks>
		/// <example>
		/// Refer to <see cref="M:InterlockedEx.And(System.Int64, System.Int64)">Interlocked.And</see>
		/// for an example of using And which is identical to using this method.
		/// </example>
        [SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference", MessageId = "0#")]
        public static long Xor ( ref long target, long andValue )
		{
			long initValue, newValue, prevValue;

			do
			{
				//Get the current value
				initValue = target;

				//Xor
				newValue = initValue ^ andValue;

				//Try and swap, if it changed then this fails so we'll loop again
				prevValue = Interlocked.CompareExchange(ref target, newValue, initValue);
			} while (initValue != prevValue);

			return newValue;
		}
		#endregion
	}
}
