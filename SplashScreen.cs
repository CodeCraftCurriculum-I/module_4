using Utils;
using static Utils.Constants;

namespace Adventure
{
    public class SplashScreen : IGameScreen
    {
        private const string sloagan = "Double the Action";
        private const string lineSymbole = "*";
        private const int ANIMATION_SEGMENT_LOGO = 0;
        private const int ANIMATION_SEGMENT_LINE = 1;
        private const int MAX_LINE_LENGTH = 110;
        private const int PADDING = 3;
        private string drawing;
        private int artHeight;
        private int steps;
        private int currentRow;
        private string line = "";
        private int animationSegment = 0;
        public Action<Type> OnExitScreen { get; set; }

        public SplashScreen(string source)
        {
            drawing = FileUtils.ReadFromFile(source);
            artHeight = drawing.Split(NEW_LINE).Length + PADDING;
            steps = Console.WindowHeight - artHeight;
            currentRow = steps;
            animationSegment = ANIMATION_SEGMENT_LOGO;
        }

        public void Init() { return; }
        public void Input() { return; }
        public void Update()
        {
            if (animationSegment == ANIMATION_SEGMENT_LOGO)
            {
                if (currentRow > 0)
                {
                    currentRow--;
                }
                else
                {
                    animationSegment = ANIMATION_SEGMENT_LINE;
                }
            }
            else if (animationSegment == ANIMATION_SEGMENT_LINE)
            {
                currentRow = artHeight;
                line += lineSymbole;
                if (line.Length == MAX_LINE_LENGTH)
                {
                    animationSegment++;
                }
            }
            else
            {
                OnExitScreen(typeof(MenuScreen));
            }

        }
        public void Draw()
        {
            if (animationSegment == ANIMATION_SEGMENT_LOGO)
            {
                Console.Clear();
                Console.Write(ANSICodes.Positioning.SetCursorPos(currentRow, 0));
                (new Print()).Color(ANSICodes.Colors.Green).Append(drawing, Alignment.CENTER).Reset().Write();
                (new Print()).Color(ANSICodes.Colors.White).Bold().Append(sloagan, Alignment.CENTER, true).Reset().Write();
            }
            else if (animationSegment == ANIMATION_SEGMENT_LINE)
            {
                Console.Write(ANSICodes.Positioning.SetCursorPos(currentRow, 0));
                (new Print()).Color(ANSICodes.Colors.Red).Append(line, Alignment.CENTER).Reset().Write();
            }
        }

    }


}