using Base.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Utilities.Network
{
    internal class LocalNetwork
    {
        public static ToolResult GetMac()
        {
            try
            {
                string mac = NetworkInterface
                    .GetAllNetworkInterfaces()
                    .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    .Select(nic => nic.GetPhysicalAddress().ToString())
                    .FirstOrDefault()!;
                return new ToolResult
                {
                    Message = mac,
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                return new ToolResult
                {
                    Message = "Got exception when get mac address",
                    IsSuccess = false
                };
            }
        }

        public static ToolResult GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return new ToolResult { Message = ip.ToString(), IsSuccess = true };

            return new ToolResult { Message = "No network adapters with an IPv4 address in the system!", IsSuccess = false };
        }

        public static ToolResult Adapters()
        {
            var existAdapters = NetworkInterface.GetAllNetworkInterfaces();

            int maxNameLength = existAdapters.Max(x => x.Name.Length);
            int maxSpeedLength = existAdapters.Max(x => x.Speed.ToString().Length);
            int maxDescriptionLength = existAdapters.Max(x => x.Description.Length);
            string result = String.Join("\n", existAdapters.Select(x =>
            $"Name : {x.Name.PadRight(maxNameLength, ' ')} | " +
            $"OperationalStatus : {x.OperationalStatus} | " +
            $"IsReceiveOnly : {x.IsReceiveOnly} | " +
            $"Speed : {x.Speed.ToString().PadRight(maxSpeedLength, ' ')} B | " +
            $"Description : {x.Description.PadRight(maxDescriptionLength, ' ')}"));
            return new ToolResult
            {
                Message = result,
                IsSuccess = true
            };
        }
    }
}
