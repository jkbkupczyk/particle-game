using System.Windows.Media;

using ParticleGame.Util;
using ParticleGame.Game.State;
using ParticleGame.Game.Graphics;

namespace ParticleGame.Game.Entity
{
    public abstract class Entity : IGameEntity
    {
        public readonly long Id;

        public int Width { get; protected set; }
        public int Height { get; protected set; }

        private Vector2d LastPosition { get; set; }
        public Vector2d Position { get; protected set; }

        private Vector2d LastMotionVector { get; set; }
        public Vector2d MotionVector { get; protected set; }

        public bool Moving { get; protected set; }
        public bool ToBeRemoved { get; protected set; }

        protected Color Color { get; }
        protected ShapeRenderable Element { get; }

        
        protected Entity() : this(Vector2d.Zero, Vector2d.Zero, 0xFF0000)
        {
        }

        protected Entity(Vector2d Position, Vector2d MotionVector, uint Color)
            : this(9, Position, MotionVector, Color)
        {
        }

        protected Entity(int Size, Vector2d Position, Vector2d MotionVector, uint Color)
            : this(Size, Size, Position, MotionVector, false, false, Color)
        {
        }

        protected Entity(int Width, int Height, Vector2d Position, Vector2d MotionVector, uint Color) 
            : this(Width, Height, Position, Position, MotionVector, MotionVector, false, false, Color)
        {
        }

        protected Entity(int Width, int Height, Vector2d Position, Vector2d MotionVector, bool Moving, bool ToBeRemoved, uint Color) 
            : this(Width, Height, Position, Position, MotionVector, MotionVector, Moving, ToBeRemoved, Color)
        {
        }

        private Entity(int Width, int Height, Vector2d LastPosition, Vector2d Position, Vector2d LastMotionVector, Vector2d MotionVector, bool Moving, bool ToBeRemoved, uint Color)
        {
            this.Id = GlobalState.GetNextEntityId();
            this.Width = Width;
            this.Height = Height;
            this.LastPosition = LastPosition;
            this.Position = Position;
            this.LastMotionVector = LastMotionVector;
            this.MotionVector = MotionVector;
            this.Moving = Moving;
            this.ToBeRemoved = ToBeRemoved;
            this.Color = ColorUtils.FromHex(Color);
            this.Element = new ShapeRenderable(this.Width, this.Height, this.Color, this.Position);
            this.Element.UIElement.MouseDown += ShowProperties;
        }

        private void ShowProperties(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            GlobalState.CurrentlySelectedEntity = this;
        }

        public long GetId()
        {
            return this.Id;
        }

        public Vector2d GetCurrentPosition()
        {
            return this.Position;
        }

        public virtual void Init()
        {
            this.Element.Init();
        }

        public virtual void Tick()
        {
            this.LastMotionVector = this.MotionVector;
            this.MotionVector.X = RandomUtils.NextBool() ? -1 : 1;
            this.MotionVector.Y = RandomUtils.NextBool() ? -1 : 1;
            this.Moving = this.LastPosition.X != this.Position.X ||
                this.LastPosition.Y != this.Position.Y;
            this.Move(this.MotionVector.X, this.MotionVector.Y);
        }

        public virtual void Update()
        {
        }

        public virtual void Render()
        {
            this.Element.Render();
        }

        public virtual void Destroy()
        {
            this.ToBeRemoved = true;
            this.Width = 0;
            this.Height = 0;
            this.Element.Destroy();
        }

        public virtual void Interact(IGameEntity GameEntity)
        {

        }

        public virtual void Collide(IGameEntity GameEntity)
        {

        }

        public virtual void Move(int Dx, int Dy)
        {
            this.Position.X += Dx;
            this.Position.Y += Dy;
        }

        public virtual bool IsToBeRemoved()
        {
            return this.ToBeRemoved;
        }

        public Color GetColor()
        {
            return this.Color;
        }

        public ShapeRenderable GetElement()
        {
            return this.Element;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }
}
