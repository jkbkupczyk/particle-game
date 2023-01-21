using ParticleGame.Util;

namespace ParticleGame.Game.Entity.Particle
{
    class Proton : SubatomicParticle
    {
        public Proton(int size, long timeToLive) :
            base(size, 0xFF002366, 1, ParticleCharge.POSITIVE, 0, RandomUtils.Next(32000, 64000), timeToLive)
        {
        }

        public Proton(int size, Vector2d position, Vector2d motionVector, long timeToLive) :
            base(size, position, motionVector, 0xFF002366, 1, ParticleCharge.POSITIVE, 0, 256, timeToLive)
        {
        }

        public static Proton Random()
        {
            return new Proton(RandomUtils.Next(12, 15), RandomUtils.Next(600L, 60000L));
        }
    }
}
