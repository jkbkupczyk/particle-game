using System;
using System.Diagnostics;

using ParticleGame.Game.Screen.Menu;

namespace ParticleGame.Game.Screen
{
    public class SettingsScreen : MenuScreen
    {

        // TODO: implement settings change
        public SettingsScreen(MenuScreen parentMenuScreen) : base(parentMenuScreen)
        {
            this.Options = new MenuOption[]
            {
                new MenuOption("Coming soon...", 32, new Action(() => Debug.WriteLine("Coming Soon..."))),
            };
        }

    }
}
