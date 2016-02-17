using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace ParichayLite.Domain.Util
{
    public class Helper
    {
        public static string GetHash(string input)
        {
             HMACSHA1 SHA1KeyedHasher = new HMACSHA1();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = SHA1KeyedHasher.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }
    }
}