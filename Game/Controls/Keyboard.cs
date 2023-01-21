using System;

namespace ParticleGame.Game.Controls
{
    public static class Keyboard
    {
        public static readonly KeyboardKey[] Keys;

        static Keyboard()
        {
            var allKeys = Enum.GetValues(typeof(System.Windows.Input.Key));
            Keys = new KeyboardKey[allKeys.Length];
            for (int i = 0; i < Keys.Length; i++)
            {
                Keys[i] = new KeyboardKey();
            }
        }

        public static void Update()
        {
            foreach (KeyboardKey key in Keys)
            {
                key.Pressed = key.Down && !key.Last;
                key.Last = key.Down;
            }
        }

        public static void Tick()
        {
            foreach (KeyboardKey key in Keys)
            {
                key.PressedTick = key.Down && !key.LastTick;
                key.LastTick = key.Down;
            }
        }
    }

    public sealed class KeyboardKey
    {
        public bool Down { get; set; }
        public bool Pressed { get; set; }
        public bool PressedTick { get; set; }
        public bool Last { get; set; }
        public bool LastTick { get; set; }
    }

}
