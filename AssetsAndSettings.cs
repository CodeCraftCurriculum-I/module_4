
using System.Diagnostics;

namespace Adventure
{
    public static class AssetsAndSettings
    {
        // Turn on or off all debug statments in the code. 
        public const bool DEBUG = true;

        // Toggle music
        public const bool MUSIC_IS_ENABLED = true;

        // Max line width for game 
        public const int MAX_LINE_WIDTH = 100;

        // The file containing the art renderd as part of the spalsh screen.
        public const string SPLASH_ART_FILE = "assets/splash.txt";

        // The music played in the background of the game on repeat.
        // Music from https://incompetech.com/music/royalty-free/index.html?isrc=USUAN1200066
        public const string GAME_MUSIC_FILE = "assets/song.mp3";

        public const string GAME_SOURCE = "game/start.adv";


    }
}