using System;
using System.Windows.Media;

using ParticleGame.Util;
using ParticleGame.Game.Graphics;

namespace ParticleGame.Game.Screen.Menu
{
    public class MenuOption
    {
        public const string ACTIVE_PREFIX = "→";
        public static readonly Color ACTIVE_COLOR = ColorUtils.FromHex(0xFFFEFEFE);
        public static readonly Color DEFAULT_COLOR = ColorUtils.FromHex(0xFF333333);

        public string Text { get; set; }
        public Action Action { get; set; }
        public TextRenderable TextRenderable { get; }

        public MenuOption(string text, int fontSize, Action action)
        {
            this.Text = text;
            this.Action = action;
            this.TextRenderable = new TextRenderable(text, null, fontSize, DEFAULT_COLOR);
        }

        public MenuOption(string text, int fontSize, System.Windows.Media.Color color, Action action)
        {
            this.Text = text;
            this.Action = action;
            this.TextRenderable = new TextRenderable(text, null, fontSize, color);
        }

        public void MarkAsActive()
        {
            this.TextRenderable.SetText(ACTIVE_PREFIX + this.Text);
            this.TextRenderable.SetColor(ACTIVE_COLOR);
        }

        public void UnMarkAsActive()
        {
            this.TextRenderable.SetText(this.Text);
            this.TextRenderable.SetColor(DEFAULT_COLOR);
        }
    }
}
