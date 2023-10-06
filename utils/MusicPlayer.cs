using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

public class MusicPlayer
{
    private static bool keepPlaying = true;
    private static Thread songThread;
    private static Process songProcess;

    public static void StartPlaying(string path)
    {
        keepPlaying = true;
        songThread = new Thread(() => Play(path));
        songThread.Start();
    }

    public static void StopPlaying()
    {
        keepPlaying = false;
        if (songProcess != null)
        {
            songProcess.Kill();
        }
        if (songThread != null)
        {
            songThread.Join();
        }
    }

    private static void Play(string path)
    {
        while (keepPlaying)
        {
            try
            {
                songProcess = null;
                // Outsourcing playing the song to a diffrent program that should be available.
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    songProcess = Process.Start("wmplayer.exe", path);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    songProcess = Process.Start("afplay", path);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    songProcess = Process.Start("aplay", path);
                }
                else
                {
                    // Not one of the opperating systems that is suported for this game.
                    return;
                }

                if (songProcess != null)
                {
                    songProcess.WaitForExit();
                }
            }
            catch (Exception)
            {
                // In this instance I am just ignoring the error and saying no music for you.
            }
        }
    }
}
