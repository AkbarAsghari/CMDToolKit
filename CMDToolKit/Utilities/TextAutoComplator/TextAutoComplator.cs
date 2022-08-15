using CMDToolKit.Enums;
using CMDToolKit.Enums.EncodersDecoders;
using CMDToolKit.Enums.Network;
using CTK.Enums.Hash;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTK.Utilities.TextAutoComplator
{
    internal class TextAutoComplator
    {
        public static string ReadInput()
        {
            List<string> data = new List<string>();

            foreach (var masterCommands in ((MasterCommandsEnum[])Enum.GetValues(typeof(MasterCommandsEnum))))
            {
                data.Add($"{masterCommands} ");

                if (!(masterCommands == MasterCommandsEnum.Copy ||
                    masterCommands == MasterCommandsEnum.Clear ||
                    masterCommands == MasterCommandsEnum.Help ))
                {
                    data.Add($"Help {masterCommands} ");
                }
                if (masterCommands == MasterCommandsEnum.Network)
                {
                    foreach (var child in ((NetworkEnum[])Enum.GetValues(typeof(NetworkEnum))))
                    {
                        data.Add($"{masterCommands} {child} ");
                        data.Add($"Help {masterCommands} {child} ");
                    }
                }
                else if (masterCommands == MasterCommandsEnum.Encode || masterCommands == MasterCommandsEnum.Decode)
                {
                    foreach (var child in ((EncodersDecodersEnum[])Enum.GetValues(typeof(EncodersDecodersEnum))))
                    {
                        data.Add($"{masterCommands} {child} ");
                        data.Add($"Help {masterCommands} {child} ");
                    }
                }
                else if (masterCommands == MasterCommandsEnum.Hash)
                {
                    foreach (var child in ((HashEnum[])Enum.GetValues(typeof(HashEnum))))
                    {
                        data.Add($"{masterCommands} {child} ");
                        data.Add($"Help {masterCommands} {child} ");
                    }
                }
            }

            var builder = new StringBuilder();
            var input = Console.ReadKey(intercept: true);

            while (input.Key != ConsoleKey.Enter)
            {
                var currentInput = builder.ToString();
                if (input.Key == ConsoleKey.Tab)
                {
                    var match = data.FirstOrDefault(item => item != currentInput && item.StartsWith(currentInput, true, CultureInfo.InvariantCulture));
                    if (string.IsNullOrEmpty(match))
                    {
                        input = Console.ReadKey(intercept: true);
                        continue;
                    }

                    ClearCurrentLine();
                    builder.Clear();

                    Console.Write(match);
                    builder.Append(match);
                }
                else
                {
                    if (input.Key == ConsoleKey.Backspace)
                    {
                        if (currentInput.Length > 0)
                        {
                            builder.Remove(builder.Length - 1, 1);
                            ClearCurrentLine();

                            currentInput = currentInput.Remove(currentInput.Length - 1);
                            Console.Write(currentInput);
                        }
                        else
                        {
                            ClearCurrentLine();
                        }
                    }
                    else
                    {
                        var key = input.KeyChar;
                        builder.Append(key);
                        Console.Write(key);
                    }
                }

                input = Console.ReadKey(intercept: true);
            }
            Console.WriteLine();
            return builder.ToString();
        }
        private static void ClearCurrentLine()
        {
            //5 for ctk>
            var currentLine = Console.CursorTop;
            Console.SetCursorPosition(5, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(5, currentLine);
        }
    }
}
