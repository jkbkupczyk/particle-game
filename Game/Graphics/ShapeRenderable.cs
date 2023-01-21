using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

using ParticleGame.Util;

namespace ParticleGame.Game.Graphics
{
    public sealed class ShapeRenderable : RenderableObject
    {
        public Color Color { get; set; }

        public ShapeRenderable(int width, int height, Color color) : this(width, height, color, Vector2d.Zero)
        {
        }

        public ShapeRenderable(int width, int height, Color color, Vector2d position) : this(position, CreateShapeElement(width, height, color))
        {
            this.Color = color;
        }

        private ShapeRenderable(Vector2d position, FrameworkElement uIElement) : base(position, uIElement)
        {
        }

        private static Shape CreateShapeElement(int width, int height, Color color)
        {
            var brushColor = new SolidColorBrush(color);
            return new Ellipse
            {
                MinWidth = width,
                Width = width,
                MaxWidth = width,
                MinHeight = height,
                Height = height,
                MaxHeight = height,
                Fill = brushColor,
                Stroke = brushColor
            };
        }

    }
}
