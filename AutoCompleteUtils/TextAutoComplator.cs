using AutoCompleteUtils;
using Base.Enums;
using Base.Enums.EncodersDecoders;
using Base.Enums.Generators;
using Base.Enums.Hash;
using Base.Enums.Network;
using ConsoleUtils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Utilities.TextAutoComplator
{
    public class TextAutoComplator
    {
        public static string GetValue()
        {
            List<string> data = new List<string>();

            foreach (var masterCommands in ((MasterCommandsEnum[])Enum.GetValues(typeof(MasterCommandsEnum))))
            {
                data.Add($"{masterCommands}");

                if (!(masterCommands == MasterCommandsEnum.Copy ||
                    masterCommands == MasterCommandsEnum.Clear ||
                    masterCommands == MasterCommandsEnum.Help ))
                {
                    data.Add($"Help {masterCommands}");
                }
                if (masterCommands == MasterCommandsEnum.Network)
                {
                    foreach (var child in ((NetworkEnum[])Enum.GetValues(typeof(NetworkEnum))))
                    {
                        data.Add($"{masterCommands} {child}");
                        data.Add($"Help {masterCommands} {child}");
                    }
                }
                else if (masterCommands == MasterCommandsEnum.Encode || masterCommands == MasterCommandsEnum.Decode)
                {
                    foreach (var child in ((EncodersDecodersEnum[])Enum.GetValues(typeof(EncodersDecodersEnum))))
                    {
                        data.Add($"{masterCommands} {child}");
                        data.Add($"Help {masterCommands} {child}");
                    }
                }
                else if (masterCommands == MasterCommandsEnum.Hash)
                {
                    foreach (var child in ((HashEnum[])Enum.GetValues(typeof(HashEnum))))
                    {
                        data.Add($"{masterCommands} {child}");
                        data.Add($"Help {masterCommands} {child}");
                    }
                }
                else if (masterCommands == MasterCommandsEnum.Generate)
                {
                    foreach (var child in ((GeneratorsEnum[])Enum.GetValues(typeof(GeneratorsEnum))))
                    {
                        data.Add($"{masterCommands} {child}");
                        data.Add($"Help {masterCommands} {child}");
                    }
                }
            }

            var running = true;
            var cyclingAutoComplete = new CyclingAutoComplete();
            while (running)
            {
                var result = ConsoleExt.ReadKey();
                switch (result.Key)
                {
                    case ConsoleKey.Enter:
                        return result.LineBeforeKeyPress.Line;
                    case ConsoleKey.Tab:
                        var autoCompletedLine = cyclingAutoComplete.AutoComplete(
                    result.LineBeforeKeyPress.LineBeforeCursor, data);
                        ConsoleExt.SetLine(autoCompletedLine + result.LineBeforeKeyPress.LineAfterCursor);
                        break;
                }
            }

            return String.Empty;
        }
    }
}
