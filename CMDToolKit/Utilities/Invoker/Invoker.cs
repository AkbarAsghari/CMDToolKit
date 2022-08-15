using CMDToolKit.DTOs;
using CMDToolKit.Utilities.CustomConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDToolKit.Utilities.Invoker
{
    internal class Invoker
    {
        public static void InvokeTools(Func<ToolResult> func)
        {
            ToolResult toolResult = null;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                toolResult = func.Invoke();
            }
            catch (Exception ex)
            {
                Printer.PrintError("Got error when invoke");
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            if (toolResult != null)
            {
                if (toolResult.IsSuccess == true)
                {
                    ClipboardTool.ClipboardTool.SetResult(toolResult.Message);
                    Printer.PrintSuccess($"{toolResult.Message}");
                }
                else if (toolResult.IsSuccess == false)
                    Printer.PrintError($"{toolResult.Message}");
                else
                    Printer.PrintWarning($"{toolResult.Message}");
            }
            Printer.PrintInfo($"---> time: {elapsedMs} ms");
        }
    }
}
