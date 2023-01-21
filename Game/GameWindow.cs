using System;
using System.Threading;

using System.Windows;
using System.Windows.Media.Imaging;

using ParticleGame.Util;
using ParticleGame.Game.Controls;
using ParticleGame.Game.State;

namespace ParticleGame.Game
{
    sealed class GameWindow
    {
        private static readonly long REFRESH_RATE = 16 * Time.NS_PER_MS;

        private readonly Window Window;

        private bool Closed;
        private bool Focused;

        private int Frames;
        private int Ticks;

        private int Fps;
        private int Tps;

        private long LastSecond;
        private long LastFrame;
        private long FrameTime;
        private long TicksRemaining;


        public GameWindow(in string Title, in int Width, in int Height)
        {
            this.Closed = false;
            this.Focused = false;
            this.LastSecond = Time.NanoTime();
            this.LastFrame = Time.NanoTime();

            this.Window = new Window
            {
                Uid = "pg-mainwin",
                Name = "MainWindow",
                Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/Images/icon.jpg")),
                AllowsTransparency = false,
                AllowDrop = false,
                ShowActivated = true,
                Title = Title,
                Width = Width,
                Height = Height,
                ResizeMode = ResizeMode.CanResize,
                Content = Renderer.Renderer.GetCanvas()
            };

            this.Window.Closed += OnWindowClose;
            this.Window.Activated += OnWindowActivate;
            this.Window.Deactivated += OnWindowDeactivate;
            this.Window.SizeChanged += OnWindowSizeChange;
            this.Window.KeyDown += OnKeyDown;
            this.Window.KeyUp += OnKeyUp;
        }

        public Window GetWindow() => this.Window;

        private void OnWindowClose(object sender, EventArgs e)
        {
            this.Closed = true;
        }

        private void OnWindowActivate(object sender, EventArgs e)
        {
            this.Focused = true;
            GlobalState.Paused = false;
        }

        private void OnWindowDeactivate(object sender, EventArgs e)
        {
            this.Focused = false;
            GlobalState.Paused = true;
        }

        private void OnWindowSizeChange(object sender, SizeChangedEventArgs e)
        {
            Renderer.Renderer.Resize(e.NewSize.Width, e.NewSize.Height);
        }

        private void OnKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Keyboard.Keys[(int)e.Key].Down = true;
        }

        private void OnKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            Keyboard.Keys[(int)e.Key].Down = false;
        }

        public void Init()
        {
            this.Window.Show();
        }

        public void Loop(ThreadStart Init,
                         ThreadStart Tick,
                         ThreadStart Update,
                         ThreadStart Render,
                         ThreadStart Destroy)
        {
            Application.Current.Dispatcher.BeginInvoke(Init);

            while (!this.Closed)
            {
                long now = Time.NanoTime();
                long start = now;

                if (now - this.LastSecond >= Time.NS_PER_SECOND)
                {
                    this.LastSecond = now;
                    this.Fps = GlobalState.Frames = this.Frames;
                    this.Tps = GlobalState.Ticks = this.Ticks;
                    this.Frames = 0;
                    this.Ticks = 0;
                }

                this.FrameTime = now - this.LastFrame;
                this.LastFrame = now;
                long tickTime = this.FrameTime + this.TicksRemaining;

                if (!this.Focused)
                {
                    continue;
                }

                while (tickTime >= Time.NS_PER_TICK)
                {
                    Application.Current.Dispatcher.BeginInvoke(Tick);
                    tickTime -= Time.NS_PER_TICK;
                    this.Ticks++;
                }
                this.TicksRemaining = tickTime;

                Application.Current.Dispatcher.BeginInvoke(Update);
                Application.Current.Dispatcher.BeginInvoke(Render);

                // Render Frame === refresh canvas
                Renderer.Renderer.RenderFrame();
                this.Frames++;

                long sleepMs = (REFRESH_RATE - (Time.NanoTime() - start)) / Time.NS_PER_MS;
                Thread.Sleep(sleepMs < 0 ? 0 : (int) sleepMs);
            }

            Application.Current.Dispatcher.BeginInvoke(Destroy);
            Environment.Exit(0);
        }
    }
}
