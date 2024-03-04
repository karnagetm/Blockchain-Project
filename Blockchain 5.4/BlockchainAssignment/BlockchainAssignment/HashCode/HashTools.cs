using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace BlockchainAssignment.HashCode
{
    public static class HashTools
    {
        /// <summary>
        /// Takes byte array and returns hexadecimal string
        /// </summary>
        /// <param name="ba"></param>
        /// <returns></returns>
        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }


        public static String combineHash(String hash1, String hash2)
        {
            Byte[] bytes1 = StringToByteArray(hash1);
            Byte[] bytes2 = StringToByteArray(hash2);

            SHA256 hashSys = SHA256Managed.Create();
            Byte[] combinedbytes = hashSys.ComputeHash(bytes1.Concat(bytes2).ToArray());

            return ByteArrayToString(combinedbytes);
        }

    }
}
