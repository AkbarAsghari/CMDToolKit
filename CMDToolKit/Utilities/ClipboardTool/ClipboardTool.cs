using CMDToolKit.Utilities.CustomConsole;

namespace CMDToolKit.Utilities.ClipboardTool
{
    internal class ClipboardTool
    {
        static string _result;

        public static void AddResultToClipboard()
        {
            TextCopy.ClipboardService.SetText(_result);
            Printer.PrintSuccess("Copied");
        }

        public static void SetResult(string text)
        {
            _result = text; 
        }

        public static void Clear()
        {
            _result = String.Empty; 
        }
    }
}
