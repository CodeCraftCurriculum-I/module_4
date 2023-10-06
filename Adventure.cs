using Utils;
using static Utils.Constants;
using static System.Console;
using System.Collections;

namespace Adventure
{

    public class AdvenureGame : IGameScreen
    {
        const string QUIT_COMMAND = "quit";
        const string CLEAR_COMMAND = "clear";
        const string HELP_COMMAND = "help";
        Dictionary<string, Action<AdvenureGame>> basicActions = new Dictionary<string, Action<AdvenureGame>>()
        {
            [QUIT_COMMAND] = (game) => { game.OnExitScreen(typeof(SplashScreen), new object[] { "assets/splash.txt", true }); },
            [CLEAR_COMMAND] = (game) => { Clear(); },
            [HELP_COMMAND] = (game) => { WriteLine("This should print a helpfull message, maybe a list of commands? But it doesn't yet."); }
        };

        string commandBuffer;
        string command;

        public Action<Type, Object[]> OnExitScreen { get; set; }
        public void Init()
        {
            command = commandBuffer = String.Empty;
        }
        public void Input()
        {
            if (KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = ReadKey(false);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    command = commandBuffer.ToLower();
                    commandBuffer = String.Empty;
                }
                else
                {
                    commandBuffer += keyInfo.KeyChar;
                }
            }

        }
        public void Update()
        {

            if (command != String.Empty)
            {
                if (basicActions.ContainsKey(command))
                {
                    basicActions[command](this);
                }

                command = String.Empty;
            }



        }
        public void Draw()
        {
            Clear();
            Write(commandBuffer);
        }


    }


}