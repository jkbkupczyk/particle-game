using System.Windows;
using System.Windows.Controls;

using ParticleGame.Util;

namespace ParticleGame.Game.Graphics
{
    public abstract class RenderableObject : IRenderable
    {
        public bool Visible { get; private set; }
        public bool PositionChanged { get; private set; }
        private Vector2d LastPosition { get; set; }

        public Vector2d Position { get; private set; }

        public FrameworkElement UIElement { get; }

        public double Width
        {
            get { return this.UIElement.ActualWidth; }
        }

        public double Height
        {
            get { return this.UIElement.ActualHeight; }
        }

        protected RenderableObject(Vector2d position, FrameworkElement uIElement)
        {
            this.Visible = true;
            this.PositionChanged = true;
            this.LastPosition = Vector2d.Of(int.MinValue, int.MinValue);
            this.Position = position;
            this.UIElement = uIElement;
        }

        protected RenderableObject(FrameworkElement uIElement) : this(Vector2d.Zero, uIElement)
        {
        }

        public virtual void Init()
        {
            Renderer.Renderer.InitElement(this);
        }

        public virtual void Render()
        {
            Renderer.Renderer.RenderElement(this);
        }

        public virtual void Destroy()
        {
            Renderer.Renderer.DestroyElement(this);
        }

        public bool SetVisible(bool visible)
        {
            this.Visible = visible;
            this.UIElement.Visibility = this.Visible 
                ? Visibility.Visible 
                : Visibility.Hidden;
            return this.Visible;
        }

        public Vector2d SetPosition(double x, double y)
        {
            return this.SetPosition(Vector2d.Of((int) x, (int) y));
        }

        public Vector2d SetPosition(int x, int y)
        {
            return this.SetPosition(Vector2d.Of(x, y));
        }

        public Vector2d SetPosition(Vector2d newPosition)
        {
            this.PositionChanged = this.LastPosition != newPosition;
            this.LastPosition = this.Position;
            this.Position = newPosition;
            return this.Position;
        }

        public void SetZIndex(int index)
        {
            Panel.SetZIndex(this.UIElement, index);
        }

    }
}
