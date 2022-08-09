using CMDToolKit.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Utilities.Network
{
    internal class IPTool
    {
        public async Task<ToolResult> HostOrIPHavePing(string hostOrIPAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = await pinger.SendPingAsync(hostOrIPAddress);
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

        public async Task<ToolResult> IsHHostOrIPAndPortOpen(string address)
        {
            var splitedAddress = address.Split(':');
            if (splitedAddress.Length < 1)
                return new ToolResult { Message = "Fomat is wrong {IP}:{Port}" };

            int port = splitedAddress.Length == 1 ? 80 : int.Parse(splitedAddress[1]);
            try
            {
                using (var client = new TcpClient())
                {
                    var ct = new CancellationTokenSource(3000).Token;
                    await client.ConnectAsync(splitedAddress[0], port, ct);
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
