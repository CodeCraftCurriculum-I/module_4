using Adventure;
using Utils;
using static Adventure.AssetsAndSettings;

public class Game
{
    private const int FPS = 60;
    private const int MS_PER_FRAME = 1000 / FPS;
    private static IGameScreen? currentScreen = null;
    private static IGameScreen? nextScreen = null;
    private static bool isRuning = true;

    public static Action<Type, Object[]> OnExitScreen = (Type nextScreenType, Object[] args) =>
    {
        if (nextScreenType == null)
        {
            isRuning = false;
            return;
        }

        if (!typeof(IGameScreen).IsAssignableFrom(nextScreenType))
        {
            throw new ArgumentException("next screen must implement IGameScreen.");
        }

        nextScreen = (IGameScreen)Activator.CreateInstance(nextScreenType, args);
    };

    public static void Main()
    {
        Debug.ENABLED = AssetsAndSettings.DEBUG;

        Init();

        while (isRuning)
        {
            currentScreen.Input();
            currentScreen.Update();
            currentScreen.Draw();

            if (nextScreen != null)
            {
                SwapScreens();
            }

            Debug.Display();
            System.Threading.Thread.Sleep(MS_PER_FRAME);
        }

        Exit(null, null);
    }

    private static void SwapScreens()
    {
        currentScreen = nextScreen;
        currentScreen.OnExitScreen = OnExitScreen;
        currentScreen.Init();
        nextScreen = null;
    }

    private static void Init()
    {
        Console.CancelKeyPress += new ConsoleCancelEventHandler(Exit);

        Console.Write(ANSICodes.HideCursor);
        Console.Title = "The Adventure Game";

        currentScreen = new SplashScreen(AssetsAndSettings.SPLASH_ART_FILE)
        {
            OnExitScreen = OnExitScreen
        };
        currentScreen.Init();

        if (AssetsAndSettings.MUSIC_IS_ENABLED)
        {
            MusicPlayer.StartPlaying(AssetsAndSettings.GAME_MUSIC_FILE);
        }
    }

    private static void Exit(object sender, ConsoleCancelEventArgs args)
    {
        Console.Title = "";
        Console.Write(ANSICodes.ShowCursor);
        Console.Clear();
        MusicPlayer.StopPlaying();
    }


}