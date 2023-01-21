using ParticleGame.Game.Controls;
using ParticleGame.Game.Graphics;
using ParticleGame.Game.State;
using ParticleGame.Util;

namespace ParticleGame.Game.Screen.Game
{
    public class PauseScreen : IScreen
    {
        private readonly TextRenderable PauseText;

        public PauseScreen()
        {
            this.PauseText = new TextRenderable("PAUSED", null, 48, ColorUtils.FromHex(0xFFFF0000));
        }

        public void Init()
        {
            this.PauseText.SetZIndex(int.MaxValue);
            this.PauseText.SetVisible(false);
            this.PauseText.Init();
        }

        public void Tick()
        {
            if (KeyboardHandler.PAUSE.PressedTick())
            {
                Sound.MENU_SELECT.Play();
                this.Pause();
            }
        }

        public void Update()
        {
        }

        public void Render()
        {
            this.PauseText.SetVisible(GlobalState.Paused);
            this.PauseText.SetPosition((Renderer.Renderer.Width - this.PauseText.Width) / 2, (Renderer.Renderer.Height - 2 * this.PauseText.Height) / 2);
            this.PauseText.Render();
        }

        public void Destroy()
        {
            this.PauseText.Destroy();
        }

        private void Pause()
        {
            GlobalState.Paused = !GlobalState.Paused;
        }

    }
}
