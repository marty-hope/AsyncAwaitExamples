using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SampleWPFApp.Commands;

namespace SampleWPFApp.Services
{
    public class PrimeNumberService  : IPrimeNumberService
    {
        public async Task<IEnumerable<PrimeNumberCandidate>>
            DeterminePrimeCandidates(IEnumerable<int> candidates)
        {

            var result = new List<PrimeNumberCandidate>();
            var tasks = candidates.Select(async c =>
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                try
                {
                    var isPrime = await IsPrime(c);
                    double calculationTime = watch.ElapsedMilliseconds.MillisecondsToSeconds();
                    return new PrimeNumberCandidate
                    {
                        PrimeCandidate = c,
                        IsPrime = isPrime,
                        CalculationTime = calculationTime
                    };
                }
                finally
                {
                    watch.Stop();
                }
            });
            foreach (var t in tasks.InCompletionOrder())
            {
                result.Add(await t);
                
            }

            return result;
        }


        private async Task<bool> IsPrime(int candidate)
        {
            await Task.Delay(1);
            if (candidate < 0)
                return false;
            switch (candidate)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                case 2:
                    return true;
                default:
                    for (int i = 3; i < candidate / 2; i++)
                    {
                        if (candidate % i == 0)
                            return false;
                    }
                    return true;
            }
        }
    }
}