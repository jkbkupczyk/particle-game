using System;

using ParticleGame.Util;
using ParticleGame.Game.Graphics;
using ParticleGame.Game.Properties;
using ParticleGame.Game.Screen.Menu;
using ParticleGame.Game.State;
using ParticleGame.Game.Screen.Game;

namespace ParticleGame.Game.Screen
{
    public class MainMenuScreen : MenuScreen
    {
        public const int LOGO_PRESET = 32;

        private TextRenderable Logo { get; set; }
        private TextRenderable Version { get; set; }
        private TextRenderable Creator { get; set; }

        public MainMenuScreen() : base(null)
        {
            this.Options = new MenuOption[]
            {
                new MenuOption("Start", 32, new Action(() => StartGame())),
                new MenuOption("Settings", 32, new Action(() => ChangeSettings())),
                new MenuOption("Exit", 32, new Action(() => ExitGame()))
            };
            this.Logo = new TextRenderable(GameProperties.WindowTitle.ToUpper(), null, 64, ColorUtils.FromHex(0xFF0047AB));
            this.Version = new TextRenderable("VERSION 1.0", null, 20, ColorUtils.FromHex(0xFF_FEFEFE));
            this.Creator = new TextRenderable("MADE BY: jkbkupczyk", null, 20, ColorUtils.FromHex(0xFF_FEFEFE));
        }

        public override void Init()
        {
            base.Init();

            this.Logo.Init();
            this.Version.Init();
            this.Creator.Init();
        }

        public override void Render()
        {
            base.Render();

            this.Logo.SetPosition((Renderer.Renderer.Width - Logo.Width) / 2, LOGO_PRESET);
            this.Logo.Render();

            this.Version.SetPosition(16, Renderer.Renderer.Height - (Version.Height * 2.75));
            this.Version.Render();

            this.Creator.SetPosition(Renderer.Renderer.Width - (Version.Width * 2), Renderer.Renderer.Height - (Creator.Height * 2.75));
            this.Creator.Render();
        }

        public override void Destroy()
        {
            base.Destroy();

            this.Logo.Destroy();
            this.Version.Destroy();
            this.Creator.Destroy();
        }

        private void StartGame()
        {
            GlobalState.CurrentState = new GameState();
            GlobalState.CurrentScreen = new GameScreen();
            GlobalState.Init();
        }

        private void ChangeSettings()
        {
            GlobalState.CurrentState = null;
            GlobalState.CurrentScreen = new SettingsScreen(this);
            GlobalState.Init();
        }

        private void ExitGame()
        {
            GlobalState.Destroy();
            Environment.Exit(0);
        }

    }
}
