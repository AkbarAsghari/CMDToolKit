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
        public static async void InvokeToolsAsync(Task<ToolResult> task)
        {
            ToolResult toolResult = null;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                toolResult = await task;
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
                    Printer.PrintSuccess($"{toolResult.Message} (time: {elapsedMs} ms)");
                }
                else if (toolResult.IsSuccess == false)
                    Printer.PrintError($"{toolResult.Message} (time: {elapsedMs} ms)");
                else
                    Printer.PrintWarning($"{toolResult.Message} (time: {elapsedMs} ms)");
            }

        }

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
                    Printer.PrintSuccess($"{toolResult.Message} (time: {elapsedMs} ms)");
                }
                else if (toolResult.IsSuccess == false)
                    Printer.PrintError($"{toolResult.Message} (time: {elapsedMs} ms)");
                else
                    Printer.PrintWarning($"{toolResult.Message} (time: {elapsedMs} ms)");
            }
        }
    }
}
