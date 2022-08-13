using CMDToolKit.Enums;
using CMDToolKit.Enums.EncodersDecoders;
using CMDToolKit.Enums.Network;
using CMDToolKit.Utilities.ClipboardTool;
using CMDToolKit.Utilities.CustomConsole;
using CMDToolKit.Utilities.EncodersDecoders;
using CMDToolKit.Utilities.Invoker;
using CMDToolKit.Utilities.Network;

var _DNSTool = new DNSTool();
var _IPTool = new IPTool();

int _ThreadSleep = 1000;

Printer.PrintInfo("Welcome to CMDToolKit . for information type 'help'");

while (true)
{
    try
    {
        var input = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(input))
            continue;

        if (input.EndsWith("/t"))
        {
            Printer.PrintInfo("Press Q to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    ProcessInput(input);
                    Thread.Sleep(_ThreadSleep);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Q);
            Printer.PrintWarning("Canceled by user");
        }
        else
            ProcessInput(input);
    }
    catch (Exception ex)
    {
        Printer.PrintError(ex.Message);
    }
}



void ProcessInput(string input)
{
    input = input.Trim();

    var splitedInput = input.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();

    if (splitedInput.Length > 0)
    {
        if (!Enum.TryParse(splitedInput[0].ToUpper(), true, out MasterCommandsEnum masterCommand))
            Printer.PrintWarning("Command Not Found!");

        switch (masterCommand)
        {
            case MasterCommandsEnum.Clear:
                ClipboardTool.Clear();
                Printer.Clear();
                break;
            case MasterCommandsEnum.Copy:
                ClipboardTool.AddResultToClipboard();
                break;
            case MasterCommandsEnum.Help:
                HelpProcess(splitedInput);
                break;
            case MasterCommandsEnum.Network:
                NetworkProcess(splitedInput);
                break;
            case MasterCommandsEnum.Encode:
            case MasterCommandsEnum.Decode:
                EncodersDecodersProcess(splitedInput);
                break;
        }
    }
    else
    {
        Printer.PrintWarning("Command Not Found!");
    }
}

void EncodersDecodersProcess(string[] splitedInput)
{
    if (splitedInput.Length < 3)
    {
        Printer.PrintInfo("wrong command ,please type 'help encode'");
        return;
    }
    if (!Enum.TryParse(splitedInput[0].ToUpper(), true, out MasterCommandsEnum masterCommand))
        Printer.PrintWarning("Command Not Found!");

    if (!Enum.TryParse(splitedInput[1].ToUpper(), true, out EncodersDecodersEnums command))
        Printer.PrintWarning("Command Not Found!");

    string commandInput = splitedInput[2];

    switch (masterCommand, command)
    {
        case (MasterCommandsEnum.Encode, EncodersDecodersEnums.Base64):
            Invoker.InvokeTools(() => Base64TextEncoderDecoder.Base64Encode(commandInput));
            break;
        case (MasterCommandsEnum.Decode, EncodersDecodersEnums.Base64):
            Invoker.InvokeTools(() => Base64TextEncoderDecoder.Base64Decode(commandInput));
            break;
    }
}

void NetworkProcess(string[] splitedInput)
{
    if (splitedInput.Length < 3)
    {
        Printer.PrintInfo("wrong command ,please type 'help network'");
        return;
    }

    if (!Enum.TryParse(splitedInput[1].ToUpper(), true, out NetworkEnum command))
        Printer.PrintWarning("Command Not Found!");

    string commandInput = splitedInput[2];

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

void HelpProcess(string[] splitedInput)
{
    if (splitedInput.Length == 1)
    {
        Printer.PrintInfo("Available Commands -> " + String.Join(" , ", (MasterCommandsEnum[])Enum.GetValues(typeof(MasterCommandsEnum))));
        Printer.PrintInfo("Example : Network");
    }
    else if (splitedInput.Length == 2)
    {
        if (!Enum.TryParse(splitedInput[1].ToUpper(), true, out MasterCommandsEnum command))
            Printer.PrintWarning("help not found!");

        switch (command)
        {
            case MasterCommandsEnum.Network:
                Printer.PrintInfo("Available Commands -> " + String.Join(" , ", (NetworkEnum[])Enum.GetValues(typeof(NetworkEnum))));
                Printer.PrintInfo("Example : network ping google.com");
                break;

        }
    }
    else if (splitedInput.Length == 3)
    {
        if (!Enum.TryParse(splitedInput[2].ToUpper(), true, out NetworkEnum command))
            Printer.PrintWarning("help not found!");

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

}