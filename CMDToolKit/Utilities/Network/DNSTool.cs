using CMDToolKit.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Utilities.Network
{
    internal class DNSTool
    {
        public async Task<ToolResult> DNSLookup(string hostOrIPAddress)
        {
            try
            {
                var hostAddresses = await Dns.GetHostAddressesAsync(hostOrIPAddress);
                if (hostAddresses != null)
                    return new ToolResult { IsSuccess = true, Message = hostAddresses.FirstOrDefault()!.ToString() };

                return new ToolResult { IsSuccess = true, Message = "DNSLookUp was success but no result" };
            }
            catch (Exception)
            {
                return new ToolResult { IsSuccess = false, Message = "DNSLookUp was not success" };
            }
        }

        public async Task<ToolResult> ReverseLookup(string iPAddress)
        {
            try
            {
                IPHostEntry entry = await Dns.GetHostEntryAsync(iPAddress);
                if (entry != null)
                {
                    return new ToolResult { IsSuccess = true, Message = entry.HostName };
                }
            }
            catch (Exception)
            {
                return new ToolResult { IsSuccess = true, Message = "ReverseLookup was not success" };
            }

            return new ToolResult { IsSuccess = null, Message = "Nothing found" };
        }
    }
}
