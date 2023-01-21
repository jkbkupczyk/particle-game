using System;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ParticleGame.Util
{
    public static class HardwareDiag
    {
        private const string CPU_KEY = "CPU";
        private const string MEMORY_AVAILABLE_KEY = "Memory Available";
        private const string MEMORY_USED_KEY = "Memory Used";
        private const string MEMORY_PERCENTAGE_KEY = "Memory Percentage";

        private const int DELTA_TIME = 500;
        public const double BYTES_IN_MB = 1048576.0;

        private static readonly string ProcessorName;
        private static readonly Dictionary<string, PerformanceCounter> PerformanceCounters;
        private static readonly Dictionary<string, float> CachedValues;
        private static readonly Dictionary<string, DateTime> LastUpdateTimes;

        private static double AllocationRatePerSecond;
        private static DateTime AllocationRateLastUpdateTime;

        static HardwareDiag()
        {
            ProcessorName = InitProcessorName();
            PerformanceCounters = new Dictionary<string, PerformanceCounter>
            {
                { CPU_KEY, new PerformanceCounter("Processor", "% Processor Time", "_Total", true) },
                { MEMORY_AVAILABLE_KEY, new PerformanceCounter("Memory", "Available MBytes", true) },
                { MEMORY_USED_KEY, new PerformanceCounter("Memory", "Committed Bytes", true) },
                { MEMORY_PERCENTAGE_KEY, new PerformanceCounter("Memory", "% Committed Bytes In Use", true) }
            };
            CachedValues = new Dictionary<string, float>();
            LastUpdateTimes = new Dictionary<string, DateTime>();
        }

        private static string InitProcessorName()
        {
            try
            {
                return Process.GetCurrentProcess().ProcessorAffinity.ToString();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Could not obtain current processor name. Nested exception is: {e.Message}");
                return "Processor undefined";
            }
        }

        public static string GetCurrentProcessorName()
        {
            return ProcessorName;
        }

        public static async Task<float> GetCpuUsage()
        {
            return await GetPerformanceCounterValue(CPU_KEY);
        }

        public static async Task<float> GetAvailableRam()
        {
            return await GetPerformanceCounterValue(MEMORY_AVAILABLE_KEY);
        }

        public static async Task<float> GetUsedRam()
        {
            return await GetPerformanceCounterValue(MEMORY_USED_KEY);
        }

        public static async Task<float> GetUsedRamPercentage()
        {
            return await GetPerformanceCounterValue(MEMORY_PERCENTAGE_KEY);
        }

        private static async Task<float> GetPerformanceCounterValue(string counterName)
        {
            if (!PerformanceCounters.TryGetValue(counterName, out var counter))
            {
                throw new ArgumentException($"Performance counter '{counterName}' does not exist.");
            }

            if (LastUpdateTimes.TryGetValue(counterName, out var lastUpdateTime) && (DateTime.Now - lastUpdateTime).TotalMilliseconds < DELTA_TIME)
            {
                return CachedValues[counterName];
            }

            try
            {
                var counterValue = await Task.Run(() => counter.NextValue());
                CachedValues[counterName] = counterValue;
                LastUpdateTimes[counterName] = DateTime.Now;
                return counterValue;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Could not load nextValue for counterName = {counterName}. Nested exception is: {e.Message}");
                return CachedValues[counterName];
            }
        }

        public static double GetAllocationRatePerSecondMb()
        {
            try
            {
                if (DateTime.Now - AllocationRateLastUpdateTime < TimeSpan.FromMilliseconds(DELTA_TIME))
                {
                    return AllocationRatePerSecond;
                }

                var stopwatch = new Stopwatch();
                long bytesAllocated = GC.GetAllocatedBytesForCurrentThread();

                stopwatch.Start();
                Timer _timer = null;
                _timer = new Timer((state) =>
                {
                    _timer.Dispose();
                    long bytesAllocatedAfterOneSecond = GC.GetAllocatedBytesForCurrentThread();
                    long bytesAllocatedInOneSecond = bytesAllocatedAfterOneSecond - bytesAllocated;
                    AllocationRatePerSecond = (bytesAllocatedInOneSecond / stopwatch.Elapsed.TotalSeconds) / BYTES_IN_MB;
                    AllocationRateLastUpdateTime = DateTime.Now;
                }, null, 500, Timeout.Infinite);

                return AllocationRatePerSecond;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"Could not get ALlocRatePerSecond. Nested exception is: {e.Message}");
                return 0;
            }
        }

    }
}
