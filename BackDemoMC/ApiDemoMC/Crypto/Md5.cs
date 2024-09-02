// <copyright file="Md5.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApiDemoMC.Crypto
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public static class Md5
    {
        public static string Convert(string str)
        {
            MD5 hashMaker = MD5.Create();
            byte[] hash = hashMaker.ComputeHash(Encoding.Unicode.GetBytes(str));
            StringBuilder sBuilder = new StringBuilder();
            foreach (var b in hash)
            {
                sBuilder.Append(b.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        public static bool VerifyPass(string inputPass, string hash)
        {
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;
            if (comparer.Compare(Convert(inputPass), hash) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
