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
                    Printer.PrintInfo("Examples : ");
                    Printer.PrintInfo("network port example.com");
                    break;
                case NetworkEnum.ReverseLookUp:
                    Printer.PrintInfo("command -> network ReverseLookUp {IP}");
                    Printer.PrintInfo("Examples : ");
                    Printer.PrintInfo("network port 8.8.8.8");
                    break;
            }
        }

        public void Process()
        {

            if (_splitedInput.Length < 3)
            {
                Printer.PrintInfo("wrong command ,please type 'help network'");
                return;
            }

            if (!Enum.TryParse(_splitedInput[1].ToUpper(), true, out NetworkEnum command))
                Printer.PrintWarning("Command Not Found!");

            string commandInput = _splitedInput[2];

            switch (command)
            {
                case NetworkEnum.DNSLookUp:
                    Invoker.InvokeToolsAsync(_DNSTool.DNSLookup(commandInput));
                    break;

                case NetworkEnum.ReverseLookUp:
                    Invoker.InvokeToolsAsync(_DNSTool.ReverseLookup(commandInput));
                    break;

                case NetworkEnum.Ping:
                    Invoker.InvokeToolsAsync(_IPTool.HostOrIPHavePing(commandInput));
                    break;

                case NetworkEnum.Port:
                    Invoker.InvokeToolsAsync(_IPTool.IsHHostOrIPAndPortOpen(commandInput));
                    break;
            }
        }
    }
}
