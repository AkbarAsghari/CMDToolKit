using CMDToolKit.Enums;
using CMDToolKit.Enums.EncodersDecoders;
using CMDToolKit.Enums.Network;
using CMDToolKit.Providers.EncodersDecoders;
using CMDToolKit.Providers.Hash;
using CMDToolKit.Providers.Network;
using CMDToolKit.Utilities.ClipboardTool;
using CMDToolKit.Utilities.CustomConsole;
using CMDToolKit.Utilities.EncodersDecoders;
using CMDToolKit.Utilities.Invoker;
using CMDToolKit.Utilities.Network;
using CTK.Enums.Hash;
using CTK.Utilities.TextAutoComplator;

int _ThreadSleep = 1000;

while (true)
{
    try
    {
        

        string input = TextAutoComplator.GetValue(); 


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
    input = input.TrimStart();

    var splitedInput = input.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();

    if (splitedInput.Length > 0)
    {
        if (!Enum.TryParse(splitedInput[0].ToUpper(), true, out MasterCommandsEnum masterCommand))
        {
            Printer.PrintWarning("Command Not Found!");
            return;
        }

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
                if (splitedInput.Length == 1)
                {
                    Printer.PrintInfo("All Available Commands");

                    foreach (var master in ((MasterCommandsEnum[])Enum.GetValues(typeof(MasterCommandsEnum))))
                    {
                        Printer.PrintInfo($"\n───| {master} ");
                        switch (master)
                        {
                            case MasterCommandsEnum.Clear:
                            case MasterCommandsEnum.Copy:
                            case MasterCommandsEnum.Help:
                                break;
                            case MasterCommandsEnum.Network:
                                foreach (var child in ((NetworkEnum[])Enum.GetValues(typeof(NetworkEnum))))
                                {
                                    Printer.PrintInfo($"   |-{String.Empty.PadRight(master.ToString().Length, '-')} {child}");
                                }
                                break;
                            case MasterCommandsEnum.Encode:
                            case MasterCommandsEnum.Decode:
                                foreach (var child in ((EncodersDecodersEnum[])Enum.GetValues(typeof(EncodersDecodersEnum))))
                                {
                                    Printer.PrintInfo($"   |-{String.Empty.PadRight(master.ToString().Length, '-')} {child}");
                                }
                                break;
                            case MasterCommandsEnum.Hash:
                                foreach (var child in ((HashEnum[])Enum.GetValues(typeof(HashEnum))))
                                {
                                    Printer.PrintInfo($"   |-{String.Empty.PadRight(master.ToString().Length, '-')} {child}");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                }
                if (!Enum.TryParse(splitedInput[1].ToUpper(), true, out MasterCommandsEnum masterCommandForHelp))
                {
                    Printer.PrintWarning("help not found!");
                    break;
                }
                switch (masterCommandForHelp)
                {
                    case MasterCommandsEnum.Network:
                        new NetworkProvider(input).Help();
                        break;
                    case MasterCommandsEnum.Encode:
                    case MasterCommandsEnum.Decode:
                        new EncodersDecodersProvider(input).Help();
                        break;
                    case MasterCommandsEnum.Hash:
                        new HashProvider(input).Help();
                        break;
                };
                break;


            case MasterCommandsEnum.Network:
                new NetworkProvider(input).Process();
                break;
            case MasterCommandsEnum.Encode:
            case MasterCommandsEnum.Decode:
                new EncodersDecodersProvider(input).Process();
                break;
            case MasterCommandsEnum.Hash:
                new HashProvider(input).Process();
                break;
        }
    }
    else
    {
        Printer.PrintWarning("Command Not Found!");
    }
}