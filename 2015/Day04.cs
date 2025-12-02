using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace _2015
{
    public class Day04 : IDay
    {

        public static long Part_1(string input)
        {
            long idx = 1;
            while (true)
            {
                string hexHash = CreateMD5(input + idx.ToString());
                if (Regex.IsMatch(hexHash, @"^00000")) { return idx; }
                idx++;
            }
        }

        public static long Part_2(string input)
        {
            long idx = 1;
            while (true)
            {
                string hexHash = CreateMD5(input + idx.ToString());
                if (Regex.IsMatch(hexHash, @"^000000")) { return idx; }
                idx++;
            }
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
    }
}
