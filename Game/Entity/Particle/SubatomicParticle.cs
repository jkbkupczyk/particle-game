using ParticleGame.Util;

namespace ParticleGame.Game.Entity.Particle
{
    public abstract class SubatomicParticle : Entity
    {
        public uint Mass { get; protected set; }
        public ParticleCharge Charge { get; protected set; }
        public int Heat { get; protected set; }
        public int CriticalHeat { get; protected set; }
        public long TimeToLive { get; protected set; }


        protected SubatomicParticle(int Size, uint Color, uint Mass, ParticleCharge Charge, int Heat, int CriticalHeat, long TimeToLive)
            : base(Size, Vector2d.Of(RandomUtils.Next(0, (int)Renderer.Renderer.Width - Size), RandomUtils.Next(0, (int)Renderer.Renderer.Height - Size)), Vector2d.Of(1, 1), Color)
        {
            this.Mass = Mass;
            this.Charge = Charge;
            this.Heat = Heat;
            this.CriticalHeat = CriticalHeat;
            this.TimeToLive = TimeToLive;
        }

        protected SubatomicParticle(int Size, Vector2d Position, Vector2d MotionVector, uint Color, uint Mass, ParticleCharge Charge, int Heat, int CriticalHeat, long TimeToLive) 
            : base(Size, Position, MotionVector, Color)
        {
            this.Mass = Mass;
            this.Charge = Charge;
            this.Heat = Heat;
            this.CriticalHeat = CriticalHeat;
            this.TimeToLive = TimeToLive;
        }

        public virtual void Burst()
        {
            base.Destroy();
        }

        public override void Init()
        {
            base.Init();
        }

        public override void Tick()
        {
            base.Tick();
            this.Heat += RandomUtils.NextBool() ? 1 : 0;

            if (this.Position.X < 0 || this.Position.Y < 0 ||
                this.Position.X > Renderer.Renderer.Width || this.Position.Y > Renderer.Renderer.Height)
            {
                this.TimeToLive = 0;
            }

            if (this.TimeToLive <= 0)
            {
                base.Destroy();
                return;
            } 

            if (this.Heat > this.CriticalHeat)
            {
                this.Burst();
                return;
            }

            this.TimeToLive--;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Render()
        {
            base.Render();
        }

        public override void Destroy()
        {
            base.Destroy();
        }

        public override void Interact(IGameEntity GameEntity)
        {
            base.Interact(GameEntity);
        }

        public override void Collide(IGameEntity GameEntity)
        {
            base.Collide(GameEntity);
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
