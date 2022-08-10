using CMDToolKit.Enums;
using CMDToolKit.Utilities.ClipboardTool;
using CMDToolKit.Utilities.CustomConsole;
using CMDToolKit.Utilities.Invoker;
using CMDToolKit.Utilities.Network;

var _DNSTool = new DNSTool();
var _IPTool = new IPTool();

int _ThreadSleep = 1000;

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
                if (splitedInput.Length > 2)
                {
                    if (!Enum.TryParse(splitedInput[1].ToUpper(), true, out CommandsEnum command))
                        Printer.PrintWarning("Command Not Found!");

                    string commandInput = splitedInput[2];

                    switch (command)
                    {
                        case CommandsEnum.DNSLookUp:
                            Invoker.InvokeToolsTask(_DNSTool.DNSLookup(commandInput));
                            break;

                        case CommandsEnum.ReverseLookUp:
                            Invoker.InvokeToolsTask(_DNSTool.ReverseLookup(commandInput));
                            break;

                        case CommandsEnum.Ping:
                            Invoker.InvokeToolsTask(_IPTool.HostOrIPHavePing(commandInput));
                            break;

                        case CommandsEnum.Port:
                            Invoker.InvokeToolsTask(_IPTool.IsHHostOrIPAndPortOpen(commandInput));
                            break;
                    }
                }
                break;


        }
    }
    else
    {
        Printer.PrintWarning("Command Not Found!");
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
                Printer.PrintInfo("Available Commands -> " + String.Join(" , ", (CommandsEnum[])Enum.GetValues(typeof(CommandsEnum))));
                Printer.PrintInfo("Example : network ping google.com");
                break;

        }
    }

}