using System;

using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

using ParticleGame.Util;

namespace ParticleGame.Game.Graphics
{
    public sealed class TextRenderable : RenderableObject
    {
        private static readonly FontFamily DEFAULT_FONT = new FontFamily(new Uri("pack://application:,,,/Resources/Font/dogicapixel.ttf"), "Dogica");

        private const string DEFAULT_TEXT = "";
        private const int DEFAULT_FONT_SIZE = 32;
        private static readonly Color DEFAULT_COLOR = ColorUtils.FromHex(0xFF000000);

        public string Text { get; set; }
        public FontFamily FontFamily { get; set; }
        public int FontSize { get; set; }
        public Color Color { get; set; }

        public TextRenderable() : base(Vector2d.Zero, CreateTextElement(DEFAULT_TEXT, null, DEFAULT_FONT_SIZE, DEFAULT_COLOR))
        {
            this.Text = DEFAULT_TEXT;
            this.FontSize = DEFAULT_FONT_SIZE;
            this.Color = DEFAULT_COLOR;
        }

        public TextRenderable(string text, FontFamily fontFamily, int fontSize, Color color)
            : this(text, fontFamily, fontSize, color, Vector2d.Zero)
        {
            this.Text = text;
            this.FontFamily = fontFamily;
            this.FontSize = fontSize;
            this.Color = color;
        }

        public TextRenderable(string text, FontFamily fontFamily, int fontSize, Color color, Vector2d position) 
            : base(position, CreateTextElement(text, fontFamily, fontSize, color))
        {
            this.Text = text;
            this.FontFamily = fontFamily;
            this.FontSize = fontSize;
            this.Color = color;
        }

        public void SetText(string newText)
        {
            this.Text = newText;
            ((TextBlock)this.UIElement).Text = this.Text;
        }

        public void SetColor(Color newColor)
        {
            this.Color = newColor;
            ((TextBlock)this.UIElement).Foreground = new SolidColorBrush(newColor);
        }

        public void SetColor(Color textColor, Color background)
        {
            var target = (TextBlock)this.UIElement;
            this.Color = textColor;
            target.Foreground = new SolidColorBrush(textColor);
            target.Background = new SolidColorBrush(background);
        }

        private static TextBlock CreateTextElement(string text, FontFamily fontFamily, int fontSize, Color color)
        {
            if (fontFamily == null)
            {
                return new TextBlock()
                {
                    Text = text,
                    FontSize = fontSize,
                    FlowDirection = FlowDirection.LeftToRight,
                    FontFamily = DEFAULT_FONT,
                    Foreground = new SolidColorBrush(color)
                };
            }
            return new TextBlock()
            {
                Text = text,
                FontFamily = fontFamily,
                FlowDirection = FlowDirection.LeftToRight,
                FontSize = fontSize,
                Foreground = new SolidColorBrush(color)
            };
        }

    }
}
