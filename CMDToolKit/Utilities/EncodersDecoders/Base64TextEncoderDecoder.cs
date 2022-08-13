using CMDToolKit.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Utilities.EncodersDecoders
{
    internal class Base64TextEncoderDecoder
    {
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
    }
}
