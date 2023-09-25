namespace Utils
{


    static class IO
    {
        public static string ReadInput()
        {
            return (Console.ReadLine() ?? "").ToLower().Trim();
        }

        public static void Print(string text, bool newLine = true)
        {
            if (newLine)
            {
                Console.WriteLine(text);
            }
            else
            {
                Console.Write(text);
            }
        }


        public static void Clear()
        {
            Console.Clear();
        }
    }

}