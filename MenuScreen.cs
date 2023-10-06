using Utils;
using static Utils.Constants;
using static Utils.Output;

namespace Adventure
{
    public class MenuScreen : IGameScreen
    {
        #region Constants And Variables 
        const String NEW_GAME = "Start new Game";
        const String CONTINUE_GAME = "Continue Game";
        const string DISPLAY_SETTINGS = "Settings";
        const string QUIT = "Quit";
        const int MENU_ITEM_WIDTH = 50;
        readonly string[] menuItems = { NEW_GAME, CONTINUE_GAME, DISPLAY_SETTINGS, QUIT };
        int currentMenuIndex = 0;
        int startRow = 0;
        int startColumn = 0;
        int menuChange = 0;

        #endregion
        public Action<Type, Object[]> OnExitScreen { get; set; }
        public void Init()
        {
            startRow = 14; ///TODO: fix, should not be static. 
            startColumn = 0;
        }
        public void Input()
        {
            if (Console.KeyAvailable)
            {
                int keyCode = (int)Console.ReadKey(true).Key;
                if ((int)NavigationKeyCodes.ArrowDown == keyCode)
                {
                    menuChange = 1;
                }
                else if ((int)NavigationKeyCodes.ArrowUp == keyCode)
                {
                    menuChange = -1;
                }
                else if ((int)NavigationKeyCodes.Enter == keyCode)
                {

                    if (menuItems[currentMenuIndex] == QUIT)
                    {
                        OnExitScreen(null, null);
                    }
                    else if (menuItems[currentMenuIndex] == NEW_GAME)
                    {
                        OnExitScreen(typeof(AdvenureGame), null);
                    }
                }
            }
            else
            {
                menuChange = 0;
            }

        }
        public void Update()
        {
            currentMenuIndex += menuChange;
            currentMenuIndex = Math.Clamp(currentMenuIndex, 0, menuItems.Length - 1);
            menuChange = 0;
        }
        public void Draw()
        {
            Console.WriteLine(ANSICodes.Positioning.SetCursorPos(startRow, startColumn));
            for (int index = 0; index < menuItems.Length; index++)
            {
                if (index == currentMenuIndex)
                {
                    printActiveMenuItem($"* {menuItems[index]} *");
                }
                else
                {
                    printMenuItem($"  {menuItems[index]}  ");
                }
            }
        }
        void printActiveMenuItem(string item)
        {
            Output.Write(Reset(Bold(Align(item, Alignment.CENTER))), newLine: true);
        }
        void printMenuItem(string item)
        {
            Output.Write(Reset(Align(item, Alignment.CENTER)), newLine: true);
        }

    }
}