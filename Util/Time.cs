using System;
using System.Diagnostics;

namespace ParticleGame.Util
{
    public static class Time
    {
        public const long NS_PER_SECOND = 1000000000;
        public const long NS_PER_MS = 1000000;
        public const byte TICKS_PER_SECOND = 60;
        public const long NS_PER_TICK = NS_PER_SECOND / TICKS_PER_SECOND;

        public static long NanoTime()
        {
            return ((10000L * Stopwatch.GetTimestamp()) / TimeSpan.TicksPerMillisecond) * 100L;
        }

    }
}
