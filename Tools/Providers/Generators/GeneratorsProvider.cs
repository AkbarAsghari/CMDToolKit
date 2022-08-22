using Base.Enums;
using Base.Enums.Generators;
using Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools.Generators;
using Tools.Utilities.CustomConsole;
using Tools.Utilities.Invoker;

namespace Tools.Providers.Generators
{
    public class GeneratorsProvider : IProvider
    {
        private readonly string?[] _splitedInput;
        public GeneratorsProvider(string input)
        {
            _splitedInput = input.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();
        }
        public void Help()
        {
            if (_splitedInput.Where(x => x != null).ToArray().Length == 2)
            {
                Printer.PrintInfo("Available Commands -> " + String.Join(" , ", (GeneratorsEnum[])Enum.GetValues(typeof(GeneratorsEnum))));
                Printer.PrintInfo("Example : generate guid");
                return;
            }

            if (!Enum.TryParse(_splitedInput[2]!.ToUpper(), true, out GeneratorsEnum command))
            {
                Printer.PrintWarning("Command Not Found!");
                return;
            }

            switch (command)
            {
                case (GeneratorsEnum.Guid):
                    Printer.PrintInfo("Command -> generate guid");
                    break;
                default:
                    Printer.PrintError($"Help Not Found For {command}");
                    break;
            }
        }

        public void Process()
        {
            if (_splitedInput.Where(x => x != null).ToArray().Length < 2)
            {
                Printer.PrintInfo("wrong command ,please type 'help generate'");
                return;
            }

            if (!Enum.TryParse(_splitedInput[1]!.ToUpper(), true, out GeneratorsEnum command))
            {
                Printer.PrintWarning("Command Not Found!");
                return;
            }

            switch (command)
            {
                case (GeneratorsEnum.Guid):
                    Invoker.InvokeTools(() => Generator.GuidGenerator());
                    break;
            }
        }

    }
}
