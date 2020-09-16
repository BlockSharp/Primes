using System;
using System.Diagnostics;
using System.Numerics;

namespace Primes
{
    public static class PrimeGenerator
    {
        public static BigInteger GetPrime1(int bitSize = 1024)
        {
            int iter = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            while (true)
            {
                BigInteger prime_canidate = PrimeChecker.GetLowLevelPrime(bitSize);
                if (!PrimeChecker.IsPrimeMillerRabin(prime_canidate))
                {
                    iter++;
                    Console.WriteLine(iter);
                }
                else
                {
                    Console.WriteLine($"PRIME FOUND in {sw.Elapsed}!! "+prime_canidate);
                    sw.Stop();
                    return prime_canidate;
                }
            }
        }

        public static BigInteger GetPrime2(int n = 1024)
        {
            int iter = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            
            while (true)
            {
                var canidate = PrimeChecker.GetLowLevelPrime(n);

                if (PrimeChecker.IsPrime(canidate))
                {
                    Console.WriteLine($"PRIME FOUND in {sw.Elapsed}!! "+canidate);
                    sw.Stop();
                    return canidate;
                }
                
                Console.WriteLine(++iter);
            }
        }
    }
}