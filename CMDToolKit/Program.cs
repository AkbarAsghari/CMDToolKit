using CMDToolKit.Enums;
using CMDToolKit.Utilities.CustomConsole;
using CMDToolKit.Utilities.Invoker;
using CMDToolKit.Utilities.Network;

var _DNSTool = new DNSTool();
var _IPTool = new IPTool();

int _ThreadSleep = 1000;

string _Result;

while (true)
{
    try
    {
        var input = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(input))
            continue;

        if (input.EndsWith("/t"))
        {
            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    ProcessInput(input);
                    Thread.Sleep(_ThreadSleep);
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
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

    var splitedInput = input.Split(' ');

    if (splitedInput.Length > 0)
    {
        if (!Enum.TryParse(splitedInput[0].ToUpper(), true, out CommandsEnum command))
            Printer.PrintWarning("Command Not Found!");

        if (command == CommandsEnum.Clear)
        {
            _Result = String.Empty;
            Printer.Clear();
            return;
        }

        if (splitedInput.Length > 1)
        {
            string commandInput = splitedInput[1];

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
    }
    else
    {
        Printer.PrintWarning("Command Not Found!");
    }
}