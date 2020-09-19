using System;
using System.Diagnostics;
using System.Numerics;

namespace Primes
{
    public static class PrimeGenerator
    {
        public static BigInteger GetPrime1(int size = 1024)
        {
            var sw = Stopwatch.StartNew();

            for (int i = 0;; i++)
            {
                Console.WriteLine(i);
                BigInteger candidate = PrimeChecker.GetLowLevelPrime(size);

                if (PrimeChecker.IsPrimeMillerRabin(candidate))
                {
                    Console.WriteLine($"PRIME FOUND in {sw.Elapsed}!! " + candidate);
                    return candidate;
                }
            }
        }

        public static BigInteger GetPrime2(int size = 1024)
        {
            var sw = Stopwatch.StartNew();

            for (int i = 0;; i++)
            {
                Console.WriteLine(i);
                var candidate = PrimeChecker.GetLowLevelPrime(size);

                if (candidate.IsPrime())
                {
                    Console.WriteLine($"PRIME FOUND in {sw.Elapsed}!! " + candidate);
                    return candidate;
                }
            }
        }
    }
}