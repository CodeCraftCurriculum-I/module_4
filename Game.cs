using Adventure;
using Utils;

public class Game
{
    private const int FPS = 60;
    private const int MS_PER_FRAME = 1000 / FPS;
    private static IGameScreen? currentScreen = null;
    private static IGameScreen? nextScreen = null;

    public static Action<Type> OnExitScreen = (Type nextScreenType) =>
    {
        if (!typeof(IGameScreen).IsAssignableFrom(nextScreenType))
        {
            throw new ArgumentException("next screen must implement IGameScreen.");
        }
        nextScreen = (IGameScreen)Activator.CreateInstance(nextScreenType);
    };

    public static void Main()
    {
        Init();
        while (true)
        {
            currentScreen.Input();
            currentScreen.Update();
            currentScreen.Draw();
            System.Threading.Thread.Sleep(MS_PER_FRAME);

            if (nextScreen != null)
            {
                SwapScreens();
            }
        }
        Exit();


    }

    private static void SwapScreens()
    {
        currentScreen = nextScreen;
        currentScreen.OnExitScreen = OnExitScreen;
        nextScreen = null;
    }

    private static void Init()
    {
        Console.Write(ANSICodes.HideCursor);
        currentScreen = new SplashScreen("assets/splash.txt")
        {
            OnExitScreen = OnExitScreen
        };
        currentScreen.Init();
    }

    private static void Exit()
    {
        Console.Write(ANSICodes.ShowCursor);
    }


}