using System;

namespace ParticleGame.Util
{
    public static class RandomUtils
    {
        private static readonly Random Random;

        static RandomUtils()
        {
            Random = new Random(new Guid().GetHashCode());
        }

        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        public static long Next(long minValue, long maxValue)
        {
            long result = Random.Next((int)(minValue >> 32), (int)(maxValue >> 32));
            result <<= 32;
            result |= (long)Random.Next((int)minValue, (int)maxValue);
            return result;
        }

        public static uint NextUint()
        {
            uint thirtyBits = (uint)Random.Next(1 << 30);
            uint twoBits = (uint)Random.Next(1 << 2);
            return (thirtyBits << 2) | twoBits;
        }

        public static bool NextBool()
        {
            return Random.Next(0, 2) == 1;
        }

        public static double NextDouble()
        {
            return Random.NextDouble();
        }

        public static void NextBytes(byte[] buffer)
        {
            Random.NextBytes(buffer);
        }

    }
}
