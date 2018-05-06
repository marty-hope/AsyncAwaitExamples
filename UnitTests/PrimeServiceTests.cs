using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleWPFApp.Services;

namespace UnitTests
{
    [TestClass]
    public class PrimeServiceTests
    {
        private readonly PrimeNumberService _sut = new PrimeNumberService();

        [TestMethod]
        public async Task TestPrimeService()
        {
            var candidates = new List<int> {1000, 5, 2, 33, 99, 1099};
            var results = new List<PrimeNumberCandidate>();
            await _sut.DeterminePrimeCandidates(results, candidates);

            Assert.IsTrue(results.FirstOrDefault(c => c.PrimeCandidate == 5).IsPrime, "5 is Prime");
            Assert.IsTrue(!(results.FirstOrDefault(c => c.PrimeCandidate == 1000).IsPrime), "1000 is not Prime");

        }
    }
}
