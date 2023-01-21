using ParticleGame.Util;

namespace ParticleGame.Game.Entity.Particle
{
    class Electron : SubatomicParticle
    {
        public Electron(int size, long timeToLive) : 
            base(size, 0xFF228C22, 1, ParticleCharge.NEGATIVE, 0, RandomUtils.Next(66000, 66000), timeToLive)
        {
        }

        public Electron(int size, Vector2d position, Vector2d motionVector, long timeToLive) :
            base(size, position, motionVector, 0xFF228C22, 0, ParticleCharge.NEGATIVE, 0, 256, timeToLive)
        {
        }

        public static Electron Random()
        {
            return new Electron(RandomUtils.Next(7, 9), RandomUtils.Next(600L, 60000L));
        }
    }
}

