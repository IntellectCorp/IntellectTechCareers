using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using IntellectTechCareers.Models;

namespace IntellectTechCareers.Utils
{
    public class StringUtils
    {
        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string getMD5Hash(string input)
        {
            // given a string, byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(input);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
                // without dashes
                .Replace("-", string.Empty)
                // make lowercase
                .ToLower();

            return encoded;
        }

        public static string convertQualificationListToString(List<string> list)
        {
            string convString = "";

            foreach (var item in list)
            {
                convString = item + ",";
            }
            return convString.Substring(0, convString.Length - 1);
        }
    }
}