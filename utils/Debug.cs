
using static Utils.ANSICodes;

using static Utils.Output;

namespace Utils
{
    public static class Debug
    {
        public static bool ENABLED;
        private static string currentMessage = String.Empty;
        private static int bottomRow = System.Console.WindowHeight - 1;
        public static void Log(params object[] args)
        {
            if (ENABLED)
            {
                Console.Write(Positioning.SetCursorPos(bottomRow, 0));
                currentMessage = Output.Write(Reset(Color(Color(Align(string.Join(" ", args), Alignment.CENTER), Colors.White), BgColors.Yellow)));
            }
        }

        public static void Display()
        {
            if (ENABLED)
            {
                Console.Write(Positioning.SetCursorPos(bottomRow, 0));
                Console.Write(currentMessage);
            }
        }
    }
}