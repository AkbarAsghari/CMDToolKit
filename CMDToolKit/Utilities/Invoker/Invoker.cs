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
        public static async void InvokeToolsTask(params Task<ToolResult>[] tasks)
        {
            ToolResult toolResult = null;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                foreach (var task in tasks)
                {
                    toolResult = await task;
                }
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
