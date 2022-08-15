using CMDToolKit.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Utilities.EncodersDecoders
{
    internal class EncoderDecoderTools
    {
        #region Base64
        public static ToolResult Base64Encode(string plainText)
        {
            if (String.IsNullOrEmpty(plainText))
            {
                return new ToolResult
                {
                    Message = "Plain Text cannot be empty ,please type 'help encode base64'"
                };
            }
            try
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return new ToolResult
                {
                    Message = Convert.ToBase64String(plainTextBytes),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ToolResult
                {
                    Message = "Got exception when encoding",
                    IsSuccess = false
                };
            }
        }

        public static ToolResult Base64Decode(string base64EncodedData)
        {
            if (String.IsNullOrEmpty(base64EncodedData))
            {
                return new ToolResult
                {
                    Message = "base64 Encoded Data cannot be empty ,please type 'help decode base64'"
                };
            }

            try
            {
                var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                return new ToolResult
                {
                    Message = Encoding.UTF8.GetString(base64EncodedBytes),
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ToolResult
                {
                    Message = "Got exception when decoding",
                    IsSuccess = false
                };
            }
        }
        #endregion

        #region Base32
        public static ToolResult Base32Encode(string plainText)
        {
            byte[] input = Encoding.UTF8.GetBytes(plainText);

            if (input == null || input.Length == 0)
            {
                throw new ArgumentNullException("input");
            }

            int charCount = (int)Math.Ceiling(input.Length / 5d) * 8;
            char[] returnArray = new char[charCount];

            byte nextChar = 0, bitsRemaining = 5;
            int arrayIndex = 0;

            foreach (byte b in input)
            {
                nextChar = (byte)(nextChar | (b >> (8 - bitsRemaining)));
                returnArray[arrayIndex++] = ValueToChar(nextChar);

                if (bitsRemaining < 4)
                {
                    nextChar = (byte)((b >> (3 - bitsRemaining)) & 31);
                    returnArray[arrayIndex++] = ValueToChar(nextChar);
                    bitsRemaining += 5;
                }

                bitsRemaining -= 3;
                nextChar = (byte)((b << bitsRemaining) & 31);
            }

            //if we didn't end with a full char
            if (arrayIndex != charCount)
            {
                returnArray[arrayIndex++] = ValueToChar(nextChar);
                while (arrayIndex != charCount) returnArray[arrayIndex++] = '='; //padding
            }

            return new ToolResult { Message = new string(returnArray), IsSuccess = true };
        }

        public static ToolResult Base32Decode(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }

            input = input.TrimEnd('='); //remove padding characters
            int byteCount = input.Length * 5 / 8; //this must be TRUNCATED
            byte[] returnArray = new byte[byteCount];

            byte curByte = 0, bitsRemaining = 8;
            int mask = 0, arrayIndex = 0;

            foreach (char c in input)
            {
                int cValue = CharToValue(c);

                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    returnArray[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }

            //if we didn't end with a full byte
            if (arrayIndex != byteCount)
            {
                returnArray[arrayIndex] = curByte;
            }

            return new ToolResult { Message = Encoding.UTF8.GetString(returnArray), IsSuccess = true };
        }

        private static int CharToValue(char c)
        {
            int value = (int)c;

            //65-90 == uppercase letters
            if (value < 91 && value > 64)
            {
                return value - 65;
            }
            //50-55 == numbers 2-7
            if (value < 56 && value > 49)
            {
                return value - 24;
            }
            //97-122 == lowercase letters
            if (value < 123 && value > 96)
            {
                return value - 97;
            }

            throw new ArgumentException("Character is not a Base32 character.", "c");
        }

        private static char ValueToChar(byte b)
        {
            if (b < 26)
            {
                return (char)(b + 65);
            }

            if (b < 32)
            {
                return (char)(b + 24);
            }

            throw new ArgumentException("Byte is not a value Base32 value.", "b");
        }
        #endregion

        #region HTML

        public static ToolResult HTMLEncode(string input)
        {
            return new ToolResult { Message = System.Web.HttpUtility.HtmlEncode(input), IsSuccess = true };
        }

        public static ToolResult HTMLDecode(string input)
        {
            return new ToolResult { Message = System.Web.HttpUtility.HtmlDecode(input), IsSuccess = true };
        }

        #endregion

        #region URL

        public static ToolResult URLEncode(string input)
        {
            return new ToolResult { Message = System.Web.HttpUtility.UrlEncode(input), IsSuccess = true };
        }

        public static ToolResult URLDecode(string input)
        {
            return new ToolResult { Message = System.Web.HttpUtility.encode(input), IsSuccess = true };
        }

        #endregion
    }
}
