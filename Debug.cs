namespace Utils
{

    static class Debug
    {
        public enum DebugLevel
        {
            Verbose,
            Info,
            Warning,
            Error
        }

        public static bool enabled = true;
        public static DebugLevel level = DebugLevel.Info;
        public static string color = ANSI.Color.Blue;


        public static void Log(string message, DebugLevel level = DebugLevel.Info)
        {
            if (enabled && level >= Debug.level)
            {
                Console.WriteLine($"{color}{message}{ANSI.Reset}");
            }
        }

        public static bool Assert(bool condition, string message)
        {
            if (enabled && !condition)
            {
                Console.WriteLine($"Assertion failed: {message}");
            }
            return condition;
        }
    }

}