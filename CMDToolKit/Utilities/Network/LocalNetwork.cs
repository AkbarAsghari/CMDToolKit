using CMDToolKit.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Utilities.Network
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
    }
}
