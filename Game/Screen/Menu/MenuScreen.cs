using System.Windows;

using ParticleGame.Game.Controls;
using ParticleGame.Game.State;
using ParticleGame.Util;

namespace ParticleGame.Game.Screen.Menu
{
    public abstract class MenuScreen : IScreen
    {
        protected MenuScreen ParentMenuScreen { get; set; }
        protected int CurrentOption { get; set; }
        protected MenuOption[] Options { get; set; }

        protected MenuScreen(MenuScreen parentMenuScreen)
        {
            this.ParentMenuScreen = parentMenuScreen;
            this.CurrentOption = 0;
            this.Options = new MenuOption[] { };
        }

        protected MenuScreen(MenuScreen parentMenuScreen, MenuOption[] options)
        {
            this.ParentMenuScreen = parentMenuScreen;
            this.CurrentOption = 0;
            this.Options = options;
        }

        public virtual void Init()
        {
            foreach (var option in this.Options)
            {
                option.TextRenderable.Init();
            }
        }

        public virtual void Tick()
        {
            if (this.ParentMenuScreen != null && KeyboardHandler.MENU_PREVIOUS.PressedTick())
            {
                GlobalState.CurrentScreen = this.ParentMenuScreen;
                this.ParentMenuScreen = this;
                this.Destroy();
                GlobalState.Init();
            }

            if (this.CurrentOption < Options.Length - 1 && KeyboardHandler.MOVE_DOWN.PressedTick())
            {
                this.CurrentOption++;
                Sound.MENU_SELECT.Play();
            }

            if (this.CurrentOption > 0 && KeyboardHandler.MOVE_UP.PressedTick())
            {
                this.CurrentOption--;
                Sound.MENU_SELECT.Play();
            }

            if (KeyboardHandler.MENU_SELECT.PressedTick())
            {
                this.Destroy();
                Application.Current.Dispatcher.BeginInvoke(Options[CurrentOption].Action);
            }
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {
            for (int i = 0; i < this.Options.Length; i++)
            {
                if (this.CurrentOption == i)
                {
                    this.Options[i].MarkAsActive();
                }
                else
                {
                    this.Options[i].UnMarkAsActive();
                }

                this.Options[i].TextRenderable.SetPosition((Renderer.Renderer.Width - this.Options[i].TextRenderable.Width) / 2, 256 + (i * 32));
                this.Options[i].TextRenderable.Render();
            }
        }

        public virtual void Destroy()
        {
            foreach (var option in this.Options)
            {
                option.TextRenderable.Destroy();
            }
            Renderer.Renderer.Clear();
        }

    }
}
