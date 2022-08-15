using CMDToolKit.Enums;
using CMDToolKit.Enums.EncodersDecoders;
using CMDToolKit.Interfaces;
using CMDToolKit.Utilities.CustomConsole;
using CMDToolKit.Utilities.EncodersDecoders;
using CMDToolKit.Utilities.Invoker;
using CTK.Enums.Hash;
using CTK.Utilities.Hash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Providers.Hash
{
    internal class HashProvider : IProvider
    {
        private readonly string?[] _splitedInput;

        public HashProvider(string input)
        {
            _splitedInput = new string[3];
            var commands = input.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x)).Take(2).ToArray();
            Array.Copy(commands, _splitedInput, commands.Length > 2 ? 2 : commands.Length);
            if (commands.Length > 1)
            {
                string[] splitedCommandAndInput = input.Split(commands[1] + " ");
                if (splitedCommandAndInput.Length > 1)
                {
                    _splitedInput[2] = splitedCommandAndInput[1];
                }
            }
        }
        public void Help()
        {
            if (_splitedInput.Where(x => x != null).ToArray().Length == 2)
            {
                Printer.PrintInfo("Available Commands -> " + String.Join(" , ", (HashEnum[])Enum.GetValues(typeof(HashEnum))));
                Printer.PrintInfo("Example : hash sha256 Hello World");
                return;
            }

            if (!Enum.TryParse(_splitedInput[2]!.ToUpper(), true, out HashEnum command))
            {
                Printer.PrintWarning("Command Not Found!");
                return;
            }

            switch (command)
            {
                case HashEnum.MD5:
                    Printer.PrintInfo("Command -> hash md5 [plainText]");
                    Printer.PrintInfo("Example : hash md5 Hello World");
                    break;
                case HashEnum.SHA1:
                    Printer.PrintInfo("Command -> hash sha1 [plainText]");
                    Printer.PrintInfo("Example : hash sha1 Hello World");
                    break;
                case HashEnum.SHA256:
                    Printer.PrintInfo("Command -> hash sha256 [plainText]");
                    Printer.PrintInfo("Example : hash sha256 Hello World");
                    break;
                case HashEnum.SHA384:
                    Printer.PrintInfo("Command -> hash sha384 [plainText]");
                    Printer.PrintInfo("Example : hash sha384 Hello World");
                    break;
                case HashEnum.SHA512:
                    Printer.PrintInfo("Command -> hash sha512 [plainText]");
                    Printer.PrintInfo("Example : hash sha512 Hello World");
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
                Printer.PrintInfo("wrong command ,please type 'help hash'");
                return;
            }

            if (!Enum.TryParse(_splitedInput[1]!.ToUpper(), true, out HashEnum command))
            {
                Printer.PrintWarning("Command Not Found!");
                return;
            }

            string commandInput = _splitedInput[2]!;

            switch (command)
            {
                case HashEnum.MD5:
                    Invoker.InvokeTools(() => HashTools.ComputeMD5Hash(commandInput));
                    break;
                case HashEnum.SHA1:
                    Invoker.InvokeTools(() => HashTools.ComputeSHA1Hash(commandInput));
                    break;
                case HashEnum.SHA256:
                    Invoker.InvokeTools(() => HashTools.ComputeSHA256Hash(commandInput));
                    break;
                case HashEnum.SHA384:
                    Invoker.InvokeTools(() => HashTools.ComputeSHA384Hash(commandInput));
                    break;
                case HashEnum.SHA512:
                    Invoker.InvokeTools(() => HashTools.ComputeSHA512Hash(commandInput));
                    break;
                default:
                    Printer.PrintError($"Command Not Found For {command}");
                    break;
            }
        }
    }
}
