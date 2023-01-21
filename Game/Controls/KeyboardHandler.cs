using System.Windows.Input;

namespace ParticleGame.Game.Controls
{
    public static class KeyboardHandler
    {
        public static readonly GameControl DEBUG_TOGGLE = new GameControl(Key.F3);

        public static readonly GameControl PROPERTIES_TOGGLE = new GameControl(Key.F2);

        public static readonly GameControl MENU_UP = new GameControl(Key.Up, Key.W);
        public static readonly GameControl MENU_DOWN = new GameControl(Key.Down, Key.S);
        public static readonly GameControl MENU_LEFT = new GameControl(Key.Left, Key.A);
        public static readonly GameControl MENU_RIGHT = new GameControl(Key.Right, Key.D);
        public static readonly GameControl MENU_SELECT = new GameControl(Key.Enter);
        public static readonly GameControl MENU_PREVIOUS = new GameControl(Key.Escape);

        public static readonly GameControl MOVE_UP = new GameControl(Key.Up, Key.W);
        public static readonly GameControl MOVE_DOWN = new GameControl(Key.Down, Key.S);
        public static readonly GameControl MOVE_LEFT = new GameControl(Key.Left, Key.A);
        public static readonly GameControl MOVE_RIGHT = new GameControl(Key.Right, Key.D);

        public static readonly GameControl PAUSE = new GameControl(Key.Space, Key.Escape);
    }

    public sealed class GameControl
    {
        private readonly int[] KeyCodes;

        public GameControl(params Key[] keys)
        {
            this.KeyCodes = new int[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                this.KeyCodes[i] = ((int)keys[i]);
            }
        }

        public bool Down()
        {
            foreach (int code in this.KeyCodes)
            {
                if (Keyboard.Keys[code].Down)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Pressed()
        {
            foreach (int code in this.KeyCodes)
            {
                if (Keyboard.Keys[code].Pressed)
                {
                    return true;
                }
            }
            return false;
        }

        public bool PressedTick()
        {
            foreach (int code in this.KeyCodes)
            {
                if (Keyboard.Keys[code].PressedTick)
                {
                    return true;
                }
            }
            return false;
        }

    }

}
