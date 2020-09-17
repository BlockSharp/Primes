using System;
using System.Numerics;
using Xunit;

namespace Primes.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void FirstPrimesArePrimes()
        {
            bool isPrime(int number)
            {
                if (number <= 1) return false;
                if (number == 2) return true;
                if (number % 2 == 0) return false;

                for (int i = 3; i <= (int)Math.Floor(Math.Sqrt(number)); i+=2)
                    if (number % i == 0)
                        return false;

                return true;
            }
            
            foreach (var prime in PrimeChecker.FirstPrimes)
                Assert.True(isPrime(prime));
        }

        [Fact]
        public void GeneratedPrimeIsPrimeMillerRabin()
        {
            var prime = PrimeGenerator.GetPrime2();
            Assert.True(PrimeChecker.IsPrimeMillerRabin(prime));
        }
        
        [Fact]
        public void GeneratedPrimeIsPrime()
        {
            var prime = PrimeGenerator.GetPrime1();
            Assert.True(PrimeChecker.IsPrime(prime));
        }
    }
}