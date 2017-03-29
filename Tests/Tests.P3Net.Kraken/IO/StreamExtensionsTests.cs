#region Impots

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using FluentAssertions;

using P3Net.Kraken.IO;
using P3Net.Kraken.UnitTesting;
#endregion

namespace Tests.P3Net.Kraken.IO
{
    [TestClass]
    public partial class StreamExtensionsTests : UnitTest
    {
        private static byte[] CreateCompressedString ( string value, Encoding encoding )
        {
            //The length is in characters
            byte[] lengthData = GetCompressedInt32(value.Length);

            var stringData = encoding.GetBytes(value);            

            var data = new byte[stringData.Length + lengthData.Length];
            Buffer.BlockCopy(lengthData, 0, data, 0, lengthData.Length);
            Buffer.BlockCopy(stringData, 0, data, lengthData.Length, stringData.Length);

            return data;
        }

        private static byte[] CreateFixedString ( string value, Encoding encoding, int length, char fillChar = ' ')
        {
            var newValue = value.PadRight(length, fillChar);

            return encoding.GetBytes(newValue);
        }

        private static byte[] CreateLengthPrefixedString ( string value, StringLengthPrefix length, Encoding encoding )
        {
            var stringLength = value.Length;
            byte[] lengthData = null;

            switch (length)
            {
                case StringLengthPrefix.One: lengthData = new byte[] { (byte)stringLength }; break;
                case StringLengthPrefix.Two: lengthData = BitConverter.GetBytes((short)stringLength); break;
                case StringLengthPrefix.Four: lengthData = BitConverter.GetBytes(stringLength); break;
            };

            var stringData = encoding.GetBytes(value);

            var data = new byte[stringData.Length + lengthData.Length];
            Buffer.BlockCopy(lengthData, 0, data, 0, lengthData.Length);
            Buffer.BlockCopy(stringData, 0, data, lengthData.Length, stringData.Length);

            return data;
        }

        private static byte[] CreateNullTerminatedString ( string value, Encoding encoding )
        {
            return encoding.GetBytes(value + '\0');
        }
        
        //From the actual code for accuracy
        private static byte[] GetCompressedInt32 ( int value )
        {
            var lengthValue = new List<byte>();

            uint num = (uint)value;
            while (num >= 128)
            {
                lengthValue.Add((byte)(num | 128));
                num >>= 7;
            };

            lengthValue.Add((byte)num);

            return lengthValue.ToArray();
        }
    }
}
