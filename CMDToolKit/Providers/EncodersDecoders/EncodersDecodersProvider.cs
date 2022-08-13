using CMDToolKit.Enums;
using CMDToolKit.Enums.EncodersDecoders;
using CMDToolKit.Interfaces;
using CMDToolKit.Utilities.CustomConsole;
using CMDToolKit.Utilities.EncodersDecoders;
using CMDToolKit.Utilities.Invoker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Providers.EncodersDecoders
{
    internal class EncodersDecodersProvider : IProvider
    {
        private readonly string[] _splitedInput;

        public EncodersDecodersProvider(string input)
        {
            _splitedInput = input.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();

        }
        public void Help()
        {
            if (_splitedInput.Length == 2)
            {
                Printer.PrintInfo("Available Commands -> " + String.Join(" , ", (EncodersDecodersEnums[])Enum.GetValues(typeof(EncodersDecodersEnums))));
                Printer.PrintInfo("Example : encode base64 Hello World");
                return;
            }

            if (!Enum.TryParse(_splitedInput[1].ToUpper(), true, out MasterCommandsEnum masterCommand))
                Printer.PrintWarning("Command Not Found!");

            if (!Enum.TryParse(_splitedInput[2].ToUpper(), true, out EncodersDecodersEnums command))
                Printer.PrintWarning("Command Not Found!");

            switch (masterCommand, command)
            {
                case (MasterCommandsEnum.Encode, EncodersDecodersEnums.Base64):
                    Printer.PrintInfo("Command -> encode base64 [plainText]");
                    Printer.PrintInfo("Example : encode base64 Hello World");
                    break;
                case (MasterCommandsEnum.Decode, EncodersDecodersEnums.Base64):
                    Printer.PrintInfo("Command -> decode base64 [base64EncodedData]");
                    Printer.PrintInfo("Example : encode base64 SGVsbG9Xb3JsZA==");
                    break;
                default:
                    Printer.PrintError($"Help Not Found For {masterCommand} {command}");
                    break;
            }
        }

        public void Process()
        {
            if (_splitedInput.Length < 3)
            {
                Printer.PrintInfo("wrong command ,please type 'help encode'");
                return;
            }

            if (!Enum.TryParse(_splitedInput[0].ToUpper(), true, out MasterCommandsEnum masterCommand))
                Printer.PrintWarning("Command Not Found!");

            if (!Enum.TryParse(_splitedInput[1].ToUpper(), true, out EncodersDecodersEnums command))
                Printer.PrintWarning("Command Not Found!");

            string commandInput = String.Join(String.Empty, _splitedInput.Skip(2));

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
    }
}
