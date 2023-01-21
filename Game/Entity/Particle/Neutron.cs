using ParticleGame.Util;

namespace ParticleGame.Game.Entity.Particle
{
    class Neutron : SubatomicParticle
    {
        public Neutron(int size, long timeToLive) : 
            base(size, 0xFFEE0000, 1, ParticleCharge.NEUTRAL, 0, RandomUtils.Next(256, 512), timeToLive)
        {
        }

        public Neutron(int size, Vector2d position, Vector2d motionVector, long timeToLive) :
            base(size, position, motionVector, 0xFFEE0000, 1, ParticleCharge.NEUTRAL, 0, 256, timeToLive)
        {
        }

        public static Neutron Random()
        {
            return new Neutron(RandomUtils.Next(12, 15), RandomUtils.Next(600L, 60000L));
        }
    }
}
