using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Utilities.Network
{
    public class DNSTool
    {
        public ToolResult DNSLookup(string hostOrIPAddress)
        {
            if (String.IsNullOrWhiteSpace(hostOrIPAddress))
            {
                return new ToolResult
                {
                    Message = "Host or IP address cannot be empty ,please type 'help network dnslookup'"
                };
            }

            try
            {
                var hostAddresses = Dns.GetHostAddresses(hostOrIPAddress);
                if (hostAddresses != null)
                    return new ToolResult { IsSuccess = true, Message = hostAddresses.FirstOrDefault()!.ToString() };

                return new ToolResult { IsSuccess = true, Message = "DNSLookUp was success but no result" };
            }
            catch (Exception)
            {
                return new ToolResult { IsSuccess = false, Message = "DNSLookUp was not success" };
            }
        }

        public ToolResult ReverseLookup(string iPAddress)
        {
            if (String.IsNullOrWhiteSpace(iPAddress))
            {
                return new ToolResult
                {
                    Message = "IP address cannot be empty ,please type 'help network reverselookup'"
                };
            }

            try
            {
                IPHostEntry entry = Dns.GetHostEntry(iPAddress);
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
