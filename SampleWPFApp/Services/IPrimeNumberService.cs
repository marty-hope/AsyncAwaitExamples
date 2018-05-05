using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SampleWPFApp.Services
{
    public interface IPrimeNumberService
    {
        Task DeterminePrimeCandidates(IList<PrimeNumberCandidate> finalPrimeNumbers, IEnumerable<int>  candidates);
    }
}
