using CMDToolKit.Enums.Network;
using CMDToolKit.Interfaces;
using CMDToolKit.Utilities.CustomConsole;
using CMDToolKit.Utilities.Invoker;
using CMDToolKit.Utilities.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Providers.Network
{
    internal class NetworkProvider : IProvider
    {
        private readonly string[] _splitedInput;
        private readonly DNSTool _DNSTool;
        private readonly IPTool _IPTool;

        public NetworkProvider(string input)
        {
            _splitedInput = input.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();

            _DNSTool = new DNSTool();
            _IPTool = new IPTool();
        }

        public void Help()
        {
            if (_splitedInput.Length == 2)
            {
                Printer.PrintInfo("Available Commands -> " + String.Join(" , ", (NetworkEnum[])Enum.GetValues(typeof(NetworkEnum))));
                Printer.PrintInfo("Example : network ping google.com");
                return;
            }

            if (!Enum.TryParse(_splitedInput[2].ToUpper(), true, out NetworkEnum command))
            {
                Printer.PrintWarning("help not found!");
                return;
            }

            switch (command)
            {
                case NetworkEnum.Port:
                    Printer.PrintInfo("Command -> network port {host name or Ip address}:{port}");
                    Printer.PrintInfo("Examples : ");
                    Printer.PrintInfo("network port example.com");
                    Printer.PrintInfo("network port example.com:8080");
                    Printer.PrintInfo("network port 8.8.8.8");
                    Printer.PrintInfo("network port 8.8.8.8:80");
                    break;
                case NetworkEnum.Ping:
                    Printer.PrintInfo("command -> network ping {host name or Ip address}");
                    Printer.PrintInfo("Examples : ");
                    Printer.PrintInfo("network port example.com");
                    Printer.PrintInfo("network port 8.8.8.8");
                    break;
                case NetworkEnum.DNSLookUp:
                    Printer.PrintInfo("command -> network dnslookup {host name}");
                    Printer.PrintInfo("Example : network port example.com");
                    break;
                case NetworkEnum.ReverseLookUp:
                    Printer.PrintInfo("command -> network ReverseLookUp {IP}");
                    Printer.PrintInfo("Example : network port 8.8.8.8");
                    break;
                case NetworkEnum.Mac:
                    Printer.PrintInfo("command -> network mac");
                    break;
                case NetworkEnum.LocalIP:
                    Printer.PrintInfo("command -> network localip");
                    break;
                case NetworkEnum.Adapters:
                    Printer.PrintInfo("command -> network adapters");
                    break;
                default:
                    Printer.PrintError($"Help Not Found For {command}");
                    break;
            }
        }

        public void Process()
        {
            if (!Enum.TryParse(_splitedInput[1].ToUpper(), true, out NetworkEnum command))
            {
                Printer.PrintWarning("Command Not Found!");
                return;
            }

            string commandInput = _splitedInput.Length > 2 ? _splitedInput[2] : String.Empty;

            switch (command)
            {
                case NetworkEnum.DNSLookUp:
                    Invoker.InvokeTools(() => _DNSTool.DNSLookup(commandInput));
                    break;
                case NetworkEnum.ReverseLookUp:
                    Invoker.InvokeTools(() => _DNSTool.ReverseLookup(commandInput));
                    break;
                case NetworkEnum.Ping:
                    Invoker.InvokeTools(() => _IPTool.HostOrIPHavePing(commandInput));
                    break;
                case NetworkEnum.Port:
                    Invoker.InvokeTools(() => _IPTool.IsHHostOrIPAndPortOpen(commandInput));
                    break;
                case NetworkEnum.Mac:
                    Invoker.InvokeTools(() => LocalNetwork.GetMac());
                    break;
                case NetworkEnum.LocalIP:
                    Invoker.InvokeTools(() => LocalNetwork.GetLocalIPAddress());
                    break;
                case NetworkEnum.Adapters:
                    Invoker.InvokeTools(() => LocalNetwork.Adapters());
                    break;
            }
        }
    }
}
