using CMDToolKit.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CTK.Utilities.Hash
{
    internal class HashTools
    {
        public static ToolResult ComputeSHA1Hash(string input)
        {
            using var sha = SHA1.Create();
            return new ToolResult
            {
                Message = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(input))),
                IsSuccess = true
            };
        }

        public static ToolResult ComputeSHA256Hash(string input)
        {
            using var sha = SHA256.Create();
            return new ToolResult
            {
                Message = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(input))),
                IsSuccess = true
            };
        }

        public static ToolResult ComputeSHA384Hash(string input)
        {
            using var sha = SHA384.Create();
            return new ToolResult
            {
                Message = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(input))),
                IsSuccess = true
            };
        }

        public static ToolResult ComputeSHA512Hash(string input)
        {
            using var sha = SHA512.Create();
            return new ToolResult
            {
                Message = Convert.ToHexString(sha.ComputeHash(Encoding.UTF8.GetBytes(input))),
                IsSuccess = true
            };
        }

        public static ToolResult ComputeMD5Hash(string input)
        {
            using MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            return new ToolResult
            {
                Message = Convert.ToHexString(hashBytes),
                IsSuccess = true
            };
        }

    }
}
