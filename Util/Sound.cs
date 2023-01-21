using System;
using System.Windows;
using System.Media;
using System.Diagnostics;

namespace ParticleGame.Util
{
    public sealed class Sound
    {
        public static readonly Sound START = new Sound("pack://application:,,,/Resources/Sound/res_sound_start.wav");
        public static readonly Sound MENU_SELECT = new Sound("pack://application:,,,/Resources/Sound/menu_select.wav");
        public static readonly Sound PAUSE = new Sound("pack://application:,,,/Resources/Sound/pause.wav");

        private readonly SoundPlayer Player;

        public Sound(in string Path)
        {
            try
            {
                var uri = new Uri(Path);
                this.Player = new SoundPlayer(Application.GetResourceStream(uri).Stream);
                this.Player.LoadAsync();
            } catch (Exception e)
            {
                Debug.WriteLine($"Could not initialize sound: {e}");
            }
        }

        public void Play()
        {
            try
            {
                this.Player.Play();
            } catch (Exception e)
            {
                Debug.WriteLine($"Could not play sound: {e}");
            }
        }
    }
}
