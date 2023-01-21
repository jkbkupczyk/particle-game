using System;
using System.Diagnostics;
using System.Threading;

using ParticleGame.Game;

namespace ParticleGame
{

    public class Program
    {

        public static void Main(string[] args)
        {
            try
            {
                var appThread = new Thread(StartGame)
                {
                    Name = "main-app-t1",
                    Priority = ThreadPriority.Highest,
                    IsBackground = false,
                };
                appThread.SetApartmentState(ApartmentState.STA);
                appThread.Start();
                Debug.WriteLine($"App started on thread: {appThread.Name}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exception: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private static void StartGame()
        {
            new GameApplication().StartGame();
            System.Windows.Threading.Dispatcher.Run();
        }
    }
}
