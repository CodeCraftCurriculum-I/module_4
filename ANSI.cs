namespace Utils
{

    public static class ANSI
    {

        public const string Reset = "\x1b[0m";
        public const string Bold = "\x1b[1m";
        public const string Dim = "\x1b[2m";
        public const string Italic = "\x1b[3m";
        public const string Underline = "\x1b[4m";
        public const string Blink = "\x1b[5m";

        public static class Color
        {
            public const string Black = "\x1b[30m";
            public const string Red = "\x1b[31m";
            public const string Green = "\x1b[32m";
            public const string Yellow = "\x1b[33m";
            public const string Blue = "\x1b[34m";
            public const string Magenta = "\x1b[35m";
            public const string Cyan = "\x1b[36m";
            public const string White = "\x1b[37m";
        }
    }

}