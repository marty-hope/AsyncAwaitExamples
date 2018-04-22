using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWPFApp.Services
{
    public interface IPrimeNumberService
    {
        Task<IEnumerable<PrimeNumberCandidate>> DeterminePrimeCandidates(IEnumerable<int> candidates);
    }
}
