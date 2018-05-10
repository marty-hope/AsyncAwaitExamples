using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using SampleWPFApp.Commands;

namespace SampleWPFApp.Services
{
    public class PrimeNumberService  : IPrimeNumberService
    {
        public async Task
            DeterminePrimeCandidates(IList<PrimeNumberCandidate> finalCandidates,
                IEnumerable<int> candidates)
        {

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
                        CalculationTime = calculationTime, 
                        CollectionIndex = candidates.IndexOf(c),
                        ThreadIndex = System.Threading.Thread.CurrentThread.ManagedThreadId
                    };
                }
                finally
                {
                    watch.Stop();
                }
            });
            foreach (var t in tasks.InCompletionOrder())
            {
                var result = await t;
                if (!finalCandidates.Contains(result))
                {
                    finalCandidates.Add(result);
                }
            }
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
                    for (int i = 2; i < candidate; i++)
                    {
                        if (candidate % i == 0)
                            return false;
                    }
                    return true;
            }
        }
    }
}