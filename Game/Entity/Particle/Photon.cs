using ParticleGame.Util;

namespace ParticleGame.Game.Entity.Particle
{
    class Photon : SubatomicParticle
    {
        public Photon(int size, long timeToLive) :
            base(size, 0xFFFFFFFF, 0, ParticleCharge.NEUTRAL, 0, int.MaxValue, timeToLive)
        {
        }

        public Photon(int size, Vector2d position, Vector2d motionVector, long timeToLive) :
            base(size, position, motionVector, 0xFFFFFFFF, 0, ParticleCharge.NEUTRAL, 0, 256, timeToLive)
        {
        }

        public static Photon Random()
        {
            return new Photon(RandomUtils.Next(4, 5), RandomUtils.Next(600L, 60000L));
        }
    }
}
