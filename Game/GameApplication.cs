using System;
using System.Threading;
using System.Diagnostics;

using System.Windows;

using ParticleGame.Game.State;
using ParticleGame.Game.Controls;
using ParticleGame.Game.Properties;

namespace ParticleGame.Game
{
    sealed class GameApplication
    {
        private readonly Application Application;
        private readonly GameWindow GameWindow;
        private readonly Thread LoopThread;

        private bool Active;

        public GameApplication()
        {
            this.LoopThread = new Thread(GameLoop)
            {
                Name = "main-game-loop-t0"
            };
            this.GameWindow = new GameWindow(GameProperties.WindowTitle, GameProperties.WindowWidth, GameProperties.WindowHeight);
            this.Application = new Application()
            {
                MainWindow = this.GameWindow.GetWindow(),
                ShutdownMode = ShutdownMode.OnExplicitShutdown
            };
            
            this.Active = false;

            this.Application.Startup += OnStartup;
            this.Application.Exit += OnExit;
            this.Application.Activated += OnActivated;
            this.Application.Deactivated += OnDeactivated;
            this.Application.SessionEnding += OnShutdown;
            this.Application.DispatcherUnhandledException += HandleException;
        }

        public void StartGame() => this.Application.Run();

        public void Shutdown() => this.Application.Shutdown();

        private void OnStartup(object sender, StartupEventArgs e)
        {
            this.GameWindow.Init();
            this.LoopThread.Start();
        }

        private void GameLoop()
        {
            this.GameWindow.Loop(Init, Tick, Render, Update, Destroy);
        }

        private void Init()
        {
            GlobalState.Init();
        }

        private void Tick()
        {
            Keyboard.Tick();
            GlobalState.Tick();
        }

        private void Update()
        {
            Keyboard.Update();
            GlobalState.Update();
        }

        private void Render()
        {
            GlobalState.Render();
        }

        private void Destroy()
        {
            GlobalState.Destroy();
        }

        private void OnExit(object sender, ExitEventArgs e)
        {
            this.GameWindow.GetWindow().Close();
            Environment.Exit(e.ApplicationExitCode);
        }

        private void OnActivated(object sender, EventArgs e)
        {
            this.Active = true;
        }

        private void OnDeactivated(object sender, EventArgs e)
        {
            this.Active = false;
        }

        private void OnShutdown(object sender, SessionEndingCancelEventArgs e)
        {
            Debug.WriteLine($"Session end abnormally. Cause: {e.ReasonSessionEnding}");
            Environment.Exit(1);
        }

        private void HandleException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Debug.WriteLine("Unhandled exception: " + e.Exception.Message);
            Environment.Exit(1);
        }

    }
}
