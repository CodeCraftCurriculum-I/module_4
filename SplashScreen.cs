using Utils;
using static Utils.Constants;
using static Utils.Output;
using static Utils.ANSICodes;

namespace Adventure
{
    public class SplashScreen : IGameScreen
    {
        private const string sloagan = "Double the Action";
        private const string lineSymbole = "**";
        private const int ANIMATION_SEGMENT_LOGO = 0;
        private const int ANIMATION_SEGMENT_LINE = 1;
        private const int ANIMATION_SEGMENT_NO_ANIMATION = 99;
        private const int MAX_LINE_LENGTH = 110;
        private const int PADDING = 2;

        private string drawing;
        private int artHeight;
        private int steps;
        private int currentRow;
        private string line = "";
        private int animationSegment = 0;
        private bool skipAnimation = false;
        public Action<Type, Object[]> OnExitScreen { get; set; }
        private Action<string> drawGraphics = (graphics) => Write(Reset(Color(Align(graphics, Alignment.CENTER), Colors.Green)));
        private Action<string> writeSlogan = (slogan) => Write(Reset(Color(Align(sloagan, Alignment.CENTER), Colors.Green)), newLine: true);
        private Action<string> drawLine = (line) => Write(Reset(Color(Align(line, Alignment.CENTER), Colors.Red)));

        public SplashScreen(string source, bool noAnimation = false)
        {
            drawing = FileUtils.ReadFromFile(source);
            artHeight = drawing.Split(NEW_LINE).Length + PADDING;
            steps = Console.WindowHeight - artHeight;
            currentRow = steps;
            animationSegment = ANIMATION_SEGMENT_LOGO;
            skipAnimation = noAnimation;
        }

        public void Init() { return; }
        public void Input() { return; }
        public void Update()
        {
            if (skipAnimation && animationSegment != ANIMATION_SEGMENT_NO_ANIMATION)
            {
                currentRow = PADDING;
                line = new string(lineSymbole[0], MAX_LINE_LENGTH);
                animationSegment = ANIMATION_SEGMENT_NO_ANIMATION;
            }
            else if (animationSegment == ANIMATION_SEGMENT_LOGO)
            {
                if (currentRow > PADDING)
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
                OnExitScreen(typeof(MenuScreen), null);
            }

        }
        public void Draw()
        {
            if (animationSegment == ANIMATION_SEGMENT_LOGO)
            {
                Console.Clear();
                Console.Write(ANSICodes.Positioning.SetCursorPos(currentRow, 0));
                drawGraphics(drawing);
                writeSlogan(sloagan);
            }
            else if (animationSegment == ANIMATION_SEGMENT_LINE)
            {
                Console.Write(ANSICodes.Positioning.SetCursorPos(currentRow, 0));
                drawLine(line);
            }
            else if (animationSegment == ANIMATION_SEGMENT_NO_ANIMATION)
            {
                Console.Clear();
                Console.Write(ANSICodes.Positioning.SetCursorPos(PADDING, 0));
                drawGraphics(drawing);
                writeSlogan(sloagan);
                drawLine(line);
            }
        }



    }




}