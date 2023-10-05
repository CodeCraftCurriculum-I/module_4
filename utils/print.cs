namespace Utils
{
    using System.Collections.Generic;

    enum Alignment
    {
        LEFT,
        RIGHT,
        CENTER
    }

    class Print
    {
        private List<string> steps;

        public Print()
        {
            steps = new List<string>();
        }

        public Print Color(string color)
        {
            steps.Add(color);
            return this;
        }

        public Print BgColor(string color)
        {
            steps.Add(color);
            return this;
        }

        public Print Append(string text, Alignment alignment = Alignment.LEFT, bool newLine = false)
        {
            if (text.Split("\n").Length > 1)
            {
                string[] lines = text.Split("\n");
                foreach (string line in lines)
                {
                    Append($"{line}", alignment);
                    steps.Add("\n");
                }
                return this;
            }

            if (alignment != Alignment.LEFT)
            {
                int width = System.Console.WindowWidth;
                int textWidth = text.Length;
                int padding = 0;

                if (alignment == Alignment.CENTER)
                {
                    padding = (width - textWidth) / 2;
                }
                else if (alignment == Alignment.RIGHT)
                {
                    padding = width - textWidth;
                }

                string paddingString = new string(' ', padding);
                steps.Add(paddingString);
            }

            steps.Add(text);
            if (newLine)
            {
                steps.Add("\n");
            }
            return this;
        }

        public Print Bold()
        {
            steps.Add(ANSICodes.Effects.Bold);
            return this;
        }
        public Print Reset()
        {
            steps.Add(ANSICodes.Reset);
            return this;
        }
        public Print Write()
        {
            string text = string.Join("", steps.ToArray());
            System.Console.Write(text);
            return this;
        }

    }
}
