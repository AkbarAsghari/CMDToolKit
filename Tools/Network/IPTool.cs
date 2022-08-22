using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Utilities.Network
{
    internal class IPTool
    {
        public ToolResult HostOrIPHavePing(string hostOrIPAddress)
        {
            if (String.IsNullOrWhiteSpace(hostOrIPAddress))
            {
                return new ToolResult
                {
                    Message = "Host or IP address cannot be empty ,please type 'help network ping'"
                };
            }

            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(hostOrIPAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return new ToolResult { IsSuccess = pingable, Message = pingable ? "We have ping" : "We have no ping" };
        }

        public ToolResult IsHHostOrIPAndPortOpen(string address)
        {
            if (String.IsNullOrWhiteSpace(address))
            {
                return new ToolResult
                {
                    Message = "Address cannot be empty ,please type 'help network port'"
                };
            }

            var splitedAddress = address.Split(':');
            if (splitedAddress.Length < 1)
                return new ToolResult { Message = "Fomat is wrong {IP}:{Port}" };

            int port = splitedAddress.Length == 1 ? 80 : int.Parse(splitedAddress[1]);
            try
            {
                using (var client = new TcpClient())
                {
                    client.Connect(splitedAddress[0], port);
                    return new ToolResult { IsSuccess = true, Message = $"Port {port} is open" };
                }
            }
            catch
            {
                return new ToolResult { IsSuccess = false, Message = $"Port {port} is close" };
            }
        }
    }
}
