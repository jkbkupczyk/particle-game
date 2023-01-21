using System;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

using ParticleGame.Game.Graphics;

namespace ParticleGame.Game.Renderer
{
    internal class GameCanvas
    {
        private readonly Canvas Canvas;

        public GameCanvas()
        {
            this.Canvas = new Canvas
            {
                Uid = "pg-canva-main",
                Name = "GameCanvas",
                AllowDrop = false,
                Focusable = true,
                Background = Brushes.Black
            };
        }

        public Canvas GetCanvas() => this.Canvas;

        public void InitElement(RenderableObject renderable)
        {
            this.Canvas.Children.Add(renderable.UIElement);
            Canvas.SetTop(renderable.UIElement, renderable.Position.Y);
            Canvas.SetLeft(renderable.UIElement, renderable.Position.X);
        }

        public void RenderElement(RenderableObject renderable)
        {
            if (renderable.PositionChanged)
            {
                Canvas.SetTop(renderable.UIElement, renderable.Position.Y);
                Canvas.SetLeft(renderable.UIElement, renderable.Position.X);
            }
        }

        public void DestroyElement(RenderableObject renderable)
        {
            this.Canvas.Children.Remove(renderable.UIElement);
        }

        public void Resize(double newWidth, double newHeight)
        {   
            this.Canvas.Width = newWidth;
            this.Canvas.Height = newHeight;
        }

        public void RenderFrame()
        {
            Application.Current.Dispatcher.BeginInvoke((Action)delegate { 
                this.Canvas.InvalidateVisual();
            });
        }

        public void Clear()
        {
            this.Canvas.Children.Clear();
        }

    }
}
